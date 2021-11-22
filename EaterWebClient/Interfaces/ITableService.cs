namespace EaterWebClient.Interfaces
{
    public interface ITableService
    {
        Task<Table?> GetTableAsync(string id);
    }
}
