using System;
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
    protected Enemy[] allEnemies;
    protected Animator anim;
    protected float speed = 0.08F;
    protected float distanceX, distanceY;
    protected Vector3 direction;
    protected float maxHealth = 100f;
    protected float currenthealth;
    private float movementTimer = 0F;
    protected bool isInfected = false;
    protected string type;

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

    public string Type()
    {
        return type;
    }

    protected Enemy LocateClosestEnemy()
    {
        float distance = 100000000f;
        Enemy closestEnemy = null;

        foreach(var enemy in allEnemies)
        {
            if (enemy != this.GetComponent<Enemy>() && enemy.enabled && enemy.type != "Guard" && Vector3.Distance(enemy.transform.position, transform.position) < distance)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    protected Vector3 MoveTowardsTarget(Transform targetTransform)
    {
        Vector2 targetPosition = targetTransform.position;
        Vector3 directionOfTarget;
        directionOfTarget.x = 0f;
        directionOfTarget.y = 0f;
        directionOfTarget.z = 0f;
        if (transform.position != targetTransform.position)
        {
            distanceX = transform.position.x - targetPosition.x;
            distanceY = transform.position.y - targetPosition.y;
            if(distanceX == distanceY)
            {
                distanceY += 0.0000001f;
            }

            if (distanceX > 0)
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    directionOfTarget.x = speed * (distanceX / (distanceX - distanceY));
                    directionOfTarget.y = -speed * (distanceY / (distanceX - distanceY));
                }
                else
                {
                    directionOfTarget.x = speed * (distanceX / (distanceX + distanceY));
                    directionOfTarget.y = -speed * (distanceY / (distanceX + distanceY));
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 180F, 0F);
            }
            else
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    directionOfTarget.x = speed * (distanceX / (distanceX - distanceY));
                    directionOfTarget.y = speed * (distanceY / (distanceX - distanceY));
                }
                else
                {
                    directionOfTarget.x = speed * (distanceX / (distanceX + distanceY));
                    directionOfTarget.y = speed * (distanceY / (distanceX + distanceY));
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 0F, 0F);
            }
        }
        return directionOfTarget;
    }

    public void Infect()
    {
        isInfected = true;
    }

    protected Vector3 MoveAwayFromTarget(Transform targetTransform)
    {
        Vector2 targetPosition = targetTransform.position;
        Vector3 directionOfTarget;
        directionOfTarget.z = 0f;
        distanceX = transform.position.x - targetPosition.x;
        distanceY = transform.position.y - targetPosition.y;

        if (distanceX > 0)
        {
            if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
            {
                directionOfTarget.x = speed * (distanceX / (distanceX - distanceY));
                directionOfTarget.y = speed * (distanceY / (distanceX - distanceY));
            }
            else
            {
                directionOfTarget.x = speed * (distanceX / (distanceX + distanceY));
                directionOfTarget.y = speed * (distanceY / (distanceX + distanceY));
            }
            anim.SetBool("isWalking", true);
            transform.rotation = Quaternion.Euler(0F, 0F, 0F);
        }
        else
        {
            if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
            {
                directionOfTarget.x = speed * (distanceX / (distanceX - distanceY));
                directionOfTarget.y = -speed * (distanceY / (distanceX - distanceY));
            }
            else
            {
                directionOfTarget.x = speed * (distanceX / (distanceX + distanceY));
                directionOfTarget.y = -speed * (distanceY / (distanceX + distanceY));
            }
            anim.SetBool("isWalking", true);
            transform.rotation = Quaternion.Euler(0F, 180F, 0F);
        }
        return directionOfTarget;
    }

    protected abstract void MovementController();
}
