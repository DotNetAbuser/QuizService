using System.Collections.ObjectModel;

namespace Mobile.ViewModels.Main;

public partial class CreateTestVM(
    IAlertService _alertService,
    INavigationService _navigationService,
    ITestManager _testManager)
    : BaseVM(_alertService, _navigationService)
{

    public ObservableCollection<Question> QuestionList { get; } = [];
    
    [ObservableProperty] private string title = string.Empty;
    [ObservableProperty] private int correctAnswersCount = 1;

    [RelayCommand]
    private async Task AddQuestionAsync() => QuestionList.Add(new Question
    {
        Options = [new(), new(), new(),new()]
    });
    

    [RelayCommand]
    private async Task RemoveQuestionAsync(Question question) => QuestionList.Remove(question);

    [RelayCommand]
    private async Task CreateTestAsync()
    {
        try
        {
            IsBusy = true;
            var request = new CreateTestRequest(
                Title: title,
                CorrectAnswersCount: CorrectAnswersCount,
                Questions: QuestionList.ToList());
            var result = await _testManager.CreateAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in result.Messages)
                await _alertService.ShowAlertAsync(AlertType.Success, message);
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