using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataLight : BaseItem, 
                         IHasName, 
                         IHasModel, 
                         IHasObjectBounds, 
                         IHasWeightValue,
                         IHasFlags
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string? ModelPath { get; set; }
    public bool? ModelPathDeleted { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string[]? Flags { get; set; }

    // Light Specific Data
    public int? Time { get; set; }
    public uint? Radius { get; set; }
    public byte[]? Color { get; set; }
    public float? Falloff { get; set; }
    public float? FOV { get; set; }
    public float? NearClip { get; set; }

    public float? FlickerPeriod { get; set; }
    public float? FlickerIntensity { get; set; }
    public float? FlickerMovement { get; set; }

    public string? Sound { get; set; }
    public bool? SoundDeleted { get; set; }

    public DataLight()
    {
        PatchFileName = "Lights";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ILightGetter x && oldRef is ILightGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ILightGetter newRef, ILightGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasFlags)this).SaveChangesInterface(newRef, oldRef);

        Time = Utility.GetChangesNumber(newRef.Time, oldRef.Time);
        Radius = Utility.GetChangesNumber(newRef.Radius, oldRef.Radius);
        
        if (!newRef.Color.Equals(oldRef.Color))
        {
            Color = new byte[3];
            Color[0] = newRef.Color.R;
            Color[1] = newRef.Color.G;
            Color[2] = newRef.Color.B;
        }

        Falloff = Utility.GetChangesNumber(newRef.FalloffExponent, oldRef.FalloffExponent);
        FOV = Utility.GetChangesNumber(newRef.FOV, oldRef.FOV);
        NearClip = Utility.GetChangesNumber(newRef.NearClip, oldRef.NearClip);
        FlickerPeriod = Utility.GetChangesNumber(newRef.FlickerPeriod, oldRef.FlickerPeriod);
        FlickerIntensity = Utility.GetChangesNumber(newRef.FlickerIntensityAmplitude, oldRef.FlickerIntensityAmplitude);
        FlickerMovement = Utility.GetChangesNumber(newRef.FlickerMovementAmplitude, oldRef.FlickerMovementAmplitude);

        SoundDeleted = Utility.GetDeleted(newRef.Sound, oldRef.Sound);
        Sound = Utility.GetChangesString(newRef.Sound, oldRef.Sound);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ILight recLight)
            Patch(recLight);
    }

    public void Patch(ILight rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);

        rec.Time = Time ?? rec.Time;
        rec.Radius = Radius ?? rec.Radius;

        if (Color is not null)
        {
            rec.Color = System.Drawing.Color.FromArgb(0, Color[0], Color[1], Color[2]);
        }

        rec.FalloffExponent = Falloff ?? rec.FalloffExponent;
        rec.FOV = FOV ?? rec.FOV;
        rec.NearClip = NearClip ?? rec.NearClip;
        rec.FlickerPeriod = FlickerPeriod ?? rec.FlickerPeriod;
        rec.FlickerIntensityAmplitude = FlickerIntensity ?? rec.FlickerIntensityAmplitude;
        rec.FlickerMovementAmplitude = FlickerMovement ?? rec.FlickerMovementAmplitude;

        if (SoundDeleted is not null)
            rec.Sound.SetToNull();

        if (Sound is not null)
            rec.Sound.SetTo(Sound.ToFormKey());
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
               Falloff is not null ||
               FOV is not null ||
               NearClip is not null ||
               FlickerPeriod is not null ||
               FlickerIntensity is not null ||
               FlickerMovement is not null ||
               Sound is not null ||
               SoundDeleted is not null;
    }
}