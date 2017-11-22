using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Money,
    Scrap,
    Goo
}

public class WalletData : SavableData
{
    public static event Action onWalletUpdated = null;

    #region Saving
    public static WalletData CreateFromSave(string saveString)
    {
        string[] splitSave = saveString.Split('|');

        WalletData wallet = new WalletData();
        wallet._currency[CurrencyType.Money] = uint.Parse(splitSave[1]);
        wallet._currency[CurrencyType.Scrap] = uint.Parse(splitSave[3]);
        wallet._currency[CurrencyType.Goo] = uint.Parse(splitSave[5]);
        return wallet;
    }
    public override string ToSaveString()
    {
        return string.Format("Money|{0}|Scrap|{1}|Goo|{2}",
                             GetCurrency(CurrencyType.Money),
                             GetCurrency(CurrencyType.Scrap),
                             GetCurrency(CurrencyType.Goo));
    }
    #endregion

    private readonly Dictionary<CurrencyType, uint> _currency = new Dictionary<CurrencyType, uint>()
    {
        {CurrencyType.Money, 0u},
        {CurrencyType.Scrap, 0u},
        {CurrencyType.Goo, 0u}
    };

    public bool CanAfford(CurrencyType type, uint amount)
    {
        return _currency[type] >= amount;
    }
    public uint GetCurrency(CurrencyType type)
    {
        return _currency[type];
    }
    public void AddCurrency(CurrencyType type, uint amount)
    {
        _currency[type] += amount;

        if (onWalletUpdated != null)
            onWalletUpdated();
    }
    public void RemoveCurrency(CurrencyType type, uint amount)
    {
        Debug.AssertFormat(CanAfford(type, amount), "Cannot afford to deduct {0} {1}", amount, type);

        _currency[type] -= amount;

        if (onWalletUpdated != null)
            onWalletUpdated();
    }
}
