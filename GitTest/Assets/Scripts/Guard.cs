using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{

    void Start()
    {
        type = "Guard";
        Entity[] temp = GameObject.FindObjectsOfType<Entity>();
        allEnemies = new List<Entity>();
        foreach (var entity in temp)
        {
            if (!entity.CompareTag("Guard") && !entity.CompareTag("Scientist"))
            {
                allEnemies.Add(entity.GetComponent<Entity>());
            }
        }
        anim = GetComponent<Animator>();
        direction.z = 0F;
    }


    void Update()
    {
        if (firstupdate)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            Die(anim);
        }
         
        Attack();
        firstupdate = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    protected override void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemyLayers);
        //if (hitEnemies.Length != 0)
            foreach (var enemy in hitEnemies)
            {
                if (enemy.enabled)
                {
                    if (enemy.CompareTag("Player"))
                    {
                        enemy.GetComponent<Player>().CaptureEntity();
                    }
                    else
                    {
                        enemy.GetComponent<Entity>().CaptureEntity();
                    }
                }
            }
    }

    override protected void MovementController()
    {
        anim.SetBool("isWalking", false);
        try
        {
            MoveTowardsTarget(LocateClosestEnemy().transform);
        }
        catch (Exception)
        {
        }
       
    }
}
