using System.Collections;
using UnityEngine;
using TMPro;
using Prime31.TransitionKit;
using Prime31.ZestKit;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

public class Root_Splash : MonoBehaviour
{
    [SerializeField] private TMP_Text _developerLabel = null;
    [SerializeField] private TMP_Text _presentsLabel = null;
    [SerializeField] private TMP_Text _titleLabel = null;

    private IEnumerator Start()
    {
        UnityServices.InitializeAsync();
        
        _developerLabel.alpha = 0f;
        _presentsLabel.alpha = 0f;
        _titleLabel.alpha = 0f;

        SceneManager.LoadScene(k.Scenes.SCENE_HARNESS, LoadSceneMode.Additive);
        _developerLabel.ZKalphaTo(1f, 1f).start();

        yield return new WaitForSeconds(1f);
        _presentsLabel.ZKalphaTo(1f, 1f).start();

        yield return new WaitForSeconds(0.5f);
        _titleLabel.ZKalphaTo(1f, 1f).start();

        yield return new WaitForSeconds(2f);

        Transition_MainMenu();
    }

    private void Transition_MainMenu()
    {
        TransitionKit.instance.transitionWithDelegate(new TriangleSlicesTransition()
        {
            duration = 2f,
            divisions = 7,
            nextScene = k.Scenes.SCENE_MAIN_MENU,
        });
    }
}