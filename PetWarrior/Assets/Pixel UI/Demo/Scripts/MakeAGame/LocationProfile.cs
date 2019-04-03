/******************************************************************************************
 * Name: LocationProfile.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * Location Profile is a scriptable object that serves as a data container for various
 * locations found in the Make a Game project.  Data associated with a game location
 * such as what fish can be found there, what actions can be performed there, etc. are
 * all stored here.
 ******************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace PixelsoftGames.PixelUI
{
    [CreateAssetMenu(fileName = "New Location Profile", menuName = "Pixel UI/Make a Game/Location Profile")]
    public class LocationProfile : ScriptableObject
    {
        #region Fields & Properties

        /// <summary>
        /// The location associated with this location profile.
        /// </summary>
        public Locations Location;
        /// <summary>
        /// The available actions in this location.
        /// </summary>
        public List<Actions> AvailableActions = null;

        #endregion
    }
}