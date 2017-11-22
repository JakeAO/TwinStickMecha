using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveData
{
    private const string SAVE_KEY = "Save";
    private const uint SAVE_VERSION = 0;

    public static event Action onEnergyUpdated = null;

    private WalletData _wallet = null;
    private readonly List<PilotData> _pilots = new List<PilotData>();
    private readonly List<MechData> _mechs = new List<MechData>();
    private readonly List<PartData> _parts = new List<PartData>();
    private uint _energy = 10u;
    private uint _maxEnergy = 10u;
    private uint _pilotSlots = 2;
    private uint _mechSlots = 2;

    public WalletData Wallet { get { return _wallet; } }
    public List<PilotData> Pilots { get { return _pilots; } }
    public List<MechData> Mechs { get { return _mechs; } }
    public List<PartData> Parts { get { return _parts; } }
    public uint Energy { get { return _energy; } }
    public uint MaxEnergy { get { return _maxEnergy; } }
    public uint PilotSlots { get { return _pilotSlots; } }
    public uint MechSlots { get { return _mechSlots; } }

    public void IncreaseEnergy(uint amount)
    {
        _energy = (uint)Mathf.Min(_energy + amount, _maxEnergy);

        if (onEnergyUpdated != null)
            onEnergyUpdated();
    }
    public void DecreaseEnergy(uint amount)
    {
        _energy = (uint)Mathf.Max(_energy - amount, 0);

        if (onEnergyUpdated != null)
            onEnergyUpdated();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            _wallet = new WalletData();
            _pilots.Clear();
            _mechs.Clear();
            _parts.Clear();
            _energy = 10;
            _maxEnergy = 10;
            Save();
        }
        else if (!TryLoad())
        {
            Debug.LogError("Failed to load save data from PlayerPrefs:\n" + PlayerPrefs.GetString("SAVE_KEY", "No Save Data"));
        }
    }
    public void Save()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Version&{0}&", SAVE_VERSION);
        sb.AppendFormat("Wallet&{0}&", _wallet.ToSaveString());
        sb.AppendFormat("Energy&{0}&", _energy);
        sb.AppendFormat("PilotCount&{0}&", _pilots.Count);
        foreach (var pilot in _pilots)
            sb.AppendFormat("{0}&", pilot.ToSaveString());
        sb.AppendFormat("MechCount&{0}&", _mechs.Count);
        foreach (var mech in _mechs)
            sb.AppendFormat("{0}&", mech.ToSaveString());
        sb.AppendFormat("PartCount&{0}&", _parts.Count);
        foreach (var part in _parts)
            sb.AppendFormat("{0}&", part.ToSaveString());
        sb.AppendFormat("PilotSlots&{0}&", _pilotSlots);
        sb.AppendFormat("MechSlots&{0}&", _mechSlots);

        PlayerPrefs.SetString(SAVE_KEY, sb.ToString());
        PlayerPrefs.Save();

        Debug.Log(sb.ToString());
    }

    private bool TryLoad()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return false;

        int idx = 1;
        string rawSave = PlayerPrefs.GetString(SAVE_KEY);
        string[] splitSave = rawSave.Split('&');

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Load() -> {0}\n", rawSave);
        for (int i = 0; i < splitSave.Length; i++)
            sb.AppendFormat("  {0,2}: {1}\n", i, splitSave[i]);
        Debug.Log(sb.ToString());

        uint version = uint.Parse(splitSave[idx]);

        if (version != SAVE_VERSION)
        {
            Debug.LogWarningFormat("Version of save file ({0}) does not match the version in SaveData.cs ({1}).", version, SAVE_VERSION);
        }

        _pilots.Clear();
        _mechs.Clear();
        _parts.Clear();

        _wallet = WalletData.CreateFromSave(splitSave[idx += 2]);
        _energy = uint.Parse(splitSave[idx += 2]);

        uint pilotCount = uint.Parse(splitSave[idx += 2]);
        for (int i = 0; i < pilotCount; i++)
            _pilots.Add(PilotData.CreateFromSave(splitSave[idx++]));

        uint mechCount = uint.Parse(splitSave[idx += 2]);
        for (int i = 0; i < mechCount; i++)
            _mechs.Add(MechData.CreateFromSave(splitSave[idx++]));

        uint partCount = uint.Parse(splitSave[idx += 2]);
        for (int i = 0; i < partCount; i++)
            _parts.Add(PartData.CreateFromSave(splitSave[idx++]));

        _pilotSlots = uint.Parse(splitSave[idx += 2]);
        _mechSlots = uint.Parse(splitSave[idx += 2]);

        return true;
    }

#if UNITY_EDITOR
    [MenuItem("TSM/Clear Save")]
    private static void ClearSaveData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
    }
#endif
}
