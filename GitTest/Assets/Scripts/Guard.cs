using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{

    void Start()
    {
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
        if (player != null)
        {
            playerPosition = player.transform.position;
            distanceX = GetComponent<Rigidbody2D>().transform.position.x - playerPosition.x;
            distanceY = GetComponent<Rigidbody2D>().transform.position.y - playerPosition.y;
            //print("X=" + distanceX + " Y=" + distanceY);
            if (distanceX > 0)
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    direction.x = speed * (distanceX / (distanceX - distanceY));
                    direction.y = -speed * (distanceY / (distanceX - distanceY));
                    //print(direction.x + ", " + direction.y);
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = -speed * (distanceY / (distanceX + distanceY));
                    //print(direction.x + ", " + direction.y);
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 180F, 0F);
            }
            else
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    direction.x = speed * (distanceX / (distanceX - distanceY));
                    direction.y = speed * (distanceY / (distanceX - distanceY));
                    //print(direction.x + ", " + direction.y);
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = speed * (distanceY / (distanceX + distanceY));
                    //print(direction.x + ", " + direction.y);
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 0F, 0F);
            }
            //direction.x = 0.01F;
            //direction.y = 0.01F;
            transform.Translate(direction);
        }
        else
        {
            //print("NO PLAYER!");
        }
    }
}
