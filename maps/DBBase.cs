using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DBBase : MonoBehaviour
{
    [Header("左上点")]
    public Transform tl;

    [Header("右下点")]
    public Transform rd;

    public Light2D light2d;

    public GameObject diban1;
    public GameObject diban2;
    public GameObject diban3;
    public GameObject diban4;


    [Header("上面 自动景 左点")]
    public Transform topL;
    [Header("上面 自动景 右点")]
    public Transform topR;

    [Header("上面 自动景 左点2")]
    public Transform topL2;
    [Header("上面 自动景 右点2")]
    public Transform topR2;



    [Header("上面的 连接点")]
    public Transform lianjiedianU;

    [Header("下面的 连接点")]
    public Transform lianjiedianD;

    [Header("右边的 连接点")]
    public Transform lianjiedianR;


    [Header("左边的 连接点")]
    public Transform lianjiedianL;

    //根据这个类型来出怪
    [Header("地形type")]
    public string DXType = "";


    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalSetDate.instance.IsCMapHasCreated) {
            GetJing();
            SetLightColor();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


    public float GetWidth()
    {
        return Mathf.Abs(tl.position.x - rd.position.x);
    }

    public float GetHight()
    {
        return Mathf.Abs(tl.position.y - rd.position.y);
    }



    public bool IsPingDiJing = false;

    

    GameObject maps;

    //左右景布置  这里 后面会根据全局 来判断和调整 景是哪些内容  区别后缀不要用数字
    void GetJing()
    {
        //这里根据 当前 关卡  来判断  是否 生成 树 背景远景 等 复杂的 类型       ***** 判断  用哪个大关卡的 景


        //近前景  石头  草 啥的
        //前排的 近景 档在玩家前面  的栅栏 什么的  这个后面看  DD
        //背景  石头 草 树 花  栅栏 路灯 等   
        //背远景 树林影子 等
        //前远景 加一层 前景   石头 草树  黑色栅栏  模糊的 黑色景

        maps = GlobalTools.FindObjByName("maps");


        if (IsHasShu) GetShu();
        if (IsZJY) GetZYJ();
        if (IsPingDiJing) GetLRJinBG();
        if (IsPingDiJing) GetJQJ();
        if (IsSCWu) GetWus();
        if (IsJYJ) GetJYJ();
        //装饰物
        if (IsZhuangshiwu) Zhuanshiwu();

        if (IsTopJ) GetTopJ();
        if (IsTopJ2) GetTopJ2();
    }


    public bool IsZhuangshiwu = false;
    //栅栏什么的 可以放进 近景
    //装饰物 是单个 等 什么的 只要单个的   一个底面只要一个 位置随机
    void Zhuanshiwu()
    {
        //判断是否有装饰物  只有一个
        if (GlobalTools.GetRandomNum() < 20) return;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        //int nums = 1 + GlobalTools.GetRandomNum(2);
        //SetJingByDistanceU("jyj_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(2), 0, 0, -10, "u");

        GameObject Jobj = GetJObjByListName("zsw_1");

        float jingW = 0; 

        if (Jobj.GetComponent<J_SPBase>())
        {
            jingW = Jobj.GetComponent<J_SPBase>().GetWidth();
            if (Jobj.GetComponent<J_SPBase>().light2d != null) {
                Jobj.GetComponent<J_SPBase>().light2d.color = GlobalTools.RandomColor();
            }
            
            Jobj.GetComponent<J_SPBase>().SetSD(-10);
        }

        
        
        float _w = GetWidth() - jingW;
        float __x = tl.position.x + GlobalTools.GetRandomDistanceNums(_w);
        float __y = tl.position.y - GlobalTools.GetRandomDistanceNums(1);

        

        Jobj.transform.position = new Vector3(__x,__y,0);
        
    }


    public bool IsJYJ = false;
    //中远景
    void GetJYJ()
    {
        int nums = 4 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        SetJingByDistanceU("jyj_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(2), 1, 0.6f, -30, "u", 3);
    }




    public bool IsZJY = false;
    //中远景
    void GetZYJ()
    {
        int nums = 4 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        SetJingByDistanceU("zyj_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(3), 2, 1, -40, "u",3);
    }



    public bool IsHasShu = false;
    void GetShu()
    {


        //print(" 树叔叔时速！！！！！！！！！！！！ ");
        int nums = 1+GlobalTools.GetRandomNum(2);
        //SetJingByDistanceU("shu_1", nums, pos1, pos2, pos1.y - 3f, 0, 0, -10, "d");

        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        SetJingByDistanceU("shu_1", nums, pos1, pos2, pos1.y - GlobalTools.GetRandomDistanceNums(2), 0, 0, -10, "u",2);


        //这里加 木栅栏  

        //路灯

        //铁栅栏  这种纯 排的 景
    }

    public bool IsTopJ = false;
    void GetTopJ()
    {
        int nums = 2 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = topL.position;
        Vector2 pos2 = topR.position;
        SetJingByDistanceU("qju_1", nums, pos1, pos2, pos1.y-3f, 0, 0, 20, "d");
    }

    public bool IsTopJ2 = false;
    void GetTopJ2()
    {
        int nums = 2 + GlobalTools.GetRandomNum(4);
        Vector2 pos1 = topL2.position;
        Vector2 pos2 = topR2.position;
        SetJingByDistanceU("qju_1", nums, pos1, pos2, pos1.y - 3f, 0, 0, 20, "d");
    }





    public bool IsSCWu = false;
    //生成 粒子雾 
    void GetWus()
    {
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        //SetJingByDistanceU("liziWu_1", nums, pos1, pos2, pos1.y+1, 0, 0, 0, "u");
        //Color color1 = new Color(0.1f, 1f, 1f, 0.1f);
        //GetWu("", pos1, pos2, -30, color1);
        Color color2 = new Color(0.1f, 1f, 1f, 0.2f);
        GetWu("", pos1, pos2, -60, color2);


        List<string> liziArr = GetDateByName.GetInstance().GetListByName("liziWu_1", MapNames.GetInstance());
        SetLiziByNums(1, liziArr, pos1.x, pos2.x, pos1.y-1);
    }


    void SetLiziByNums(int nums, List<string> liziList, float _x1, float _x2, float _y)
    {
        for (var i = 0; i < nums; i++)
        {
            //print("---------->  "+jingNameTou);
            string jingName = liziList[GlobalTools.GetRandomNum(liziList.Count)];
            //print("-----------------> 啥啊  "+jingName);
            GameObject jing = GlobalTools.GetGameObjectByName(jingName);
            jing.transform.parent = GlobalTools.FindObjByName("maps").transform;
            //public static void SetLizi(GameObject jingObj, float _x1, float _x2, float _y, int i, int nums)
            GlobalTools.SetLizi(jing, _x1, _x2, _y, i, nums);
        }
    }





    void GetWu(string wuName, Vector2 qidian, Vector2 zhongdian, int SDOrder, Color color)
    {
        string _wuName = "wu_1_1";
        GameObject _wu = GlobalTools.GetGameObjectByName(_wuName);
        //补的雾 看后面的需求
        //GameObject _wu2 = GlobalTools.GetGameObjectByName(_wuName);
        _wu.transform.parent = GlobalTools.FindObjByName("maps").transform;
        float _w = GlobalTools.GetJingW(_wu);
        float _h = GlobalTools.GetJingH(_wu);
        float _w2 = Mathf.Abs(zhongdian.x - qidian.x);
        float _h2 = Mathf.Abs(zhongdian.y - qidian.y) + 5;

        //print(" 地板宽度     "+_w2);


        _wu.transform.localScale = new Vector3(_w2 / (_w + 0.6f), _h2 / _h * 3, 1);
        //print("  缩放 "+ _wu.transform.localScale);
        _wu.transform.position = new Vector2(qidian.x + _w2 * 0.5f + (_w2 - GlobalTools.GetJingW(_wu)) * 0.5f, zhongdian.y + GlobalTools.GetJingH(_wu) * 0.5f-0.3f);

        //print(">?????????????????????????????????????????????????????????????    "+ _w+"  >-缩放后  " +GlobalTools.GetJingW(_wu));

        GlobalTools.SetMapObjOrder(_wu, SDOrder);

        //print("雾的宽度  "+_w+"  _w2宽度  "+_w2+"   宽度缩放比例    "+_w2/_w+"   weizhi "+_wu.transform.position);
        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  "+_wu.transform.position+"   起点  "+qidian+"   终点 "+zhongdian);


        //改变雾的颜色
        _wu.GetComponent<SpriteRenderer>().color = color;// new Color(0.1f,1f,1f,0.5f);//new Color((129 / 255)f, (69 / 255)f, (69 / 255)f, (255 / 255)f); //Color.red;
        
    }



    //近前景
    void GetJQJ()
    {
        int nums = 9 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);
        //float _y = pos1.y - 1.5f;
        SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y -GlobalTools.GetRandomDistanceNums(1)   , -1f,0.5f,30, "u");
        //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");

        SetJingByDistanceU("qjd2_1", nums, pos1, pos2, pos1.y - 1.5f, -0.1f,0, 30, "u");

        SetJingByDistanceU("qjd3_1", nums, pos1, pos2, pos1.y - 1.6f, -0.1f, 0, 30, "u");

        nums = 1 + GlobalTools.GetRandomNum(1);
        SetJingByDistanceU("qyjd_1", nums, pos1, pos2, pos1.y - 2.2f, -6.3f, 1, 40, "u",2);
    }

    //近背景
    void GetLRJinBG()
    {
        int nums = 2 + GlobalTools.GetRandomNum(3);
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x,tl.position.y);
        SetJingByDistanceU("jjd_1",nums,pos1,pos2, pos1.y, 0,0,-15,"u");
    }

    //_cx 朝向  xzds 旋转度数
    void SetJingByDistanceU(string jinglistName,int nums,Vector2 pos1,Vector2 pos2,float _y,float _z,float _dz, int sd, string _cx,float xzds = 0)
    {
        List<string> strArr = GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        
        for(int i = 0; i < nums; i++)
        {
            string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
            GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
            jingObj.transform.parent = maps.transform;
            //大于宽度的景 直接删除了
            bool IsShu = false;
            if(jinglistName.Split('_')[0] != "shu")
            {
                if (IsDaYuDis(jingObj, pos1.x, pos2.x))
                {
                    Destroy(jingObj);
                    continue;
                }
            }
            else
            {
                IsShu = true;
            }
            
            GlobalTools.SetJingTY(jingObj, pos1.x, pos2.x, _y, _z, _dz, i, nums, xzds, sd,false, IsShu);
        }
    }



    GameObject GetJObjByListName(string jinglistName)
    {
        List<string> strArr = GetDateByName.GetInstance().GetListByName(jinglistName, MapNames.GetInstance());
        string objName = strArr[GlobalTools.GetRandomNum(strArr.Count)];
        GameObject jingObj = GlobalTools.GetGameObjectByName(objName);
        jingObj.transform.parent = maps.transform;
        return jingObj;
    }
    


    bool IsDaYuDis(GameObject obj,float _x1,float _x2) {
        if (GlobalTools.GetJingW(obj) > Mathf.Abs(_x2 - _x1))
        {
            return true;
        }
        return false;
    }

    //顶部景布置





    public virtual Vector2 GetRightPos()
    {
        return lianjiedianR.position;
    }

    public virtual Vector2 GetLeftPos()
    {
        return lianjiedianL.position;
    }


    public Vector2 GetUpPos()
    {
        return lianjiedianU.position;
    }


    public Vector2 GetDownPos()
    {
        return lianjiedianD.position;
    }


    //设置深度
    public virtual void SetSD(int sd)
    {
        if (diban1) diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd;
        if (diban2) diban2.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd+1;
        if (diban3) diban3.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 2;
        if (diban4) diban4.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder = sd + 3;
    }


    public virtual int GetSD()
    {
        return diban1.GetComponent<Ferr2DT_PathTerrain>().GetComponent<Renderer>().sortingOrder;
    }

    public virtual void SetLightColor()
    {
        if (light2d) {
            light2d.GetComponent<Light2D>().color = GlobalTools.RandomColor();
            light2d.GetComponent<Light2D>().intensity = 0.6f + GlobalTools.GetRandomDistanceNums(0.6f);
        }
        
    }

    public void SetLightColorByValue(Color _color)
    {
        if (light2d) light2d.GetComponent<Light2D>().color = _color;
    }


    public Color GetLightColor()
    {
        if (!light2d) return Color.white;
        return light2d.GetComponent<Light2D>().color;
    }
   

}
