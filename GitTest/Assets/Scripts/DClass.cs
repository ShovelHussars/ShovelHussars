using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DClass : Enemy
{
    private bool isTouchingWall = false;

    public float lookRadius;
    private float nextAttack = -1f;
    private readonly float attackCooldown = 5f;

    void Start()
    {
        GenerateBehaviourType();
        speed = 0.05f;
        allEnemies = GameObject.FindObjectsOfType<Enemy>();
        player = GameObject.FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        currenthealth = maxHealth;
        direction.z = 0F;
    }

    void Update()
    {
        if(currenthealth <= 0)
        {
            Die(anim);
        }
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    private void GenerateBehaviourType()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                type = "PassiveD-Class";
                break;
            case 1:
                type = "CowardD-Class";
                break;
            case 2:
                type = "AggressiveD-Class";
                break;
        }
    }


    protected override void MovementController()
    {
        switch (type)
        {
            case "PassiveD-Class":
                PassiveBehaviour();
                break;
            case "CowardD-Class":
                CowardBehaviour();
                break;
            case "AggressiveD-Class":
                AggressiveBehaviour();
                break;
        }
    }

    private void PassiveBehaviour()
    {
        
    }

    private void CowardBehaviour()
    {
        Enemy closestEnemy = LocateClosestEnemy();
        if (closestEnemy != null && !isTouchingWall)
        {
            direction = MoveAwayFromTarget(closestEnemy.transform);
            transform.Translate(direction);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void AggressiveBehaviour()
    {
        Enemy closestEnemy = LocateClosestEnemy();
        if (closestEnemy != null)
        {
            direction = MoveTowardsTarget(closestEnemy.transform);
            transform.Translate(direction);
            Attack();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CollidedWitwall(collision))
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }
    }

    private bool CollidedWitwall(Collision2D collision)
    {
        GameObject wall = collision.gameObject;
        if (wall.tag.Equals("Wall") || wall.tag.Equals("Door"))
        {
            return true;
        }
        return false;
    }
    override protected void Attack()
    {
        float currentTime = Time.time;
        if (currentTime > nextAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemyLayers);
            foreach(var enemy in hitEnemies)
            {
                if (enemy.enabled)
                {
                    if (enemy.tag != "Player")
                    {
                        if (enemy.GetComponent<Enemy>() != this)
                        {
                            enemy.GetComponent<Enemy>().TakeDamage(20f);
                            nextAttack = currentTime + attackCooldown;
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Player>().TakeDamage(20f);
                        nextAttack = currentTime + attackCooldown;
                    }
                }
            }
        }
    }
}
