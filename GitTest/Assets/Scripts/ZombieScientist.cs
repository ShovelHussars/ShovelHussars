using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScientist : Enemy
{
    public float lookRadius;

    private readonly float attackCooldown = 5f;
    private float nextAttack = -1f;
    Transform target;
    

    void Start()
    {
        
        type = "Zombie";
        speed = 0.01f;
        Entity[] temp = GameObject.FindObjectsOfType<Entity>();
        allEnemies = new List<Entity>();
        foreach (var entity in temp)
        {
            allEnemies.Add(entity);
        }
        target = LocateClosestEnemy().transform;
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
        firstupdate = false;
    }

    void FixedUpdate()
    {
        target = LocateClosestEnemy().transform;
        MovementController();
        Attack();
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
                if (enemy.enabled && enemy.GetComponent<Entity>() != this)
                {
                    enemy.GetComponent<Entity>().TakeDamage(20f);
                    enemy.GetComponent<Entity>().Infect();
                    nextAttack = currentTime + attackCooldown;
                }
            }
        }
    }

    override protected void MovementController()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        anim.SetBool("isWalking", false);
        if (distance < lookRadius)
        {
            MoveTowardsTarget(target);
        }
        else
        {

        }
    }

}
