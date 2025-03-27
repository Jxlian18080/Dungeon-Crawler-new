using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    public int CurrentHealth { get; private set; }

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
    }

    public int TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        return CurrentHealth;
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }
}

