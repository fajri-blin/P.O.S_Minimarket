using API.Contracts.Repositories.Entities;
using API.Data;
using API.DTOs.TransactionsDTO;
using API.DTOs.TransactionsItemDTO;
using API.Model.Entities;
using System.Data;
using System.Net;

namespace API.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionItemRepository _transactionItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly PosDbContext _posDbContext;

    public TransactionService(ITransactionRepository transactionRepository, 
                              ITransactionItemRepository transactionItemRepository, 
                              IEmployeeRepository employeeRepository,
                              IPriceRepository priceRepository,
                              IProductRepository productRepository,
                              PosDbContext posDbContext)
    {
        _transactionRepository = transactionRepository;
        _transactionItemRepository = transactionItemRepository;
        _employeeRepository = employeeRepository;
        _priceRepository = priceRepository;
        _productRepository = productRepository;
        _posDbContext = posDbContext;
    }

    public IEnumerable<TransactionDTO>? GetAll()
    {
        var transactions = _transactionRepository.GetAll();
        if (transactions == null) return null;

        ICollection<TransactionDTO> transactionDto = new List<TransactionDTO>();
        foreach (var transaction in transactions) 
        {
            transactionDto.Add((TransactionDTO)transaction);
        }
        return transactionDto;
    }

    public TransactionDTO? GetDetailTransaction(Guid guid)
    {
        var getTransaction = _transactionRepository.GetByGuid(guid);
        if (getTransaction == null) return null;
        var transactionDto = (TransactionDTO)getTransaction;

        var TransactionItems = _transactionItemRepository.GetByTransactionsGuid(transactionDto.Guid);
        if (TransactionItems != null)
        {
            foreach(var transactionItem in TransactionItems)
            {
                transactionDto.TransactionItemsDTO!.Add((TransactionItemDTO)transactionItem);
            }
        }
        return transactionDto;
    }

    public TransactionDTO? Create(NewTransactionDTO transactionDTO)
    {
        using(var transactionContext = _posDbContext.Database.BeginTransaction()) 
        {
            try
            {
                //check if the Employee is exist
                var isExistEmployee = _employeeRepository.IsExits((Guid)transactionDTO.EmployeeGuid!);
                if (!isExistEmployee)
                {
                    return null;
                }

                //insert the Transaction Detail to DB
                var transaction = _transactionRepository.Create((Transaction)transactionDTO);
                if(transaction == null)
                {
                    transactionContext.Rollback();
                    return null;
                }

                //insert the List of Transaction Item to DB
                foreach(var transactionItem in transactionDTO.TransactionItemDTOs!)
                {
                    try
                    {
                        //checking the existing Price and Product 
                        if(!_productRepository.IsExits((Guid)transactionItem.ProductGuid!))
                        {
                            return null;
                        }
                        if(!_priceRepository.IsExits((Guid)transactionItem.PriceGuid!))
                        {
                            return null;
                        }

                        var createdItem = (TransactionItem) transactionItem;
                        createdItem.TransactionGuid = transaction.Guid;
                        var resultTransactionItem = _transactionItemRepository.Create(createdItem);
                        if (resultTransactionItem == null)
                        {
                            transactionContext.Rollback();
                            return null;
                        };
                    }catch
                    {

                    }
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
                    foreach(var transactionsItem in transactionDTO.TransactionItemsDTO!)
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
                    foreach(var transactionItem in transactionDTO.TransactionItemsDTO!)
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

    public int Delete(Guid guid)
    {
        using(var transactionContext = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                //Check Existing Transactions
                var isExist = _transactionRepository.IsExits(guid);
                if (isExist == false) return (int)HttpStatusCode.NotFound;
                var getTransaction = _transactionRepository.GetByGuid(guid)!;

                //Delete TransactionsItem
                if (getTransaction.TransactionItems != null)
                {
                    var getTransactionItem = _transactionItemRepository.GetByTransactionsGuid(guid)!;
                    foreach(var transactionItem in getTransactionItem)
                    {
                        var deleteTransactionItem = _transactionItemRepository.Delete(transactionItem);
                        if (!deleteTransactionItem)
                        {
                            transactionContext.Rollback();
                            return (int)HttpStatusCode.BadRequest;
                        }
                    }
                }

                //Delete Transaction
                var deleteTransaction = _transactionRepository.Delete(getTransaction);
                if (!deleteTransaction)
                {
                    transactionContext.Rollback();
                    return (int)HttpStatusCode.BadRequest;
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
