using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected float maxHealth = 100f;
    protected float currentHealth;
    protected float currentCaptureLevel;

    protected bool isInfected = false;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void Infect()
    {
        isInfected = true;
    }

    public virtual void CaptureEntity()
    {
        currentCaptureLevel += 1f;
    }

    protected void EntityCaptureCooldown()
    {
        if (currentCaptureLevel != 0f)
        {
            currentCaptureLevel -= 0.5f;
        }
    }
}
