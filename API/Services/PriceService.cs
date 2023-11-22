using API.Contracts.Repositories.Entities;
using API.Data;
using API.DTOs.PriceDTO;
using API.DTOs.ProductDTO;
using API.DTOs.UnitDTO;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Services;

public class PriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly PosDbContext _posDbContext;

    public PriceService(IPriceRepository priceRepository, IProductRepository productRepository, PosDbContext posDbContext, IUnitRepository unitRepository)
    {
        _priceRepository = priceRepository;
        _productRepository = productRepository;
        _posDbContext = posDbContext;
        _unitRepository = unitRepository;
    }

    public IEnumerable<PriceDTO> GetAll() 
    {
        var listPrice = _priceRepository.GetAll();
        if (listPrice == null || !listPrice.Any()) return null;

        var dto = listPrice.Select(price => (PriceDTO)price);
        return dto;
    }

    public PriceDTO GetPrice(Guid guid)
    {
        var price = _priceRepository.GetByGuid(guid);
        if (price == null) return null;

        var dto = (PriceDTO)price;
        return dto;
    }

    public PriceDTO? Create(NewPriceDTO newPriceDTO)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                //check if the product is exist
                var isProductExist = _productRepository.IsExits(newPriceDTO.ProductGuid);
                if (!isProductExist) 
                {
                    return null;
                }

                //check existing unit price
                var getUnit = _unitRepository.GetByName(newPriceDTO.UnitName);
                Unit newUnit = null;
                if (getUnit == null)
                {
                    newUnit = (Unit)new NewUnitDTO
                    {
                        Name = newPriceDTO.UnitName,
                    };
                    var createdUnit = _unitRepository.Create(newUnit);
                    if (createdUnit == null) return null;
                }
                var unitToUse = getUnit ?? newUnit;

                var newPrice = (Price)newPriceDTO;
                newPrice.UnitGuid = unitToUse!.Guid;

                var createdPrice = _priceRepository.Create(newPrice);
                if (createdPrice == null) return null;
                transactions.Commit();
                return (PriceDTO)createdPrice;
            }
            catch
            {
                transactions.Rollback();
                return null;
            }
        }
    }

    public bool Edit(PriceDTO priceDto)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                //get price
                var getPrice = _priceRepository.GetByGuid(priceDto.Guid);
                if (getPrice == null) return false;

                //set new price
                var newPrice = (Price)priceDto;
                newPrice.UnitGuid = getPrice.UnitGuid;
                newPrice.ProductGuid = getPrice.ProductGuid;


                //update the price
                var updatePrice = _priceRepository.Update(newPrice);
                if (!updatePrice) return false;

                transactions.Commit();
                return true;
            }
            catch
            {
                transactions.Rollback();
                return false;
            }
        }
    }

    public int Delete(Guid guid)
    {
        using(var transaction  = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var getPrice = _priceRepository.GetByGuid(guid);
                if (getPrice == null) return 0;

                var deletedPrice = _priceRepository.Delete(getPrice);
                if (!deletedPrice) return 0;
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }
        
    }
}
