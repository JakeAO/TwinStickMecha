using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


namespace Prime31.TransitionKit
{
    public class FadeTransition : TransitionBase, ITransitionKitDelegate
    {
        public Color fadeToColor = Color.black;
        public float duration = 0.5f;
        /// <summary>
        /// the effect looks best when it pauses before fading back. When not doing a scene-to-scene transition you may want
        /// to pause for a breif moment before fading back.
        /// </summary>
        public float fadedDelay = 0f;

        #region TransitionKitDelegate

        public Shader shaderForTransition()
        {
            return Shader.Find("prime[31]/Transitions/Fader");
        }


        public Mesh meshForDisplay()
        {
            return null;
        }


        public Texture2D textureForDisplay()
        {
            return null;
        }


        public IEnumerator onScreenObscured(TransitionKit transitionKit)
        {
            transitionKit.transitionKitCamera.clearFlags = CameraClearFlags.Nothing;
            transitionKit.material.color = fadeToColor;

            yield return transitionKit.StartCoroutine(transitionKit.performLoadUnloadOperation(sceneLoadData));

            yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration));

            transitionKit.makeTextureTransparent();

            if (fadedDelay > 0)
                yield return new WaitForSeconds(fadedDelay);

            yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration, true));
        }

        #endregion

    }
}