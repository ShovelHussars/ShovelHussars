using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private Player player;
    private DClass[] dClasses;
 //   private GameObject[] _possibleTargets;
 //   private GameObject target;
    private Vector2 playerPosition;
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemyLayers;
    private float speed = 0.08F;
    private float distanceX, distanceY;
    private Vector3 direction;
    private Animator anim;
    public float Maxhealth = 100f;
    private float currenthealth;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = Maxhealth;
        dClasses = GameObject.FindObjectsOfType<DClass>();
        direction.z = 0F;
        player = GameObject.FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currenthealth <= 0)
        {
            Die();
        }

        Attack();
    }

    private void Attack()
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private void Die()
    {
        this.enabled = false;
        anim.SetBool("isWalking", false);
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    public void takeDamage(float damage)
    {
        currenthealth -= damage;
    }

    private void FixedUpdate()
    {
        MovementController();
    }

    private void MovementController()
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
