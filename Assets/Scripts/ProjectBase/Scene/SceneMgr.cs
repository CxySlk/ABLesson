using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseMgr<SceneMgr>
{
    /// <summary>
    /// 同步加载场景，加载完成后执行回调函数
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="callBack">回调函数</param>
    public void LoadScene(string sceneName, UnityAction callBack)
    {
        SceneManager.LoadScene(sceneName);
        callBack();
    }

    /// <summary>
    /// 异步加载场景，加载完后调用回调函数
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="callBack">回调函数</param>
    public void LoadSceneAsync(string sceneName, UnityAction callBack)
    {
        // 这里可以添加事件中心的触发器事件

        // 协程加载场景
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName, callBack));
    }

    /// <summary>
    /// 协程，用于协助异步加载处理完后调用回调函数
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsync(string sceneName, UnityAction callback)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);

        // 返回进度
        while (!ao.isDone)
        {
            yield return ao.progress;
        }

        // 执行回调函数
        callback?.Invoke();
    }
}
