using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.ResumeGame();
    }
}
