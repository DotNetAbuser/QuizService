namespace Mobile.ViewModels.Main;

public partial class TestsResultsVM(
    IAlertService _alertService,
    INavigationService _navigationService,
    ITokenManager _tokenManager,
    IResultManager _resultManager) 
    : BaseVM(_alertService, _navigationService)
{
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await RefreshDataAsync();
    }

    public ObservableCollection<UserTestResultResponse> UserTestResultList { get; } = [];

    private int pageNumber = 1;
    private int pageSize = 5;
    
    [ObservableProperty] private int totalCount; 
    [ObservableProperty] private string searchTerm = string.Empty;
    [ObservableProperty] private string sortOrder = "desc";

    [ObservableProperty] private bool isStudent;

    [ObservableProperty] private bool isFooterLoading;

    [RelayCommand]
    private async Task SearchAsync() =>
        IsBusy = true;
    
    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            pageNumber = 1;
            UserTestResultList.Clear();
            var token = await _tokenManager.GetJwtAsync();
            var role = await _tokenManager.GetUserRole(token);
            IsStudent = role.Contains("student");
            IResult<PaginatedDataResponse<UserTestResultResponse>> result;
            if (IsStudent)
            {
                var userId = await _tokenManager.GetUserIdAsync(token);
                result = await _resultManager.GetPaginatedUserTestResultsByUserIdAsync(userId,
                    pageNumber, pageSize, searchTerm, sortOrder);
            }
            else
            {
                result = await _resultManager.GetPaginatedUsersTestResultsAsync(
                    pageNumber, pageSize, searchTerm, sortOrder);
            }
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var userTestResult in result.Data.List)
                UserTestResultList.Add(userTestResult);
            TotalCount = result.Data.TotalCount;
        }
        catch (Exception ex)
        {
            _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    [RelayCommand]
    private async Task GetNextDataAsync()
    {
        try
        {
            IsFooterLoading = true;

            if (UserTestResultList.Count <= 0)
                return;

            if (UserTestResultList.Count >= TotalCount)
                return;

            pageNumber += 1;

            var token = await _tokenManager.GetJwtAsync();
            IResult<PaginatedDataResponse<UserTestResultResponse>> result;
            
            if (IsStudent)
            {
                var userId = await _tokenManager.GetUserIdAsync(token);
                result = await _resultManager.GetPaginatedUserTestResultsByUserIdAsync(userId,
                    pageNumber, pageSize, searchTerm, sortOrder);
            }
            else
            {
                result = await _resultManager.GetPaginatedUsersTestResultsAsync(
                    pageNumber, pageSize, searchTerm, sortOrder);
            }
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            foreach (var item in result.Data.List)
                UserTestResultList.Add(item);
            TotalCount = result.Data.TotalCount;
        }
        catch (Exception ex)
        {
            await _alertService
                .ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsFooterLoading = false;
        }
    }
}