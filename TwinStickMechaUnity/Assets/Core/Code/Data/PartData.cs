using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartData : SavableData
{
    #region Saving
    public static PartData CreateFromSave(string saveString)
    {
        PartData part = new PartData();

        return part;
    }
    public override string ToSaveString()
    {
        return string.Empty;
    }
    #endregion

    public string Name { get; set; }

    public PartDataAsset Data { get; private set; }

    public PartData()
    {
        Data = ScriptableObject.CreateInstance<PartDataAsset>();
    }
    public PartData(PartDataAsset asset)
    {
        Data = asset;
    }
}
