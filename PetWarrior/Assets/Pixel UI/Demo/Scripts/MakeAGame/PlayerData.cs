using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    [System.Serializable]
    public class PlayerData
    {
        #region Events

        public delegate void PlayerDataChangedEvent(PlayerData instance);
        public static event PlayerDataChangedEvent CurrentEnergyValueChanged;
        public static event PlayerDataChangedEvent MaximumEnergyValueChanged;
        public static event PlayerDataChangedEvent CurrentHealthValueChanged;
        public static event PlayerDataChangedEvent MaximumHealthValueChanged;
        public static event PlayerDataChangedEvent MoneyValueChanged;

        #endregion

        #region Fields & Properties

        /// <summary>
        /// The maximum energy of the player.
        /// </summary>
        int maximumEnergy = Global.Default_Maximum_Energy;

        /// <summary>
        /// The maximum health of the player.
        /// </summary>
        int maximumHealth = Global.Default_Maximum_Health;

        /// <summary>
        /// The current energy of the player.
        /// </summary>
        int currentEnergy = Global.Default_Maximum_Energy;

        /// <summary>
        /// The current health of the player.
        /// </summary>
        int currentHealth = Global.Default_Maximum_Health;

        /// <summary>
        /// Total experience points accrued by the player.
        /// </summary>
        public int TotalExperiencePoints = 0;

        /// <summary>
        /// The total amount of money the player has.
        /// </summary>
        int money = Global.Default_Starting_Money;

        /// <summary>
        /// The total amount of money the player has earned today. (Non-Serialized)
        /// </summary>
        [NonSerialized]
        int moneyEarned = 0;

        /// <summary>
        /// The total amount of money the player has earned throughout the game.
        /// </summary>
        int totalMoneyEarned = 0;

        /// <summary>
        /// The total amount of money the player has spent today. (Non-Serialized)
        /// </summary>
        [NonSerialized]
        int moneySpent = 0;

        /// <summary>
        /// The total amount of money the player has spent throughout the game.
        /// </summary>
        int totalMoneySpent = 0;

        /// <summary>
        /// The highest total score the player has achieved in the end of day summary.
        /// </summary>
        public int HighScore { get; set; }

        /// <summary>
        /// The current game day of this player.
        /// </summary>
        public int GameDay { get; set; }

        /// <summary>
        /// The current game month of this player.
        /// </summary>
        public int GameMonth { get; set; }

        /// <summary>
        /// The current game year of this player.
        /// </summary>
        public int GameYear { get; set; }

        /// <summary>
        /// Gets the player's health values in a string format.
        /// </summary>
        public string GetHealthString { get { return "Health: " + CurrentHealth + "/" + MaximumHealth; } }

        /// <summary>
        /// Gets the player's energy levels in a string format.
        /// </summary>
        public string GetEnergyString { get { return "Energy: " + CurrentEnergy + "/" + MaximumEnergy; } }

        /// <summary>
        /// Accessor for getting the amount of money spent today.
        /// </summary>
        public int GetMoneySpent { get { return moneySpent; } }

        /// <summary>
        /// Accessor for getting the amount of money earned today.
        /// </summary>
        public int GetMoneyEarned { get { return moneyEarned; } }

        /// <summary>
        /// Private mutator used to change money value and raises
        /// the money value changed event.
        /// </summary>
        public int Money
        {
            get { return money; }
            private set
            {
                money = value;

                if (MoneyValueChanged != null)
                    MoneyValueChanged(this);
            }
        }

        /// <summary>
        /// Private mutator used to change the current energy value
        /// and raises the current energy value changed event.
        /// </summary>
        public int CurrentEnergy
        {
            get { return currentEnergy; }
            set
            {
                currentEnergy = value;

                if (CurrentEnergyValueChanged != null)
                    CurrentEnergyValueChanged(this);
            }
        }

        /// <summary>
        /// Private mutator used to change the current health value
        /// and raises the current health value changed event.
        /// </summary>
        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;

                if (CurrentHealthValueChanged != null)
                    CurrentHealthValueChanged(this);
            }
        }

        /// <summary>
        /// Private mutator used to change the maximum energy value
        /// and raises the maximum energy value changed event.
        /// </summary>
        public int MaximumEnergy
        {
            get { return maximumEnergy; }
            set
            {
                maximumEnergy = value;

                if (MaximumEnergyValueChanged != null)
                    MaximumEnergyValueChanged(this);
            }
        }

        /// <summary>
        /// Private mutator used to change the maximum health value
        /// and raises the maximum health value changed event.
        /// </summary>
        public int MaximumHealth
        {
            get { return maximumHealth; }
            set
            {
                maximumHealth = value;

                if (MaximumHealthValueChanged != null)
                    MaximumHealthValueChanged(this);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for the Player Data class.
        /// </summary>
        public PlayerData()
        {
            SetDate(Global.Starting_Game_Day, Global.Starting_Game_Month, Global.Starting_Game_Year);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// If the player passes out they will wake up the next day having lost a % of their money and energy.
        /// </summary>
        public void PassOut()
        {
            CurrentEnergy = (int)(MaximumEnergy * Global.Energy_Penalty_Multiplier);
            Money = (int)(Money * Global.Money_Penalty_Multiplier);
        }

        /// <summary>
        /// If the player dies they will wake up the next day having lost a % of their money and health.
        /// </summary>
        public void Die()
        {
            CurrentHealth = (int)(MaximumHealth * Global.Health_Penalty_Multiplier);
            CurrentEnergy = (int)(MaximumEnergy * Global.Energy_Penalty_Multiplier);
            Money = (int)(money * Global.Money_Penalty_Multiplier);
        }

        /// <summary>
        /// Completely restores all player health & energy.
        /// </summary>
        public void HealAll()
        {
            RestoreHealth();
            RestoreEnergy();
        }

        /// <summary>
        /// Completely restores all player health.
        /// </summary>
        public void RestoreHealth()
        {
            CurrentHealth = MaximumHealth;
        }

        /// <summary>
        /// Recovers some health.
        /// </summary>
        /// <param name="amount">The amount of health to recover.</param>
        public void RecoverHealth(int amount)
        {
            CurrentHealth += amount;
        }

        /// <summary>
        /// Damages current health.
        /// </summary>
        /// <param name="amount">The amount of health lost.</param>
        public void DamageHealth(int amount)
        {
            CurrentEnergy -= amount;
        }

        /// <summary>
        /// Increases maximum health.
        /// </summary>
        /// <param name="amount">The amount to increase by.</param>
        public void IncreaseMaximumHealth(int amount)
        {
            MaximumHealth += amount;
        }

        /// <summary>
        /// Completely restores all player energy.
        /// </summary>
        public void RestoreEnergy()
        {
            CurrentEnergy = MaximumEnergy;
        }

        /// <summary>
        /// Recovers some energy.
        /// </summary>
        /// <param name="amount">The amount of energy to recover.</param>
        public void RecoverEnergy(int amount)
        {
            CurrentEnergy += amount;
        }

        /// <summary>
        /// Damages current energy.
        /// </summary>
        /// <param name="amount">The amount of energy lost.</param>
        public void DamageEnergy(int amount)
        {
            CurrentEnergy -= amount;
        }

        /// <summary>
        /// Increases maximum energy.
        /// </summary>
        /// <param name="amount">The amount to increase by.</param>
        public void IncreaseMaximumEnergy(int amount)
        {
            MaximumEnergy += amount;
        }

        /// <summary>
        /// Takes money from the player.
        /// </summary>
        /// <param name="amount">Amount of money to take.</param>
        public void SpendMoney(int amount)
        {
            Money -= amount;
            moneySpent += amount;
        }

        /// <summary>
        /// Gives money to the player.
        /// </summary>
        /// <param name="amount">Amount of money to give.</param>
        public void GiveMoney(int amount)
        {
            Money += amount;
            moneyEarned += amount;
        }

        /// <summary>
        /// Sets date information to player data
        /// </summary>
        /// <param name="day">The current game day</param>
        /// <param name="month">The current game month</param>
        /// <param name="year">The current game year</param>
        public void SetDate(int day, int month, int year)
        {
            GameDay = day;
            GameMonth = month;
            GameYear = year;
        }

        /// <summary>
        /// Tells the player data object to save itself.  This is called when the player clicks the
        /// Continue button at the end of day summary screen.
        /// </summary>
        public void Save()
        {
            // TODO: Implement saving data
            // TotalExperiencePoints = UIManager.Instance.GetEXPBar.GetTotalExperience;
            // gameDay = GameClock.Instance.GetDay;
            // gameMonth = GameClock.Instance.GetMonth;
            // gameYear = GameClock.Instance.GetYear;
            // totalMoneySpent += moneySpent;
            // moneySpent = 0;
            // totalMoneyEarned += moneyEarned;
            // moneyEarned = 0;
            // Save(this);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Saves player data and syncs it to the local system.
        /// </summary>
        /// <param name="data">The data to be saved</param>
        public static void Save(PlayerData data)
        {
            string dataPath = string.Format("{0}/PlayerData.dat", Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs;

            try
            {
                if (File.Exists(dataPath))
                {
                    File.WriteAllText(dataPath, string.Empty);
                    fs = File.Open(dataPath, FileMode.Open);
                }
                else
                    fs = File.Create(dataPath);

                bf.Serialize(fs, data);
                fs.Close();

                if (Application.platform == RuntimePlatform.WebGLPlayer)
                    SyncFiles();
            }
            catch (Exception ex)
            {
                PlatformSafeMessage("Failed to Save: " + ex.Message);
            }
        }

        /// <summary>
        /// Loads saved player data and returns it.
        /// </summary>
        /// <returns></returns>
        public static PlayerData Load()
        {
            PlayerData data = null;
            string dataPath = string.Format("{0}/PlayerData.dat", Application.persistentDataPath);

            try
            {
                if (File.Exists(dataPath))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = File.Open(dataPath, FileMode.Open);

                    data = (PlayerData)bf.Deserialize(fs);
                    fs.Close();
                }
                else
                    data = new PlayerData();
            }
            catch (Exception ex)
            {
                PlatformSafeMessage("Failed to Load: " + ex.Message);
            }

            return data;
        }

        /// <summary>
        /// Raises a message to notify the user of critical information.
        /// </summary>
        /// <param name="message">The message to post</param>
        private static void PlatformSafeMessage(string message)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                WindowAlert(message);
            else
                Debug.Log(message);
        }

        /// <summary>
        /// Responsible for syncing save data with the WebGL game.  NOTE: In order to get this to work properly in your own project you
        /// must move the plugins folder from the Pixel UI directory to the root Assets folder of your game project.  
        /// If you do not do this extra step it will fail in your WebGL projects.
        /// </summary>
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        /// <summary>
        /// Raises a message in browser to ntofiy the user of critical information.
        /// NOTE: In order to get this to work properly in your own project you must move the plugins folder from the Pixel 
        /// UI directory to the root Assets folder of your game project.  If you do not do this extra step it will fail 
        /// in your WebGL projects.
        /// </summary>
        /// <param name="message">The message to post</param>
        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);

        #endregion
    }
}