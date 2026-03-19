using UnityEngine;

/// <summary>
/// 单例模式基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseMgr<T> where T : BaseMgr<T>, new()
{
    private static T instance = new T();
    public static T Instance => instance;
    protected BaseMgr()
    {

    }
}