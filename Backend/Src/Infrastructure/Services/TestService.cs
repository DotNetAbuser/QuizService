namespace Infrastructure.Services;

public class TestService(
    IQuizRepository _quizRepository,
    IUserRepository _userRepository,
    IResultRepository _resultRepository,
    IQuestionRepository _questionRepository)
    : ITestService
{
    public async Task<Result<IEnumerable<TestResponse>>> GetAllAsync()
    {
        var quizzesEntities = await _quizRepository.GetAllAsync();
        var quizzesResponse = quizzesEntities
            .Select(quizEntity => new TestResponse
            {
                Id = quizEntity.Id.ToString(),
                Title = quizEntity.Title,
                CorrectAnswerCount = quizEntity.CorrectAnswersCount
            });
        return await Result<IEnumerable<TestResponse>>
            .SuccessAsync(quizzesResponse, "Тесты успешно получены.");
    }

    public async Task<Result<IEnumerable<Question>>> GetQuestionsByQuizIdAsync(string quizId)
    {
        var questionsEntities = await _questionRepository
            .GetAllByQuizId(Guid.Parse(quizId));
        var questionsResponse = questionsEntities.Select(questionEntity => new Question
        {
            QuizId = quizId,
            Id = questionEntity.Id.ToString(),
            QuestionContent = questionEntity.QuestionContent,
            Options = questionEntity.Options.Select(optionEnity => new Option
            {
                QustionId = optionEnity.QuestionId.ToString(),
                Id = optionEnity.Id.ToString(),
                OptionContent = optionEnity.OptionContent,
                IsSelected = false,
                IsRight = optionEnity.IsRight
            }).ToList()
        }).ToList();
        
        return await Result<IEnumerable<Question>>.SuccessAsync
            (questionsResponse, "Тест с вопросами успешно получен.");
    }
    
    public async Task<Result> CreateAsync(CreateTestRequest request)
    {
        var titleIsExist = await _quizRepository
            .IsExistByTitleAsync(request.Title);
        if (titleIsExist)
        {
            return await Result<string>
                .FailAsync("Тест с данным названием уже существует!");
        }

        var quizEntity = new QuizEntity
        {
            Title = request.Title,
            CorrectAnswersCount = request.CorrectAnswersCount,
            Created = DateTime.UtcNow
        };
        
        foreach (var questionEntity in request.Questions.Select(question => new QuestionEntity
                 {
                     QuizId = quizEntity.Id,
                     QuestionContent = question.QuestionContent,
                     Created = DateTime.UtcNow,
                     Options = question.Options
                         .Select(option => new OptionEntity
                         {
                             OptionContent = option.OptionContent,
                             IsRight = option.IsRight,
                             Created = DateTime.UtcNow
                         }).ToList()
                 }))
        {
            quizEntity.Questions.Add(questionEntity);
        }

        await _quizRepository.CreateAsync(quizEntity);
        return await Result<string>
            .SuccessAsync("Новый тест успешно создан.");
    }

    public async Task<Result> DeleteAsync(string testId)
    {
        var quizEnitity = await _quizRepository
            .GetByIdAsync(Guid.Parse(testId));
        if (quizEnitity == null)
        {
            return await Result<string>
                .FailAsync("Тест с данным идентификатором не существует!");
        }
        await _quizRepository.DeleteAsync(quizEnitity);
        return await Result<string>
            .SuccessAsync("Тест успешно удалён.");
    }
}