using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(VerticalLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(ScrollRect))]
public class ScalingButtonWindow : MonoBehaviour
{
    public struct ButtonData
    {
        public string Text;
        public UnityAction Callback;
    }

    public Button ButtonPrefab = null;
    public int MaximumHeight = -1;
    public int MaximumButtons = -1;

    private readonly Dictionary<Button, ButtonData> _buttons = new Dictionary<Button, ButtonData>();

    private RectTransform _parentRect = null;
    private RectTransform _thisRect = null;
    private VerticalLayoutGroup _layout = null;
    private ScrollRect _scroll = null;

    private void Awake()
    {
        _thisRect = transform as RectTransform;
        _parentRect = transform.parent as RectTransform;
    }

    public void AddButton(ButtonData buttonData)
    {
        Button newButton = TrashMan.spawn(ButtonPrefab.gameObject).GetComponent<Button>();
        newButton.GetComponentInChildren<TMP_Text>().SetText(buttonData.Text);
        newButton.onClick.RemoveAllListeners();
        newButton.onClick.AddListener(buttonData.Callback);
        newButton.transform.SetParent(_thisRect);
        newButton.transform.SetAsLastSibling();

        _buttons.Add(newButton, buttonData);
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_parentRect);
    }
}
