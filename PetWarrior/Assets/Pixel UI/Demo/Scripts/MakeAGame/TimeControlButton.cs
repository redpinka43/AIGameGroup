/******************************************************************************************
 * Name: TimeControlButton.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * Used to control the speed in which minutes pass in the game clock, essentially define
 * a minute's second length.
 ******************************************************************************************/
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    public class TimeControlButton : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("The value in which we divide a minute length to speed up the game clock.")]
        [Range(Global.Min_Speed_Multiplier, Global.Max_Speed_Multiplier)]
        int speedValue = Global.Min_Speed_Multiplier;

        #endregion

        #region Callbacks

        /// <summary>
        /// Called when the control button gets clicked and is used to speed up or slow down time.
        /// </summary>
        public void OnClick()
        {
            GameClock.Instance.SpeedMultiplier = speedValue;
        }

        #endregion
    }
}