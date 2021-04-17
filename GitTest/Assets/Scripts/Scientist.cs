using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : Enemy
{
    private Guard[] guards;
    private Guard chosenGuard;
    private float _time = -1F;
    public float scareRange = 3f;
    private bool spooked = false;
    private bool allGuardsDead = false;
    private bool isTouchingWall = false;
    private Vector2 targetPosition;

    void Start()
    {
        type = "Scientist";
        anim = GetComponent<Animator>();
        guards = GameObject.FindObjectsOfType<Guard>();
        player = GameObject.FindObjectOfType<Player>();

        chosenGuard = LookForGuard();
        if(chosenGuard == null)
        {
            allGuardsDead = true;
        }

        direction.z = 0F;
        currenthealth = maxHealth;

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
        if(wall.tag.Equals("Wall") || wall.tag.Equals("Door"))
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
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
    override protected void MovementController()
    {
        if (Time.time.Equals(_time))
        {
            spooked = false;
        }
        anim.SetBool("isWalking", false);

        if ((chosenGuard != null) && !chosenGuard.GetComponent<Guard>().enabled)
        {
            chosenGuard = LookForGuard();
        }

        if(chosenGuard == null)
        {
            allGuardsDead = true;
        }

        if (!allGuardsDead)
        {
            Collider2D[] veryCloseEnemies = Physics2D.OverlapCircleAll(transform.position, scareRange, enemyLayers);

            if(veryCloseEnemies.Length != 0)
            {
                if (!spooked)
                {
                    spooked = true;
                    _time = Time.time + 5F;
                }
            }

            if(spooked && !isTouchingWall)
            {
                direction = MoveAwayFromTarget(player.transform);
                transform.Translate(direction);
            }
            else if (!spooked)
            {
                direction = MoveTowardsTarget(chosenGuard.transform);
                transform.Translate(direction);
            }
            
        }
        else
        {
            Collider2D[] veryCloseEnemies = Physics2D.OverlapCircleAll(transform.position, scareRange, enemyLayers);

            if (veryCloseEnemies.Length != 0)
            {
                if (!spooked)
                {
                    spooked = true;
                    _time = Time.time + 5F;
                }
            }

            if (spooked && !isTouchingWall)
            {
                direction = MoveAwayFromTarget(player.transform);
                transform.Translate(direction);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

        }
    }
    private Guard LookForGuard()
    {
        foreach(var guard in guards)
        {
            if (guard.GetComponent<Guard>().enabled)
            {
                return guard;
            }
        }
        return null;
    }

    protected override void Attack(){}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scareRange);

    }
}
