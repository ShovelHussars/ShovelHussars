using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Player player;
    protected Vector2 playerPosition;
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemyLayers;
    protected Animator anim;
    protected float speed = 0.08F;
    protected float distanceX, distanceY;
    protected Vector3 direction;
    protected float maxHealth = 100f;
    protected float currenthealth;

    abstract protected void Attack();

    protected void Die(Animator animator)
    {
        this.enabled = false;
        animator.SetBool("isWalking", false);
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;
    }

    protected abstract void MovementController();
}
