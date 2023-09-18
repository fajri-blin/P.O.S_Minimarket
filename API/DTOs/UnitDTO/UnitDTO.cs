using API.Model.Entities;

namespace API.DTOs.UnitDTO;

public class UnitDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static explicit operator UnitDTO(Unit unit)
    {
        return new UnitDTO
        {
            Guid = unit.Guid,
            Name = unit.Name,
        };
    }
}
