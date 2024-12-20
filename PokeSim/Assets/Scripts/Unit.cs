using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    
    public RawImage pokeImage;


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

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP) { 
            
            currentHP = maxHP;
        }
    }
}
