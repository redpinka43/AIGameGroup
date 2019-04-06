using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

/// Put itself in the <T> generic.
public class DemoSubclassPlayer : IntroloopPlayer<DemoSubclassPlayer>
{
	// We now owns the subclass, that means it is possible to pre assign game-specific songs/settings here.
	// And have them ready for YourSubclass.Get static call.
	// You can still use `IntroloopSettings` and assign AudioMixerGroup with the subclass.

	public IntroloopAudio assault;
	[Range(0,2f)]
	public float assaultFade;

	[Space]

	public IntroloopAudio compete;
	[Range(0,2f)]
	public float competeFade;

    /// <summary>
    /// Now you also have a friendly function name tailored for your game.
    /// </summary>
    public void PlayAssault()
	{
		PlayFade(assault, assaultFade);
	}

	public void PlayCompete()
	{
		PlayFade(compete, competeFade);
	}

	/// <summary>
	/// If you do not wish to use a template prefab which already has IntroloopAudio ready, 
	/// you might do it later like this.
	/// </summary>
	public void LateAssign(IntroloopAudio assault, IntroloopAudio compete)
	{
		this.assault = assault;
		this.compete = compete;
	}
}
