using System.Collections.ObjectModel;

namespace Mobile.ViewModels.Main;

public partial class UsersPanelVM(
    IAlertService _alertService, 
    INavigationService _navigationService,
    IUserManager _userManager)
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await RefreshDataAsync();
    }

    public ObservableCollection<UserResponse> UsersList { get; private set; } = [];
    [ObservableProperty] private int totalCount;
    
    [ObservableProperty] private int pageNumber = 1;
    [ObservableProperty] private int pageSize = 10;
    [ObservableProperty] private string searchTerm = string.Empty;
    [ObservableProperty] private string sortOrder = "asc";

    [ObservableProperty] private bool isFooterLoading;

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            PageNumber = 1;
            
            var result = await _userManager.GetPaginatedUsersAsync(PageNumber, PageSize, SearchTerm, SortOrder);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
            }

            UsersList.Clear();
            foreach (var user in result.Data.List)
                UsersList.Add(user);
            TotalCount = result.Data.TotalCount;
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
    private async Task SearchAsync() =>
        IsBusy = true;

    [RelayCommand]
    private async Task GetNextDataAsync()
    {
        try
        {
            IsFooterLoading = true;

            if (UsersList.Count <= 0)
                return;

            if (UsersList.Count >= TotalCount)
                return;

            pageNumber += 1;

            var result = await _userManager.GetPaginatedUsersAsync(
                PageNumber, PageSize, SearchTerm, sortOrder);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            foreach (var user in result.Data.List)
                UsersList.Add(user);
            TotalCount = result.Data.TotalCount;
        }
        catch (Exception ex)
        {
            await _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsFooterLoading = false;
        }
    }

    [RelayCommand]
    private async Task GoToEditUserViewAsync(UserResponse model) =>
        await _navigationService.NavigateToAsync(nameof(EditUserView),
            new Dictionary<string, object>
            {
                { "UserId", model.Id },
                { "Username", model.Username },
                { "LastName", model.LastName },
                { "FirstName", model.FirstName },
                { "MiddleName", model.MiddleName },
                { "Email", model.Email },
                { "IsActive", model.IsActive }
            });
}