using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Plugins.Assets;
using Mutagen.Bethesda.Skyrim.Assets;
using Noggog;

namespace AnyRecordData.Interfaces;
using DataTypes;

public interface IHasModel
{
    public string? ModelPath { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public void SaveChangesInterface(IModeledGetter newRef, IModeledGetter oldRef)
    {
        if ((oldRef.Model != null && newRef.Model != null && !newRef.Model.File.RawPath.Equals(oldRef.Model.File.RawPath)) ||
            (oldRef.Model is null && newRef.Model is not null))
        {
            ModelPath = newRef.Model.File.RawPath;
        }

        if ((oldRef.Model != null &&
             newRef.Model is { AlternateTextures: not null } &&
             !newRef.Model.AlternateTextures.Equals(oldRef.Model.AlternateTextures)) ||
            (oldRef.Model is null && newRef.Model is not null))
        {
            if (newRef.Model.AlternateTextures is null)
                return;

            ModelTextures = new AltTexSet[newRef.Model.AlternateTextures.Count];
            for (int i = 0; i < ModelTextures.Length; i++)
            {
                AltTexSet a = new()
                {
                    Name = newRef.Model.AlternateTextures[i].Name,
                    Texture = newRef.Model.AlternateTextures[i].NewTexture.FormKey.ToString(),
                    Index = newRef.Model.AlternateTextures[i].Index
                };
                ModelTextures[i] = a;
            }
        }
    }
    
    public void PatchInterface<T>(T rec)
        where T : IModeled
    {
        if (ModelPath is not null)
        {
            rec.Model ??= new Model();
            rec.Model.File = new AssetLink<SkyrimModelAssetType>(ModelPath);
        }

        if (ModelTextures is not null)
        {
            rec.Model ??= new Model();
            rec.Model.AlternateTextures ??= new ExtendedList<AlternateTexture>();
            rec.Model.AlternateTextures.Clear();
            foreach (AltTexSet altTex in ModelTextures)
            {
                AlternateTexture texture = new()
                {
                    Name = altTex.Name,
                    NewTexture = new FormLink<ITextureSetGetter>(FormKey.Factory(altTex.Texture)),
                    Index = altTex.Index
                };
                rec.Model.AlternateTextures.Add(texture);
            }
        }
    }

    public bool IsModifiedInterface()
    {
        return ModelPath is not null ||
               ModelTextures is not null;
    }
}
