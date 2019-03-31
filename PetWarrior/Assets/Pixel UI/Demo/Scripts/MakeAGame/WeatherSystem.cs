/******************************************************************************************
 * Name: WeatherSystem.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * Weather System is a class used to determine weather within the game using data containers
 * called Weather Profiles.  These weather profiles contain weightings for each of the
 * possible weather types.  In order to establish the weather for the day, the weights
 * in that profile are all totaled together, and the conditions with the highest weight
 * will have the largest range within that total.  This ensures that higher weights will
 * have a larger chance of occuring and allows for precision outside of a percentage
 * range.
 ******************************************************************************************/
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    public class WeatherSystem : MonoBehaviour
    {
        #region Singleton Implementation

        static WeatherSystem _Instance = null;
        public static WeatherSystem Instance { get { return _Instance; } }

        #endregion

        #region Events

        public delegate void WeatherEvent(WeatherSystem weather);
        public static event WeatherEvent WeatherCalculated;

        #endregion

        #region Fields & Properties

        [SerializeField]
        [Tooltip("The weather profiles used for determining weather based on season.")]
        WeatherProfile springWeatherProfile = null, summerWeatherProfile = null, fallWeatherProfile = null, winterWeatherProfile = null;

        /// <summary>
        /// The current weather.
        /// </summary>
        Weather currentWeather = Weather.Clear;
        /// <summary>
        /// Accessor for getting the current weather.
        /// </summary>
        public Weather GetCurrentWeather { get { return currentWeather; } }
        /// <summary>
        /// The currently active weather profile determining weather.
        /// </summary>
        WeatherProfile activeProfile = null;
        /// <summary>
        /// Flag that lets the class know if it needs to get a new weather profile and
        /// recalculate the weather ranges.
        /// </summary>
        bool isNewSeason = true;
        /// <summary>
        /// Used in determining weather alongside weather ranges.
        /// </summary>
        int totalWeight = 0;
        /// <summary>
        /// Used in determining weather alongside total weighting.
        /// </summary>
        RangeInt[] ranges = null;

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            if (Instance == null)
            {
                _Instance = this;
                GameClock.NewSeason += GameClock_NewSeason;
                GameClock.DayStart += GameClock_DayStart;
            }
            else
                Destroy(this);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                _Instance = null;
                GameClock.NewSeason -= GameClock_NewSeason;
                GameClock.DayStart -= GameClock_DayStart;
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by the Game Clock when a new day begins.
        /// </summary>
        /// <param name="clock"></param>
        private void GameClock_DayStart(GameClock clock)
        {
            // Did the seasons change?  If so, we need to recalculate weather ranges
            if (isNewSeason)
            {
                isNewSeason = false;
                ResetWeather();
            }

            // Calculate the weather for today.
            CalculateWeather();
        }

        /// <summary>
        /// Called by the Game Clock when the season changes.
        /// </summary>
        /// <param name="clock"></param>
        private void GameClock_NewSeason(GameClock clock)
        {
            // Toggle the new season flag
            isNewSeason = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Forces the weather system to recalculate weather ranges, get the current season, etc.
        /// </summary>
        public void ResetWeather()
        {
            // Determine the active weather profile based on the new season
            switch(GameClock.Instance.GetSeason)
            {
                case Seasons.Spring: // Spring
                    activeProfile = springWeatherProfile;
                    break;
                case Seasons.Summer: // Summer
                    activeProfile = summerWeatherProfile;
                    break;
                case Seasons.Fall: // Fall
                    activeProfile = fallWeatherProfile;
                    break;
                case Seasons.Winter: // Winter
                    activeProfile = winterWeatherProfile;
                    break;
            }

            // Recalculate the weather weighting ranges for randomizing weather
            CalculateRanges();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculates todays weather randomly based on the active weather profile.
        /// </summary>
        void CalculateWeather()
        {
            int randomSelection = Random.Range(1, totalWeight + 1);
            currentWeather = (Weather)System.Array.FindIndex(ranges, r => randomSelection >= r.Min && randomSelection <= r.Max);

            if (WeatherCalculated != null)
                WeatherCalculated(this);
        }

        /// <summary>
        /// Used in the calculation of weather randomization, this determines the weather weighting ranges.
        /// </summary>
        void CalculateRanges()
        {
            totalWeight = activeProfile.ClearWeight + activeProfile.PrecipWeight + activeProfile.StormyWeight + activeProfile.WindyWeight;
            int lastMax = 0;
            ranges = new RangeInt[System.Enum.GetValues(typeof(Weather)).Length];
            for (int i = 0; i < ranges.Length; i++)
            {
                switch ((Weather)i)
                {
                    case Weather.Clear: // Clear
                        ranges[i] = new RangeInt(lastMax + 1, lastMax + activeProfile.ClearWeight);
                        lastMax += activeProfile.ClearWeight;
                        break;
                    case Weather.Stormy: // Stormy
                        ranges[i] = new RangeInt(lastMax + 1, lastMax + activeProfile.StormyWeight);
                        lastMax += activeProfile.StormyWeight;
                        break;
                    case Weather.Precipitation: // Rain
                        ranges[i] = new RangeInt(lastMax + 1, lastMax + activeProfile.PrecipWeight);
                        lastMax += activeProfile.PrecipWeight;
                        break;
                    case Weather.Windy: // Windy
                        ranges[i] = new RangeInt(lastMax + 1, lastMax + activeProfile.WindyWeight);
                        lastMax += activeProfile.WindyWeight;
                        break;
                }
            }
        }

        #endregion
    }
}