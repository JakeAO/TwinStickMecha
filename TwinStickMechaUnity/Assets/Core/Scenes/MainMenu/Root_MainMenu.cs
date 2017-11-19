using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31.TransitionKit;

public class Root_MainMenu : MonoBehaviour
{
    public Button MissionsButton = null;
    public Button GarageButton = null;
    public Button ShopButton = null;
    public Button BarracksButton = null;

    private void Awake()
    {
        MissionsButton.interactable = true;
        GarageButton.interactable = false;
        ShopButton.interactable = false;
        BarracksButton.interactable = false;

        MissionsButton.onClick.AddListener(MissionsButton_OnClick);
        GarageButton.onClick.AddListener(GarageButton_OnClick);
        ShopButton.onClick.AddListener(ShopButton_OnClick);
        BarracksButton.onClick.AddListener(BarracksButton_OnClick);
    }

    private void MissionsButton_OnClick()
    {
        TransitionKit.instance.transitionWithDelegate(new BlurTransition()
        {
            duration = 1f,
            sceneLoadData = new SceneLoadData()
            {
                LoadIndex = k.Scenes.SCENE_PRE_GAME_IDX,
                UnloadIndex = k.Scenes.SCENE_MAIN_MENU_IDX,
                LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive,
                LoadOrder = SceneLoadData.Order.UnloadFirst
            }
        });
    }
    private void GarageButton_OnClick()
    {
        TransitionKit.instance.transitionWithDelegate(new BlurTransition()
        {
            duration = 1f,
            sceneLoadData = new SceneLoadData()
            {
                LoadIndex = k.Scenes.SCENE_GARAGE_IDX,
                UnloadIndex = k.Scenes.SCENE_MAIN_MENU_IDX,
                LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive,
                LoadOrder = SceneLoadData.Order.UnloadFirst
            }
        });
    }
    private void ShopButton_OnClick()
    {
        TransitionKit.instance.transitionWithDelegate(new BlurTransition()
        {
            duration = 1f,
            sceneLoadData = new SceneLoadData()
            {
                LoadIndex = k.Scenes.SCENE_SHOP_IDX,
                UnloadIndex = k.Scenes.SCENE_MAIN_MENU_IDX,
                LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive,
                LoadOrder = SceneLoadData.Order.UnloadFirst
            }
        });
    }
    private void BarracksButton_OnClick()
    {
        TransitionKit.instance.transitionWithDelegate(new BlurTransition()
        {
            duration = 1f,
            sceneLoadData = new SceneLoadData()
            {
                LoadIndex = k.Scenes.SCENE_BARRACKS_IDX,
                UnloadIndex = k.Scenes.SCENE_MAIN_MENU_IDX,
                LoadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive,
                LoadOrder = SceneLoadData.Order.UnloadFirst
            }
        });
    }
}
