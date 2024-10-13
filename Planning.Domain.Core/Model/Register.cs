using Planning.Domain.Core.Enums;

namespace Planning.Domain.Core.Model;

public record Register(string Name, List<BaseRegister> BaseRegister, RegisterType Type)
{
    public Register() : this("Vazio", new(), default)
    {
    }

    public List<BaseRegister> BaseRegister { get; protected set; } = BaseRegister;
    public string Name { get; protected set; } = Name;
    public RegisterType Type { get; protected set; } = Type;
}
