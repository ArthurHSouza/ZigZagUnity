using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public Transform referencePoint;
    private Animator animator;
    private Rigidbody body;
    private bool isMovinRight;
    private GameManager gameManager;
    private float speed = 2f;
    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        isMovinRight = transform.rotation.y <= 10f;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if(!gameManager.GetStarted())
        {
            return;
        }
        else
        {
            animator.SetTrigger("isRunning");
        }

        body.transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void Update()
    {
        if (!gameManager.GetStarted())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isMovinRight = !isMovinRight;
            if (isMovinRight)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (!Physics.Raycast(referencePoint.position, -transform.up, 20) && transform.position.y < 0)
        {
            animator.SetTrigger("isFalling");
            if(transform.position.y < -5)
            {
                gameManager.RestartGame();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Battery"))
        {
            Destroy(other.gameObject);
            gameManager.IncressScore();
        }
    }

    public void IncressSpeed(float percente)
    {
        speed *= percente;
        animator.speed += 0.1f; 
    }
}
