using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Portal destination;
    public Transform outPos;
    public void SetDestination(Portal _destination)
    {
        destination = _destination;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IPortalInteractable>()?.Teleport(destination.outPos.position);
    }
}
