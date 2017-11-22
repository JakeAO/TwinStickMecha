using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionDetailWindow : MonoBehaviour
{
    public RectTransform WindowRect = null;
    public RectTransform HeaderRect = null;
    public RectTransform BodyRect = null;
    public TMP_Text NameLabel = null;
    public TMP_Text DescLabel = null;
    public VerticalLayoutGroup FeeGroup = null;
    public MissionFeeEntry FeePrefab = null;
    public VerticalLayoutGroup RewardGroup = null;
    public MissionRewardEntry RewardPrefab = null;
    public Button AcceptButton = null;

    private MissionDataAsset _mission = null;
    private readonly List<MissionFeeEntry> _feeEntries = new List<MissionFeeEntry>();
    private readonly List<MissionRewardEntry> _rewardEntries = new List<MissionRewardEntry>();

    private void Awake()
    {
        AcceptButton.onClick.AddListener(Button_AcceptButton);
        if (TrashMan.recycleBinForGameObject(FeePrefab.gameObject) == null)
        {
            TrashMan.manageRecycleBin(new TrashManRecycleBin()
            {
                prefab = FeePrefab.gameObject
            });
        }
        if (TrashMan.recycleBinForGameObject(RewardPrefab.gameObject) == null)
        {
            TrashMan.manageRecycleBin(new TrashManRecycleBin()
            {
                prefab = RewardPrefab.gameObject
            });
        }
    }
    private void LateUpdate()
    {
        Vector2 windowSize = WindowRect.sizeDelta;
        windowSize.y = Mathf.Abs(HeaderRect.sizeDelta.y) + Mathf.Abs(BodyRect.sizeDelta.y);
        WindowRect.sizeDelta = windowSize;
    }

    public void Cleanup()
    {
        _feeEntries.ForEach(x => TrashMan.despawn(x.gameObject));
        _rewardEntries.ForEach(x => TrashMan.despawn(x.gameObject));
    }
    public void SetMission(MissionDataAsset mission)
    {
        _mission = mission;

        if (_mission != null)
        {
            NameLabel.SetText(mission.Name);
            DescLabel.SetText(mission.Desc);
            SetupFeeEntries();
            SetupRewardEntries();
            AcceptButton.interactable = CheckCanAfford();

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Button_AcceptButton()
    {
        MissionData mission = new MissionData(_mission);
        SaveData save = Root_Harness.Instance.SaveData;
        WalletData wallet = save.Wallet;

        if (mission.Data.HasEnergyFee)
            save.DecreaseEnergy(mission.Data.EnergyFee);
        if (mission.Data.HasCurrencyFee)
            foreach (var fee in mission.Data.CurrencyFee)
                wallet.RemoveCurrency(fee.Currency, fee.Amount);

        if (mission.Data.HasCurrencyReward)
            foreach (var rwd in mission.Data.CurrencyReward)
                wallet.AddCurrency(rwd.Currency, rwd.Amount);
        if (mission.Data.HasPartReward)
            foreach (var rwd in mission.Data.PartReward)
                save.Parts.Add(new PartData(rwd));
        if (mission.Data.HasMechReward)
            foreach (var rwd in mission.Data.MechReward)
                if (save.Mechs.Count < save.MechSlots)
                    save.Mechs.Add(new MechData(rwd));
        if (mission.Data.HasPilotReward)
            foreach (var rwd in mission.Data.PilotReward)
                if (save.Pilots.Count < save.PilotSlots)
                    save.Pilots.Add(new PilotData(rwd));

        save.Save();

        SetMission(_mission);
    }

    private bool CheckCanAfford()
    {
        SaveData save = Root_Harness.Instance.SaveData;
        WalletData wallet = save.Wallet;

        //if (save.Pilots.Count == 0 || save.Mechs.Count == 0)
        //    return false;
        if (_mission.HasCurrencyReward)
        {
            foreach (var currEntry in _mission.CurrencyFee)
            {
                if (currEntry.Amount > wallet.GetCurrency(currEntry.Currency))
                    return false;
            }
        }
        if (_mission.HasEnergyFee &&
           _mission.EnergyFee > save.Energy)
            return false;

        return true;
    }

    private void EnsureEnoughFeeEntries()
    {
        uint totalRequired = 0;
        if (_mission.HasCurrencyFee)
            totalRequired += (uint)_mission.CurrencyFee.Length;
        if (_mission.HasEnergyFee)
            totalRequired++;

        while (_feeEntries.Count < totalRequired)
        {
            MissionFeeEntry newFee = TrashMan.spawn(FeePrefab.gameObject).GetComponent<MissionFeeEntry>();
            newFee.transform.SetParent(FeeGroup.transform);
            newFee.transform.localScale = Vector3.one;
            _feeEntries.Add(newFee);
        }
    }
    private void EnsureEnoughRewardEntries()
    {
        uint totalRequired = 0;
        if (_mission.HasCurrencyReward)
            totalRequired += (uint)_mission.CurrencyReward.Length;
        if (_mission.HasPartReward)
            totalRequired += (uint)_mission.PartReward.Length;
        if (_mission.HasMechReward)
            totalRequired += (uint)_mission.MechReward.Length;
        if (_mission.HasPilotReward)
            totalRequired += (uint)_mission.PilotReward.Length;

        while (_rewardEntries.Count < totalRequired)
        {
            MissionRewardEntry newReward = TrashMan.spawn(RewardPrefab.gameObject).GetComponent<MissionRewardEntry>();
            newReward.transform.SetParent(RewardGroup.transform);
            newReward.transform.localScale = Vector3.one;
            _rewardEntries.Add(newReward);
        }
    }

    private void SetupFeeEntries()
    {
        EnsureEnoughFeeEntries();

        int feeIdx = 0;
        if (_mission.HasEnergyFee)
        {
            _feeEntries[feeIdx++].SetEnergyFee("Energy", _mission.EnergyFee);
        }
        if (_mission.HasCurrencyFee)
        {
            foreach (var currEntry in _mission.CurrencyFee)
            {
                _feeEntries[feeIdx++].SetCurrencyFee(currEntry.Desc, currEntry.Currency, currEntry.Amount);
            }
        }
        for (; feeIdx < _feeEntries.Count; feeIdx++)
        {
            _feeEntries[feeIdx].gameObject.SetActive(false);
        }
    }
    private void SetupRewardEntries()
    {
        EnsureEnoughRewardEntries();

        int rewardIdx = 0;
        if (_mission.HasCurrencyReward)
        {
            foreach (var currEntry in _mission.CurrencyReward)
            {
                _rewardEntries[rewardIdx++].SetCurrencyReward(currEntry.Desc, currEntry.Currency, currEntry.Amount);
            }
        }
        if (_mission.HasMechReward)
        {
            foreach (var mechEntry in _mission.MechReward)
            {
                _rewardEntries[rewardIdx++].SetMechReward("Mecha", mechEntry);
            }
        }
        if (_mission.HasPilotReward)
        {
            foreach (var pilotEntry in _mission.PilotReward)
            {
                _rewardEntries[rewardIdx++].SetPilotReward("Pilot", pilotEntry);
            }
        }
        if (_mission.HasPartReward)
        {
            foreach (var partEntry in _mission.PartReward)
            {
                _rewardEntries[rewardIdx++].SetPartReward("Part", partEntry);
            }
        }
        for (; rewardIdx < _rewardEntries.Count; rewardIdx++)
        {
            _rewardEntries[rewardIdx].gameObject.SetActive(false);
        }
    }
}
