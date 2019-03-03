﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pets : MonoBehaviour
{
    public string name;
    public string animal;
    public int health;
    public int currentHealth;
    public int attack;
    public int defense;
    public int special;
    public int speed;
    public List<string> moves = new List<string>();

    public int SlashAttack(int attack, int defense)
    {

        int damage = (attack / defense);
        return damage;

    }
}
