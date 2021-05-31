using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;

    [SerializeField] private BoolVariable isMoving;
    [SerializeField] private BoolVariable movingLeft;
    [SerializeField] private BoolVariable movingRight;
    [SerializeField] private float moveSpeed;

    [SerializeField] private BoolVariable isGrounded;
    [SerializeField] private Transform groundCheck;
    private float checkRadius = 0.05f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameEvent jump;

    // Start is called before the first frame update
    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        isMoving.Value = false;
        movingLeft.Value = false;
        movingRight.Value = false;
        jump.AddListener(Jump);
    }

    private void OnDisable()
    {
        jump.RemoveListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded.Value = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void FixedUpdate()
    {
        if (isMoving.Value && movingLeft.Value)
        {
            MoveLeft();
        }
        else if (isMoving.Value && movingRight.Value)
        {
            MoveRight();
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    void MoveLeft()
    {
        rb2D.velocity = new Vector2(-1 * moveSpeed, rb2D.velocity.y);
    }

    void MoveRight()
    {
        rb2D.velocity = new Vector2(1 * moveSpeed, rb2D.velocity.y);
    }

    void Jump()
    {
        if (isGrounded.Value)
        {
            rb2D.AddForce(Vector2.up * jumpForce);
        }           
        
        //isGrounded.Value = false;
    }
}
