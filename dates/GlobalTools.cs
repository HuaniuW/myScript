using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

using System;

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
        //print("ObjName    "+ ObjName);
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
    public static void CanvasGroupAlpha(CanvasGroup CGroup,float num)
    {
        if (num > 1) num = 1;
        if (num < 0) num = 0;
        bool isTrue = num == 0 ? false : true;
        CGroup.alpha = num;
        CGroup.interactable = isTrue;
        CGroup.blocksRaycasts = isTrue;
    }



    public static void PlayAudio(string AudioName, System.Object obj,float _value = 1)
    {
        //(this[AudioName] as AudioSource)
        //print(AudioName);
        AudioSource cAudio = GetDicSSByName(AudioName, obj);
        if (cAudio)
        {
            cAudio.volume = _value * GlobalSetDate.instance.GetSoundEffectValue();
            cAudio.Play();
        }

    }

    public static AudioSource GetDicSSByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        //print("  type " + type+ "    fieldInfo  "+ fieldInfo);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as AudioSource;
    }


    public static bool IsHasDate(string changeDate, string[] currentGKDateArr)
    {
        string name = changeDate.Split('-')[0];
        for (var i = 0; i < currentGKDateArr.Length; i++)
        {
            string date = currentGKDateArr[i];
            string dateName = date.Split('-')[0];
            if (name == dateName) return true;
        }
        return false;
    }

    public static string GetHasDate(string changeDate, string[] currentGKDateArr)
    {
        string getDate = "";
        string name = changeDate.Split('-')[0];
        for (var i = 0; i < currentGKDateArr.Length; i++)
        {
            string date = currentGKDateArr[i];
            string dateName = date.Split('-')[0];
            if (name == dateName) {
                getDate = date;
                return getDate;
            }
        }
        return getDate;
    }


    //-------------------------地图处理 工具函数-------------------------------------------

    //记录 起始点

    //随机数 int 0-100
    public static int GetRandomNum()
    {
        int num = 0;
        num = (int)UnityEngine.Random.Range(0, 100);
        return num; 
    }
    //距离随机 给一个距离 算出随机的距离
    public static float GetRandomDistanceNums(float disatnce)

    {
        float d = UnityEngine.Random.Range(0, disatnce);
        return d;
    }

    //设置物体 深度 order
    public void SetMapObjOrder(GameObject mapObj,int orderNum = 0)
    {
        mapObj.GetComponent<SpriteRenderer>().sortingOrder = orderNum;
    }


    //获取物体 深度 order
    public int GetMapObjOrder(GameObject mapObj)
    {
        return mapObj.GetComponent<SpriteRenderer>().sortingOrder;
    }


    //获取 景 物体长度 
    public static float GetJingW(GameObject mapObj)
    {
        return mapObj.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    //获取 景 物体 宽度
    public static float GetJingH(GameObject mapObj)
    {
        return mapObj.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    //获取有 tl 和rd点的物体 宽度 
    public static float GetHasPointMapObjW(GameObject mapObj)
    {
        float w = 0;
        Transform tl = mapObj.transform.Find("tl");
        Transform rd = mapObj.transform.Find("rd");
        w = Math.Abs(tl.transform.position.x - rd.transform.position.x);
        return w;
    }

    //获取有 tl 和rd点的物体 高度 
    public static float GetHasPointMapObjH(GameObject mapObj)
    {
        float h = 0;
        Transform tl = mapObj.transform.Find("tl");
        Transform rd = mapObj.transform.Find("rd");
        h = Math.Abs(tl.transform.position.y - rd.transform.position.y);
        return h;
    }


    //获取上一个 地图的连接点  X方向
	public static Vector2 GetXMapLJDian(GameObject mapObj){
		Vector2 ljDian = Vector2.zero;
		Transform tl = mapObj.transform.Find("tl");
		float __x = tl.transform.position.x + GetHasPointMapObjW(mapObj);
		float __y = tl.transform.position.y;
		ljDian = new Vector2(__x,__y);
		return ljDian;
	}


    //获取中心点位置 全局位置 zxd
    //获取 点位置 tl rd zxd
    public static Vector2 GetPointWorldPosByName(GameObject mapObj,String pointName) {
        Transform pointObj = mapObj.transform.Find(pointName);
        if(pointObj != null)
        {
            //考虑全局转换 这个有没有意义 待定
            //*****  需要找到该全局 位置的点  这样来 做对接 transform.Localposition
            //Vector2 p = mapObj.transform.position;

            //相对于父对象的坐标
            Vector2 p = mapObj.transform.InverseTransformPoint(pointObj.position);

            //世界坐标
            Vector2 p3 = pointObj.transform.position;
            return p3;
        }
        return Vector2.zero;
    }


	//设置 临近 位置 Y轴位移的范围   通用吗？
	public static void SetXYLinJin(GameObject mapObj, GameObject mapObj2, string qifu = "no"){
		float __x = 0;
		if(qifu == "no"){
			
		}else if(qifu == "up"){
			
		}else if(qifu == "down"){
			
		}
	}
    //设置 空中块的位置  设置之间的最大位置  

    
    //设置 近景 相对于mapObj x的位置 给出 初始x 超出 就按最大位置来 0 的话直接放第一个位置
	public static void SetJinJingXY(GameObject mapObj,float _x,float _y){

    }

	//根据 随机出来的 放置 景的数量来循环  **有个i来比例位置**    x方向上的 景 放置
	public static void SetJinJingPoint(GameObject mapObj, GameObject jingObj, int i, int nums, string chaoxiang = "up")
	{
		//获取 物体全局位置
		Vector2 tlPot = GetPointWorldPosByName(mapObj, "tl");
		//获取 物体 宽度
		float w = GetHasPointMapObjW(mapObj);
		//这里必须保证 景的宽度 小于地板宽度**************

		float jingW = GetJingW(jingObj);
		float jingH = GetJingH(jingObj);
		float __x = 0;
		float __y = 0;

		if (nums == 1)
		{
			float xiuzhengNums = GetRandomNum() >= 50 ? 0.3f : -0.3f;
			__x = tlPot.x + GetHasPointMapObjW(mapObj) * 0.5f + GetRandomDistanceNums(xiuzhengNums);
		}
		else
		{
			if (i == 0)
			{
				__x = tlPot.x + jingW * 0.5f + 0.2f;
			}
			else if (i == nums - 1)
			{
				__x = tlPot.x + jingW * 0.5f + w / nums * i - 0.2f;
			}
			else
			{
				__x = tlPot.x + jingW * 0.5f + w / nums * i;
			}

		}

		if (chaoxiang == "up")
		{
			__y = tlPot.y + jingH * 0.5f - 0.4f + GetRandomDistanceNums(0.2f);
		}
		else
		{
			__y = tlPot.y - jingH * 0.5f + 0.4f - GetRandomDistanceNums(0.2f);
			//翻转
			jingObj.transform.localScale = new Vector3(jingObj.transform.localScale.x, -jingObj.transform.localScale.y, jingObj.transform.localScale.z);
		}

		jingObj.transform.position = new Vector3(__x, __y, jingObj.transform.position.z);
	}
    
    //设置 近景 相对于mapObj x的位置 给出 初始x 超出 就按最大位置来 0 的话直接放第一个位置
    //根据 随机出来的 放置 景的数量来循环  **有个i来比例位置**
    public static void SetJinJingPoint(GameObject mapObj,int i)
    {
        //获取 物体全局位置
        //获取 物体 宽度
    }

    //设置物体 全局 位置 xyz

    //上一个生成物体
    public static GameObject tempRecordMapObj = null;
    //设置为上一个地图物体 
    public static void SetTempMapRecordObj(GameObject mapObj)
    {
        tempRecordMapObj = mapObj;
    }

    //获取上一个 物体 的 信息  ？？？？？
    
}
