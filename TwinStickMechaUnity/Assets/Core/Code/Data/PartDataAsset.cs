using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDataAsset : ScriptableObject
{
    public enum PartType
    {
        Frame,
        Mobility,
        Weapon
    }

    public string Name;
    public PartType Category;
}
