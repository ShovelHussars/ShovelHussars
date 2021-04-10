using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    private Guard[] guards;
    private Guard guard;
    private Player player;
    private float _time = -1F;
    private bool spooked = false;
    private bool allGuardsDead = false;
    private bool isTouchingWall = false;
    private int guardIndex = 0;
    private Vector2 targetPosition;
    private float speed = 0.09F;
    private float distanceX, distanceY;
    private Vector3 direction;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        guards = GameObject.FindObjectsOfType<Guard>();
        anim = GetComponent<Animator>();
        if (guards != null && guards.Length != 0)
        {
            guard = guards[guardIndex];
            //print("I was here");
        }
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
        
            try
            {
                if (guard == null || !guard.gameObject.activeSelf)
                {
                    guard = LookForGuard();
                }
            }
            catch (Exception e)
            {
                guard = null;
            }
        


    }

    private Guard LookForGuard()
    {
        foreach(var guard in guards)
        {
            if (guard.gameObject.activeSelf)
            {
                return guard;
            }
        }
        return null;
    }

    private void FixedUpdate()
    {
        if (Time.time.Equals(_time))
        {
            spooked = false;
        }
        anim.SetBool("isWalking", false);
        if (guard != null)
        {
            if (!(Mathf.Abs(player.transform.position.x - transform.position.x) < 3 &&
                Mathf.Abs(player.transform.position.y - transform.position.y) < 3) &&
                !spooked)
            {
                RunTowardsGuard();
            }
            else
            {
                if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3 &&
                Mathf.Abs(player.transform.position.y - transform.position.y) < 3)
                {
                    if (!spooked)
                    {
                        spooked = true;
                        _time = Time.time + 5F;
                    }
                }

                if ((Mathf.Abs(player.transform.position.x - transform.position.x) < 7 &&
                Mathf.Abs(player.transform.position.y - transform.position.y) < 7)&&
                !isTouchingWall)
                {
                    RunFromDClass();
                }
            }
        }
        else
        {
            allGuardsDead = true;
            if(guard != null)
            {
                allGuardsDead = false;
            }
            if (!isTouchingWall)
            {
                RunFromDClass();
            }
        }
    }

    private void RunTowardsGuard()
    {
        targetPosition = guard.transform.position;
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
}
