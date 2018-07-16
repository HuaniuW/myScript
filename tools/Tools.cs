using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tools : MonoBehaviour {
    //UNITY  帧率 跟龙骨帧率同步就OK   1:1    目前unity默认是60帧  只要龙骨是60帧 就可以1:1取到帧数
    // Use this for initialization
    void Start () {
        
    }

    //输出系统时间 可用于玩家数量统计
    public static void TimeData()
    {
        DateTime dt = new DateTime();
        dt = System.DateTime.Now;
        //此处的大小写必须完全按照如下才能输出长日期长时间，时间为24小时制式，hh:mm:ss格式输出12小时制式时间
        string strFu = dt.ToString("yyyy-MM-dd HH:mm:ss");
        print("strFu:" + strFu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //限制帧率  没什么卵用
    //Application.targetFrameRate = 30;
    //-1 代表不限定帧率
    //Application.targetFrameRate = -1;
    //Application.targetFrameRate 是用来让游戏以指定的帧率运行，如果设置为 -1 就让游戏以最快的速度运行。
    //但是 这个 设定会 垂直同步 影响。
    //如果设置了垂直同步，那么 就会抛弃这个设定 而根据 屏幕硬件的刷新速度来运行。
    //如果设置了垂直同步为1，那么就是 60 帧。
    //如果设置了为2 ，那么就是 30 帧。
    // print(Application.targetFrameRate);  获取帧率 默认-1

    //**  测试可用  float.Parse(str),
    //或者float.TryParse(str,out value);
    //或者Convert.ToFloat（string）

    //动态取资源
    //Transform tf = this.transform.Find(nIndex.ToString());

    //层级 order  -1  ---  -10  近景
    //地板 0-10 给予足够图层空间 特效之类的乱七八糟的东西
    //前景 11-10

    //Mathf.Lerp(x, x2, t);  从 x 到x2 中间插值 t 接近x2
    //Mathf.Clamp(x, min, max);  限制

    //box collider  设置不参与碰撞 is Trigger  √上  

    //public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);

    //TransformDirection 将一个方向从局部坐标系转换到世界坐标系
    //InverseTransformDirection 将一个方向从世界坐标系转换到局部坐标系
    //TransformPoint 将一个点从局部坐标系转换到世界坐标系
    //InverseTransformPoint 将一个点从世界坐标系转换到局部坐标系

    //在Resources文件夹 动态获取 Prefab（翻译:预制） 预制资源
    //GameObject obj = Resources.Load("fk") as GameObject;
    //obj = Instantiate(obj);


    //Vector3 s = new Vector3((float)1.5, 1, 0);
    //obj.transform.position = s;  改变位置
    //obj.transform.localScale = s;  缩放

    // public IEnumerator IEDestory(GameObject gameObject, float time)  协程 伪进程
    //StartCoroutine(ObjectPools.GetInstance().IEDestory(hitBar,2f));  执行 协程


    //创建一个A.cs脚本挂载在空物体B上，
    //在.cs文件中输入this表示的是当前的脚本，就是脚本A
    //transform当前物体的transform组件，也就是空物体B的transform组件
    //gameObject当前物体的GameObjectB，也就是空物体B

    //OnEnable  每次进场会触发这个函数 有点像 onStage()
}
