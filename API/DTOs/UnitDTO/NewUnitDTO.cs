using API.Model.Entities;

namespace API.DTOs.UnitDTO;

public class NewUnitDTO
{
    public string Name { get; set; } = string.Empty;

    public static explicit operator Unit(NewUnitDTO newUnitDTO)
    {
        return new Unit
        {
            Guid = Guid.NewGuid(),
            Name = newUnitDTO.Name,
        };
    }
}
