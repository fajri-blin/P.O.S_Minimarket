using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class TransactionItemRepository : GeneralRepository<TransactionItem>, ITransactionItemRepository
{
    public TransactionItemRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
    
    public IEnumerable<TransactionItem>? GetByTransactionsGuid(Guid guid)
    {
        return _posDbContext.Set<TransactionItem>().Where(x => x.TransactionGuid == guid);
    }
}
