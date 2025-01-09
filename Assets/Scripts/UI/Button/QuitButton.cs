using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.GoToStartScene();
    }
}
