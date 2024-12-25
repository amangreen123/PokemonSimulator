using Palmmedia.ReportGenerator.Core;
using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// Unit class holds Pokémon data like name, HP, damage, and sprite
public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    
    public RawImage pokeImage;
    private PokeMoves moves;
    // Damage handling
    
   
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    // Heal method
    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP) { 
            
            currentHP = maxHP;
        }
    }
}
