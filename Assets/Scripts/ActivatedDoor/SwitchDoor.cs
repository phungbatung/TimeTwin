using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0, Down = 1, Left = 2, Right = 3
}
public class SwitchDoor : MonoBehaviour
{
    [SerializeField] private Direction openDirEnum;
    [SerializeField] private float openSpeed;


    private Vector3[] openDirection = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
    private Vector3 closePos;
    private Vector3 openPos;

    private Vector3 closeScale;
    private Vector3 openScale;
    private float closeLength=0.3f;

    private void Awake()
    {
        Vector3 openDir = openDirection[(int)openDirEnum];
        closeScale = transform.localScale;
        closePos= transform.position;
        openScale = new Vector3(openDir.x==0?closeScale.x : closeLength, openDir.y == 0 ? closeScale.y : closeLength, transform.localScale.z);
        openPos = new Vector3(closePos.x + openDir.x * (closeScale.x - openScale.x) / 2, 
                                closePos.y + openDir.y * (closeScale.y - openScale.y) / 2, 0);
    }
    public void Open()
    {
        StartCoroutine("OpenTheDoor");
    }

    public void Close() 
    {

    }

    

    public IEnumerator OpenTheDoor()
    {

        Vector3 openDir = openDirection[(int)openDirEnum];
        while (transform.localScale != openScale)
        {

            transform.localScale = new Vector3(transform.localScale.x - Mathf.Abs(openDir.x) * openSpeed * Time.deltaTime,
                                                transform.localScale.y - Mathf.Abs(openDir.y) * openSpeed * Time.deltaTime, 1);
            if (transform.localScale.x < openScale.x)
                transform.localScale = new Vector3(openScale.x, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.y < openScale.y)
                transform.localScale = new Vector3(transform.localScale.x, openScale.y, transform.localScale.z);
            transform.position = new Vector3(openDir.x == 0 ? closePos.x : (closePos.x - openDir.x * (transform.localScale.x-closeScale.x) / (openScale.x-closeScale.x)* (openPos.x - closePos.x)),
                                                openDir.y == 0 ? closePos.y : (closePos.y - openDir.y * (transform.localScale.y - closeScale.y) / (openScale.x - closeScale.y) * (openPos.y - closePos.y)), 0);
            yield return null;
        }    
    }

    public IEnumerator CloseTheDoor()
    {
        Vector3 openDir = openDirection[(int)openDirEnum];

        while (transform.localScale != closeScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + Mathf.Abs(openDir.x) * openSpeed * Time.deltaTime,
                                                transform.localScale.y + Mathf.Abs(openDir.y) * openSpeed * Time.deltaTime, 1);
            transform.position = new Vector3(openDir.x == 0 ? closePos.x : (closePos.x - openDir.x * (transform.localScale.x - closeScale.x) / (openScale.x - closeScale.x) * (openPos.x - closePos.x)),
                                                 openDir.y == 0 ? closePos.y : (closePos.y - openDir.y * (transform.localScale.y - closeScale.y) / (openScale.x - closeScale.y) * (openPos.y - closePos.y)), 0);
            yield return null;
        }
    }
}
