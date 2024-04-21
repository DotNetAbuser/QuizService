namespace Mobile.ViewModels.Main;

public partial class TestsVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    ITestManager _testManager)
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await RefreshDataAsync();
    }

    [ObservableProperty] private List<TestResponse> testList = [];

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            var result = await _testManager.GetAllAsync();
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            TestList = [..result.Data];
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

    [RelayCommand]
    private async Task GoToStartTestViewAsync(TestResponse model) =>
        await _navigationService.NavigateToAsync(nameof(StartTestView),
            new Dictionary<string, object>
            {
                { "TestId", model.Id },
                { "Title", model.Title },
                { "CorrectAnswerCount", model.CorrectAnswerCount }
            });

}