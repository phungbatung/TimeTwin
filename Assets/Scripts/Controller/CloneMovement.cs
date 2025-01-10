using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CloneMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    private ReplaySystem rep;
    private bool facingRight = true;
    private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (rep != null)
        {
            RecordData recData = rep.GetRecordData();
            if (recData != null)
            {
                transform.position = recData.pos;
                FlipCheck(recData.facingRight);
            }
            if(!rep.hasNextRecordData)
            {
                rep = null;
            }
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnRestart -= DestroyGO;
    }
    public void Flip()
    {
        sr.flipX = !sr.flipX;
    }
    public void FlipCheck(bool _facingRight)
    {
        if (_facingRight != facingRight)
        {
            Flip();
            facingRight = _facingRight;
        }
    }
    public void SetupData(ReplaySystem _rep)
    {
        rep = _rep;
        GameManager.Instance.OnRestart += DestroyGO;
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

}
