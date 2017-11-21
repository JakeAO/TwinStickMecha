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
    public TMP_Text PilotFieldLabel = null;
    public GameObject MechaFieldRoot = null;
    public TMP_Text MechaFieldLabel = null;
    public GameObject EnergyFieldRoot = null;
    public Image EnergyFieldActualImage = null;
    public Image EnergyFieldVisableImage = null;

    [Header("Currency Bar")]
    public GameObject CurrencyBarRoot = null;
    public GameObject MoneyFieldRoot = null;
    public TMP_Text MoneyFieldLabel = null;
    public GameObject ScrapFieldRoot = null;
    public TMP_Text ScrapFieldLabel = null;
    public GameObject GooFieldRoot = null;
    public TMP_Text GooFieldLabel = null;

    private void Start()
    {
        bool scrapUnlocked = false;
        bool gooUnlocked = false;

        StatusBarRoot.SetActive(true);
        PilotFieldRoot.SetActive(true);
        PilotFieldLabel.SetText("1");
        MechaFieldRoot.SetActive(true);
        MechaFieldLabel.SetText("1");
        EnergyFieldRoot.SetActive(true);
        EnergyFieldActualImage.fillAmount = 1f;
        EnergyFieldVisableImage.fillAmount = 1f;

        CurrencyBarRoot.SetActive(true);
        MoneyFieldRoot.SetActive(true);
        MoneyFieldLabel.SetText("250");
        ScrapFieldRoot.SetActive(scrapUnlocked);
        ScrapFieldLabel.SetText("30");
        GooFieldRoot.SetActive(gooUnlocked);
        GooFieldLabel.SetText("9");
    }

    public void SetVisibleEnergyOverride(float value)
    {
        EnergyFieldVisableImage.fillAmount = value;
    }
    public void ClearVisibleEnergyOverride()
    {
        EnergyFieldVisableImage.fillAmount = 1f;
    }
}
