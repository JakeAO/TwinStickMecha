using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDataAsset : ScriptableObject
{
    public enum MissionType
    {
        Battle,
        Survival,
        Scavenge,
        Recovery,
        Escort
    }
    [System.Serializable]
    public class CurrencyData
    {
        public string Desc;
        public CurrencyType Currency;
        public uint Amount;
    }

    public string Name;
    public string Desc;
    public MissionType Category;
    public uint EnergyFee;
    public CurrencyData[] CurrencyFee;
    public CurrencyData[] CurrencyReward;
    public PartDataAsset[] PartReward;
    public MechDataAsset[] MechReward;
    public PilotDataAsset[] PilotReward;

    public bool HasCurrencyFee { get { return CurrencyFee.Length != 0; } }
    public bool HasEnergyFee { get { return EnergyFee != 0; } }

    public bool HasCurrencyReward { get { return CurrencyReward.Length != 0; } }
    public bool HasPartReward { get { return PartReward.Length != 0; } }
    public bool HasMechReward { get { return MechReward.Length != 0; } }
    public bool HasPilotReward { get { return PilotReward.Length != 0; } }
}
