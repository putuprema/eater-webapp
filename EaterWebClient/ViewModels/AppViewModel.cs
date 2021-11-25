using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace EaterWebClient.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private readonly ITableService _tableService;
        private readonly IUserService _userService;
        private readonly NavigationManager _navigationManager;
        private readonly ISessionStorageService _sessionStorage;

        public string? ErrorMessage { get; set; }
        public Table? CurrentTable { get; set; }

        public AppViewModel(ITableService tableService, IUserService userService, NavigationManager navigationManager, ISessionStorageService sessionStorage)
        {
            _tableService = tableService;
            _userService = userService;
            _navigationManager = navigationManager;
            _sessionStorage = sessionStorage;
        }

        public async Task InitializeAsync()
        {
            Loading = true;

            Task<Table?> getTableTask = Task.FromResult<Table?>(null);

            if (!_navigationManager.TryGetQueryString("tableId", out string? tableId))
            {
                tableId = await _sessionStorage.GetItemAsync<string>(LocalStorageKeys.TableId);
            }

            if (!string.IsNullOrEmpty(tableId))
            {
                getTableTask = _tableService.GetTableAsync(tableId!);
            }

            var getIdentityTask = _userService.GetIdentityAsync();
            await Task.WhenAll(getTableTask, getIdentityTask);

            CurrentTable = await getTableTask;
            
            if (CurrentTable != null)
            {
                await _sessionStorage.SetItemAsync(LocalStorageKeys.TableId, CurrentTable.Id);
            }
            else
            {
                _navigationManager.NavigateTo("/");
            }

            Loading = false;
        }
    }
}
