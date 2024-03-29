﻿using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechData : SavableData
{
    #region Saving
    public static MechData CreateFromSave(string saveString)
    {
        MechData mech = new MechData();

        return mech;
    }
    public override string ToSaveString()
    {
        return string.Empty;
    }
    #endregion

    public string Name { get; set; }

    public MechDataAsset Data { get; private set; }

    public MechData()
    {
        Data = ScriptableObject.CreateInstance<MechDataAsset>();
    }
    public MechData(MechDataAsset asset)
    {
        Data = asset;
    }
}
