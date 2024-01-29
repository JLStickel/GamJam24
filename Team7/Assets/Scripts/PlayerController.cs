using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 10;
    public float dashForce = 20;
    public float dashDuration = 0.2f; 

    Rigidbody2D rb;
    float moveHorizontal;
    float moveVertical;
    bool isDashing = false;
    float dashTime;

    private void Awake()
    { 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartDash();
        }

        if (isDashing && Time.time >= dashTime)
        {
            EndDash();
        }
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;

        Vector2 dashDirection = new Vector2(moveHorizontal, moveVertical).normalized;
        rb.velocity = dashDirection * dashForce;

    }

    void EndDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

}
