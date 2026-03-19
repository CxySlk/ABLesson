using UnityEngine;

/// <summary>
/// 继承MonoBehaviour的单例模式基类
/// 与SingletonMono不同的是，当instance为空时，会自动创建一个空物体并且挂载该脚本，且过场景不删除
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingetonAutoMono<T> : MonoBehaviour where T : SingetonAutoMono<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }
}
