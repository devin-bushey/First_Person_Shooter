using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }
    public void Hit()
    {
        health -= 1;
        Debug.Log("Health: " + health);
        if (health == 0)
        {
            if (health <= 0)
            { 
                //Debug.Break();
                Messenger.Broadcast (GameEvent.PLAYER_DEAD); 
            }
        }
        float percent = (float)health / maxHealth;
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, percent);
    }

    public void FirstAid(int healthAdded)
    {
        health += healthAdded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        float healthPercent = ((float)health) / maxHealth; 
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, healthPercent);
    }

}
