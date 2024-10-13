using Planning.Domain.Core.Enums;
using Planning.Domain.Core.Model;

namespace Planning.Application.Api.Dto;

public class RegisterDto(string name, RegisterType type, List<BaseRegisterDto> registers)
{
    public string Name { get; protected set; } = name;
    public RegisterType Type { get; protected set; } = type;
    public List<BaseRegisterDto> Registers { get; protected set; } = registers;
}

public class BaseRegisterDto
{
    public BaseRegisterDto(decimal? referenceValue, DateTime dueDate)
    {
        ReferenceValue = referenceValue;
        DueDate = dueDate;
    }

    public decimal? ReferenceValue { get; protected set; }
    public DateTime DueDate { get; protected set; }

}

public static class Extension
{
    public static Register MapToDomain(this RegisterDto dto) => new(dto.Name, dto.Registers.MapToDomain(), dto.Type);
    public static List<BaseRegister> MapToDomain(this List<BaseRegisterDto> dto) => dto.Select(e => new BaseRegister(e.ReferenceValue, e.DueDate)).ToList();

}
