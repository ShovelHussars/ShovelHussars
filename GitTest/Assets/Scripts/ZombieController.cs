using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float lookRadius;

    private Animator anim;
    private Vector2 playerPosition;
    private float distanceX, distanceY;
    private Vector3 direction;
    private float speed = 0.01F;
    private float attackCooldown = 5f;
    private float nextAttack = -1f;
    Transform target;
    Player player;
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemyLayers;
    public float Maxhealth = 100f;
    private float currenthealth;

    void Start()
    {
        currenthealth = Maxhealth;
        player = GameObject.FindObjectOfType<Player>();
        target = player.transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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

    private void Attack()
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
                    hitEnemies[0].GetComponent<Player>().takeDamage(20f);
                    nextAttack = currentTime + attackCooldown;
                }
            }
        }
    }

    public void takeDamage(float damage)
    {
        currenthealth -= damage;
    }

    private void MovementController()
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
