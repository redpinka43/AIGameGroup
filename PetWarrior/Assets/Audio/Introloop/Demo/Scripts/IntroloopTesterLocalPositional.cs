using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class IntroloopTesterLocalPositional : MonoBehaviour {

	public IntroloopPlayer leftSphere;
	public GameObject rightSphere;

	public IntroloopAudio assault;
	public IntroloopAudio compete;

	// You can attach a local IntroloopPlayer beforehand.
	// Left sphere already have an IntroloopPlayer, and the necessary child object will be spawned for you when entering play mode.

	// Or you could attach it later.
	// Right sphere does not already have anything yet, but we can still use `.AddComponent` and then the `Awake` code of it will prepare child objects.

	public void Awake()
	{
		rightSphere.AddComponent<IntroloopPlayer>();
	}

	public void LeftPlay()
	{
		leftSphere.Play(assault);
	}

	public void LeftStop()
	{
		leftSphere.Stop();
	}

	public void RightPlay()
	{
		rightSphere.GetComponent<IntroloopPlayer>().Play(compete);
	}

	public void RightStop()
	{
		rightSphere.GetComponent<IntroloopPlayer>().Stop();
	}

}
