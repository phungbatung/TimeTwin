using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.LoadLevelMenuScene();
    }
}
