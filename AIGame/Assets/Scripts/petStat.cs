using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class petStat
{
    [SerializeField] private healthBarScript bar;
	
	[SerializeField] private float maxVal;
	
	[SerializeField] private float currentVal;
	
	public float CurrentVal
	{
		get
		{
			return currentVal;
		}
		
		set
		{
			this.currentVal = value;
			bar.Value = currentVal;
		}
	}
	
	public float MaxVal
	{
		get
		{
			return maxVal;
		}
		
		set
		{
			this.maxVal = value;
			bar.maxValue = maxVal;
		}
	}
}
