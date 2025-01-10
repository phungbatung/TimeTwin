using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0, Down = 1, Left = 2, Right = 3
}
public class SwitchDoor : MonoBehaviour, IResettable
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
    private void Start()
    {
        GameManager.Instance.OnReplay += ResetToDefault;
        
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void Open()
    {
        StartCoroutine("OpenTheDoor");
    }

    public void Close() 
    {
        StartCoroutine("CloseTheDoor");
    }

    

    public IEnumerator OpenTheDoor()
    {
        StopCoroutine("CloseTheDoor");
        Vector3 openDir = openDirection[(int)openDirEnum];
        while (transform.localScale != openScale)
        {

            transform.localScale = new Vector3(transform.localScale.x - Mathf.Abs(openDir.x) * openSpeed * Time.deltaTime,
                                                transform.localScale.y - Mathf.Abs(openDir.y) * openSpeed * Time.deltaTime, 1);
            transform.localScale = new Vector3(transform.localScale.x < openScale.x ? openScale.x : transform.localScale.x,
                                               transform.localScale.y < openScale.y ? openScale.y : transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(openDir.x == 0 ? closePos.x : (openPos.x + (transform.localScale.x - openScale.x) / (closeScale.x - openScale.x) * (closePos.x - openPos.x)),
                                             openDir.y == 0 ? closePos.y : (openPos.y + (transform.localScale.y - openScale.y) / (closeScale.y - openScale.y) * (closePos.y - openPos.y)), 0);
            yield return null;
        }
        StopCoroutine("OpenTheDoor");
    }

    public IEnumerator CloseTheDoor()
    {
        StopCoroutine("OpenTheDoor");
        Vector3 openDir = openDirection[(int)openDirEnum];

        while (transform.localScale != closeScale)
        {
            transform.localScale = new Vector3(transform.localScale.x + Mathf.Abs(openDir.x) * openSpeed * Time.deltaTime,
                                                transform.localScale.y + Mathf.Abs(openDir.y) * openSpeed * Time.deltaTime, 1);
            if (transform.localScale.x > closeScale.x)
                transform.localScale = new Vector3(closeScale.x, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.y > closeScale.y)
                transform.localScale = new Vector3(transform.localScale.x, closeScale.y, transform.localScale.z);
            transform.position = new Vector3(openDir.x == 0 ? closePos.x : (openPos.x + (transform.localScale.x - openScale.x) / (closeScale.x - openScale.x) * (closePos.x - openPos.x)),
                                             openDir.y == 0 ? closePos.y : (openPos.y + (transform.localScale.y - openScale.y) / (closeScale.y - openScale.y) * (closePos.y - openPos.y)), 0); 
            yield return null;
        }
        StopCoroutine("CloseTheDoor");
    }

    public void ResetToDefault()
    {
        transform.position = closePos;
        transform.localScale=closeScale;
        StopAllCoroutines();
    }
}
