using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
	[SerializeField] private float fillAmount;
	
	[SerializeField] private Image filler;
	
	// Max value of the pet's health
	public float maxValue { get; set; }
	
	public float Value
	{
		set
		{
			fillAmount = Map(value,0,maxValue,0,1);
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
	
	// updates how far the health bar should be filled
	private void HandleBar()
	{
		if (fillAmount != filler.fillAmount)
		{
			filler.fillAmount = fillAmount;
		}
	}
	
	// returns value between 0 and 1 that represents the fillAmount.
	// "value": amount of health pet has left, "inMin": pet's min health,
    // "inMax": pet's max health, "outMin": min value fillAmount can be,
 	// "outMax": max value fillAmount can be
	private float Map(float value, float inMin, float inMax, float outMin, 
	float outMax)  
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
