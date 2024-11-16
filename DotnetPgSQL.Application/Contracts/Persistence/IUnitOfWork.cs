namespace DotnetPgSQL.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        Task<int> Save();
    }
}