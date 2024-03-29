﻿using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


namespace Prime31.TransitionKit
{
	public class BlurTransition : TransitionKitDelegate
	{
		public float duration = 0.5f;
		public string nextScene = null;
		public float blurMin = 0.0f;
		public float blurMax = 0.01f;


		#region TransitionKitDelegate implementation

		public Shader shaderForTransition()
		{
			return Shader.Find( "prime[31]/Transitions/Blur" );
		}


		public Mesh meshForDisplay()
		{
			return null;
		}


		public Texture2D textureForDisplay()
		{
			return null;
		}


		public IEnumerator onScreenObscured( TransitionKit transitionKit )
		{
			transitionKit.transitionKitCamera.clearFlags = CameraClearFlags.Nothing;

			if (!string.IsNullOrWhiteSpace(nextScene))
			{
				SceneManager.LoadSceneAsync(nextScene);
			}

			var elapsed = 0f;
			while( elapsed < duration )
			{
				elapsed += transitionKit.deltaTime;
				var step = Mathf.Pow( elapsed / duration, 2f );
				var blurAmount = Mathf.Lerp( blurMin, blurMax, step );

				transitionKit.material.SetFloat( "_BlurSize", blurAmount );

				yield return null;
			}

			if (!string.IsNullOrWhiteSpace(nextScene))
			{
				// we dont transition back to the new scene unless it is loaded
				yield return transitionKit.StartCoroutine(transitionKit.waitForLevelToLoad(nextScene));
			}
		}

		#endregion

	}
}