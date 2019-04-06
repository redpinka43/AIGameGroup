/* 
/// Copyright (c) 2015 Sirawat Pitaksarit, Exceed7 Experiments LP 
/// http://www.exceed7.com/introloop
*/

using UnityEngine;
using UnityEngine.Audio;

namespace E7.Introloop
{
    /// <summary>
    /// This class should be attaced together with `IntroloopPlayer` on your template prefab.
    /// </summary>
    internal class IntroloopSettings : MonoBehaviour
    {
        ///<summary>
        ///This is the path of IntroloopPlayer template relative to Resources folder.
        ///</summary>
        internal const string defaultTemplatePathWithoutFileName = "Introloop/";

    /// <summary>
    /// When using `IntroloopPlayer.Instance` or `Subclass.Get` for the first time,
    /// a new game object in `DontDestroyOnLoad` scene will have its name prefixed.
    /// </summary>
        internal const string singletonObjectPrefix = "Singleton-";

        [Space(8)]
        [Header("Settings")]

        ///<summary>
        ///Drag your AudioMixerGroup to this in IntroloopPlayer template.
        ///</summary>
        public AudioMixerGroup routeToMixerGroup;

        ///<summary>
        /// Method with "Fade" and without fade time parameter will use this length.
        ///</summary>
        [PositiveFloat("Sec.")]
        public float defaultFadeLength;

        ///<summary>
        /// Check this in your IntroloopPlayer template to log various debug data.
        ///</summary>
        public bool logInformation = false;
    }

}