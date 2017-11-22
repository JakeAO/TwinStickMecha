using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionFeeEntry : MonoBehaviour
{
    public TMP_Text DescLabel = null;
    public Image IconImage = null;
    public TMP_Text ValueLabel = null;

    public void SetCurrencyFee(string desc, CurrencyType type, uint amount)
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
    public void SetEnergyFee(string desc, uint amount)
    {
        DescLabel.SetText(desc);
        IconImage.sprite = IconDataAsset.Instance.EnergySprite;
        ValueLabel.SetText(amount.ToString());
        gameObject.SetActive(true);
    }
}
