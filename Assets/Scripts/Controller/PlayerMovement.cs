using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour, IPortalInteractable
{
    private float horizontal = 0;
    public bool facingRight { get; private set; }
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector3 rootPosition;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        facingRight = true;
        rootPosition = transform.position;
    }

    private void Start()
    {
        GameManager.Instance.OnRestart += ResetPostion;
        
    }
    public void Update()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        FlipCheck();
    }
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }
    public void FlipCheck()
    {
        if( (horizontal<0 && facingRight) || (horizontal>0 && !facingRight))
            Flip();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started&& IsGrounded())
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
        }
    }
    public void GetHorizontal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            horizontal = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            horizontal = 0;
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    public void ResetPostion()
    {
        transform.position = rootPosition;
    }

    public void Teleport(Vector3 destination)
    {
        transform.position=destination;
        rb.velocity=Vector2.zero;
    }
}
