using System.Text.Json.Serialization;

namespace Planning.Domain.Core.Model;
public class BaseRegister
{
    [JsonConstructor]
    protected BaseRegister(Guid id, decimal? referenceValue, DateTime dueDate, bool active, bool isPaid)
    {
        Id = id;
        ReferenceValue = referenceValue;
        DueDate = dueDate;
        Active = active;
        IsPaid = isPaid;
    }

    public BaseRegister(decimal? referenceValue, DateTime dueDate)
    {
        Id = Guid.NewGuid();
        ReferenceValue = referenceValue;
        DueDate = dueDate;
        Active = true;
        IsPaid = false;
    }

    public BaseRegister()
    {
    }

    public Guid Id { get; protected set; }
    public decimal? ReferenceValue { get; protected set; }
    public DateTime DueDate { get; protected set; }
    public bool Active { get; protected set; }
    public bool IsPaid { get; protected set; }

    public void UpdateReferenceValue(decimal referenceValue) => ReferenceValue = referenceValue;
    public void Paid() => IsPaid = true;
    public void Unpaid() => IsPaid = false;
    public void Deactivate() => Active = false;
    public void Activate() => Active = true;
}