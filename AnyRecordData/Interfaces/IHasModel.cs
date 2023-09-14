using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;
using DataTypes;

public interface IHasModel
{
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    
    public void GetDataInterface(IModeledGetter newRef, IModeledGetter oldRef)
    {
        ModelFile = DataUtils.GetString(newRef.Model?.File.RawPath, oldRef.Model?.File.RawPath);
        ModelTextures = DataAltTexSet.GetTextureList(newRef.Model?.AlternateTextures, oldRef.Model?.AlternateTextures);
    }
    
    public void PatchInterface<T>(T rec)
        where T : IModeled
    {
        if (ModelFile is not null)
        {
            rec.Model ??= new Model();
            DataUtils.PatchString(rec.Model.File , ModelFile);
        }

        if (ModelTextures is null) 
            return;
        
        rec.Model ??= new Model();
        DataAltTexSet.PatchModelTextures(rec.Model.AlternateTextures, ModelTextures);
    }

    public bool IsModifiedInterface()
    {
        return ModelFile is not null ||
               ModelTextures is not null;
    }
}
