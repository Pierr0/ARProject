using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public enum Elements { FIRE = 0, EARTH, WATER };

    [SerializeField]
    private GameRuler gameruler;
    
    private List<Elements> shields = new List<Elements>();

    private Elements elementInfused;

    public float m_StartingHealth = 100f;               // The amount of health each player starts with.
    public Slider m_Slider;                             // The slider to represent how much health the player currently has.
    public Image m_FillImage;                           // The image component of the slider.
    public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.

    private float m_CurrentHealth;                      // How much health the player currently has.
    private bool m_Dead;                                // Has the player been reduced beyond zero health yet?

    public Elements getElementInfused()
    {
        return elementInfused;
    }

    public void setElementInfused(int elementId)
    {
        if (elementId == 0)
        {
            elementInfused = Elements.FIRE;
        }
        else if (elementId == 1)
        {
            elementInfused = Elements.EARTH;
        }
        else if (elementId == 2)
        {
            elementInfused = Elements.WATER;
        }
        else
        {
            elementInfused = Elements.FIRE;
        }
    }

    public void setEnable()
    {
        // When the player is enabled, reset the player's health and whether or not it's dead.
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }
    
    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    public void damageGolem(Elements typeDamage)
    {
        if (shields.Count > 0)
        {
            if (shields[0] != typeDamage)
            {
                TakeDamage(10);
            }
            shields.RemoveAt(0);
        }
        else
        {
            TakeDamage(10);
        }
        
    }

    public void damageDemon(int typeDamage)
    {
        if ((Elements)typeDamage == elementWheel(elementInfused))
        {
            TakeDamage(10);
        }
    }

    public void addShield(int typeShield)
    {
        if (shields.Count < 3)
        {
            shields.Add((Elements)typeShield);
        }
        else
        {
            shields[2] = (Elements)typeShield;
        }
    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }

    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        m_Dead = true;
        // Turn the player off.
        //gameObject.SetActive(false);
        gameruler.endGame();
    }

    public bool getDeath()
    {
        return m_Dead;
    }

    public Elements elementWheel(Elements elementId)
    {
        //1 = earth > 2 = water > 3= Fire > earth
        if (elementId == Elements.EARTH)
        {
            return Elements.FIRE;
        }
        else if (elementId == Elements.WATER)
        {
            return Elements.EARTH;
        }
        else if (elementId == Elements.FIRE) 
        {
            return Elements.WATER;
        }
        return 0;
    }
}