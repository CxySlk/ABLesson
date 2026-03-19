using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIMgr : BaseMgr<UIMgr>
{
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvas;

    public UIMgr()
    {
        if (canvas == null)
            canvas = ResMgr.Instance.Load<GameObject>("UI/Canvas").transform;
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack"></param>
    public void ShowPanel<T>(UnityAction callBack = null) where T : BasePanel
    {
        // 面板名字
        string panelName = typeof(T).Name;

        // 判断该面板是否已经打开
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].ShowMe(callBack);
            return;
        }

        // 异步加载完成后执行回调函数
        ResMgr.Instance.LoadAsync<GameObject>("UI/" + panelName, (panelObj) =>
        {
            // 设置父对象
            panelObj.transform.SetParent(canvas, false);

            // 创建面板
            T panel = panelObj.GetComponent<T>();
            panelDic.Add(panelName, panel);
            // 显示完成后调用回调函数
            panel.ShowMe(callBack);
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">是否谈出</param>
    /// <param name="callBack"></param>
    public void HidePanel<T>(bool isFade = true, UnityAction callBack = null) where T : BasePanel
    {
        // 面板名字
        string panelName = typeof(T).Name;

        // 没有该面板
        if (!panelDic.ContainsKey(panelName))
            return;

        // 获取面板并从容器中移除
        T panel = panelDic[panelName].GetComponent<T>();
        panelDic.Remove(panelName);

        switch (isFade)
        {
            // 面板渐隐关闭
            case true:
                panel.HideMe(() =>
                {
                    callBack?.Invoke();
                    GameObject.Destroy(panel.gameObject);
                });
                break;
            // 面板直接关闭
            case false:
                callBack?.Invoke();
                GameObject.Destroy(panel.gameObject);
                break;
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPnael<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        return null;
    }

    /// <summary>
    /// 添加自定义事件
    /// </summary>
    /// <param name="control">控件</param>
    /// <param name="triggerType">事件类型</param>
    /// <param name="action">事件触发调用的委托函数</param>
    public void AddCustomEventListener(UIBehaviour control, EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        // 获取控件的事件触发器脚本
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        // 创建自定义事件监听
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);

        // 添加自定义事件监听到脚本上
        trigger.triggers.Add(entry);
    }
}
