namespace Mobile.ViewModels.Start;

public partial class SignUpVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    IUserManager _userManager)
    : BaseVM(_alertService, _navigationService)
{
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    [RelayCommand]
    private async Task SignUpAsync()
    {
        try
        {
            IsBusy = true;
            var request = new SignUpRequest(
                LastName: LastName,
                FirstName: FirstName,
                MiddleName: MiddleName,
                Username: Username,
                Password: Password,
                Email: Email,
                Phone: Phone);
            var result = await _userManager.SignUpAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
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