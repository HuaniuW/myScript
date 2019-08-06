using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tools2 : MonoBehaviour {
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


    //限制帧率  关闭垂直同步 有效  edit - project settings - quality
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
    //var cubeF = GameObject.Find("/MainCamera");   记得加“/”获取全部资源可以

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
    //StartCoroutine(ObjectPools.GetInstance().IEDestory(hitBar,2f));  执行 协程IEnumerator


    //创建一个A.cs脚本挂载在空物体B上，
    //在.cs文件中输入this表示的是当前的脚本，就是脚本A
    //transform当前物体的transform组件，也就是空物体B的transform组件
    //gameObject当前物体的GameObjectB，也就是空物体B

    //OnEnable  每次进场会触发这个函数 有点像 onStage() 

    //当无法改变 localScale 的时候找一下 是否在哪设置了 scale将其拉回去了

    //if (hit.rigidbody) hit.rigidbody.AddForceAtPosition(force * direction, hitInfo.point);  z作用于点的 力  离中心越远 力越小

    /**
     * https://blog.csdn.net/f786587718/article/details/49208023
     * 
        5.爆炸的局部空间动力效果的实现。首先获取在爆炸点的某个球体半径范围内的所有碰撞体：
        Collider[] colliders = Physics.OverlapSphere( transform.position,explosionRadius );
        此函数除了包含位置，半径外还有默认参数遮罩层级（进行碰撞器筛选）。
        接着对所有碰撞体施加力，unity内置了添加爆炸力函数：
        foreach (Collider hit in colliders) {   if (!hit)   continue;  //防止碰撞体不存在？貌似多余？ 
        //防止碰撞体hit不存在rigidbody程序出错，只对存在刚体属性的碰撞体添加爆炸作用力。通过这个区别也可以设置一些不受爆炸影响的物体（比如terrain）。
        if (hit.rigidbody) hit.rigidbody.AddExplosionForce(explosionPower, explosionPos,explosionRadius);
        //此函数还含有第四个参数upwardsModifier（正数n代表虚拟爆炸点在物体中心的下方n米处），可适当设置以增加一个物体下方的虚拟爆炸力，炸飞效果更酷。参数要适当大，否则虚拟爆炸点在物体内部效果有点奇怪。
        附注：AddExplosionForce也可以用于制作球形范围内的引力，只要设置了负的作用力。

        6.在5中的情况，如果除了有爆炸作用力还需要计算按距离衰减的爆炸杀伤力，首先需要计算每个物体离爆炸点最近的表面点坐标：
        Vector3 closestPoint = hit.rigidbody.ClosestPointOnBounds(explosionPosition);
        坐标与爆炸中心距离：float distance = Vector3.Distance(closestPoint, explosionPos);
        最后依照爆炸威力随距离衰减的原则计算物体所受伤害占爆炸总伤害的百分比：
        float damage = (1.0F - Mathf.Clamp01(distance /  explosionRadius))*explosionDamage;
     */


    //float distance = Vector3.Distance(closestPoint, explosionPos);  计算距离

    //float.yan  ------Parse强制转换

    //dict.ContainsKey("atkDistance")  Dictionary是否包含键


}
//延迟调用 StartCoroutine(IEnumerator)    IEnumerator类型的方法


//设置 UI  Image 的宽高
//1. var rt = loadingBar.GetComponent<RectTransform>();  取到RectTransform
//2. rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);   宽
//   rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);     高
//拖入图片要先 设置 sprite editor  设置九宫格border  否则拖入到Image后 无法设置九宫格
//  图片设置九宫格  Image 拖入图片后  设置为 sliced 



/**
 3、用单个字符来分隔：
string str="aaajbbbjccc";
string[] sArray=str.Split('j');
foreach(string i in sArray) Response.Write(i.ToString() + "<br>");
输出结果：
aaa
bbb
ccc
    */


/**
 if (Input.anyKey)
    {//得到按下什么键
        print("anyKey  " + Input.inputString);
    }
 */


/**
 改变物体的 rotation
由于transform.rotation是 Quaternion类型,并不能像transform.position一样通过直接给rotation赋值Vector3(X,Y,Z)，
    但是通过transform.localEulerAngles我们可以直接改变rotation的值，如
    transform.localEulerAngles = new Vector3(X, Y, Z);
    其中X,Y,Z代表角度。
 */
//下面这种改变角度 也可以用
// print(this.transform.localRotation);
//this.transform.localRotation = new Quaternion(0,180,0,1);
/**
if (_sacaleX >0)
{
    this.transform.localRotation = new Quaternion(0, 0, 0, 1);
}
else
{
    this.transform.localRotation = new Quaternion(0, 180, 0, 1);
}
*/

/**
 iTween  缓动 好像无法控制 UGUI的透明度
   iTween.ScaleBy(obj, iTween.Hash("x", 1f, "time", 0.1f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
   iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 0f, "time", 2f, "easeType", iTween.EaseType.easeInOutExpo, "oncomplete", "Fd"));
*/


/**  隐藏对象
 GameObject.renderer.enabled   
//是控制一个物体是否在屏幕上渲染或显示  而物体实际还是存在的 只是想当于隐身 而物体本身的碰撞体还依然存在的  

GameObject.Destroy()    
//表示移除物体或物体上的组件 代表销毁该物体  实际上该物体的内存并没有立即释放，而是等到这一帧的结束才会真正销毁

GameObject.SetActive()     
//是否在场景中停用该物体,设置gameObject.SetActive(false)，则你在场景中用find找不到该物体    
//如果该物体有子物体 你要用SetActiveRecursively(false) 来控制是否在场景中停用该物体（递归的）
//SetActive(false)的时候，物体不再渲染，释放占用资源

    //Destroy(this.gameObject);
    //立即销毁 并且释放内存  *要是gameObject 才能删除  只能在场景切换后才能执行 否则报错
    //DestroyImmediate(this.gameObject, true);

Camera.cullingMask
//设置相机的渲染层次，在不需要某个物体的时候，cullingMask中将此物体的layer去掉，但是前提是要规划好layer，不能影响其他不希望隐藏掉的物体。

GameObject.transform.position = FAR_AWAY
//设置一个无限远的位置，再不需要的时候就将物体移动至这个位置，但是这样物体并没有释放，占用的所有资源都会继续占用
 */


// 动态加载预制资源 prefab 需要将prefab放在resources文件夹内 首字母大小写不限

//FieldInfo 映射 动态获取属性

/****************************
 * 凡是加了事件侦听的地方 要么1次的直接 取消侦听 要么 在OnDeStory()函数里面统一移除侦听 否则会重复侦听
 * **/





////很好用的排序方法  适用list和array
//public static int Min(int[] array)
//{
//    if (array == null) throw new Exception("数组空异常");
//    int value = 0;
//    bool hasValue = false;
//    foreach (int x in array)
//    {
//        if (hasValue)
//        {
//            if (x < value) value = x;
//        }
//        else
//        {
//            value = x;
//            hasValue = true;
//        }
//    }
//    if (hasValue) return value;
//    throw new Exception("没找到");
//}



//this.transform.SetAsLastSibling();   将obj的显示对象提到父类 最高



//public class SkillBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  继承点击事件接口

//Time.timeScale = 1;  控制播放速度 =0暂停

//int n = (int)UnityEngine.Random.Range(0,100);随机数




/*******************
 * 防止怪物卡墙
 * 物理 Rigidbody 中设置 属性  Collision Detection （碰撞检测算法，用于防止刚体因快速移动而穿过其他对象）
 * 默认是 Discrete 离散的 速度快会卡墙
 * continuous  连续不间断的  不会卡墙 消耗会大一些
 * 
 * 
 * ferr 里面 物理块要勾选填充Fill Collider
 */

//Application.loadedLevelName 获取场景名字(API 过时)
//SceneManager.GetActiveScene().name  获取场景名字  需要using UnityEngine.SceneManagement;


//鼠标事件失灵  有可能是被image遮挡了  比如 场景过度的黑色渐变


/*
 * image 宽高的获取和修改
 * 
 宽：gameObject.GetComponent<RectTransform>().rect.width

高：gameObject.GetComponent<RectTransform>().rect.height

GetComponent<RectTransform>().sizeDelta

或者GetComponent<RectTransform>().rect.size
     
     */



//SetAsFirstSibling(); UI层级提到开始 
//SetAsLastSibling();  放到最下层 显示在最高层