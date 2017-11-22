using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuHeaderBar : MonoBehaviour
{
    [Header("Status Bar")]
    public GameObject StatusBarRoot = null;
    public GameObject PilotFieldRoot = null;
    public Image PilotIconImage = null;
    public TMP_Text PilotFieldLabel = null;
    public GameObject MechaFieldRoot = null;
    public Image MechaIconImage = null;
    public TMP_Text MechaFieldLabel = null;
    public GameObject EnergyFieldRoot = null;
    public Image EnergyIconImage = null;
    public Image EnergyFieldActualImage = null;
    public Image EnergyFieldVisableImage = null;

    [Header("Currency Bar")]
    public GameObject CurrencyBarRoot = null;
    public GameObject MoneyFieldRoot = null;
    public Image MoneyIconImage = null;
    public TMP_Text MoneyFieldLabel = null;
    public GameObject ScrapFieldRoot = null;
    public Image ScrapIconImage = null;
    public TMP_Text ScrapFieldLabel = null;
    public GameObject GooFieldRoot = null;
    public Image GooIconImage = null;
    public TMP_Text GooFieldLabel = null;

    private void Start()
    {
        SaveData.onEnergyUpdated += OnEnergyUpdated;
        WalletData.onWalletUpdated += OnWalletUpdated;

        PilotIconImage.sprite = IconDataAsset.Instance.PilotSprite;
        MechaIconImage.sprite = IconDataAsset.Instance.MechSprite;
        EnergyIconImage.sprite = IconDataAsset.Instance.EnergySprite;
        MoneyIconImage.sprite = IconDataAsset.Instance.MoneySprite;
        ScrapIconImage.sprite = IconDataAsset.Instance.ScrapSprite;
        GooIconImage.sprite = IconDataAsset.Instance.GooSprite;

        OnWalletUpdated();

        SaveData saveData = Root_Harness.Instance.SaveData;

        StatusBarRoot.SetActive(true);
        PilotFieldRoot.SetActive(true);
        PilotFieldLabel.SetText(saveData.PilotSlots.ToString());
        MechaFieldRoot.SetActive(true);
        MechaFieldLabel.SetText(saveData.MechSlots.ToString());

        OnEnergyUpdated();
    }
    private void OnDestroy()
    {
        WalletData.onWalletUpdated -= OnWalletUpdated;
    }

    public void SetVisibleEnergyOverride(int change)
    {
        SaveData saveData = Root_Harness.Instance.SaveData;

        float visibleEnergy = Mathf.Clamp(saveData.Energy + change, 0, saveData.MaxEnergy);

        EnergyFieldVisableImage.fillAmount = visibleEnergy / saveData.MaxEnergy;
    }
    public void ClearVisibleEnergyOverride()
    {
        EnergyFieldVisableImage.fillAmount = EnergyFieldActualImage.fillAmount;
    }

    private void OnEnergyUpdated()
    {
        SaveData saveData = Root_Harness.Instance.SaveData;

        float energyPerc = saveData.Energy / (float)saveData.MaxEnergy;

        EnergyFieldRoot.SetActive(true);
        EnergyFieldActualImage.fillAmount = energyPerc;
        EnergyFieldVisableImage.fillAmount = energyPerc;
    }
    private void OnWalletUpdated()
    {
        SaveData saveData = Root_Harness.Instance.SaveData;

        CurrencyBarRoot.SetActive(true);
        MoneyFieldRoot.SetActive(true);
        MoneyFieldLabel.SetText(saveData.Wallet.GetCurrency(CurrencyType.Money).ToString());
        ScrapFieldRoot.SetActive(true);
        ScrapFieldLabel.SetText(saveData.Wallet.GetCurrency(CurrencyType.Scrap).ToString());
        GooFieldRoot.SetActive(true);
        GooFieldLabel.SetText(saveData.Wallet.GetCurrency(CurrencyType.Goo).ToString());
    }
}
