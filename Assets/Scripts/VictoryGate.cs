using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
            GameManager.Instance.Victory();
    }
}
