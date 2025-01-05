using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ButtonBase
{
    public override void OnClickAction()
    {
        GameManager.Instance.OpenMenu();
    }
}
