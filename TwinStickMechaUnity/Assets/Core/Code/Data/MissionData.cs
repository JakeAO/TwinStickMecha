using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public MissionDataAsset Data { get; private set; }

    public MissionData()
    {
        Data = ScriptableObject.CreateInstance<MissionDataAsset>();
    }
    public MissionData(MissionDataAsset asset)
    {
        Data = asset;
    }
}
