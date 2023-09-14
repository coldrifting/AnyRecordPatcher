using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

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
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }

    public string? ModelPath { get; set; }
    public bool? ModelPathDeleted { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }

    public string? EquipmentType { get; set; }
    public bool? EquipmentTypeDeleted { get; set; }

    public string? ObjectEffect { get; set; }
    public bool? ObjectEffectDeleted { get; set; }
    public ushort? EnchantmentAmount { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }
    
    // Weapon Specific
    [YamlMember] public uint? Value { get; set; }
    [YamlMember] public float? Weight { get; set; }
    [YamlMember] public ushort? Damage { get; set; }
    [YamlMember] public ushort? CriticalDamage { get; set; }
    [YamlMember] public float? CriticalMult { get; set; }
    [YamlMember] public float? Speed { get; set; }
    [YamlMember] public float? Reach { get; set; }
    [YamlMember] public float? Stagger { get; set; }
    [YamlMember] public float? RangeMin { get; set; }
    [YamlMember] public float? RangeMax { get; set; }
    [YamlMember] public string? DetectSoundLevel { get; set; }

    public DataWeapon()
    {
        PatchFileName = "Weapons";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IWeaponGetter x && oldRef is IWeaponGetter y)
            SaveChanges(x, y);
    }
    
    private void SaveChanges(IWeaponGetter newRef, IWeaponGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEquipmentType)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEnchantInfo)this).SaveChangesInterface(newRef, oldRef);
        ((IHasDescription)this).SaveChangesInterface(newRef, oldRef);

        Weight = Utility.GetChangesNumber(newRef.BasicStats?.Weight, oldRef.BasicStats?.Weight);
        Value = Utility.GetChangesNumber(newRef.BasicStats?.Value, oldRef.BasicStats?.Value);
        Damage = Utility.GetChangesNumber(newRef.BasicStats?.Damage, oldRef.BasicStats?.Damage);
        
        Reach = Utility.GetChangesNumber(newRef.Data?.Reach, oldRef.Data?.Reach);
        Speed = Utility.GetChangesNumber(newRef.Data?.Speed, oldRef.Data?.Speed);
        Stagger = Utility.GetChangesNumber(newRef.Data?.Stagger, oldRef.Data?.Stagger);
        RangeMin = Utility.GetChangesNumber(newRef.Data?.RangeMin, oldRef.Data?.RangeMin);
        RangeMax = Utility.GetChangesNumber(newRef.Data?.RangeMax, oldRef.Data?.RangeMax);
        
        CriticalDamage = Utility.GetChangesNumber(newRef.Critical?.Damage, oldRef.Critical?.Damage);
        CriticalMult = Utility.GetChangesNumber(newRef.Critical?.PercentMult, oldRef.Critical?.PercentMult);

        DetectSoundLevel = Utility.GetChangesString(newRef.DetectionSoundLevel, oldRef.DetectionSoundLevel);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IWeapon recWeapon)
            Patch(recWeapon);
    }

    public void Patch(IWeapon rec)
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
        
        rec.BasicStats.Weight = Weight ?? rec.BasicStats.Weight;
        rec.BasicStats.Value = Value ?? rec.BasicStats.Value;
        rec.BasicStats.Damage = Damage ?? rec.BasicStats.Damage;
        
        rec.Critical ??= new CriticalData();
        rec.Critical.Damage = CriticalDamage ?? rec.Critical.Damage;
        rec.Critical.PercentMult = CriticalMult ?? rec.Critical.PercentMult;

        rec.Data ??= new WeaponData();
        rec.Data.Speed = Speed ?? rec.Data.Speed;
        rec.Data.Reach = Reach ?? rec.Data.Reach;
        rec.Data.Stagger = Stagger ?? rec.Data.Stagger;
        rec.Data.RangeMin = RangeMin ?? rec.Data.RangeMin;
        rec.Data.RangeMax = RangeMax ?? rec.Data.RangeMax;

        rec.DetectionSoundLevel = Enum.Parse<SoundLevel>(DetectSoundLevel ?? "Normal");
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
