using API.Model.Entities;

namespace API.Contract.Entities
{
    public interface ITransactionItemRepository : IGeneralRepository<TransactionItem>
    {
        IEnumerable<TransactionItem>? GetByTransactionsGuid(Guid guid);
    }
}
