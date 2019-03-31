/******************************************************************************************
 * Name: GameManager.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * The Game Manager is a hugely important class that helps run our game smoothly.  It
 * does not handle UI based functions, although at times is used to call upon the UI
 * Manager (another hugely important class) to perform UI functions.
 ******************************************************************************************/
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    public class GameManager : MonoBehaviour
    {
        #region Events

        public delegate void GameEvent(GameManager instance);
        /// <summary>
        /// Called when the game is paused.
        /// </summary>
        public static event GameEvent Pause;
        /// <summary>
        /// Called when the game is unpaused.
        /// </summary>
        public static event GameEvent Unpause;
        /// <summary>
        /// Called when the player changes their location in the game.
        /// </summary>
        public static event GameEvent LocationChanged;

        #endregion

        #region Singleton Implementation

        static GameManager _Instance = null;
        public static GameManager Instance { get { return _Instance; } }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// The current instance of data for the player.
        /// </summary>
        PlayerData playerData = null;
        /// <summary>
        /// The current instance of data for the player.
        /// </summary>
        public PlayerData GetPlayerData { get { return playerData; } }

        /// <summary>
        /// Is the game currently paused? Default: Paused
        /// </summary>
        bool isPaused = true;

        [SerializeField]
        [Tooltip("Location profile data for each location in the game.")]
        LocationProfile farmLocationProfile = null, forestLocationProfile = null, mountainLocationProfile = null, townLocationProfile = null;

        /// <summary>
        /// The player's current location in the game.
        /// </summary>
        LocationProfile currentLocation = null;
        /// <summary>
        /// The player's current location in the game.
        /// </summary>
        public LocationProfile GetCurrentLocation { get { return currentLocation; } }

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            if (Instance == null)
            {
                _Instance = this;
                playerData = PlayerData.Load();
                GameClock.GameDayEnd += GameClock_GameDayEnd;
            }
            else
                Destroy(this);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                _Instance = null;
                GameClock.GameDayEnd += GameClock_GameDayEnd;
            }
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by GameClock when the game day ends and is used to hide the game panel
        /// </summary>
        /// <param name="clock"></param>
        private void GameClock_GameDayEnd(GameClock clock)
        {
            PauseGame();
        }

        /// <summary>
        /// Called by the End of Day Panel when the player clicks Continue and is used
        /// to continue the game into the next day.  Responsible for saving and unpausing
        /// the game after this event.
        /// </summary>
        public void ContinueGame()
        {
            GetPlayerData.Save();
            UnpauseGame();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to change the player's location in the game and load
        /// the appropriate location profile.
        /// </summary>
        /// <param name="location">The location we are changing to</param>
        public void ChangeLocation(Locations location)
        {
            switch(location)
            {
                case Locations.Farm: // Farm
                    currentLocation = farmLocationProfile;
                    break;
                case Locations.Forest: // Forest
                    currentLocation = forestLocationProfile;
                    break;
                case Locations.Mountains: // Mountains
                    currentLocation = mountainLocationProfile;
                    break;
                case Locations.Town: // Town
                    currentLocation = townLocationProfile;
                    break;
            }

            if (LocationChanged != null)
                LocationChanged(this);
        }

        /// <summary>
        /// Used to perform an action by the player in the game.
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        public void PerformAction(Actions action)
        {
            // TODO: Implement Action Usage
        }

        /// <summary>
        /// This gets called by the information window once the player clicks the OK button and
        /// is how we officially start the game.
        /// </summary>
        public void StartGame()
        {
            if(GetPlayerData != null)
            {
                ChangeLocation(Global.Default_Location);
                GameClock.Instance.SetDateTime(Global.Starting_Game_Hour, Global.Starting_Game_Minute,
                    GetPlayerData.GameDay, GetPlayerData.GameMonth, GetPlayerData.GameYear);
                UIManager.Instance.GetEXPBar.GiveExperiencePoints(GetPlayerData.TotalExperiencePoints, true);
                UnpauseGame();
                UIManager.Instance.ShowGamePanel();
            }
        }

        /// <summary>
        /// Pauses the current game.
        /// </summary>
        public void PauseGame()
        {
            if (isPaused)
                return;

            isPaused = true;
            if (Pause != null)
                Pause(this);
        }

        /// <summary>
        /// Unpauses the current game.
        /// </summary>
        public void UnpauseGame()
        {
            if (!isPaused)
                return;

            isPaused = false;
            if (Unpause != null)
                Unpause(this);
        }

        #endregion
    }
}