using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPosition;
    private float speed = 0.01F;
    private float distanceX, distanceY;
    private Vector3 direction;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        direction.z = 0F;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
        if (player != null)
        {
            distanceX = GetComponent<Rigidbody2D>().transform.position.x - playerPosition.x;
            distanceY = GetComponent<Rigidbody2D>().transform.position.y - playerPosition.y;
            print("X=" + distanceX + " Y=" + distanceY);
            if (distanceX > 0)
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    direction.x = speed * (distanceX / (distanceX - distanceY));
                    direction.y = -speed * (distanceY / (distanceX - distanceY));
                    print(direction.x + ", " + direction.y);
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = -speed * (distanceY / (distanceX + distanceY));
                    print(direction.x + ", " + direction.y);
                }
                anim.SetBool("isWalking",true);
                transform.rotation = Quaternion.Euler(0F, 180F, 0F);
            }
            else
            {
                if ((distanceX > 0 && distanceY < 0) || (distanceX < 0 && distanceY > 0))
                {
                    direction.x = speed * (distanceX / (distanceX - distanceY));
                    direction.y = speed * (distanceY / (distanceX - distanceY));
                    print(direction.x + ", " + direction.y);
                }
                else
                {
                    direction.x = speed * (distanceX / (distanceX + distanceY));
                    direction.y = speed * (distanceY / (distanceX + distanceY));
                    print(direction.x + ", " + direction.y);
                }
                anim.SetBool("isWalking", true);
                transform.rotation = Quaternion.Euler(0F, 0F, 0F);
            }
            //direction.x = 0.01F;
            //direction.y = 0.01F;
            transform.Translate(direction);
        }
    }
}
