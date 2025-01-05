using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.GoToNextLevel();
    }
}
