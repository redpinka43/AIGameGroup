/******************************************************************************************
 * Name: StatBarTooltip.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * This class is used for establishing tooltips when hovering over stat bars.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelsoftGames.PixelUI
{
    public class StatBarTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields & Properties
        
        [SerializeField]
        [Tooltip("The type of stat bar this is so we get the correct data from player data.")]
        StatBarType type;

        #endregion

        #region Callbacks

        /// <summary>
        /// Called by OnPointerEnterHandler interface when the mouse enters the control area.
        /// </summary>
        /// <param name="eventData">The pointer data associated with this event.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            switch(type)
            {
                case StatBarType.Energy: // Energy
                    UIManager.Instance.ShowTooltip(GameManager.Instance.GetPlayerData.GetEnergyString);
                    break;
                case StatBarType.EXP: // EXP
                    UIManager.Instance.ShowTooltip(CreateEXPString(GetComponent<UIExperienceBar>()));
                    break;
                case StatBarType.Health: // Health
                    UIManager.Instance.ShowTooltip(GameManager.Instance.GetPlayerData.GetHealthString);
                    break;
            }
        }

        /// <summary>
        /// Called by the OnPointerExitHandler interface when the mouse leaves the control area.
        /// </summary>
        /// <param name="eventData">The pointer data associated with this event.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.HideTooltip();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a specially formatted string for displaying player stats in the tooltip.
        /// </summary>
        /// <param name="expBar"></param>
        /// <returns></returns>
        string CreateEXPString(UIExperienceBar expBar)
        {
            return "Level: " + expBar.GetCurrentLevel + "\n"
                + "EXP: " + expBar.GetExperienceTowardsLevel + "/" + expBar.GetExperienceToNextLevel;
        }

        #endregion
    }
}