using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataLight : DataBaseItem, 
                         IHasName, 
                         IHasModel, 
                         IHasObjectBounds, 
                         IHasWeightValue,
                         IHasFlags
{
    public string? Name { get; set; }
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    public short[]? Bounds { get; set; }
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    public List<string>? Flags { get; set; }

    // Light Specific Data
    [UsedImplicitly] public int? Time { get; set; }
    [UsedImplicitly] public uint? Radius { get; set; }
    [UsedImplicitly] public byte[]? Color { get; set; }
    [UsedImplicitly] public float? FalloffExponent { get; set; }
    [UsedImplicitly] public float? FOV { get; set; }
    [UsedImplicitly] public float? NearClip { get; set; }
    [UsedImplicitly] public float? FlickerPeriod { get; set; }
    [UsedImplicitly] public float? FlickerIntensityAmplitude { get; set; }
    [UsedImplicitly] public float? FlickerMovementAmplitude { get; set; }
    [UsedImplicitly] public string? Sound { get; set; }

    public DataLight()
    {
        PatchFileName = "Lights";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ILightGetter x && oldRef is ILightGetter y)
            GetData(x, y);
    }

    private void GetData(ILightGetter newRef, ILightGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);

        Time = DataUtils.GetNumber(newRef.Time, oldRef.Time);
        Radius = DataUtils.GetNumber(newRef.Radius, oldRef.Radius);
        
        if (!newRef.Color.Equals(oldRef.Color))
        {
            Color = new byte[3];
            Color[0] = newRef.Color.R;
            Color[1] = newRef.Color.G;
            Color[2] = newRef.Color.B;
        }

        FalloffExponent = DataUtils.GetNumber(newRef.FalloffExponent, oldRef.FalloffExponent);
        FOV = DataUtils.GetNumber(newRef.FOV, oldRef.FOV);
        NearClip = DataUtils.GetNumber(newRef.NearClip, oldRef.NearClip);
        FlickerPeriod = DataUtils.GetNumber(newRef.FlickerPeriod, oldRef.FlickerPeriod);
        FlickerIntensityAmplitude = DataUtils.GetNumber(newRef.FlickerIntensityAmplitude, oldRef.FlickerIntensityAmplitude);
        FlickerMovementAmplitude = DataUtils.GetNumber(newRef.FlickerMovementAmplitude, oldRef.FlickerMovementAmplitude);

        Sound = DataUtils.GetString(newRef.Sound, oldRef.Sound);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ILight recLight)
            Patch(recLight);
    }

    private void Patch(ILight rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);

        rec.Time = DataUtils.PatchNumber(rec.Time, Time);
        rec.Radius = DataUtils.PatchNumber(rec.Radius, Radius);

        if (Color is not null)
        {
            rec.Color = System.Drawing.Color.FromArgb(0, Color[0], Color[1], Color[2]);
        }

        rec.FalloffExponent = DataUtils.PatchNumber(rec.FalloffExponent, FalloffExponent);
        rec.FOV = DataUtils.PatchNumber(rec.FOV, FOV);
        rec.NearClip = DataUtils.PatchNumber(rec.NearClip, NearClip);
        rec.FlickerPeriod = DataUtils.PatchNumber(rec.FlickerPeriod, FlickerPeriod);
        rec.FlickerIntensityAmplitude = DataUtils.PatchNumber(rec.FlickerIntensityAmplitude, FlickerIntensityAmplitude);
        rec.FlickerMovementAmplitude = DataUtils.PatchNumber(rec.FlickerMovementAmplitude, FlickerMovementAmplitude);

        DataUtils.PatchFormLink(rec.Sound, Sound);
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               Time is not null ||
               Radius is not null ||
               Color is not null ||
               Flags is not null ||
               FalloffExponent is not null ||
               FOV is not null ||
               NearClip is not null ||
               FlickerPeriod is not null ||
               FlickerIntensityAmplitude is not null ||
               FlickerMovementAmplitude is not null ||
               Sound is not null;
    }
}