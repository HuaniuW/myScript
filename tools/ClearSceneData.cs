﻿using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

public class ClearSceneData : MonoBehaviour
{
    //异步对象
    private AsyncOperation async;

    //下一个场景的名称
    private static string nextSceneName;

    void Awake()
    {
        /**
        Object[] objAry = Resources.FindObjectsOfTypeAll();

        for (int i = 0; i < objAry.Length; ++i)
        {
            objAry[i] = null;//解除资源的引用
        }

        Object[] objAry2 = Resources.FindObjectsOfTypeAll();

        for (int i = 0; i < objAry2.Length; ++i)
        {
            objAry2[i] = null;
        }
    */

        //卸载没有被引用的资源
        Resources.UnloadUnusedAssets();

        //立即进行垃圾回收
        GC.Collect();
        GC.WaitForPendingFinalizers();//挂起当前线程，直到处理终结器队列的线程清空该队列为止
        GC.Collect();

    }

    void Start()
    {
        StartCoroutine("AsyncLoadScene", nextSceneName);
    }

    /// 
    /// 静态方法，直接切换到ClearScene，此脚本是挂在ClearScene场景下的，就会实例化，执行资源回收
    /// 
    /// 
    public static void LoadLevel(string _nextSceneName)
    {
        nextSceneName = _nextSceneName;
        //Application.LoadLevel("ClearScene");
    }

    /// 
    /// 异步加载下一个场景
    /// 
    /// 
    /// 
    IEnumerator AsyncLoadScene(string sceneName)
    {
        //async = Application.LoadLevelAsync(sceneName);
        yield return async;
    }

    void OnDestroy()
    {
        async = null;
    }

}