using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // 开启按键检测
        InputMgr.Instance.isOpen = true;
        // 监听按键按下与抬起的事件
        EventCenter.Instance.AddEventListener<KeyCode>("KeyDown", KeyDown);
        EventCenter.Instance.AddEventListener<KeyCode>("KeyUp", KeyUp);
    }

    // Update is called once per frame
    private void KeyDown(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Mouse0:
                UIMgr.Instance.ShowPanel<LoginPanel>();
                break;
            case KeyCode.Mouse1:
                UIMgr.Instance.HidePanel<LoginPanel>();
                break;
            case KeyCode.W:
                UIMgr.Instance.GetPnael<LoginPanel>().M();
                break;
        }
    }

    private void KeyUp(KeyCode keyCode)
    {
        switch (keyCode)
        {
            
        }
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<KeyCode>("KeyDown", KeyDown);
        EventCenter.Instance.RemoveEventListener<KeyCode>("KeyUp", KeyUp);
    }
}
