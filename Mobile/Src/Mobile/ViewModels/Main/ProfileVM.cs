namespace Mobile.ViewModels.Main;

public partial class ProfileVM(
    IAlertService _alertService,
    INavigationService _navigationService,
    ITokenManager _tokenManager,
    IUserManager _userManager) 
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        RefreshDataAsync();
    }

    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string phone = string.Empty;

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var result = await _userManager.GetByIdAsync(userId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                await _tokenManager.SignOutAsync();
                await _navigationService.NavigateToAsync($"//{nameof(StartUpView)}");
                return;
            }

            LastName = result.Data.LastName;
            FirstName = result.Data.FirstName;
            MiddleName = result.Data.MiddleName;
            Username = result.Data.Username;
            Email = result.Data.Email;
            Phone = result.Data.Phone;
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
    private async Task SignOutAsync()
    {
        await _tokenManager.SignOutAsync();
        await _navigationService.NavigateToAsync($"//{nameof(StartUpView)}");
    }
}