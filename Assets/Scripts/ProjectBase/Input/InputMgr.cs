using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseMgr<InputMgr>
{
    public InputMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    public bool isOpen { get; set; }

    /// <summary>
    /// 开启输入检测
    /// </summary>
    /// <param name="isOpen">是否开启输入检测</param>
    public void OpenInputCheck(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    /// <summary>
    /// 检测按键按下与抬起，并且向事件中心发出事件触发
    /// </summary>
    /// <param name="keyCode">按键</param>
    private void CheckKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
            EventCenter.Instance.EventTrigger("KeyDown", keyCode);
        if (Input.GetKeyUp(keyCode))
            EventCenter.Instance.EventTrigger("KeyUp", keyCode);
    }

    private void Update()
    {
        if (!isOpen)
            return;

        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.Mouse0);
        CheckKeyCode(KeyCode.Mouse1);
    }
}
