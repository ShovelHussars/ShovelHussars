using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    public float lookRadius;

    private readonly float attackCooldown = 5f;
    private float nextAttack = -1f;
    Transform target;

    void Start()
    {
        speed = 0.01f;
        player = GameObject.FindObjectOfType<Player>();
        target = player.transform;
        anim = GetComponent<Animator>();
        currenthealth = maxHealth;
        direction.z = 0F;
    }

    void Update()
    {
        MovementController();
        Attack();

        if(currenthealth <= 0)
        {
            Die(anim);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
        
    }

    override protected void Attack()
    {
        float currentTime = Time.time;
        if (currentTime > nextAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemyLayers);
            if (hitEnemies.Length != 0)
            {
                if (hitEnemies[0].enabled)
                {
                    hitEnemies[0].GetComponent<Player>().Infect();
                    hitEnemies[0].GetComponent<Player>().TakeDamage(20f);
                    nextAttack = currentTime + attackCooldown;
                }
            }
        }
    }

    override protected void MovementController()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        anim.SetBool("isWalking", false);
        if (distance < lookRadius)
        {
            playerPosition = player.transform.position;
            distanceX = GetComponent<Rigidbody2D>().transform.position.x - playerPosition.x;
            distanceY = GetComponent<Rigidbody2D>().transform.position.y - playerPosition.y;
            if (distanceX > 0)
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    direction.x = speed * (distanceX / (distanceX - distanceY));
                    direction.y = -speed * (distanceY / (distanceX - distanceY));
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = -speed * (distanceY / (distanceX + distanceY));
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
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = speed * (distanceY / (distanceX + distanceY));
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 0F, 0F);
            }
            transform.Translate(direction);
        }
        else
        {

        }
    }

}
