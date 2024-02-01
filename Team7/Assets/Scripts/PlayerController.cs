using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float dashForce = 20;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2.0f;

#region Events
    public delegate void _playerEvents();
    public event _playerEvents _startDash;
    public event _playerEvents _endDash;

#endregion

    Rigidbody2D rb;
    Collider2D collider;
    public float moveHorizontal;
    public float moveVertical;
    public bool canDash = true;
    public bool isDashing = false;
    float dashTime;
    public bool invicibleDash;
    public GameObject shield;

    public GameObject dash;
    DashManager dashManager;

    private void Awake()
    {     
    }

    void Start()
    {
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        _startDash += EmptyFunction;

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
        if (canDash)
        {
            _startDash();

            if (invicibleDash)
                collider.enabled = false;
            isDashing = true;
            dashTime = Time.time + dashDuration;

            Vector2 dashDirection = new Vector2(moveHorizontal, moveVertical).normalized;
            rb.velocity = dashDirection * dashForce;

            canDash = false;

            StartCoroutine(DashCooldown());
        }
    }

    void EndDash()
    {
        collider.enabled = true;
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void EmptyFunction()
    {

    }
}
