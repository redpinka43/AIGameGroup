/******************************************************************************************
 * Name: AutoTooltip.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * A helper script that is useful when utilizing tooltips that do not make use of data
 * that changes.  The associated string for the tooltip of this object never changes.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelsoftGames.PixelUI
{
    public class AutoTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("The string to be placed on the tooltip of this object.")]
        string TooltipString = string.Empty;

        #endregion

        #region Callbacks

        /// <summary>
        /// Called when the pointer enters the control's influence area.
        /// </summary>
        /// <param name="eventData">Event data associated with the pointer event.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.ShowTooltip(TooltipString);
        }

        /// <summary>
        /// Called when the pointer exits the control's influence area.
        /// </summary>
        /// <param name="eventData">Event data associated with the pointer event.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.HideTooltip();
        }

        #endregion
    }
}