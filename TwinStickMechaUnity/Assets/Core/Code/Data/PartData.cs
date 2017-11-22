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
}
