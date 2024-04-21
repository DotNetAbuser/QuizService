namespace Mobile.ViewModels.Main;

[QueryProperty(nameof(TestId), nameof(TestId))]
[QueryProperty(nameof(Title), nameof(Title))]
[QueryProperty(nameof(CorrectAnswerCount), nameof(CorrectAnswerCount))]
public partial class StartTestVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    ITestManager _testManager,
    IResultManager _resultManager,
    ITokenManager _tokenManager)
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            var result = await _testManager.GetTestWithQuestionsByIdAsync(TestId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                await _navigationService.NavigateBackAsync();
                return;
            }
            QuestionsList = [..result.Data];
        }
        catch (Exception ex)
        {
            await _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    [ObservableProperty] private string testId = string.Empty;
    [ObservableProperty] private string title = string.Empty;
    [ObservableProperty] private int correctAnswerCount = 0;
    [ObservableProperty] private List<Question> questionsList = [];

    [RelayCommand]
    private async Task FinishTestAsync()
    {
        try
        {
            IsBusy = true;
            var isAllQuestionsAnswered = QuestionsList.Count == QuestionsList
                .Count(question => question.Options.Any(option => option.IsSelected));
            if (!isAllQuestionsAnswered)
            {
                await _alertService.ShowAlertAsync(AlertType.Information, "Вы ответили не на все вопросы!");
                return;
            }
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var request = new CreateTestResultRequest(
                UserId: userId,
                TestId: TestId,
                Questions: questionsList);
            var result = await _resultManager.CreateAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in result.Messages)
                await _alertService.ShowAlertAsync(AlertType.Information, message);
            await _navigationService.NavigateBackAsync();
        }
        catch (Exception ex)
        {
            await _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

}