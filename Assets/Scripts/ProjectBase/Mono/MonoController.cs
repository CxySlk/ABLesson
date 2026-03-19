using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono控制类
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        updateEvent?.Invoke();
    }

    /// <summary>
    /// 增加更新周期函数
    /// </summary>
    /// <param name="action">更新周期委托函数</param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// 移除更新周期函数
    /// </summary>
    /// <param name="action">更新周期委托函数</param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }

    /// <summary>
    /// 添加协程
    /// </summary>
    /// <param name="routine">协程</param>
    public void AddCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
