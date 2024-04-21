namespace Mobile.ViewModels.Start;

public partial class SignInVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    ITokenManager _tokenManager) 
    : BaseVM(_alertService, _navigationService)
{

    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string password = string.Empty;

    [RelayCommand]
    private async Task SignInAsync()
    {
        try
        {
            IsBusy = true;
            var request = new TokenRequest(
                Username: Username,
                Password: Password);
            var result = await _tokenManager.SignInAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            var token = await _tokenManager.GetJwtAsync();
            var role = await _tokenManager.GetUserRole(token);
            if (role == "admin") await _navigationService.NavigateToAsync("//AdminTab");
            else if (role == "teacher") await _navigationService.NavigateToAsync("//TeacherTab");
            else if (role == "student") await _navigationService.NavigateToAsync("//StudentTab");
            else await _tokenManager.SignOutAsync();
            Username = string.Empty;
            Password = string.Empty;
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
    private async Task GoToSignUpViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(SignUpView));
}