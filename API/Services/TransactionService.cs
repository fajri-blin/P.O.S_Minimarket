using API.Contract.Entities;
using API.Data;
using API.DTOs.TransactionsDTO;
using API.Model.Entities;

namespace API.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionItemRepository _transactionItemRepository;
    private readonly PosDbContext _posDbContext;

    public TransactionService(ITransactionRepository transactionRepository, ITransactionItemRepository transactionItemRepository, PosDbContext posDbContext)
    {
        _transactionRepository = transactionRepository;
        _transactionItemRepository = transactionItemRepository;
        _posDbContext = posDbContext;
    }

    public TransactionDTO? Create(NewTransactionDTO transactionDTO)
    {
        using(var transactionContext = _posDbContext.Database.BeginTransaction()) 
        {
            try
            {
                //insert the Transaction Detail to DB
                var transaction = _transactionRepository.Create((Transaction) transactionDTO);
                if(transaction == null)
                {
                    transactionContext.Rollback();
                    return null;
                }

                //insert the List of Transaction Item to DB
                foreach(var transactionItem in transactionDTO.TransactionItemDTOs)
                {
                    var createdItem = (TransactionItem) transactionItem;
                    var resultTransactionItem = _transactionItemRepository.Create(createdItem);
                    if (resultTransactionItem == null)
                    {
                        transactionContext.Rollback();
                    };
                }
                transactionContext.Commit();
                var dto = (TransactionDTO) transaction;
                return dto;
            }
            catch
            {
                transactionContext.Rollback();
                return null;
            }
        }
    } 
}
