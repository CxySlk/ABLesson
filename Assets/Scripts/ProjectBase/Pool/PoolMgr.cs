using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 缓存池容器类
/// </summary>
public class PoolData
{
    private Transform fartherTrans;
    public List<GameObject> poolList { get; private set; }

    public PoolData(GameObject obj, GameObject poolObj)
    {
        // 将对象的父对象设置为容器
        fartherTrans = new GameObject(obj.name).transform;
        obj.transform.SetParent(fartherTrans);

        // 设置容器的父对象
        fartherTrans.SetParent(poolObj.transform);

        poolList = new List<GameObject>() { obj };
    }

    /// <summary>
    /// 从容器中取出物体
    /// </summary>
    /// <returns></returns>
    public GameObject TakeObj()
    {
        GameObject obj = null;

        // 取出对象
        obj = poolList[0];
        poolList.RemoveAt(0);

        // 激活对象
        obj.SetActive(true);
        // 断开父子联系
        obj.transform.SetParent(null);

        return obj;
    }

    /// <summary>
    /// 将物体压进容器中
    /// </summary>
    /// <returns></returns>
    public void PushObj(GameObject obj)
    {
        obj.transform.SetParent(fartherTrans);
        obj.SetActive(false);

        poolList.Add(obj);
    }
}

/// <summary>
/// 缓存池模块
/// </summary>
public class PoolMgr : BaseMgr<PoolMgr>
{
    private Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;

    /// <summary>
    /// 将对象从缓存池中取出
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void TakeObj(string name, UnityAction<GameObject> callBack)
    {
        // 缓存池中有抽屉，并且抽屉有东西
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            GameObject obj = poolDic[name].TakeObj();
            // 激活对象
            obj.SetActive(true);
            // 断开父子联系
            obj.transform.SetParent(null);

            callBack?.Invoke(obj);
        }
        // 没有抽屉或没有东西
        else
        {
            ResMgr.Instance.LoadAsync<GameObject>(name, (obj) =>
            {
                obj.name = name;
                callBack(obj);
            });
        }
    }

    /// <summary>
    /// 将对象压进缓存池
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name, GameObject obj)
    {
        // 创建Pool对象
        if (poolObj == null)
            poolObj = new GameObject("Pool");

        // 失活对象
        obj.SetActive(false);

        // 有抽屉，直接放东西
        if (poolDic.ContainsKey(name))
            poolDic[name].PushObj(obj);
        // 没有抽屉，加个抽屉，把东西放进去
        else
            poolDic.Add(name, new PoolData(obj, poolObj));
    }
}
