using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemyLayers;
    protected List<Entity> allEnemies;
    protected Animator anim;
    protected float speed = 0.05F;
    protected float distanceX, distanceY;
    protected Vector3 direction;
    protected string type;

    abstract protected void Attack();

    protected void Die(Animator animator)
    {
        this.enabled = false;
        animator.SetBool("isWalking", false);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 90f);
    }

    public new void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        
        if (type == "PassiveD-Class")
        {
            type = "AggressiveD-Class";
        }
    }

    public string Type()
    {
        return type;
    }

    protected GameObject LocateClosestEnemy()
    {
        float distance = 100000000f;
        GameObject closestEnemy = null;

        foreach(var enemy in allEnemies)
        {
            if (enemy != this.GetComponent<Entity>() && enemy.enabled && Vector2.Distance(enemy.transform.position, transform.position) < distance)
            {
                distance = Vector2.Distance(enemy.transform.position, transform.position);
                closestEnemy = enemy.gameObject;
            }
        }
        return closestEnemy;
    }

    protected void MoveTowardsTarget(Transform targetTransform)
    {
        MoveTowardsVector3(targetTransform.position);
    }

    protected void MoveTowardsVector3(Vector3 target)
    {
        if (transform.position != target)
        {
            if(transform.position.x > target.x)
            {
                transform.rotation = Quaternion.Euler(0F, 180F, 0F);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0F, 0F, 0F);
            }
            transform.position = Vector2.MoveTowards(transform.position, target, speed);
            
            anim.SetBool("isWalking", true);
            
        }
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
