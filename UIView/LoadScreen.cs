﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {
    public Text loadTxt;

    public Image loadingBar;

	// Use this for initialization
	void Start () {
        loadTxt.text = "加载场景";
        //AsyncOperation op = SceneManager.LoadSceneAsync("guan1_1");
        print("取档游戏 场景名 " + GlobalSetDate.instance.screenName);
        //判断是否是 随机的 地图

        StartCoroutine(IEStartLoading(GlobalSetDate.instance.screenName));
        //GetMainCamera();
    }
    
    public void LoadScreenByName(string screenName)
    {
        StartCoroutine(IEStartLoading(screenName));
        GlobalSetDate.instance.CloseDieScreen();
    }


    private IEnumerator IEStartLoading(string scene)
    {
        int displayProgress = 0;
        int toProgress = 0;
        if (Globals.isDebug) {
           
        }

        print("screen>  " + scene);
        if (scene.Split('@').Length > 1)
        {
            print("进入随机 地图！！！" + scene);
            GlobalSetDate.instance.CReMapName = scene.Split('@')[1];
            scene = "RMap_1";
            if(SceneManager.GetActiveScene().name == scene) scene = "RMap_2";
            print("进入随机地图--->   " + scene+"   CMapName   "+ GlobalSetDate.instance.CReMapName);
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
      
        //是否允许自动跳场景 （如果设为false 只会加载到90 不会继续加载）
        op.allowSceneActivation = false;
        var rt = loadingBar.GetComponent<RectTransform>();
        //print("hi!!!");
        while (op.progress < 0.9f)
        {
            //print("in");
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                //SetLoadingPercentage(displayProgress);
                //loadingBar.rectTransform.se

               
                rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, displayProgress*1.5f);
                //rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

                loadTxt.text = "LOADING..." + displayProgress.ToString()+"%";
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, displayProgress*1.5f);
            loadTxt.text = "LOADING..." + displayProgress.ToString() + "%";
            //SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        //设置为true 后 加载到100后直接自动跳转
        op.allowSceneActivation = true;

    }

}
