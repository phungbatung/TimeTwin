using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerMovement>()?.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<PlayerMovement>()?.transform.SetParent(null);
    }
}
