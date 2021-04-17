using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{

    void Start()
    {
        type = "Guard";
        currenthealth = maxHealth;
        anim = GetComponent<Animator>();
        direction.z = 0F;
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currenthealth <= 0)
        {
            Die(anim);
        }
         
        Attack();
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
        if (hitEnemies.Length != 0)
            if (hitEnemies[0].enabled)
            {
                hitEnemies[0].GetComponent<Player>().CapturePlayer();
            }
            else
            {
                hitEnemies[0].GetComponent<Player>().captureScreen.SetActive(false);
            }
    }

    override protected void MovementController()
    {
        anim.SetBool("isWalking", false);

        direction = MoveTowardsTarget(player.transform);
        transform.Translate(direction);
    }
}
