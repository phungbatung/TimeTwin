using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour, IResettable
{
    private ActivatedDoorSystem activatedDoorSystem;
    private float openLength = 0.1f;
    private float closeLength;
    private Vector3 rootPos;
    private bool isPressed;
    private void Awake()
    {
        activatedDoorSystem = GetComponentInParent<ActivatedDoorSystem>();
        rootPos=transform.position;
        closeLength= transform.localScale.y;
        isPressed = false;
    }
    private void Start()
    {
        GameManager.Instance.OnReplay += ResetToDefault;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activatedDoorSystem.PressButton(this);
        transform.localScale = new Vector3(transform.localScale.x, openLength, transform.localScale.z);
        transform.position = rootPos - new Vector3(0, (closeLength-openLength)/2, 0);
        isPressed = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isPressed)
        {
            activatedDoorSystem.PressButton(this);
            transform.localScale = new Vector3(transform.localScale.x, openLength, transform.localScale.z);
            transform.position = rootPos - new Vector3(0, (closeLength - openLength) / 2, 0);
            isPressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        activatedDoorSystem.ReleaseButton(this);
        transform.localScale = new Vector3(transform.localScale.x, closeLength, transform.localScale.z);
        transform.position = rootPos;
        isPressed = false;
    }

    public void ResetToDefault()
    {

        transform.position = rootPos;
        transform.localScale = new Vector3(transform.localScale.x, closeLength, transform.localScale.z);
    }
}
