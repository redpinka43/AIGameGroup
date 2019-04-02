using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pets : MonoBehaviour
{
    public string name;
    public string animal;
    public Sprite image;
    public Species species;
    public int health;
    public int currentHealth;

    public int maxAttack;
    public int attack;

    public int maxDefense;
    public int defense;

    public int special;
    public int speed;
    public int moveNum;
    public List<string> moves = new List<string>();

    public enum Species
    {
        Gecko,
        Turtle,
        Squirrel,
        Bird,
        Dog,
        Cat,
        Rat
    }


}
