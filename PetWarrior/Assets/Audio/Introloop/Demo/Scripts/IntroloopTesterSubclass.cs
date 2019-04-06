using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class IntroloopTesterSubclass : MonoBehaviour {

	public IntroloopAudio maidBattle;

	public void LeftPlay()
	{
		IntroloopPlayer.Instance.Play(maidBattle);
	}

	public void LeftPause()
	{
		IntroloopPlayer.Instance.PauseFade(0.5f);
	}

	public void LeftResume()
	{
		IntroloopPlayer.Instance.Resume();
	}

	public void LeftStop()
	{
		IntroloopPlayer.Instance.StopFade(0.8f);
	}

	public void RightPlay1()
	{
		DemoSubclassPlayer.Get.PlayAssault();
	}

	public void RightPlay2()
	{
		DemoSubclassPlayer.Get.PlayCompete();
	}

	public void RightPause()
	{
		DemoSubclassPlayer.Get.Pause();
	}

	public void RightResume()
	{
		DemoSubclassPlayer.Get.Resume();
	}

	public void RightStop()
	{
		DemoSubclassPlayer.Get.Stop();
	}

}
