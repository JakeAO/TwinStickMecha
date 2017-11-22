using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Root_MissionBoard : ContextSceneRoot
{
    public List<MissionDataAsset> Missions = null;
    [Space(5f)]
    public Button MissionButtonPrefab = null;
    public VerticalLayoutGroup MissionParentRoot = null;
    public RectTransform MissionListRoot = null;
    public MissionDetailWindow DetailWindow = null;

    protected override void Awake()
    {
        base.Awake();
        DetailWindow.SetMission(null);

        if (TrashMan.recycleBinForGameObject(MissionButtonPrefab.gameObject) == null)
        {
            TrashMan.manageRecycleBin(new TrashManRecycleBin()
            {
                prefab = MissionButtonPrefab.gameObject

            });
        }
    }

    protected override void OnRegionEnter()
    {
        if (MissionParentRoot.transform.childCount > 1)
            return;

        for (int i = 0; i < Missions.Count; i++)
        {
            MissionDataAsset mission = Missions[i];

            Button newBtn = TrashMan.spawn(MissionButtonPrefab.gameObject).GetComponent<Button>();
            newBtn.transform.SetParent(MissionParentRoot.transform, false);
            newBtn.transform.localScale = Vector3.one;
            newBtn.name = mission.Name;
            newBtn.GetComponentInChildren<TMP_Text>().SetText(mission.Name);
            newBtn.transform.SetAsLastSibling();
            newBtn.onClick.RemoveAllListeners();
            newBtn.onClick.AddListener(() => Button_MissionSelected(mission));
            newBtn.gameObject.SetActive(true);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(MissionParentRoot.transform as RectTransform);
        float desiredHeight = (MissionParentRoot.transform as RectTransform).sizeDelta.y;

        Vector2 listSize = MissionListRoot.sizeDelta;
        listSize.y = Mathf.Min(desiredHeight, 600);
        MissionListRoot.sizeDelta = listSize;
    }
    protected override void OnRegionExit()
    {
        DetailWindow.Cleanup();
    }

    private void Button_MissionSelected(MissionDataAsset mission)
    {
        DetailWindow.SetMission(mission);
    }
}
