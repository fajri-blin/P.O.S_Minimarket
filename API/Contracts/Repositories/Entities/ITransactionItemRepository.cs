using API.Contracts.Repositories;
using API.Model.Entities;

namespace API.Contracts.Repositories.Entities
{
    public interface ITransactionItemRepository : IGeneralRepository<TransactionItem>
    {
        IEnumerable<TransactionItem>? GetByTransactionsGuid(Guid guid);
    }
}
