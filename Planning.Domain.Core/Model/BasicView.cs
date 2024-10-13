using System.Collections.Concurrent;
using Planning.Domain.Core.Enums;

namespace Planning.Domain.Core.Model;

public class BasicView(ConcurrentBag<Register> registers)
{
    public BasicView(ConcurrentBag<Register> registers, DateTime start, DateTime end) : this(registers)
    {
        Start = start;
        End = end;
    }

    public ConcurrentBag<Register> Registers { get; protected set; } = registers;
    public DateTime Start { get; protected set; }
    public DateTime End { get; protected set; }
    public void SetStart(DateTime start) => Start = start;
    public void SetEnd(DateTime end) => End = end;
    public void AddMovements(List<Register> movements) => movements.ForEach(Registers.Add);
}

public static class BasicViewExtension
{
    public static IReadOnlyList<Register> Filter(this IEnumerable<Register> registers, DateTime start, DateTime end, RegisterType type, bool isPaid = false) => registers.Where(e => e.Type == type && e.Filter(start, end, isPaid).Any()).Select(e => new Register(e.Name, [.. e.Filter(start, end, isPaid)], e.Type)).ToList();
    public static IReadOnlyList<BaseRegister> Filter(this Register register, DateTime start, DateTime end, bool isPaid) => register.BaseRegister.Where(e => e.DueDate >= start && e.DueDate <= end && e.Active && e.IsPaid == isPaid).ToList();
    public static decimal GetTotalBillsValue(this BasicView view) => view.GetBills().Sum(e => e.BaseRegister.Sum(f => f.ReferenceValue.GetValueOrDefault()));
    public static decimal GetTotalReceivableValue(this BasicView view) => view.GetReceivables().Sum(e => e.BaseRegister.Sum(f => f.ReferenceValue.GetValueOrDefault()));
    public static decimal GetInitialAsset(this BasicView view) => view.GetAssets().Sum(e => e.BaseRegister.Sum(f => f.ReferenceValue.GetValueOrDefault()));
    public static IReadOnlyList<Register> GetAssets(this BasicView view) => Filter(view.Registers, view.Start, view.End, RegisterType.Asset);
    public static IReadOnlyList<Register> GetReceivables(this BasicView view) => Filter(view.Registers, view.Start, view.End, RegisterType.Payment);
    public static IReadOnlyList<Register> GetReceived(this BasicView view) => Filter(view.Registers, view.Start, view.End, RegisterType.Payment, true);
    public static IReadOnlyList<Register> GetBills(this BasicView view) => Filter(view.Registers, view.Start, view.End, RegisterType.Bill);
    public static IReadOnlyList<Register> GetPaidBills(this BasicView view) => Filter(view.Registers, view.Start, view.End, RegisterType.Bill, true);
}
