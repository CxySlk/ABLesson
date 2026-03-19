using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : BaseMgr<MonoMgr>
{
    private MonoController monoController;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoMgr");
        monoController= obj.AddComponent<MonoController>();
    }

    /// <summary>
    /// 增加更新周期函数
    /// </summary>
    /// <param name="action">更新周期委托函数</param>
    public void AddUpdateListener(UnityAction action)
    {
        monoController.AddUpdateListener(action);
    }

    /// <summary>
    /// 移除更新周期函数
    /// </summary>
    /// <param name="action">更新周期委托函数</param>
    public void RemoveUpdateListener(UnityAction action)
    {
        monoController.RemoveUpdateListener(action);
    }

    /// <summary>
    /// 添加协程
    /// </summary>
    /// <param name="routine">协程</param>
    public void StartCoroutine(IEnumerator routine)
    {
        monoController.AddCoroutine(routine);
    }
}
