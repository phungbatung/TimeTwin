using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    private ActivatedDoorSystem activatedDoorSystem;
    private float closeLength = 0.1f;
    private float openLength = 0.3f;
    private Vector3 rootPos;
    private void Awake()
    {
        activatedDoorSystem = GetComponentInParent<ActivatedDoorSystem>();
        rootPos=transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activatedDoorSystem.PressButton(this);
        transform.localScale = new Vector3(transform.localScale.x, closeLength, transform.localScale.z);
        transform.position = rootPos - new Vector3(0, (openLength-closeLength)/2, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activatedDoorSystem.ReleaseButton(this);
        transform.localScale = new Vector3(transform.localScale.x, openLength, transform.localScale.z);
        transform.position = rootPos;
    }
}
