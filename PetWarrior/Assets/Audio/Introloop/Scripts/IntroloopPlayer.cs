/* 
/// Copyright (c) 2015 Sirawat Pitaksarit, Exceed7 Experiments LP 
/// http://www.exceed7.com/introloop
*/

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

namespace E7.Introloop
{

    /// <summary>
    /// Define your class like this `public class FieldBGMPlayer : IntroloopPlayer<FieldBGMPlayer>` (Put your class itself into the generic variable)
    /// </summary>
    public abstract class IntroloopPlayer<T> : IntroloopPlayer where T : IntroloopPlayer
    {
        private static T instance;
        protected override bool IsIntroloopSubclass { get { return true; } }

        /// <summary>
        /// With `IntroloopPlayer.Instance.Play`, it refers to the same "Instance" throughout your game. Meaning that you cannot have 2 concurrent Introloop player playing+looping at the same time.
        /// 
        /// With `MySubClassOfIntroloopPlayer.Get`, it will spawns different set of player.
        /// This means you can now have many Introloop playing at the same.
        /// It is useful for dividing the players into several parts. Like BGMPlayer, AmbientPlayer, etc.
        /// 
        /// Moreover, you can then define your own methods on your subclass to be more suitable for your game.
        /// Like `FieldBGMPlayer.Get.PlayDesertTheme()` instead of `IntroloopPlayer.Instance.Play(desertTheme);`.
        /// 
        /// The template's name was hardcoded as the same as your class name.
        /// If your class name is FieldBGMPlayer then you must have FieldBGMPlayer.prefab in 
        /// the same location as IntroloopPlayer.prefab in Resources folder. (Defined in IntroloopSettings.cs constant fields.)
        /// </summary>
        public static T Get
        {
            get
            {
                if (instance == null)
                {
                    instance = MakeSingletonInstance<T>();
                }
                return instance;
            }
        }

    }

    public class IntroloopPlayer : MonoBehaviour
    {
        private IntroloopTrack[] twoTracks;
        private float[] towardsVolume;
        private bool[] willStop;
        private bool[] willPause;
        private float[] fadeLength;
        protected virtual bool IsIntroloopSubclass { get { return false; } }

        /// <summary>
        /// If you wish to do something that affects all 4 AudioSources that Introloop utilize at once, do a `foreach` on this property.
        /// You should not use this in `Awake`, as Introloop might still not yet spawn the `AudioSource`.
        /// </summary>
        public IEnumerable<AudioSource> InternalAudioSources
        {
            get
            {
                if(twoTracks == null)
                {
                    throw new IntroloopException("Introloop is not yet initialized. Please avoid accessing internal AudioSource on Awake.");
                }

                foreach (AudioSource aSource in twoTracks[0].AllAudioSources)
                {
                    yield return aSource;
                }
                foreach (AudioSource aSource in twoTracks[1].AllAudioSources)
                {
                    yield return aSource;
                }
            }
        }

        private bool staticInstance = false;
        internal void SetStaticInstance() { staticInstance = true; }
        internal bool StaticInstance { get { return staticInstance; } }

        /// <summary>
        /// It will change to 0 on first Play(). 0 is the first track.
        /// </summary>
        private int currentTrack = 1;

        /// <summary>
        /// This fade is inaudible, it helps removing loud pop/click when you stop song suddenly. (Stop with 0 second fade length)
        /// </summary>
        private const float popRemovalFadeTime = 0.055f;

        internal IntroloopSettings introloopSettings;

        private static IntroloopPlayer instance;

        /// <summary>
        /// Singleton pattern point of access. Get the instance of IntroloopPlayer.
        /// </summary>
        public static IntroloopPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = MakeSingletonInstance<IntroloopPlayer>();
                }
                return instance;
            }
        }

        protected static T MakeSingletonInstance<T>() where T : IntroloopPlayer
        {
            System.Type type = typeof(T);
            string typeString = type.Name;

            //Try loading a template prefab
            string path = IntroloopSettings.defaultTemplatePathWithoutFileName + typeString;
            GameObject templatePrefab = Resources.Load<GameObject>(path);

            GameObject introloopPlayer;

            //Create a new one in hierarchy, and this one will persist throughout the game/scene too.
            if (templatePrefab != null)
            {
                //Based on prefab
                introloopPlayer = Instantiate(templatePrefab);
                introloopPlayer.name = IntroloopSettings.singletonObjectPrefix + typeString;
            }
            else
            {
                //New game object
                introloopPlayer = new GameObject(typeString);
                introloopPlayer.AddComponent<T>();
            }

            T singletonInstance = introloopPlayer.GetComponent<T>();
            DontDestroyOnLoad(singletonInstance.gameObject);

            singletonInstance.SetStaticInstance();
            singletonInstance.Set2DSpatialBlend();

            return singletonInstance;
        }

        void Awake()
        {
            introloopSettings = gameObject.GetComponent<IntroloopSettings>();
            if (introloopSettings == null)
            {
                introloopSettings = gameObject.AddComponent<IntroloopSettings>();
            }

            towardsVolume = new float[2];
            willStop = new bool[2];
            willPause = new bool[2];
            twoTracks = new IntroloopTrack[2];
            fadeLength = new float[2] { introloopSettings.defaultFadeLength, introloopSettings.defaultFadeLength };

            CreateImportantChilds();
            Set3DSpatialBlend(); //For local Introloop.
        }

        private void CreateImportantChilds()
        {
            // These are all the components that make this plugin works. Basically 4 AudioSources with special control script
            // to juggle music file carefully, stop/pause/resume gracefully while retaining the Introloop function.

            Transform musicPlayerTransform = transform;
            GameObject musicTrack1 = new GameObject();
            musicTrack1.AddComponent<IntroloopTrack>();
            musicTrack1.name = "Music Track 1";
            musicTrack1.transform.parent = musicPlayerTransform;
            musicTrack1.transform.localPosition = Vector3.zero;
            twoTracks[0] = musicTrack1.GetComponent<IntroloopTrack>();
            twoTracks[0].introloopSettings = this.introloopSettings;

            GameObject musicTrack2 = new GameObject();
            musicTrack2.AddComponent<IntroloopTrack>();
            musicTrack2.name = "Music Track 2";
            musicTrack2.transform.parent = musicPlayerTransform;
            musicTrack2.transform.localPosition = Vector3.zero;
            twoTracks[1] = musicTrack2.GetComponent<IntroloopTrack>();
            twoTracks[1].introloopSettings = this.introloopSettings;

            SetMixerGroup(introloopSettings.routeToMixerGroup);
        }

        /// <summary>
        /// You can also call this later to assign a different audio mixer group.
        /// </summary>
        public void SetMixerGroup(AudioMixerGroup audioMixerGroup)
        {
            foreach(AudioSource aSource in InternalAudioSources)
            {
                aSource.outputAudioMixerGroup = audioMixerGroup;
            }
        }

        private void Update()
        {
            FadeUpdate();
        }

        private void FadeUpdate()
        {
            //For two main tracks
            for (int i = 0; i < 2; i++)
            {
                float towardsVolumeBgmVolumeApplied = towardsVolume[i];
                if (twoTracks[i].FadeVolume != towardsVolumeBgmVolumeApplied)
                {
                    //Handles the fade in/out
                    if (fadeLength[i] == 0)
                    {
                        twoTracks[i].FadeVolume = towardsVolumeBgmVolumeApplied;
                    }
                    else
                    {
                        if (twoTracks[i].FadeVolume > towardsVolumeBgmVolumeApplied)
                        {
                            twoTracks[i].FadeVolume -= Time.unscaledDeltaTime / fadeLength[i];
                            if (twoTracks[i].FadeVolume <= towardsVolumeBgmVolumeApplied)
                            {
                                //Stop the fade
                                twoTracks[i].FadeVolume = towardsVolumeBgmVolumeApplied;
                            }
                        }
                        else
                        {
                            twoTracks[i].FadeVolume += Time.unscaledDeltaTime / fadeLength[i];
                            if (twoTracks[i].FadeVolume >= towardsVolumeBgmVolumeApplied)
                            {
                                //Stop the fade
                                twoTracks[i].FadeVolume = towardsVolumeBgmVolumeApplied;
                            }
                        }
                    }
                    //Stop check
                    if (willStop[i] && twoTracks[i].FadeVolume == 0)
                    {
                        willStop[i] = false;
                        willPause[i] = false;
                        twoTracks[i].Stop();
                        UnloadTrack(i);
                    }
                    //Pause check
                    if (willPause[i] && twoTracks[i].FadeVolume == 0)
                    {
                        willStop[i] = false;
                        willPause[i] = false;
                        twoTracks[i].Pause();
                        //don't unload!
                    }
                }
            }
        }

        private void UnloadTrack(int trackNumber)
        {
            //Have to check if other track is using the music or not?

            //If playing the same song again,
            //the loading of the next song might come earlier, then got immediately unloaded by this.

            //Also check for when using different IntroloopAudio with the same source file.
            //In this case .Music will be not equal, but actually the audioClip inside is the same song.

            //Note that load/unloading has no effect on "Streaming" audio type. Introloop might be not so seamless on the first loop if your use Streaming audio since Introloop could not wait for it to finish loading.

            bool musicEqualCurrent = (twoTracks[trackNumber].Music == twoTracks[(trackNumber + 1) % 2].Music);
            bool clipEqualCurrent = (twoTracks[trackNumber].Music != null && twoTracks[(trackNumber + 1) % 2].Music != null) &&
             (twoTracks[trackNumber].Music.audioClip == twoTracks[(trackNumber + 1) % 2].Music.audioClip);

            //As = AudioSource
            bool isSameSongAsCurrent = musicEqualCurrent || clipEqualCurrent;

            bool musicEqualNext = (twoTracks[trackNumber].Music == twoTracks[(trackNumber + 1) % 2].MusicAboutToPlay);
            bool clipEqualNext = (twoTracks[trackNumber].Music != null && twoTracks[(trackNumber + 1) % 2].MusicAboutToPlay != null) &&
            (twoTracks[trackNumber].Music.audioClip == twoTracks[(trackNumber + 1) % 2].MusicAboutToPlay.audioClip);

            bool isSameSongAsAboutToPlay = musicEqualNext || clipEqualNext;

            bool usingAndPlaying = twoTracks[(trackNumber + 1) % 2].IsPlaying && isSameSongAsCurrent;

            if (!usingAndPlaying && !isSameSongAsAboutToPlay)
            {
                //If not, it is now safe to unload it
                //Debug.Log("Unloading");
                twoTracks[trackNumber].Unload();
            }
        }

        internal void ApplyVolumeSettingToAllTracks()
        {
            twoTracks[0].ApplyVolume();
            twoTracks[1].ApplyVolume();
        }

        /// <summary>
        /// Play the audio using settings specified in IntroloopAudio file's inspector area.
        /// </summary>
        /// <param name="audio"> An IntroloopAudio asset file to play.</param>
        public void Play(IntroloopAudio audio)
        {
            PlayFade(audio, 0);
        }

        /// <summary>
        /// Play the audio using settings specified in IntroloopAudio file's inspector area with fade-in 
        /// or cross fade (if other IntroloopAudio is playing now) default length specified in IntroloopSettings component
        /// that is attached to IntroloopPlayer.
        /// </summary>
        /// <param name="audio"> An IntroloopAudio asset file to play.</param>
        public void PlayFade(IntroloopAudio audio)
        {
            PlayFade(audio, introloopSettings.defaultFadeLength);
        }

        /// <summary>
        /// Play the audio using settings specified in IntroloopAudio file's inspector area with fade-in 
        /// or cross fade (if other IntroloopAudio is playing now) length specified by argument.
        /// </summary>
        /// <param name="audio"> An IntroloopAudio asset file to play.</param>
        /// <param name="fadeLengthSeconds"> Fade in/Cross fade length to use.</param>
        public void PlayFade(IntroloopAudio audio, float fadeLengthSeconds)
        {
            //Auto-crossfade old ones. If no fade length specified, a very very small fade will be used to avoid pops/clicks.
            StopFade(fadeLengthSeconds == 0 ? popRemovalFadeTime : fadeLengthSeconds);

            int next = (currentTrack + 1) % 2;
            twoTracks[next].Play(audio, fadeLengthSeconds == 0 ? false : true);
            towardsVolume[next] = 1;
            fadeLength[next] = fadeLengthSeconds;

            currentTrack = next;
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio instantly, and unload the audio from memory.
        /// </summary>
        public void Stop()
        {
            StopFade(popRemovalFadeTime);
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio with fade out length specified by
        /// default length specified in IntroloopSettings component. Unload the audio from memory once
        /// the fade out finished.
        /// </summary>
        public void StopFade()
        {
            StopFade(introloopSettings.defaultFadeLength);
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio with fade out length specified by
        /// argument. Unload the audio from memory once the fade out finished.
        /// </summary>
        /// <param name="fadeLengthSeconds">Fade out length to use in seconds.</param>
        public void StopFade(float fadeLengthSeconds)
        {
            willStop[currentTrack] = true;
            willPause[currentTrack] = false;
            fadeLength[currentTrack] = fadeLengthSeconds;
            towardsVolume[currentTrack] = 0;
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio instantly without unloading,
        /// you will be able to use Resume() to continue later.
        /// </summary>
        public void Pause()
        {
            PauseFade(popRemovalFadeTime);
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio without unloading,
        /// with fade length specified by default length in IntroloopSettings component.
        /// You will be able to use Resume() to continue later.
        /// </summary>
        public void PauseFade()
        {
            PauseFade(introloopSettings.defaultFadeLength);
        }

        /// <summary>
        /// Stop the currently playing IntroloopAudio without unloading,
        /// with fade length specified by the argument.
        /// You will be able to use Resume() to continue later.
        /// </summary>
        /// <param name="fadeLengthSeconds">Fade out length to use in seconds.</param>
        public void PauseFade(float fadeLengthSeconds)
        {
            if (twoTracks[currentTrack].IsPausable())
            {
                willPause[currentTrack] = true;
                willStop[currentTrack] = false;
                fadeLength[currentTrack] = fadeLengthSeconds;
                towardsVolume[currentTrack] = 0;
            }
        }

        /// <summary>
        /// Resume playing of previously paused IntroloopAudio instantly.
        /// </summary>
        public void Resume()
        {
            ResumeFade(0);
        }

        /// <summary>
        /// Resume playing of previously paused IntroloopAudio with fade in length
        /// specified in IntroloopSettings component.
        /// </summary>
        public void ResumeFade()
        {
            ResumeFade(introloopSettings.defaultFadeLength);
        }

        /// <summary>
        /// Resume playing of previously paused IntroloopAudio with any fade in length.
        /// </summary>
        /// <param name="fadeLengthSeconds">Fade out length to use in seconds.</param>
        public void ResumeFade(float fadeLengthSeconds)
        {
            if (twoTracks[currentTrack].Resume())
            {
                //Resume success
                willStop[currentTrack] = false;
                willPause[currentTrack] = false;
                towardsVolume[currentTrack] = 1;
                fadeLength[currentTrack] = fadeLengthSeconds;
            }
        }

        /// <summary>
        /// An experimental feature in the case that you really want the audio to start in an instant you call Play.
        /// By normally using Play and Stop it loads the audio the moment you called Play. 
        /// Introloop waits for an audio to load before playing with a coroutine. (If you have "Load In Background" in the import settings, otherwise Play will be a blocking call)
        /// Introloop can't guarantee that the playback will be instant but your game can continue while it is loading.
        /// 
        /// By using Preload before actually calling Play it will instead be instant. This function is special that even songs with "Load In Background" can be loaded
        /// in a blocking fashion. (You can put Play immediately in the next line expecting a fully loaded audio)
        /// 
        /// However be aware that memory is managed less efficiently in the following case.
        /// 
        /// Normally Introloop immediately unloads the previous track to minimize memory, but if you use Preload then 
        /// did not call Play with the same IntroloopAudio afterwards, the loaded memory will be unmanaged. 
        /// (Just like if you tick "Preload Audio Data" on your clip and have them in a hierarchy somewhere, then did not use it.)
        /// 
        /// Does not work with "Streaming" audio loading type.
        /// </summary>
        public void Preload(IntroloopAudio audio)
        {
            audio.Preload();
        }

        /// <summary>
        /// Set a new audio source curve to this IntroloopPlayer. The settings will be propagated to all AudioSource it uses.
        /// </summary>
        public void SetAudioSourceCurveType(AudioSourceCurveType curveType, AnimationCurve audioCurve)
        {
            foreach (AudioSource aSource in InternalAudioSources)
            {
                aSource.SetCustomCurve(curveType, audioCurve);
            }
        }

        /// <summary>
        /// Make this IntroloopPlayer audio curve to be like `matchTo` for a specific AudioSourceCurveType.
        /// </summary>
        public void MatchAudioSourceCurveType(AudioSourceCurveType curveType, AudioSource matchTo)
        {
            SetAudioSourceCurveType(curveType, matchTo.GetCustomCurve(curveType));
        }

        /// <summary>
        /// Make this IntroloopPlayer audio curve to be like `matchTo` for all 4 curves.
        /// </summary>
        public void MatchAudioSourceCurveType(AudioSource matchTo)
        {
            MatchAudioSourceCurveType(AudioSourceCurveType.CustomRolloff, matchTo);
            MatchAudioSourceCurveType(AudioSourceCurveType.ReverbZoneMix, matchTo);
            MatchAudioSourceCurveType(AudioSourceCurveType.SpatialBlend, matchTo);
            MatchAudioSourceCurveType(AudioSourceCurveType.Spread, matchTo);
        }

        /// <summary>
        /// Introloop defaults to this setting. You can hear the song fully with respect to volume in IntroloopAudio file
        /// no matter where your AudioListener is.
        /// </summary>
        public void Set2DSpatialBlend()
        {
            SetSpatialBlend(0);
        }

        /// <summary>
        /// If you want Introloop to be positional (possibly on local IntroloopPlayer instance, not the static ones)
        /// You could use this function.
        /// </summary>
        public void Set3DSpatialBlend()
        {
            SetSpatialBlend(1);
        }

        /// <summary>
        /// If you want Introloop to be positional (possibly on local IntroloopPlayer instance, not the static ones)
        /// You could use this function.
        /// </summary>
        public void SetSpatialBlend(float spatialBlend)
        {
            foreach (AudioSource aSource in InternalAudioSources)
            {
                aSource.spatialBlend = spatialBlend;
            }
        }

        /// <summary>
        /// For debugging purpose.
        /// </summary>
        public float GetTime()
        {
            return twoTracks[currentTrack].PlayheadPositionSeconds;
        }

        /// <summary>
        /// For debugging purpose.
        /// </summary>
        public string[] GetDebugInformation1()
        {
            return twoTracks[0].DebugInformation;
        }

        /// <summary>
        /// For debugging purpose.
        /// </summary>
        public string[] GetDebugInformation2()
        {
            return twoTracks[1].DebugInformation;
        }
    }

}