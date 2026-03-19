using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private void Start()
    {
        FindChildrenControl<Button>();

        UIMgr.Instance.AddCustomEventListener(GetControl<Button>("Button1"), EventTriggerType.PointerEnter, (data) =>
        {
            print("进入");
        });
        UIMgr.Instance.AddCustomEventListener(GetControl<Button>("Button1"), EventTriggerType.PointerExit, (data) =>
        {
            print("离开");
        });
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Button1":
                print("Button1按下");
                break;
            case "Button1 (1)":
                print("Button1 (1)");
                break;
            default:
                break;
        }
    }
    public void M()
    {
        print("LoginPanel");
    }
}
