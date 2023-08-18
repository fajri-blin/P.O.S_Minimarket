using API.Contract.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class TransactionItemRepository : GeneralRepository<TransactionItem>, ITransactionItemRepository
{
    public TransactionItemRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
}
