using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTools : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    ///根据在场景根目录中位置寻找对象
    /// </summary>
    /// <param name="_urlName">对象路径名</param>
    /// <returns></returns>
    public static GameObject FindObjByName(string _urlName)
    {
        GameObject obj = GameObject.Find("/" + _urlName) as GameObject;
        if (obj == null) obj = GameObject.Find("/" + _urlName + "(Clone)") as GameObject;
        return obj;
    }

    //
    /// <summary>
    ///根据名字动态加载 并实例化 GameObject  （预制对象必须在resources 文件夹下） 
    /// </summary>
    /// <param name="ObjName"></param>
    /// <returns></returns>
    public static GameObject GetGameObjectByName(string ObjName)
    {
        GameObject obj = Resources.Load(ObjName) as GameObject;
        obj = Instantiate(obj);
        return obj;
    }

    //Text text = btn.transform.Find("Text").GetComponent<Text>();

    //public static GameObject FindObjInTransformByName(GameObject obj,string _name) {
    //    GameObject _obj = obj.Find(_name) as GameObject;
    //    return _obj;
    //}

    /// <summary>
    /// 控制对象的 透明度
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="num"></param>
    public static void CanvasGroupAlpha(CanvasGroup obj,float num)
    {
        if (num > 1) num = 1;
        if (num < 0) num = 0;
        print("hihihihi     "+num);
        obj.GetComponent<CanvasGroup>().alpha = num;
        obj.GetComponent<CanvasGroup>().interactable = true; //相互作用
        obj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
