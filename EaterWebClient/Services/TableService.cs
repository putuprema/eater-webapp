namespace EaterWebClient.Services
{
    public class TableService : ITableService
    {
        private readonly IEaterApiClient _eaterApi;

        public TableService(IEaterApiClient eaterApi)
        {
            _eaterApi = eaterApi;
        }

        public async Task<Table?> GetTableAsync(string id)
        {
            try
            {
                return await _eaterApi.GetTableByIdAsync(id);
            }
            catch (EaterApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                throw;
            }
        }
    }
}
