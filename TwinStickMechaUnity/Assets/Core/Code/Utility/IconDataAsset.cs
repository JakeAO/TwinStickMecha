using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDataAsset : ScriptableObject
{
    private static IconDataAsset _Instance = null;
    public static IconDataAsset Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = Resources.Load<IconDataAsset>("IconDataAsset");
            }
            return _Instance;
        }
    }

    [SerializeField]
    private Sprite _moneySprite;
    [SerializeField]
    private Sprite _scrapSprite;
    [SerializeField]
    private Sprite _gooSprite;
    [SerializeField]
    private Sprite _partSprite;
    [SerializeField]
    private Sprite _mechSprite;
    [SerializeField]
    private Sprite _pilotSprite;
    [SerializeField]
    private Sprite _energySprite;

    public Sprite MoneySprite { get { return _moneySprite; } }
    public Sprite ScrapSprite { get { return _scrapSprite; } }
    public Sprite GooSprite { get { return _gooSprite; } }
    public Sprite PartSprite { get { return _partSprite; } }
    public Sprite MechSprite { get { return _mechSprite; } }
    public Sprite PilotSprite { get { return _pilotSprite; } }
    public Sprite EnergySprite { get { return _energySprite; } }
}
