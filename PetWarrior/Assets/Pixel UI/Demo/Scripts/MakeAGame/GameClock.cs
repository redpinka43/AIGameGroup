/******************************************************************************************
 * Name: GameClock.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * GameClock derives from UIClock making some minor changes.  It changes from using the
 * Calendar Months and instead changes to using Seasons instead in place of months.  It
 * also adds additional functionality in the form of pausing the game at the end of the
 * day, adding new events for day start, end, and night start, a night start time, etc.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelsoftGames.PixelUI
{
    public class GameClock : UIClock, IPointerEnterHandler, IPointerExitHandler
    {
        #region Singleton Implementation

        static GameClock _Instance = null;
        public static GameClock Instance { get { return _Instance; } }

        #endregion

        #region Events

        public delegate void GameClockEvent(GameClock clock);
        /// <summary>
        /// Called every tick of the UI Clock parent.
        /// </summary>
        public static event GameClockEvent Tock;
        /// <summary>
        /// Marks the start of a new game day.
        /// </summary>
        public static event GameClockEvent DayStart;
        /// <summary>
        /// Marks the start of night time.
        /// </summary>
        public static event GameClockEvent NightStart;
        /// <summary>
        /// Marks the end of a game day.
        /// </summary>
        public static event GameClockEvent GameDayEnd;
        /// <summary>
        /// Called when the game hour changes.
        /// </summary>
        public static event GameClockEvent NewHour;
        /// <summary>
        /// Called when the game season changes.
        /// </summary>
        public static event GameClockEvent NewSeason;

        #endregion

        #region Fields & Properties

        [SerializeField]
        int speedMultiplier = 1;
        public int SpeedMultiplier
        {
            get { return speedMultiplier; }
            set
            {
                if (value < Global.Min_Speed_Multiplier)
                    value = Global.Min_Speed_Multiplier;
                else if (value > Global.Max_Speed_Multiplier)
                    value = Global.Max_Speed_Multiplier;

                speedMultiplier = value;
            }
        }
        float GetModifiedTimeScale { get { return timeScale / SpeedMultiplier; } }

        [SerializeField]
        [Tooltip("The exact hour and minute the game day will end.")]
        int endMinute = 0, endHour = 6;
        [SerializeField]
        [Tooltip("The hour that night time begins.")]
        int nightStartHour = 16;
        [SerializeField]
        [Tooltip("The minute that night time begins.")]
        int nightStartMinute = 0;

        bool isHovered = false;
        bool dayStarted = false;

        /// <summary>
        /// Gets the current date formatted as a string.
        /// </summary>
        public override string GetDateString { get { return GetMonthString + " " + currentDay.ToString() + " Year " + currentYear; } }
        /// <summary>
        /// Gets the current month as a season month string.
        /// </summary>
        public override string GetMonthString { get { return ((Seasons)currentMonth).ToString(); } }
        /// <summary>
        /// Returns the current month as a value of the seasons enum.
        /// </summary>
        public Seasons GetSeason { get { return (Seasons)currentMonth; } }
        public string GetDateTimeWeatherString
        {
            get
            {
                Weather currentWeather = WeatherSystem.Instance.GetCurrentWeather;
                if (currentWeather == Weather.Precipitation)
                {
                    if (GetSeason == Seasons.Winter)
                        return GetDateTimeString + " (Snowing)";
                    else
                        return GetDateTimeString + " (Raining)";
                }
                else
                    return GetDateTimeString + " (" + currentWeather + ")";
            }
        }

        #endregion

        #region Monobehavior Callbacks

        protected override void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
            {
                _Instance = this;
                Tock += GameClock_Tock;
            }

            base.Awake();
        }

        protected override void Start()
        {
            if (startPaused)
            {
                isPaused = true;
                GameManager.Unpause += GameManager_Unpause;
            }
            else
            {
                isPaused = false;
                GameManager.Pause += GameManager_Pause;

                if(DayStart != null)
                    DayStart(this);
            }
        }

        protected override void Update()
        {
            if (!isPaused)
            {
                time += Time.deltaTime;
                if (time >= GetModifiedTimeScale)
                {
                    if (Tock != null)
                        Tock(this);
                }
            }

            if (isHovered)
                UITooltip.Instance.SetText(GetDateTimeWeatherString);
        }

        protected override void OnDestroy()
        {
            if (Instance == this)
            {
                _Instance = null;
                Tock -= GameClock_Tock;
            }

            GameManager.Unpause -= GameManager_Unpause;
            GameManager.Pause -= GameManager_Pause;

            base.OnDestroy();
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by this class when we are updating the game time.
        /// </summary>
        /// <param name="clock">The current game clock instance.</param>
        private void GameClock_Tock(GameClock clock)
        {
            UpdateTime();
        }

        /// <summary>
        /// Called when the game manager unpauses the game.
        /// </summary>
        /// <param name="instance">The current game manager instance.</param>
        private void GameManager_Unpause(GameManager instance)
        {
            GameManager.Unpause -= GameManager_Unpause;
            GameManager.Pause += GameManager_Pause;
            Unpause();
        }

        /// <summary>
        /// Called when the game manager pauses the game.
        /// </summary>
        /// <param name="instance">The current game manager instance.</param>
        private void GameManager_Pause(GameManager instance)
        {
            GameManager.Pause -= GameManager_Pause;
            GameManager.Unpause += GameManager_Unpause;
            Pause();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to set the current date.
        /// </summary>
        /// <param name="day">The current day.</param>
        /// <param name="month">The current month.</param>
        /// <param name="year">The current year.</param>
        public override void SetDate(int day, int month, int year)
        {
            currentDay = day;
            if (month >= System.Enum.GetValues(typeof(Seasons)).Length)
                month = (int)Seasons.Winter;
            else if (month < 0)
                month = (int)Seasons.Spring;
            currentMonth = month;
            currentYear = year;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.HideTooltip();
            isHovered = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.ShowTooltip(GetDateTimeWeatherString);
            isHovered = true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Updates the current time.
        /// </summary>
        protected override void UpdateTime()
        {
            base.UpdateTime();

            if (currentMinute == endMinute && currentHour == endHour)
                EndDay();
            else if (currentMinute == nightStartMinute && currentHour == nightStartHour && NightStart != null)
                NightStart(this);
        }

        private void EndDay()
        {
            if (GameDayEnd != null)
                GameDayEnd(this);

            dayStarted = false;
        }

        /// <summary>
        /// Updates the current hour.
        /// </summary>
        protected override void UpdateHour()
        {
            base.UpdateHour();

            if (NewHour != null)
                NewHour(this);
        }

        /// <summary>
        /// Updates the current month.
        /// </summary>
        protected override void UpdateMonth()
        {
            currentMonth++;

            if(currentMonth >= System.Enum.GetValues(typeof(Seasons)).Length)
            {
                currentMonth = resetMonth;
                UpdateYear();
            }

            if (NewSeason != null)
                NewSeason(this);
        }

        /// <summary>
        /// Unpauses the clock.
        /// </summary>
        public override void Unpause()
        {
            base.Unpause();

            if (!dayStarted && DayStart != null)
                DayStart(this);
        }

        #endregion
    }
}