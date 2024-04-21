namespace Mobile.ViewModels.Main;

public partial class TestsPanelVM(
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
    private async Task GoToCreateTestViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(CreateTestView));
    
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
    private async Task DeleteTestAsync(string testId)
    {
        try
        {
            var result = await _testManager.DeleteAsync(testId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in result.Messages)
                await _alertService.ShowAlertAsync(AlertType.Success, message);
            await RefreshDataAsync();
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