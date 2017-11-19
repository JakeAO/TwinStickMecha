using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class Root_Splash : MonoBehaviour
{
    public TMP_Text DeveloperLabel = null;
    public TMP_Text PresentsLabel = null;
    public TMP_Text TitleLabel = null;

    private IEnumerator Start()
    {
        DeveloperLabel.alpha = 0f;
        PresentsLabel.alpha = 0f;
        TitleLabel.alpha = 0f;

        SceneManager.LoadScene(k.Scenes.SCENE_HARNESS, LoadSceneMode.Additive);
        Go.to(DeveloperLabel, 1f, new GoTweenConfig().floatProp("alpha", 1f)).play();

        yield return new WaitForSeconds(2f);
        Go.to(PresentsLabel, 1f, new GoTweenConfig().floatProp("alpha", 0.5f)).play();

        yield return new WaitForSeconds(1f);
        Go.to(TitleLabel, 1f, new GoTweenConfig().floatProp("alpha", 1f)).play();

        yield return new WaitForSeconds(4f);

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