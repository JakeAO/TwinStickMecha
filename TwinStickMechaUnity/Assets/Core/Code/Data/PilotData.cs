﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotData : SavableData
{
    #region Saving
    public static PilotData CreateFromSave(string saveString)
    {
        PilotData pilot = new PilotData();

        return pilot;
    }
    public override string ToSaveString()
    {
        return string.Empty;
    }
    #endregion

    public string Name { get; set; }
}
