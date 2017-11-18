using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


namespace Prime31.TransitionKit
{
    public class RippleTransition : TransitionBase, ITransitionKitDelegate
    {
        public float duration = 0.5f;
        public float speed = 50.0f;
        public float amplitude = 100.0f;


        #region TransitionKitDelegate implementation

        public Shader shaderForTransition()
        {
            return Shader.Find("prime[31]/Transitions/Ripple");
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

            // set some material properties
            transitionKit.material.SetFloat("_Speed", speed);
            transitionKit.material.SetFloat("_Amplitude", amplitude);

            yield return transitionKit.StartCoroutine(transitionKit.performLoadUnloadOperation(sceneLoadData));

            yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration));
        }

        #endregion

    }
}