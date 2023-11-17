using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class TransactionRepository : GeneralRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
}
