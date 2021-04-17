using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject captureScreen;
    public Slider slider;
    private Animator anim;
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemyLayers;
    public Camera mainCamera;
    private static CinemachineVirtualCamera virtualCamera;
    private bool isInfected;
    private float currentCaptureLevel;
    private float currentHealth;
    private static float time = -10F;
    public float defaultCaptureLevel = 0f;
    public float maxHealth = 100f;


    void Start()
    {
        virtualCamera = mainCamera.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentCaptureLevel = defaultCaptureLevel;
        isInfected = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.Translate(Vector3.zero);
    }

    private void Update()
    {
        PlayerCaptureCooldown();

        if(currentCaptureLevel > 0)
        {
            captureScreen.SetActive(true);
        }

        if(currentHealth <= 0 || currentCaptureLevel >= 100f)
        {
            Die();
        }
        
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if(virtualCamera.m_Lens.OrthographicSize < 3f)
                virtualCamera.m_Lens.OrthographicSize += 0.05f;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (virtualCamera.m_Lens.OrthographicSize > 0.5f)
                virtualCamera.m_Lens.OrthographicSize -= 0.05f;
        }


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
            anim.SetBool("isWalking", true);
        }else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Push();
        }

        
    }
    void FixedUpdate()
    {
        float speed = 0.08F;
        Vector3 direction;
        direction.x = 0F;
        direction.y = 0F;
        direction.z = 0F;

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            direction.x = speed / 2;
            direction.y = speed / 2;
            transform.Translate(direction);
            Dash(4f, 4f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            direction.x = speed / 2;
            direction.y = -speed / 2;
            transform.Translate(direction);
            Dash(4f, -4f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            direction.x = speed / 2;
            direction.y = speed / 2;
            transform.Translate(direction);
            Dash(-4f, 4f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            direction.x = speed / 2;
            direction.y = -speed / 2;
            transform.Translate(direction);
            Dash(-4f, -4f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.W))
        {
            direction.y = speed;
            transform.Translate(direction);
            Dash(0f, 8f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = speed;
            transform.Translate(direction);
            Dash(8f, 0f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -speed;
            transform.Translate(direction);
            Dash(0f, -8f, GetComponent<Rigidbody2D>());
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction.x = speed;
            transform.Translate(direction);
            Dash(-8f, 0f, GetComponent<Rigidbody2D>());
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Die()
    {
        captureScreen.SetActive(false);
        deathScreen.SetActive(true);
        this.enabled = false;
    }

    private void PlayerCaptureCooldown()
    {
        if(currentCaptureLevel != 0f)
        {
            currentCaptureLevel-= 0.5f;
            captureScreen.SetActive(false);
        }
    }

    public void CapturePlayer()
    {
        currentCaptureLevel += 1f;
        slider.value = currentCaptureLevel;
    }

    void Push()
    {
        anim.SetTrigger("Push");

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 force;
            force.x = 20;

            if (enemy.GetComponent<Enemy>().transform.position.x < transform.position.x)
            {
                force.x = -20;
            }
            force.y = 0;
            enemy.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            enemy.GetComponent<Enemy>().TakeDamage(50f);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private static void Dash(float x, float y, Rigidbody2D player)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float currentTime = Time.time;
            
            if (currentTime > time)
            {
                Vector2 force = new Vector2();
                force.x = 2f*x;
                force.y = 2f*y;
                player.AddForce(force, ForceMode2D.Impulse);
                time = currentTime + 5;
            }
        }
    }

    public void Infect()
    {
        isInfected = true;
    }
}
