using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DClass : Enemy
{
    private bool isTouchingWall = false;

    public float lookRadius;
    private float nextAttack = -1f;
    private readonly float attackCooldown = 5f;
    private float nextRandomMoveTrigger = -1f;
    private Vector3 randomLocation;

    void Start()
    {
        randomLocation = transform.position;
        GenerateBehaviourType();
        speed = 0.05f;
        Entity[] temp = GameObject.FindObjectsOfType<Entity>();
        allEnemies = new List<Entity>();
        foreach (var entity in temp)
        {
            allEnemies.Add(entity);
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
        EntityCaptureCooldown();
        if(currentHealth <= 0 || currentCaptureLevel >= 100f)
        {
            Die(anim);
        }
        firstupdate = false;
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
        
        if (Time.time > nextRandomMoveTrigger)
        {
            nextRandomMoveTrigger = Time.time + 5f;
            
            randomLocation.x = transform.position.x + Random.Range(-3f, 3f);
            randomLocation.y = transform.position.y + Random.Range(-3f, 3f);
            randomLocation.z = transform.position.z;
            if (isTouchingWall)
            {
                MoveTowardsVector3(randomLocation);
            }
        }

        if (Vector3.Distance(randomLocation,transform.position) > 0.1f)
        {
            
            MoveTowardsVector3(randomLocation);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (isTouchingWall)
        {
            randomLocation = transform.position;
        }
    }

    private void CowardBehaviour()
    {
        GameObject closestEnemy = LocateClosestEnemy();
        
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
        GameObject closestEnemy = LocateClosestEnemy();
        if (closestEnemy != null)
        {
            MoveTowardsTarget(closestEnemy.transform);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollidedWitwall(collision))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CollidedWitwall(collision))
        {
            isTouchingWall = false;
        }
    }

    private bool CollidedWitwall(Collision2D collision)
    {
        GameObject wall = collision.gameObject;
        if (wall.CompareTag("Wall") || wall.CompareTag("Door"))
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
                    if (enemy.GetComponent<Entity>() != this)
                    {
                        enemy.GetComponent<Entity>().TakeDamage(20f);
                        nextAttack = currentTime + attackCooldown;
                    }
                }
            }
        }
    }

}
