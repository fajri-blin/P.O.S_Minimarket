using API.Contract.Entities;
using API.Data;
using API.DTOs.TransactionsDTO;
using API.Model.Entities;
using System.Net;

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
    public int Edit(TransactionDTO transactionDTO)
    {
        using(var transactionContext = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var isExist = _transactionRepository.IsExits(transactionDTO.Guid);
                if (!isExist) return (int)HttpStatusCode.NotFound;

                //Edit The Transactions
                var editedTransactions = _transactionRepository.Update((Transaction)transactionDTO);
                if(editedTransactions == false)
                {
                    transactionContext.Rollback();
                    return (int)HttpStatusCode.BadRequest;
                }

                //Edit the TransactionsItem
                var getAllTransactionItems = _transactionItemRepository.GetByTransactionsGuid(transactionDTO.Guid);
                if(getAllTransactionItems == null)
                {
                    foreach(var transactionsItem in transactionDTO.TransactionItemsDTO)
                    {
                        var createTransactionsItem = _transactionItemRepository.Create((TransactionItem)transactionsItem);
                        if(createTransactionsItem == null)
                        {
                            transactionContext.Rollback();
                            return (int)HttpStatusCode.BadRequest;
                        }
                    }
                }
                else
                {
                    //Delete Existing Transactions Item
                    foreach(var transactionItem in getAllTransactionItems)
                    {
                        var deleteTransactionsItem = _transactionItemRepository.Delete(transactionItem);
                        if(deleteTransactionsItem == false)
                        {
                            transactionContext.Rollback();
                            return (int)HttpStatusCode.BadRequest;
                        }
                    }
                    //Create New TransactionItem on the Transaction
                    foreach(var transactionItem in transactionDTO.TransactionItemsDTO)
                    {
                        var createTransactionItem = _transactionItemRepository.Create((TransactionItem)transactionItem);
                        if (createTransactionItem == null)
                        {
                            transactionContext.Rollback();
                            return (int)HttpStatusCode.BadRequest;
                        }
                    }
                }
                transactionContext.Commit();
                return (int)HttpStatusCode.OK;
            }
            catch
            {
                transactionContext.Rollback();
                return (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
