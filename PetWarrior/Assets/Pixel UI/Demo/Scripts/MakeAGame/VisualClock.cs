/******************************************************************************************
 * Name: VisualClock.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * This class is responsible for visually displaying the game clock in an image format.
 * It takes into account the weather as well as the time to display the appropriate image.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    [RequireComponent(typeof(Image))]
    public class VisualClock : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("The spritesheets associated with the various day types.")]
        Texture2D bloodMoonDay = null, clearDay = null, eclipseDay = null, festivalDay = null, rainyDay = null, snowyDay = null, stormyDay = null, windyDay = null;

        /// <summary>
        /// The image component used to display the clock.
        /// </summary>
        Image image = null;
        /// <summary>
        /// The current hour in the game starting from 0 and counting up each time the game hour changes.
        /// This is used as an index to display the sprite from the currentDaySheet.
        /// </summary>
        int currentHour = 0;
        /// <summary>
        /// The current spritesheet representing the day and weather.
        /// </summary>
        Sprite[] currentDaySheet = null;
        /// <summary>
        /// Has the visual clock received weather information, necessary to display the appropriate game
        /// day type?
        /// </summary>
        bool hasUpdatedWeather = false;

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            GameClock.NewHour += GameClock_NewHour;
            GameClock.GameDayEnd += GameClock_GameDayEnd;
            WeatherSystem.WeatherCalculated += WeatherSystem_WeatherCalculated;
            image = GetComponent<Image>();
        }

        private void OnDestroy()
        {
            GameClock.NewHour -= GameClock_NewHour;
            GameClock.GameDayEnd -= GameClock_GameDayEnd;
            WeatherSystem.WeatherCalculated -= WeatherSystem_WeatherCalculated;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by the game clock when a new hour strikes.
        /// </summary>
        /// <param name="clock">The current game clock instance.</param>
        private void GameClock_NewHour(GameClock clock)
        {
            if (currentDaySheet == null)
                return;

            currentHour++;

            if (currentHour < currentDaySheet.Length)
                UpdateImage();
        }

        /// <summary>
        /// Called by the weather system once it has calculated weather data.
        /// </summary>
        /// <param name="weather">The current weather system instance.</param>
        private void WeatherSystem_WeatherCalculated(WeatherSystem weather)
        {
            hasUpdatedWeather = true;

            switch(weather.GetCurrentWeather)
            {
                case Weather.Clear: // Clear
                    currentDaySheet = Resources.LoadAll<Sprite>(Global.SpritesheetResourcePath + clearDay.name);
                    break;
                case Weather.Precipitation: // Precipitation
                    if(GameClock.Instance.GetSeason == Seasons.Winter)
                        currentDaySheet = Resources.LoadAll<Sprite>(Global.SpritesheetResourcePath + snowyDay.name);
                    else
                        currentDaySheet = Resources.LoadAll<Sprite>(Global.SpritesheetResourcePath + rainyDay.name);
                    break;
                case Weather.Stormy: // Stormy
                    currentDaySheet = Resources.LoadAll<Sprite>(Global.SpritesheetResourcePath + stormyDay.name);
                    break;
                case Weather.Windy: // Windy
                    currentDaySheet = Resources.LoadAll<Sprite>(Global.SpritesheetResourcePath + windyDay.name);
                    break;
            }

            UpdateImage();
        }

        /// <summary>
        /// Called by Game Clock when the game day ends.
        /// </summary>
        /// <param name="clock">The current game clock instance.</param>
        private void GameClock_GameDayEnd(GameClock clock)
        {
            hasUpdatedWeather = false;
            currentHour = 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the visual clock image.
        /// </summary>
        void UpdateImage()
        {
            if (!hasUpdatedWeather)
                return;

            image.sprite = currentDaySheet[currentHour];
        }

        #endregion
    }
}