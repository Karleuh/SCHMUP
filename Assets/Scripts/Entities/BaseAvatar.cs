using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour
{

    
    [SerializeField]
    private float maxSpeed;
    public float MaxSpeed {
        get {
            return this.maxSpeed;
            }
        private set {
            this.maxSpeed = value;
        }
    }


    [SerializeField] private float health;
    public float Health {
        get {
            return this.health;
            }
        private set {
            this.health = value;
        }
    }
    [SerializeField] private float maxHealth;
    
    public float MaxHealth {
        get {
            return this.maxHealth;
            }
        set {
            this.maxHealth = value;
        }
    }
    void Start(){
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage){
        health-=damage;
        if (health <= 0) {
            Die();
            health = 100000;
        }
    }

    public abstract void Die();

}
