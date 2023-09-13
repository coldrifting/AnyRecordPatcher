using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataLight : BaseItem, IHasName, IHasModel, IHasObjectBounds, IHasWeightValue
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string? ModelPath { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }

    // Light Specific Data
    public int? Time { get; set; }
    public uint? Radius { get; set; }
    public byte[]? Color { get; set; }
    public uint? Flags { get; set; }
    public float? Falloff { get; set; }
    public float? Fov { get; set; }
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

        if (newRef.Time != oldRef.Time)
            Time = newRef.Time;

        if (newRef.Radius != oldRef.Radius)
            Radius = newRef.Radius;

        if (!newRef.Color.Equals(oldRef.Color))
        {
            Color = new byte[3];
            Color[0] = newRef.Color.R;
            Color[1] = newRef.Color.G;
            Color[2] = newRef.Color.B;
        }

        if (!newRef.Flags.Equals(oldRef.Flags))
            Flags = (uint)newRef.Flags;

        if (!Utility.AreEqual(newRef.FalloffExponent, oldRef.FalloffExponent))
            Falloff = newRef.FalloffExponent;

        if (!Utility.AreEqual(newRef.FOV, oldRef.FOV))
            Fov = newRef.FOV;

        if (!Utility.AreEqual(newRef.NearClip, oldRef.NearClip))
            NearClip = newRef.NearClip;

        if (!Utility.AreEqual(newRef.FlickerPeriod, oldRef.FlickerPeriod))
            FlickerPeriod = newRef.FlickerPeriod;

        if (!Utility.AreEqual(newRef.FlickerIntensityAmplitude, oldRef.FlickerIntensityAmplitude))
            FlickerIntensity = newRef.FlickerIntensityAmplitude;

        if (!Utility.AreEqual(newRef.FlickerMovementAmplitude, oldRef.FlickerMovementAmplitude))
            FlickerMovement = newRef.FlickerMovementAmplitude;

        string newSound = newRef.Sound.FormKey.ToString();
        string oldSound = oldRef.Sound.FormKey.ToString();
        if (newSound is "Null" && oldSound is not "Null")
        {
            SoundDeleted = true;
        }

        if (newSound is not "Null" && newSound != oldSound)
        {
            Sound = newSound;
        }
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

        if (Time is not null)
            rec.Time = Time ?? -1;

        if (Radius is not null)
            rec.Radius = Radius ?? 0;

        if (Color is not null)
        {
            rec.Color = System.Drawing.Color.FromArgb(0, Color[0], Color[1], Color[2]);
        }

        if (Flags is not null)
            rec.Flags = (Light.Flag)Flags;
                
        if (Falloff is not null)
            rec.FalloffExponent = Falloff ?? 0.0f;

        if (Fov is not null)
            rec.FOV = Fov ?? 0.0f;

        if (NearClip is not null)
            rec.NearClip = NearClip ?? 0.0f;

        if (FlickerPeriod is not null)
            rec.FlickerPeriod = FlickerPeriod ?? 0.0f;

        if (FlickerIntensity is not null)
            rec.FlickerIntensityAmplitude = FlickerIntensity ?? 0.0f;

        if (FlickerMovement is not null)
            rec.FlickerMovementAmplitude = FlickerMovement ?? 0.0f;

        if (SoundDeleted is not null)
            rec.Sound = new FormLinkNullable<ISoundDescriptorGetter>();

        if (Sound is not null)
            rec.Sound = new FormLinkNullable<ISoundDescriptorGetter>(Sound.ToFormKey());
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
               Fov is not null ||
               NearClip is not null ||
               FlickerPeriod is not null ||
               FlickerIntensity is not null ||
               FlickerMovement is not null ||
               Sound is not null ||
               SoundDeleted is not null;
    }
}