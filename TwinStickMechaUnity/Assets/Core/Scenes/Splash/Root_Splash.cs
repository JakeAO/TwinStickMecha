using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class Root_Splash : MonoBehaviour
{
    private IEnumerator Start()
    {
        SceneManager.LoadSceneAsync(k.Scenes.SCENE_HARNESS, LoadSceneMode.Additive);

        yield return new WaitForSeconds(5f);

        TransitionKit.instance.transitionWithDelegate(new TriangleSlicesTransition()
        {
            duration = 2f,
            divisions = 7,
            sceneLoadData = new SceneLoadData()
            {
                LoadIndex = k.Scenes.SCENE_MAIN_MENU_IDX,
                UnloadIndex = k.Scenes.SCENE_SPLASH_IDX,
                LoadMode = LoadSceneMode.Additive,
                LoadOrder = SceneLoadData.Order.UnloadFirst
            }
        });
    }
}