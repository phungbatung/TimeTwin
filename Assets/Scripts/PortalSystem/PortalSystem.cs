using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    private Portal[] portals;
    private void Awake()
    {
        portals = GetComponentsInChildren<Portal>();
        if(portals.Length !=2)
            Destroy(gameObject);
        else
        {
            portals[0].SetDestination(portals[1]);
            portals[1].SetDestination(portals[0]);
        }    
    }
}
