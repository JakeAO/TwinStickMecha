using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Root_MainMenu : ContextSceneRoot
{
    [System.Serializable]
    public struct NavButton
    {
        public string Text;
        [Scene]
        public int SceneId;
        public bool Enabled;
    }

    public Button BackButton = null;
    public Button ExitButton = null;
    public LayoutGroup NavButtonParent = null;
    public Button NavButtonPrefab = null;
    public NavButton[] NavButtonData = new NavButton[0];

    private int _currentContextScene = -1;
    private ContextSceneRoot _currentContext = null;

    protected override void Awake()
    {
        base.Awake();
        BackButton.onClick.AddListener(Button_BackButton);
        ExitButton.onClick.AddListener(Button_ExitButton);

        TriggerRegionEntry();
    }

    private IEnumerator LoadContextScene(int sceneId)
    {
        var loadOp = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;

        Scene contextScene = SceneManager.GetSceneByBuildIndex(sceneId);
        SceneManager.SetActiveScene(contextScene);
        _currentContextScene = contextScene.buildIndex;

        foreach (var rootObj in contextScene.GetRootGameObjects())
        {
            ContextSceneRoot root = rootObj.GetComponent<ContextSceneRoot>();
            if (root != null)
            {
                _currentContext = root;
                break;
            }
        }

        yield return TriggerRegionExit();
        yield return _currentContext.TriggerRegionEntry();
    }
    private IEnumerator UnloadContextScene()
    {
        yield return _currentContext.TriggerRegionExit();

        var unloadOp = SceneManager.UnloadSceneAsync(_currentContextScene);
        while (!unloadOp.isDone)
            yield return null;

        _currentContext = null;
        _currentContextScene = -1;

        yield return TriggerRegionEntry();
    }

    private void Button_BackButton()
    {
        if (_currentContext == null)
            return;

        StartCoroutine(UnloadContextScene());
    }
    private void Button_ExitButton()
    {
        if (_currentContext != null)
            return;

        switch (Application.platform)
        {
#if UNITY_EDITOR
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.WindowsEditor:
                UnityEditor.EditorApplication.isPlaying = false;
                break;
#endif
            default:
                Application.Quit();
                break;
        }
    }

    protected override void OnRegionEnter()
    {
        if (NavButtonParent.transform.childCount > 1)
            return;

        for (int i = 0; i < NavButtonData.Length; i++)
        {
            int sceneId = NavButtonData[i].SceneId;
            Button newButton = TrashMan.spawn(NavButtonPrefab.gameObject).GetComponent<Button>();
            newButton.transform.SetParent(NavButtonParent.transform);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.SetAsLastSibling();
            newButton.GetComponentInChildren<TMP_Text>().SetText(NavButtonData[i].Text);
            newButton.interactable = NavButtonData[i].Enabled;
            newButton.onClick.RemoveAllListeners();
            newButton.onClick.AddListener(() => StartCoroutine(LoadContextScene(sceneId)));
            newButton.gameObject.SetActive(true);
        }
    }
    protected override void OnRegionExit()
    {
    }
}
