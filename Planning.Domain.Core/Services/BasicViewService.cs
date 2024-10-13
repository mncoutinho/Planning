using Parquet.Serialization;
using Planning.Domain.Core.Model;

namespace Planning.Domain.Core.Services;
public class BasicViewService
{
    BasicView Data = new([]);
    public BasicView ViewData() => Data;
    string FilePath = "C:\\Repos\\Planning\\Planning.Aplication.Api\\dataParquet.parquet";

    public async Task LoadFromFile()
    {
        if (!File.Exists(FilePath))
            await ParquetSerializer.SerializeAsync(Data.Registers, FilePath);
        else
            Data.AddMovements([.. (await ParquetSerializer.DeserializeAsync<Register>(FilePath))]);
    }

    public async Task UpdateFile() => await ParquetSerializer.SerializeAsync(Data.Registers, FilePath);

    public void CreateRegister(List<Register> registers)
    {
        Data.AddMovements(registers);
    }

    public void SetStart(DateTime start) => Data.SetStart(start);
    public void SetEnd(DateTime end) => Data.SetEnd(end);
    public decimal GetTotalBillsValue() => Data.GetTotalBillsValue();
    public decimal GetTotalReceivableValue() => Data.GetTotalReceivableValue();
    public decimal GetInitialAsset() => Data.GetInitialAsset();
    public void Deactivate(Guid id) => Data.Registers.Select(e => e.BaseRegister.First(f => f.Id == id)).First(e => e.Id == id).Deactivate();
    public void Activate(Guid id) => Data.Registers.Select(e => e.BaseRegister.First(f => f.Id == id)).First(e => e.Id == id).Activate();
    public void Paid(Guid id) => Data.Registers.Select(e => e.BaseRegister.First(f => f.Id == id)).First(e => e.Id == id).Paid();
    public void UpdateValue(Guid id, decimal referenceValue) => Data.Registers.Select(e => e.BaseRegister.First(f => f.Id == id)).First(e => e.Id == id).UpdateReferenceValue(referenceValue);
}
