namespace EaterWebClient.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetValue(ref _errorMessage, value);
        }

        private Table? _currentTable;
        public Table? CurrentTable
        {
            get => _currentTable;
            private set => SetValue(ref _currentTable, value);
        }

        private readonly ITableService _tableService;

        public AppViewModel(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task FetchTableInfoAsync(string tableId)
        {
            if (_currentTable == null)
            {
                
                Loading = true;
                CurrentTable = await _tableService.GetTableAsync(tableId);
                Loading = false;
            }
        }
    }
}
