using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

/// <summary>
/// 带参数的事件信息
/// </summary>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions = action;
    }
}

/// <summary>
/// 不带参数的事件信息
/// </summary>
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions = action;
    }
}

public class EventCenter : BaseMgr<EventCenter>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听器
    /// </summary>
    /// <param name="eventName">事件的名字</param>
    /// <param name="action">事件触发后，执行的委托函数</param>
    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        // 此前有该事件
        if (eventDic.ContainsKey(eventName))
           (eventDic[eventName] as EventInfo<T>).actions += action;
        // 此前没有该事件
        else
            eventDic.Add(eventName, new EventInfo<T>(action));
    }
    /// <summary>
    /// 添加不带参数的事件监听器
    /// </summary>
    /// <param name="eventName">事件的名字</param>
    /// <param name="action">事件触发后，执行的委托函数</param>
    public void AddEventListener(string eventName, UnityAction action)
    {
        // 此前有该事件
        if (eventDic.ContainsKey(eventName))
           (eventDic[eventName] as EventInfo).actions += action;
        // 此前没有该事件
        else
            eventDic.Add(eventName, new EventInfo(action));
    }

    /// <summary>
    /// 移除监听过该事件的监听器
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">监听该事件的监听器</param>
    public void RemoveEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T>).actions -= action;
    }

    /// <summary>
    /// 移除监听过该不带参数事件的监听器
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="action">监听该事件的监听器</param>
    public void RemoveEventListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo).actions -= action;
    }

    /// <summary>
    /// 触发事件名eventName的事件
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="info">要传入的参数</param>
    public void EventTrigger<T>(string eventName, T info)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
    }

    /// <summary>
    /// 触发事件名eventName且不带参数的事件
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="info">要传入的参数</param>
    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo).actions?.Invoke();
    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
