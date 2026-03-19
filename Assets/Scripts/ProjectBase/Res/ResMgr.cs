using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseMgr<ResMgr>
{
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T Load<T>(string resName) where T : Object
    {
        T res = null;

        // 加载资源
        res = Resources.Load<T>(resName);

        // 是GameObject类型就实例化
        if (res is GameObject)
            res = GameObject.Instantiate(res);

        return res;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadAsync<T>(string resName, UnityAction<T> callBack) where T : Object
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(resName, callBack));
    }

    /// <summary>
    /// 协程真实异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadAsync<T>(string resName, UnityAction<T> callBack) where T : Object
    {
        // 异步加载资源
        ResourceRequest r = Resources.LoadAsync(resName);

        // 等待加载完成
        yield return r;

        // 如果是GameObject就实例化对象
        if (r.asset is GameObject)
            callBack?.Invoke(GameObject.Instantiate(r.asset as T));
        else
            callBack?.Invoke(r.asset as T);
    }
}
