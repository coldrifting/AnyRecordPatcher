using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataWeapon : DataBaseItem,
                          IHasName,
                          IHasKeywords,
                          IHasModel,
                          IHasObjectBounds,
                          IHasPickUpPutDownSound,
                          IHasEquipmentType,
                          IHasEnchantInfo,
                          IHasDescription
{
    public string? Name { get; set; }
    public List<string>? Keywords { get; set; }
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    public short[]? Bounds { get; set; }
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public string? EquipmentType { get; set; }
    public string? ObjectEffect { get; set; }
    public ushort? EnchantmentAmount { get; set; }
    public string? Description { get; set; }
    
    // Weapon Specific
    [UsedImplicitly] public uint? Value { get; set; }
    [UsedImplicitly] public float? Weight { get; set; }
    [UsedImplicitly] public ushort? Damage { get; set; }
    [UsedImplicitly] public ushort? CriticalDamage { get; set; }
    [UsedImplicitly] public float? CriticalMult { get; set; }
    [UsedImplicitly] public float? Speed { get; set; }
    [UsedImplicitly] public float? Reach { get; set; }
    [UsedImplicitly] public float? Stagger { get; set; }
    [UsedImplicitly] public float? RangeMin { get; set; }
    [UsedImplicitly] public float? RangeMax { get; set; }
    [UsedImplicitly] public string? DetectSoundLevel { get; set; }

    public DataWeapon()
    {
        PatchFileName = "Weapons";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IWeaponGetter x && oldRef is IWeaponGetter y)
            GetData(x, y);
    }
    
    private void GetData(IWeaponGetter newRef, IWeaponGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasEnchantInfo)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);

        Weight = DataUtils.GetNumber(newRef.BasicStats?.Weight, oldRef.BasicStats?.Weight);
        Value = DataUtils.GetNumber(newRef.BasicStats?.Value, oldRef.BasicStats?.Value);
        Damage = DataUtils.GetNumber(newRef.BasicStats?.Damage, oldRef.BasicStats?.Damage);
        
        Reach = DataUtils.GetNumber(newRef.Data?.Reach, oldRef.Data?.Reach);
        Speed = DataUtils.GetNumber(newRef.Data?.Speed, oldRef.Data?.Speed);
        Stagger = DataUtils.GetNumber(newRef.Data?.Stagger, oldRef.Data?.Stagger);
        RangeMin = DataUtils.GetNumber(newRef.Data?.RangeMin, oldRef.Data?.RangeMin);
        RangeMax = DataUtils.GetNumber(newRef.Data?.RangeMax, oldRef.Data?.RangeMax);
        
        CriticalDamage = DataUtils.GetNumber(newRef.Critical?.Damage, oldRef.Critical?.Damage);
        CriticalMult = DataUtils.GetNumber(newRef.Critical?.PercentMult, oldRef.Critical?.PercentMult);

        DetectSoundLevel = DataUtils.GetString(newRef.DetectionSoundLevel, oldRef.DetectionSoundLevel);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IWeapon recWeapon)
            Patch(recWeapon);
    }

    private void Patch(IWeapon rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasEquipmentType)this).PatchInterface(rec);
        ((IHasEnchantInfo)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);
        
        rec.BasicStats ??= new WeaponBasicStats();
        rec.BasicStats.Weight = DataUtils.PatchNumber(rec.BasicStats.Weight, Weight);
        rec.BasicStats.Value = DataUtils.PatchNumber(rec.BasicStats.Value, Value);
        rec.BasicStats.Damage = DataUtils.PatchNumber(rec.BasicStats.Damage, Damage);
        
        rec.Critical ??= new CriticalData();
        rec.Critical.Damage = DataUtils.PatchNumber(rec.Critical.Damage, CriticalDamage);
        rec.Critical.PercentMult = DataUtils.PatchNumber(rec.Critical.PercentMult, CriticalMult);

        rec.Data ??= new WeaponData();
        rec.Data.Speed = DataUtils.PatchNumber(rec.Data.Speed, Speed);
        rec.Data.Reach = DataUtils.PatchNumber(rec.Data.Reach, Reach);
        rec.Data.Stagger = DataUtils.PatchNumber(rec.Data.Stagger, Stagger);
        rec.Data.RangeMin = DataUtils.PatchNumber(rec.Data.RangeMin, RangeMin);
        rec.Data.RangeMax = DataUtils.PatchNumber(rec.Data.RangeMax, RangeMax);

        if (DetectSoundLevel is not null)
            rec.DetectionSoundLevel = Enum.Parse<SoundLevel>(DetectSoundLevel);
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               ((IHasEquipmentType)this).IsModifiedInterface() ||
               ((IHasEnchantInfo)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               Value is not null ||
               Weight is not null ||
               Damage is not null ||
               CriticalDamage is not null ||
               CriticalMult is not null ||
               Speed is not null ||
               Reach is not null ||
               Stagger is not null ||
               RangeMin is not null ||
               RangeMax is not null ||
               DetectSoundLevel is not null;
    }
}
