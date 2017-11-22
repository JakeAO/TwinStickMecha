using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class Root_Harness : MonoBehaviour
{
    public static Root_Harness Instance { get; private set; }

    public readonly SaveData SaveData = new SaveData();

    private bool _showGUI = false;

    private void Awake()
    {
        Debug.Assert(Instance == null, "Root_Harness instance is already set!");

        Instance = this;
        SaveData.Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _showGUI = !_showGUI;
    }
    private void OnGUI()
    {
        if (_showGUI)
        {
            Rect areaRect = new Rect(0, 0, 300, 600);

            GUI.Box(areaRect, string.Empty);
            GUILayout.BeginArea(areaRect);
            {
                // SAVE/LOAD
                if (GUILayout.Button("Save Game", GUILayout.Width(100)))
                    SaveData.Save();
                if (GUILayout.Button("Load Game", GUILayout.Width(100)))
                    SaveData.Load();
                GUILayout.Space(5);

                // WALLET
                GUILayout.Label("Wallet", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
                foreach (var rawEnum in System.Enum.GetValues(typeof(CurrencyType)))
                {
                    CurrencyType currencyEnum = (CurrencyType)rawEnum;
                    GUILayout.BeginHorizontal();
                    {
                        uint amount = SaveData.Wallet.GetCurrency(currencyEnum);
                        GUILayout.Label(currencyEnum.ToString() + ": ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Italic }, GUILayout.ExpandWidth(false));
                        if (amount > 0)
                        {
                            GUILayout.Box("<", GUILayout.ExpandWidth(false));
                        }
                        else if (GUILayout.RepeatButton("<", GUILayout.ExpandWidth(false)))
                        {
                            SaveData.Wallet.RemoveCurrency(currencyEnum, 1);
                        }
                        GUILayout.Label(SaveData.Wallet.GetCurrency(currencyEnum).ToString(), GUILayout.ExpandWidth(false));
                        if (GUILayout.RepeatButton(">", GUILayout.ExpandWidth(false)))
                        {
                            SaveData.Wallet.AddCurrency(currencyEnum, 1);
                        }
                    }
                    GUILayout.EndHorizontal();
                }

                // GARAGE

                // BARRACKS

                // SHOP

                // JUNKYARD

                // MISSIONS

            }
            GUILayout.EndArea();
        }
    }
}