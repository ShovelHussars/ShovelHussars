using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.35f;
    public LayerMask enemyLayers;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.Translate(Vector3.zero);
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0F, 180F, 0F);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0F, 0F, 0F);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isRunning", true);
        }else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Push();
        }

        Vector3 cameraPos;

        cameraPos.x = transform.position.x;
        cameraPos.y = transform.position.y;
        cameraPos.z = -100f;
        camera.transform.position = cameraPos;
    }

    void Push()
    {
        anim.SetTrigger("Push");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Vector2 force;
            force.x = 20;
            if(enemy.transform.position.x < transform.position.x)
            {
                force.x = -20;
            }
            force.y = 0;
            enemy.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void FixedUpdate()
    {
        float speed = 0.08F;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        Vector3 direction;
        direction.x = 0F;
        direction.y = 0F;
        direction.z = 0F;
        
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            direction.x = speed/2;
            direction.y = speed/2;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            direction.x = speed/2;
            direction.y = -speed/2;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            direction.x = speed/2;
            direction.y = speed/2;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            direction.x = speed/2;
            direction.y = -speed/2;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direction.y = speed;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = speed;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -speed;
            transform.Translate(direction);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction.x = speed;
            transform.Translate(direction);
        }

        
        
    }
}
