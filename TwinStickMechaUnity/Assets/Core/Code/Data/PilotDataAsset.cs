using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotDataAsset : ScriptableObject
{
    public enum PilotType
    {
        Generalist,
        Sniper,
        Support
    }

    public string Name;
    public PilotType Class;
}
