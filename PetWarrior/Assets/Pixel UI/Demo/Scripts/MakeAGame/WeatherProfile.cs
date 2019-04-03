/******************************************************************************************
 * Name: WeatherProfile.cs
 * Created by: Jeremy Voss
 * Created on: 02/20/2019
 * Last Modified: 02/27/2019
 * Description:
 * A scriptable object used as a data container to store weather information used by
 * the weather system script.
 ******************************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather Profile", menuName = "Pixel UI/Make a Game/Weather Profile")]
public class WeatherProfile : ScriptableObject
{
    /// <summary>
    /// The weighting used to define how likely a weather is to occur.  This is not based upon a percentage, rather
    /// it is weighed against the cumulative values of all the weights together.  The higher the weight the more
    /// likely that weather condition is to occur.
    /// </summary>
    [Tooltip("/// The weighting used to define how likely a weather is to occur.  This is not based upon a percentage, rather it is weighed against " +
        "the cumulative values of all the weights together.  The higher the weight the more likely that weather condition is to occur.")]
    public int ClearWeight, WindyWeight, StormyWeight, PrecipWeight;
}
