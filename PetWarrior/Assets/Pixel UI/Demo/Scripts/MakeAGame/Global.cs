/******************************************************************************************
 * Name: Global.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * Used as a convenient data container in the Make a Game project to store game related 
 * constants, enumerations, etc. all in one easy to access location for changing on the
 * fly.
 ******************************************************************************************/
namespace PixelsoftGames.PixelUI
{
    /// <summary>
    /// An enum containing all possible weather conditions experienced in the game.
    /// </summary>
    public enum Weather { Clear = 0, Precipitation = 1, Stormy = 2, Windy = 3 }
    /// <summary>
    /// An enum containing all locations found in the game.
    /// </summary>
    public enum Locations { Farm, Forest, Mountains, Town }
    /// <summary>
    /// An enum containing all the possible actions that can be made in the game.
    /// </summary>
    public enum Actions { Farm, Livestock, Forage, Log, Mine, Talk, Gift, Shop, Fish, Go_To_Bed }
    /// <summary>
    /// An enum detailing all of the stat bar types found within the game.
    /// </summary>
    public enum StatBarType { Health, EXP, Energy }
    /// <summary>
    /// An enum containing all of the seasons in the game.
    /// </summary>
    public enum Seasons { Spring, Summer, Fall, Winter }

    public sealed class Global
    {
        // Game Management Variables

        /// <summary>
        /// The default location the player will always start in.
        /// </summary>
        public const Locations Default_Location = Locations.Farm;
        /// <summary>
        /// The location we will be using to load spritesheets.
        /// </summary>
        public const string SpritesheetResourcePath = "Spritesheets/";

        // Game Clock Variables

        /// <summary>
        /// The minimum amount to divide the timescale (default time) = 1 real time seconds per game minute.
        /// </summary>
        public const int Min_Speed_Multiplier = 1;
        /// <summary>
        /// The maximum amount to divide the timescale (default time) = 0.33 real time seconds per game minute.
        /// </summary>
        public const int Max_Speed_Multiplier = 3;
        /// <summary>
        /// The year in which the game starts.
        /// </summary>
        public const int Starting_Game_Year = 1;
        /// <summary>
        /// The month in which the game starts.
        /// </summary>
        public const int Starting_Game_Month = 0;
        /// <summary>
        /// The day in which the game starts.
        /// </summary>
        public const int Starting_Game_Day = 1;
        /// <summary>
        /// The minute in which the game starts each day.
        /// </summary>
        public const int Starting_Game_Minute = 0;
        /// <summary>
        /// The hour in which the game starts each day.
        /// </summary>
        public const int Starting_Game_Hour = 6;

        // Player Data Variables

        /// <summary>
        /// The default maximum health for the player.
        /// </summary>
        public const int Default_Maximum_Health = 100;
        /// <summary>
        /// The default maximum energy for the player.
        /// </summary>
        public const int Default_Maximum_Energy = 250;
        /// <summary>
        /// The default starting money for the player.
        /// </summary>
        public const int Default_Starting_Money = 100;
        /// <summary>
        /// The penalty multiplier used to determine current health after the player respawns from dying.
        /// </summary>
        public const float Health_Penalty_Multiplier = 0.1f;
        /// <summary>
        /// The penalty multiplier used to determine current energy after the player wakes up from passing out.
        /// </summary>
        public const float Energy_Penalty_Multiplier = 0.5f;
        /// <summary>
        /// The money multiplier used to determine how much money is owed after the player dies or passes out.
        /// </summary>
        public const float Money_Penalty_Multiplier = 0.9f;
    }
}