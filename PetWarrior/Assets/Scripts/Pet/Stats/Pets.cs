using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pets : MonoBehaviour
{
    public new string name;
    public string animal;

    public Sprite image;
	public Sprite panelImage;

    public int health;
    public int currentHealth;

    public int maxAttack;
    public int attack;

    public int maxDefense;
    public int defense;

    public int special;

    public int speed;
    public int currentSpeed;

    public int level;
    public int currentXP;
    public int xpNeeded;

    public bool hasAdvanatage;
    public bool hasDisadvantage;
    public bool owned;

    public List<StatusEffects> statusEffects = new List<StatusEffects>();

    public List<Moves> moves = new List<Moves>();
    public int moveNum;
}
