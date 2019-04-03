/******************************************************************************************
 * Name: TravelButton.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * The travel button is used to change locatiosn within the game.  It has a location
 * associated with it and when clicked will inform the game manager of a location
 * change.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    [RequireComponent(typeof(Button))]
    public class TravelButton : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("The location associated with this button.")]
        Locations location;

        /// <summary>
        /// The button instance this class will be targeting.
        /// </summary>
        Button button = null;

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            button = GetComponent<Button>();
            GameManager.LocationChanged += GameManager_LocationChanged;
        }

        private void OnDestroy()
        {
            GameManager.LocationChanged -= GameManager_LocationChanged;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by the Game Manager when a location has changed.  This event is used
        /// to make the button not interactible if the current game location matches the
        /// button's location.
        /// </summary>
        /// <param name="instance"></param>
        private void GameManager_LocationChanged(GameManager instance)
        {
            if (GameManager.Instance.GetCurrentLocation.Location == location)
                button.interactable = false;
            else
                button.interactable = true;
        }

        /// <summary>
        /// Called by the button instance when it is clicked.
        /// </summary>
        public void OnClick()
        {
            GameManager.Instance.ChangeLocation(location);
        }

        #endregion
    }
}