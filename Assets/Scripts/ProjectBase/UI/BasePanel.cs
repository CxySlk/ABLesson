using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlsDic = new Dictionary<string, List<UIBehaviour>>();

    // 改变面板透明度，用于控制渐变显隐
    private CanvasGroup canvasGroup;
    // 面板的Alpha透明度变化速度
    public float alphaSpeed = 5;
    // 面板的Alpha透明度
    private float alphaValue;
    // 面板是否显示
    protected bool isShow;

    // 面板显示完成后要执行的委托函数
    private UnityAction showCallBack;
    // 面板隐藏完成后要执行的委托函数
    private UnityAction hideCallBack;


    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = this.AddComponent<CanvasGroup>();
    }

    protected virtual void Update()
    {
        // 控制面板渐显
        if (isShow && alphaValue != 1)
        {
            alphaValue += Time.deltaTime * alphaSpeed;
            alphaValue = Mathf.Clamp(alphaValue, 0, 1);

            canvasGroup.alpha = alphaValue;

            if (alphaValue == 1)
                showCallBack?.Invoke();
        }
        // 控制面板渐隐
        else if (!isShow && alphaValue != 0)
        {
            alphaValue -= Time.deltaTime * alphaSpeed;
            alphaValue = Mathf.Clamp(alphaValue, 0, 1);

            canvasGroup.alpha = alphaValue;

            if (alphaValue == 0)
                hideCallBack?.Invoke();
        }
    }

    protected virtual void OnClick(string btnName)
    {

    }

    /// <summary>
    /// 返回控件类型为T，控件名为name的控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    /// <param name="name">控件名</param>
    /// <returns></returns>
    protected T GetControl<T>(string name) where T : UIBehaviour
    {
        // 查找控件容器中是否有该控件
        if (controlsDic.ContainsKey(name))
        {
            foreach (UIBehaviour c in controlsDic[name])
            {
                if (c is T)
                    return c as T;
            }
        }

        return null;
    }

    /// <summary>
    /// 找到子对象对应的控件并且添加到字典容器中
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    protected void FindChildrenControl<T>() where T : UIBehaviour
    {
        // 找到所有子对象上的该类型子控件
        T[] controls = GetComponentsInChildren<T>();

        // 将控件脚本添加到容器中
        foreach (T control in controls)
        {
            if (controlsDic.ContainsKey(control.name))
                controlsDic[control.name].Add(control);
            else
                controlsDic.Add(control.name, new List<UIBehaviour>() { control });

            // 自动添加按钮的监听函数，如果需要添加其他类型的控件，请自行添加
            if (control is Button)
            {
                (control as Button).onClick.AddListener(() =>
                {
                    string btnName = control.name;
                    OnClick(btnName);
                });
            }
        }
    }

    /// <summary>
    /// 显示面板的方法
    /// </summary>
    /// <param name="action">显示完成后调用的委托</param>
    public virtual void ShowMe(UnityAction action)
    {
        alphaValue = 0;
        isShow = true;
        showCallBack = action;
    }

    /// <summary>
    /// 隐藏面板的方法
    /// </summary>
    /// <param name="action">隐藏完成后调用的委托</param>
    public virtual void HideMe(UnityAction action)
    {
        alphaValue = 1;
        isShow = false;
        hideCallBack = action;
    }
}
