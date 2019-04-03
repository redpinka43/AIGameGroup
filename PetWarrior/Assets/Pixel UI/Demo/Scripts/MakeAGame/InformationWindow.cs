/******************************************************************************************
 * Name: InformationWindow.cs
 * Created by: Jeremy Voss
 * Created on: 02/27/2019
 * Last Modified: 02/27/2019
 * Description:
 * This object is responsible for managing the effects of the Make-A-Game Information
 * Window displayed when the Make a Game scene is loaded.
 ******************************************************************************************/
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    public class InformationWindow : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("The OK button of the window which will trigger the game panel display.")]
        GameObject okayButton = null;

        /// <summary>
        /// Instance of the attached typewriter class that we will be using to manage
        /// our effects.
        /// </summary>
        UITypewriter typewriter = null;

        #endregion

        #region Monobehavior Callbacks

        // Use this for initialization
        void Start()
        {
            typewriter = GetComponentInChildren<UITypewriter>();
            typewriter.Completed += Typewriter_Completed;
        }

        private void OnDestroy()
        {
            typewriter.Completed -= Typewriter_Completed;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called when the Typewriter class finishes typing text and triggers the display of
        /// the OK button and hiding of the click anywhere to skip fading text.
        /// </summary>
        /// <param name="typewriter"></param>
        void Typewriter_Completed(UITypewriter typewriter)
        {
            if (okayButton != null)
                okayButton.SetActive(true);

            UIFader fader = GetComponentInChildren<UIFader>();
            if (fader)
                fader.gameObject.SetActive(false);
        }

        /// <summary>
        /// Called by the Okay button when it is clicked and triggers the display of the
        /// game panel.
        /// </summary>
        public void OkayButton_Clicked()
        {
            GameManager.Instance.StartGame();
            Destroy(gameObject);
        }

        #endregion
    }
}