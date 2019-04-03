/******************************************************************************************
 * Name: MoneyLabel.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * A special label that displays how much money the player has in the game.  It
 * automatically formats the output string to a comma delimited integer format.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    [RequireComponent(typeof(Text))]
    public class MoneyLabel : MonoBehaviour
    {
        #region Fields & Properties

        /// <summary>
        /// The label used to display the money value.
        /// </summary>
        Text moneyLabel = null;

        #endregion

        #region Monobehavior Callbacks

        private void Awake()
        {
            moneyLabel = GetComponent<Text>();
            PlayerData.MoneyValueChanged += PlayerData_MoneyValueChanged;
        }

        private void Start()
        {
            PlayerData_MoneyValueChanged(GameManager.Instance.GetPlayerData);
        }

        private void OnDestroy()
        {
            PlayerData.MoneyValueChanged -= PlayerData_MoneyValueChanged;
        }

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by player data when its money value has changed.
        /// </summary>
        /// <param name="instance">The calling player data instance.</param>
        private void PlayerData_MoneyValueChanged(PlayerData instance)
        {
            moneyLabel.text = string.Format("{0:n0}", GameManager.Instance.GetPlayerData.Money);
        }

        #endregion
    }
}