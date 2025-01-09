using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivatedDoorSystem : MonoBehaviour
{
    private List<SwitchDoor> doors;
    private List<SwitchButton> buttons;
    private List<SwitchButton> pressedButtons;
    private bool hasBeenOpened;

    private void Awake()
    {
        buttons = GetComponentsInChildren<SwitchButton>().ToList();
        doors = GetComponentsInChildren<SwitchDoor>().ToList();
        pressedButtons = new();
    }
    public void FBIOpenTheDoor()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }

    public void CloseTheDoor()
    {
        foreach (var door in doors)
        {
            door.Close();
        }
    }

    public void PressButton(SwitchButton _btn)
    {
        buttons.Remove(_btn);
        pressedButtons.Add(_btn);
        if(buttons.Count <=0)
        {
            FBIOpenTheDoor();
            hasBeenOpened = true;
        }    
    }

    public void ReleaseButton(SwitchButton _btn)
    {
        buttons.Add(_btn);
        pressedButtons.Remove(_btn);
        if (hasBeenOpened)
        {
            CloseTheDoor();
            hasBeenOpened = false;
        }
    }
}
