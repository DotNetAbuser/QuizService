namespace Mobile.ViewModels.Main;

[QueryProperty(nameof(UserId), nameof(UserId))]
[QueryProperty(nameof(Username), nameof(Username))]
[QueryProperty(nameof(LastName), nameof(LastName))]
[QueryProperty(nameof(FirstName), nameof(FirstName))]
[QueryProperty(nameof(MiddleName), nameof(MiddleName))]
[QueryProperty(nameof(Email), nameof(Email))]
[QueryProperty(nameof(IsActive),nameof(IsActive))]
public partial class EditUserVM(
    IAlertService _alertService,
    INavigationService _navigationService,
    IUserManager _userManager)
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
            var result = await _userManager.GetRolesByUserIdAsync(UserId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            UserRoleList = [..result.Data.UserRoles];
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
    
    [ObservableProperty] private string userId;
    [ObservableProperty] private string username;
    [ObservableProperty] private string lastName;
    [ObservableProperty] private string firstName;
    [ObservableProperty] private string middleName;
    [ObservableProperty] private string email;
    [ObservableProperty] private bool isActive;
    
    
    [ObservableProperty] private List<UserRoleModel> userRoleList = [];

    [RelayCommand]
    private async Task SaveDataAsync()
    {
        try
        {
            IsBusy = true;
            var requestUpdateUserRole = new UpdateUserRoleRequest(
            UserRoles: UserRoleList.SingleOrDefault(x => x.Selected));
            var resultUpdateRoles = await _userManager.UpdateRoleAsync(UserId, requestUpdateUserRole);
            if (!resultUpdateRoles.Succeeded)
            {
                foreach (var message in resultUpdateRoles.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in resultUpdateRoles.Messages)
                await _alertService.ShowAlertAsync(AlertType.Success, message);

            var requestToggleUserStatus = new ToggleUserStatusRequest(
                UserId: UserId,
                ActivateUser: IsActive);
            var resultChangeUserStatus = await _userManager.ToggleUserStatusAsync(requestToggleUserStatus);
            if (!resultChangeUserStatus.Succeeded)
            {
                foreach (var message in resultChangeUserStatus.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in resultChangeUserStatus.Messages)
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