using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.Restart();
    }
}
