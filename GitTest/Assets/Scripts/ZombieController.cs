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
        type = "Zombie";
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
            foreach (var enemy in hitEnemies)
            {
                if (enemy.enabled)
                {
                    if (enemy.tag != "Player")
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(20f);
                        enemy.GetComponent<Enemy>().Infect();
                        nextAttack = currentTime + attackCooldown;
                    }
                    else
                    {
                        enemy.GetComponent<Player>().TakeDamage(20f);
                        enemy.GetComponent<Player>().Infect();
                        nextAttack = currentTime + attackCooldown;
                    }
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
            direction = MoveTowardsTarget(player.transform);
            transform.Translate(direction);
        }
        else
        {

        }
    }

}
