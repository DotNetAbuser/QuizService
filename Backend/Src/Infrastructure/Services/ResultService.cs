namespace Infrastructure.Services;

public class ResultService(
    IUserRepository _userRepository,
    IResultRepository _resultRepository,
    IQuizRepository _quizRepository)
    : IResultService
{
    public async Task<Result<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUserTestResultsByUserIdAsync(string userId, string currentUserId,
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var userEntity = await _userRepository
            .GetByIdAsync(Guid.Parse(currentUserId));
        if (userEntity == null)
        {
            return await Result<PaginatedDataResponse<UserTestResultResponse>>
                .FailAsync("Пользователь не найден!");
        }

        var isUserRequests = currentUserId == userId;
        var isCurrentUserAdminOrTeacher = userEntity.RoleId is (int)Role.Admin or (int)Role.Teacher;
        var isAccessed = isUserRequests || isCurrentUserAdminOrTeacher;
        if (!isAccessed)
        {
            return await Result<PaginatedDataResponse<UserTestResultResponse>>
                .FailAsync("Для получения не своих результатов прохождения теста, " +
                           "необходимо иметь роль учителя или админа!");
        }
        
        var paginatedResultsEntities = await _resultRepository
            .GetPaginatedResultsByUserIdAsync(Guid.Parse(userId),
                pageNumber, pageSize, searchTerm, sortOrder);
        var userTestResultsResponse = paginatedResultsEntities.List
            .Select(resultEntity => new UserTestResultResponse
            {
                Id = resultEntity.Id.ToString(),
                LastName = resultEntity.User.LastName,
                FirstName = resultEntity.User.FirstName,
                MiddleName = resultEntity.User.MiddleName,
                Title = resultEntity.Quiz.Title,
                QuestionsCount = resultEntity.Quiz.Questions.Count(),
                CorrectAnswersCount = resultEntity.ResultDetails.Count(x => x.IsRight),
                IsPassed = resultEntity.IsPassed,
                Created = resultEntity.Created
            }).ToList();
        var paginatedUserTestResultResponse = new PaginatedDataResponse<UserTestResultResponse>(
            list: userTestResultsResponse, totalCount: paginatedResultsEntities.TotalCount);
        return await Result<PaginatedDataResponse<UserTestResultResponse>>
            .SuccessAsync(paginatedUserTestResultResponse, "Результаты теста пользователя успешно получены.");
    }

    public async Task<Result<PaginatedDataResponse<UserTestResultResponse>>> GetPaginatedUsersTestResultsAsync(
        int pageNumber, int pageSize, string searchTerm, string sortOrder)
    {
        var paginatedResultsEntities = await _resultRepository
            .GetPaginatedResultsAsync(pageNumber, pageSize, searchTerm, sortOrder);
        var userTestResultsResponse = paginatedResultsEntities.List
            .Select(resultEntity => new UserTestResultResponse
            {
                Id = resultEntity.Id.ToString(),
                LastName = resultEntity.User.LastName,
                FirstName = resultEntity.User.FirstName,
                MiddleName = resultEntity.User.MiddleName,
                Title = resultEntity.Quiz.Title,
                QuestionsCount = resultEntity.Quiz.Questions.Count(),
                CorrectAnswersCount = resultEntity.ResultDetails.Count(x => x.IsRight),
                IsPassed = resultEntity.IsPassed,
                Created = resultEntity.Created
            }).ToList();
        var paginatedUserTestResultResponse = new PaginatedDataResponse<UserTestResultResponse>(
            list: userTestResultsResponse, totalCount: paginatedResultsEntities.TotalCount);
        return await Result<PaginatedDataResponse<UserTestResultResponse>>
            .SuccessAsync(paginatedUserTestResultResponse, "Результаты тестов пользователей успешно получены.");
    }

    public async Task<Result> CreateAsync(CreateTestResultRequest request, string currentUserId)
    {
        var userEntity = await _userRepository
            .GetByIdAsync(Guid.Parse(request.UserId));
        if (userEntity == null)
        {
            return await Result<UserTestResultResponse>
                .FailAsync("Пользовтеля с данным идентификатором не существует!");
        }
        
        var isUserRequests = currentUserId == request.UserId;
        var isCurrentUserAdminOrTeacher = userEntity.RoleId is (int)Role.Admin or (int)Role.Teacher;
        var isAccessed = isUserRequests || isCurrentUserAdminOrTeacher;
        if (!isAccessed)
        {
            return await Result<UserTestResultResponse>
                .FailAsync("Для получения результатов не своих результатов прохождения теста, " +
                           "необходимо иметь роль учителя или админа!");
        }
        
        var quizEntity = await _quizRepository
            .GetByIdAsync(Guid.Parse(request.TestId));
        if (quizEntity == null)
        {
            return await Result<UserTestResultResponse>
                .FailAsync("Теста с данным идентификатором не существует!");
        }
        var correctAnswersCountNeed = quizEntity.CorrectAnswersCount;
        var correctAnswersCountByUser = request.Questions
            .Count(question => question.Options.Any(option => option is { IsSelected: true, IsRight: true }));
        var isPassed = correctAnswersCountByUser >= correctAnswersCountNeed;
        
        var resultEntity = new ResultEntity()
        {
            UserId = userEntity.Id,
            QuizId = quizEntity.Id,
            IsPassed = isPassed,
            ResultDetails = request.Questions
                .Select(question => new ResultDetailEntity
                {
                    QuestionId = Guid.Parse(question.Id),
                    OptionId = Guid.Parse(question.Options.SingleOrDefault(option => option.IsSelected).Id),
                    IsRight = question.Options.SingleOrDefault(option => option.IsSelected).IsRight,
                    Created = DateTime.UtcNow
                }).ToList(),
            Created = DateTime.UtcNow
        };
        
        await _resultRepository.CreateAsync(resultEntity);
        if (!isPassed)
        {
            return await Result<string>
                .SuccessAsync("Тест провален. Пожалуйста попробуйте ещё раз!");
        }
        else
        {
            return await Result<string>
                .SuccessAsync("Тест успешно пройден.");
        }
    }

}