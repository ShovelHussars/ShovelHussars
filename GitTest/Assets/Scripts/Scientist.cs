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
    private int guardIndex = 0;
    private Vector2 targetPosition;

    void Start()
    {
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
        direction.z = 0F;

    }

    private void OnCollisionEnter2D(Collision2D collision)
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
        if(wall.tag.Equals("Wall"))
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
                RunFromDClass();
            }else if (!spooked)
            {
                RunTowardsGuard();
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
                RunFromDClass();
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


    private void RunTowardsGuard()
    {
        targetPosition = chosenGuard.transform.position;
        distanceX = GetComponent<Rigidbody2D>().transform.position.x - targetPosition.x;
        distanceY = GetComponent<Rigidbody2D>().transform.position.y - targetPosition.y;
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

    private void RunFromDClass()
    {
        targetPosition = player.transform.position;
        distanceX = GetComponent<Rigidbody2D>().transform.position.x - targetPosition.x;
        distanceY = GetComponent<Rigidbody2D>().transform.position.y - targetPosition.y;
        //print("X=" + distanceX + " Y=" + distanceY);
        if (distanceX > 0)
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
        else
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
        //direction.x = 0.01F;
        //direction.y = 0.01F;
        transform.Translate(direction);
    }

    protected override void Attack(){}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scareRange);

    }
}
