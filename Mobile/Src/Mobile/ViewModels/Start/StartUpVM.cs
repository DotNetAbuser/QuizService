namespace Mobile.ViewModels.Start;

public partial class StartUpVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    ITokenManager _tokenManager) 
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await CheckAuthStateAsync();
    }

    private async Task CheckAuthStateAsync()
    {
        try
        {
            IsBusy = true;
            var token = await _tokenManager.GetJwtAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                await _navigationService.NavigateToAsync($"//{nameof(SignInView)}");
                return;
            }
            var role = await _tokenManager.GetUserRole(token);
            if (role == "admin") await _navigationService.NavigateToAsync("//AdminTab");
            else if (role == "teacher") await _navigationService.NavigateToAsync("//TeacherTab");
            else if (role == "student") await _navigationService.NavigateToAsync("//StudentTab");
            else await _navigationService.NavigateToAsync($"//{nameof(SignInView)}");
        }
        catch(Exception ex)
        {
            await _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}