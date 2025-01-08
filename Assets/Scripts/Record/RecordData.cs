using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RecordData
{
    public float timeStamp { get; private set; }
    public Vector3 pos {  get; private set; }
    public bool facingRight { get; private set; }

    public RecordData(Vector3 _pos, bool _facingRight, float _timeStamp)
    {
        pos = _pos;
        facingRight = _facingRight;
        timeStamp = _timeStamp;
    }
}
