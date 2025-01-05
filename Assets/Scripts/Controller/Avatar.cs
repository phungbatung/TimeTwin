using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Animator anim;

    private int idle = Animator.StringToHash("Idle");
    private int blink = Animator.StringToHash("Blink");

    private float timer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = Random.Range(1, 3);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            anim.Play(blink);
        }    
    }

    public void EndBlink()
    {
        anim.Play(idle);
        timer = Random.Range(3, 5);
    }    
       
}
