using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class FadeSceneTransition : MonoBehaviour {

	#region FIELDS
	public RawImage fadeOutUIImage;
	public float fadeSpeed = 0.2f; 
	public bool fadeOutAtStart = false;

	public enum FadeDirection {
		In, // Alpha = 1
		Out // Alpha = 0
	}
	#endregion


	#region MONOBEHAVIOR

	void OnEnable() {
		// Link up the image
		fadeOutUIImage = gameObject.GetComponent<RawImage>();

		// Assign events
		MySceneManager.OnSceneChange += setFadeOutAtStart;
	}

	void OnDisable() {
		MySceneManager.OnSceneChange -= setFadeOutAtStart;
	}

	#endregion

	// This function waits until Start() has been called 
	void setFadeOutAtStart() {
		fadeOutAtStart = true;
	}



	public void beginFade() {

		// Link up the image
		fadeOutUIImage = gameObject.GetComponent<RawImage>();
		fadeOutUIImage.enabled = true; 
		StartCoroutine(Fade(FadeDirection.Out));
	}

	#region FADE
	private IEnumerator Fade(FadeDirection fadeDirection) {

		float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
		float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;

		if (fadeDirection == FadeDirection.Out) {
			while (alpha >= fadeEndValue) {
				SetColorImage (ref alpha, fadeDirection);
				yield return null;
			}
			fadeOutUIImage.enabled = false; 

			// Enable player movement again
			GUIManager.instance.setFadeState(GUIManager.FadeState.NOTHING);

		} 
	}

	#endregion


	#region HELPERS

	private void SetColorImage(ref float alpha, FadeDirection fadeDirection) {

		fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
	}
	#endregion

}