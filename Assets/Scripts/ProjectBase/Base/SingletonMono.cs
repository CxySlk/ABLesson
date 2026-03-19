using UnityEngine;

/// <summary>
/// 继承MonoBehaviour的单例模式基类，并且当前场景只能存在一个挂载该脚本的对象
/// 出现新的物体挂载该脚本时，会自动删除上一个挂载载该脚本的对象
/// </summary>
/// <typeparam name="T"></typeparam>
[DisallowMultipleComponent]
public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this as T;
        }
    }
}
