using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShape : MonoBehaviour, IPortalInteractable, IResettable
{
    private Rigidbody2D rb;
    private Vector3 rootPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rootPos = transform.position;
    }
    private void Start()
    {
        GameManager.Instance.OnReplay += ResetToDefault;
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    
    public void Teleport(Vector3 destination)
    {
        transform.position = destination;
    }

    public void ResetToDefault()
    {
        transform.position = rootPos;
    }
}
