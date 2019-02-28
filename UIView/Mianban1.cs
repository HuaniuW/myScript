using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mianban1 : MonoBehaviour {
    public RectTransform gezi1;
    public RectTransform gezi2;
    public RectTransform gezi3;
    public RectTransform gezi4;
    public RectTransform gezi5;
    public RectTransform gezi6;
    public RectTransform gezi7;
    public RectTransform gezi8;
    public RectTransform gezi9;
    public RectTransform gezi10;
    public RectTransform gezi11;
    public RectTransform gezi12;
    public RectTransform gezi13;
    public RectTransform gezi14;
    public RectTransform gezi15;
    public RectTransform gezi16;
    public RectTransform gezi17;
    public RectTransform gezi18;
    public RectTransform gezi19;
    public RectTransform gezi20;
    public RectTransform gezi21;
    public RectTransform gezi22;
    public RectTransform gezi23;
    public RectTransform gezi24;
    public RectTransform gezi25;
    public RectTransform gezi26;
    public RectTransform gezi27;
    public RectTransform kuang;
    public RectTransform xuanzhong;
    public List<RectTransform> geziArr = new List<RectTransform>();
    public List<string> hzIdList = new List<string>();
    List<RectTransform> HZzhuangbeizu = new List<RectTransform>();
    List<RectTransform> HZzhudongjineng = new List<RectTransform>();

    //被选中的物品
    RectTransform beChoseWP = null;
    void Start() {
        RectTransform[] HZzbz = { gezi21, gezi22, gezi23, gezi24, gezi25, gezi26, gezi27 };
        HZzhuangbeizu.AddRange(HZzbz);

        RectTransform[] HZzdjn = {gezi26, gezi27 };
        HZzhudongjineng.AddRange(HZzdjn);

        RectTransform[] t = { gezi1, gezi2 ,gezi3,gezi4,gezi5,gezi6, gezi7, gezi8, gezi9, gezi10, gezi11, gezi12, gezi13, gezi14, gezi15, gezi16, gezi17, gezi18, gezi19, gezi20, gezi21, gezi22, gezi23, gezi24, gezi25, gezi26, gezi27 };
        geziArr.AddRange(t);
        string[] t2 = { "huizhang4_1", "huizhang5_6","huizhang1_8", "huizhang2_9" };
        hzIdList.AddRange(t2);
        //print(geziArr.Count);
        getDate();
        GetInHZ();
        xuanzhong.GetComponent<CanvasGroup>().alpha = 0;
        kuang.position = gezi1.position;
        getRQ = gezi1;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_OBJ_NAME, this.GetObjByName);
        //初始化
        GetInit();
    }

    void GetInit()
    {
        //初始化 获取 角色背包加成属性数据
        List<RectTransform> HZs = GetInHZListHZ();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ, HZs), this);

        List<RectTransform> HZs2 = GetZDJNList();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ, HZs), this);


    }

   //将背包数据 物品放入背包格子
    void GetInHZ()
    {
        for(var i = 0; i < hzIdList.Count; i++)
        {
            if (hzIdList[i] != "")
            {
                string hzName = hzIdList[i].Split('_')[0];
                int geziNum = int.Parse(hzIdList[i].Split('_')[1]);
                GetObjByNameInGezi(hzName, geziArr[geziNum]);
            }
        }
    }
    
    //玩家捡到新物品 事件调用
    void GetObjByName(UEvent e)
    {
        //找个空位 装进去 然后在生成全部数据 提交到全局数据
        //找出最近的 物品栏空位
        RectTransform gz = GetNearGezi();
        if (gz != null) {
            GetObjByNameInGezi(e.eventParams.ToString(), gz);
            //生成全局数据
            saveDate();
        }
    }

    void GetObjByNameInGezi(string ObjName, RectTransform gz)
    {
        //GameObject obj = Resources.Load(ObjName) as GameObject;
        //obj = Instantiate(obj);
        GameObject obj = GlobalTools.GetGameObjectByName(ObjName);
        obj.transform.parent = this.transform;
        RectTransform hz = obj.GetComponent<RectTransform>();
        gz.GetComponent<Gezi>().GetInObj(hz);
    }

    //寻找空格子
    RectTransform GetNearGezi()
    {
        foreach(var gz in geziArr)
        {
            if (gz.GetComponent<Gezi>().IsHasObj() == null&& gz.tag != "zhuangbeilan") return gz;
        }
        return null;
    }

 
	
	// Update is called once per frame
	

    RectTransform choseObj = null;
    void GetChoseObj()
    {
        //查找该格子里面是否有物品 有的话就被选中了
        print("getRQ   "+ getRQ);
        if (getRQ == null) return;
        if (choseObj == null)
        {
            choseObj = getRQ.GetComponent<Gezi>().IsHasObj();
            if (choseObj != null)
            {
                xuanzhong.GetComponent<CanvasGroup>().alpha = 1;
                xuanzhong.position = kuang.position;
            }
        }
        else
        {
            if (choseObj.GetComponent<MyDrag>().OldRQ.name == getRQ.name) return;
            if (choseObj.GetComponent<HZDate>().type == "bd" && getRQ.tag == "JN_zhuangbeilan") return;
            if (choseObj.GetComponent<HZDate>().type == "zd" && getRQ.tag == "zhuangbeilan") return;
            RectTransform newObj = getRQ.GetComponent<Gezi>().IsHasObj();
            if (newObj == null)
            {
                if (getRQ.GetComponent<Gezi>().IsOpen)
                {
                    //空格子 放上去
                    getRQ.GetComponent<Gezi>().GetInObj(choseObj);
                }
               
            }
            else
            {
                RectTransform OldRQ = choseObj.GetComponent<MyDrag>().OldRQ;
                if (OldRQ != null)
                {
                    OldRQ.GetComponent<Gezi>().GetInObj(newObj, false, true);
                }
                //有物品的格子 交换物品
                getRQ.GetComponent<Gezi>().GetInObj(choseObj,true);
            }
            choseObj = null;
            xuanzhong.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    //获取徽章装备栏里面的徽章
    public List<RectTransform> GetInHZListHZ()
    {
        //获取徽章栏里面的 徽章
        List<RectTransform> HZs = new List<RectTransform>();
        foreach(var gezi in HZzhuangbeizu)
        {
            RectTransform hz = gezi.GetComponent<Gezi>().IsHasObj();
            if (hz!=null)
            {
                HZs.Add(hz);
            }
        }
        return HZs;
    }

    //获取主动技能里面的徽章
    public List<RectTransform> GetZDJNList()
    {
        //获取徽章栏里面的 徽章
        List<RectTransform> HZs = new List<RectTransform>();
        foreach (var gezi in HZzhudongjineng)
        {
            RectTransform hz = gezi.GetComponent<Gezi>().IsHasObj();
            if (hz != null)
            {
                HZs.Add(hz);
            }
        }
        return HZs;
    }




    //获取到新的徽章 并装进格子
    //public void GetNewHZ(string hzName)
    //{
    //    GameObject obj = Resources.Load(hzName) as GameObject;
    //    obj = Instantiate(obj);
    //    obj.transform.parent = this.transform;
    //    RectTransform hz = obj.GetComponent<RectTransform>();
    //    for(var i = 0; i < geziArr.Count; i++)
    //    {
    //        if (geziArr[i].tag!="zhuangbeilan"&& geziArr[i].GetComponent<Gezi>().IsHasObj() == null)
    //        {
    //            geziArr[i].GetComponent<Gezi>().GetInObj(hz);
    //            return;
    //        }
    //    }
    //}

    string saveDateStr;
    //存储数据
    public string saveDate()
    {
        saveDateStr = "";
        for (var i = 0; i < geziArr.Count; i++)
        {
            if (geziArr[i].GetComponent<Gezi>().IsHasObj() != null) {
                string objName = geziArr[i].GetComponent<Gezi>().IsHasObj().name;
                objName = objName.Split('(')[0];
                saveDateStr += (objName + "_" + i);
                if (i != geziArr.Count - 1) saveDateStr += "-";
            }
        }
        
        GlobalSetDate.instance.CurrentUserDate.bagDate = saveDateStr;
        if (Globals.isDebug) print("---saveDateStr  " + GlobalSetDate.instance.CurrentUserDate.bagDate);
        return saveDateStr;
    }

    string[] getDateStrArr = { };
    //获取并分解数据
    public void getDate()
    {
        if (GlobalSetDate.instance.CurrentUserDate == null) return;
        string tempDateStr = GlobalSetDate.instance.CurrentUserDate.bagDate;// "huizhang1_0-huizhang2_2";
        getDateStrArr = tempDateStr.Split('-');
        hzIdList.AddRange(getDateStrArr);
    }















    RectTransform getRQ = null;
    void FindNearestQR(string fx)
    {
        if (geziArr.Count == 0) return;
        float jl = 0;
        bool hasValue = false;
        float wcjl = 15;
        getRQ = null;
        List<RectTransform> tempList = new List<RectTransform>();

        if (fx == "up")
        {
            //获取在我上方的容器list
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.x-kuang.transform.position.x)< wcjl && rq.transform.position.y > kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.x - kuang.transform.position.x) < wcjl && rq.transform.position.y < kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.y - kuang.transform.position.y) < wcjl && rq.transform.position.x > kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.y - kuang.transform.position.y) < wcjl && rq.transform.position.x < kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }

        if (tempList.Count > 0)
        {
            foreach (var rq2 in tempList)
            {
                float jl2 = Vector2.Distance(rq2.position, kuang.transform.position);
                if (hasValue)
                {
                    if (jl2 < jl)
                    {
                        jl = jl2;
                        getRQ = rq2;
                    }

                }
                else
                {
                    jl = jl2;
                    getRQ = rq2;
                    hasValue = true;
                }
            }
        }

        if (getRQ != null) kuang.position = getRQ.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindNearestQR("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindNearestQR("down");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindNearestQR("left");

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindNearestQR("right");

        }
        //if (Input.anyKey)
        //{//得到按下什么键
        //    print("anyKey  " + Input.inputString);
        //}



        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //print("enter");
            GetChoseObj();
        }
    }
}
