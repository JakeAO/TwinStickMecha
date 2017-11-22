using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionRewardEntry : MonoBehaviour
{
    public TMP_Text DescLabel = null;
    public Image IconImage = null;
    public TMP_Text ValueLabel = null;

    public void SetCurrencyReward(string desc, CurrencyType type, uint amount)
    {
        DescLabel.SetText(desc);
        switch (type)
        {
            case CurrencyType.Money:
                IconImage.sprite = IconDataAsset.Instance.MoneySprite;
                break;
            case CurrencyType.Scrap:
                IconImage.sprite = IconDataAsset.Instance.ScrapSprite;
                break;
            case CurrencyType.Goo:
                IconImage.sprite = IconDataAsset.Instance.GooSprite;
                break;
        }
        ValueLabel.SetText(amount.ToString());
        gameObject.SetActive(true);
    }
    public void SetPartReward(string desc, PartDataAsset part)
    {
        DescLabel.SetText(desc);
        IconImage.sprite = IconDataAsset.Instance.PartSprite;
        ValueLabel.SetText(part.Name);
        gameObject.SetActive(true);
    }
    public void SetMechReward(string desc, MechDataAsset mech)
    {
        DescLabel.SetText(desc);
        IconImage.sprite = IconDataAsset.Instance.MechSprite;
        ValueLabel.SetText(mech.Name);
        gameObject.SetActive(true);
    }
    public void SetPilotReward(string desc, PilotDataAsset pilot)
    {
        DescLabel.SetText(desc);
        IconImage.sprite = IconDataAsset.Instance.PilotSprite;
        ValueLabel.SetText(pilot.Name);
        gameObject.SetActive(true);
    }
}
