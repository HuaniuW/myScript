﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public AudioSource cuowu;
    public AudioSource chose;
    public AudioSource XuanZhong;

    public Text HZ_information;

    public Text Player_information;

    [Header("显示 徽章的 图片")]
    public Image HZ_img;

    //被选中的物品
    RectTransform beChoseWP = null;
    void Start() {
        //print("************************************************************************************************************************************************************************************");
        //print("************************************************************************************************************************************************************************************");
        //装备徽章格子+技能徽章格子
        RectTransform[] HZzbz = { gezi21, gezi22, gezi23, gezi24, gezi25, gezi26, gezi27 };
        HZzhuangbeizu.AddRange(HZzbz);
        //技能徽章格子（主动技能）
        RectTransform[] HZzdjn = {gezi26, gezi27 };
        HZzhudongjineng.AddRange(HZzdjn);
        //所有格子
        //RectTransform[] t = { gezi1, gezi2 ,gezi3,gezi4,gezi5,gezi6, gezi7, gezi8, gezi9, gezi10, gezi11, gezi12, gezi13, gezi14, gezi15, gezi16, gezi17, gezi18, gezi19, gezi20, gezi21, gezi22, gezi23, gezi24, gezi25, gezi26, gezi27 };
        RectTransform[] t = { gezi1, gezi2, gezi3, gezi4, gezi5, gezi6, gezi7, gezi8, gezi9, gezi10, gezi11, gezi12, gezi13, gezi14, gezi15, gezi16, gezi17, gezi18, gezi19, gezi20, gezi21, gezi22, gezi23,  gezi26};
        geziArr.AddRange(t);
        //测试用
        //string[] t2 = { "huizhang4_1", "huizhang5_6","huizhang1_8", "huizhang2_9" };
        //hzIdList.AddRange(t2);

        //print(geziArr.Count);
        HZGetDate();
        GetInHZ();
        xuanzhong.GetComponent<CanvasGroup>().alpha = 0;
        kuang.position = gezi1.position;
        getRQ = gezi1;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_OBJ_NAME, this.GetObjByName);
        //初始化
        GetInit();
        getRQ = gezi1;
        GetHZInformation();
        if (HZ_information != null) HZ_information.text = "哈哈哈哈哈 \n 我去";

        //print("  ------------------------------------------------------>面板1    "+ HZSaveDate());
        //发送 事件重新 写角色数据
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.PLAYER_ZT), this.gameObject);
        print(" ************背包 事件 发送");
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_OBJ_NAME, this.GetObjByName);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BAG_OPEN, this.BagOpen);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HZ, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JIAXUE, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANEG_LIVE, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ZUZHOU, this.ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.HZ_TOUCH, ClickGetHZInformation);
    }

    void GetInit()
    {
        if (Globals.isDebug) print("徽章栏初始化   2019-4-16  这个print没写 这里居然不执行  写了后就开始执行了。。。。 （我都开始怀疑世界的真实性了）  。。。。。极思细恐 ");
        
        //初始化 获取 角色背包加成属性数据
        List<RectTransform> HZs = GetInHZListHZ();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ, HZs), this);

        List<RectTransform> HZs2 = GetZDJNList();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZD_SKILL, HZs2), this);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BAG_OPEN,this.BagOpen);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HZ, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JIAXUE, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANEG_LIVE, ShowPlayerInformation);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ZUZHOU, this.ShowPlayerInformation);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.HZ_TOUCH, ClickGetHZInformation);

    }

    void BagOpen(UEvent e)
    {
        GetHZInformation();
        ShowPlayerInformation();
        GlobalTools.FindObjByName(GlobalTag.PlayerObj).GetComponent<GameBody>().GetStand();
    }

   //将背包数据 物品放入背包格子
    void GetInHZ()
    {
        //print("  将背包数据 物品放入背包格子   "+ hzIdList.Count);
        for(var i = 0; i < hzIdList.Count; i++)
        {
            if (hzIdList[i] != "")
            {
                print("??  "+i+"  --   "+ hzIdList[i]);
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
            HZSaveDate();
        }
    }

    void GetObjByNameInGezi(string ObjName, RectTransform gz)
    {
        print("###ObjName   "+ ObjName);
        //GameObject obj = Resources.Load(ObjName) as GameObject;
        //obj = Instantiate(obj);
        GameObject obj = GlobalTools.GetGameObjectByName(ObjName);
        //obj.transform.parent = this.transform;
        obj.transform.SetParent(this.transform);
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
        //print("getRQ   "+ getRQ);
        if (getRQ == null) return;
        if (choseObj == null)
        {
            choseObj = getRQ.GetComponent<Gezi>().IsHasObj();
            if (choseObj != null)
            {
                xuanzhong.GetComponent<CanvasGroup>().alpha = 1;
                xuanzhong.position = kuang.position;
                XuanZhong.Play();
            }
        }
        else
        {
            if (choseObj.GetComponent<MyDrag>().OldRQ.name == getRQ.name) return;
            //被动 主动
            //if (choseObj.GetComponent<HZDate>().type == "bd" && getRQ.tag == "JN_zhuangbeilan") return;
            //if (choseObj.GetComponent<HZDate>().type == "zd" && getRQ.tag == "zhuangbeilan") return;
            RectTransform newObj = getRQ.GetComponent<Gezi>().IsHasObj();
            if (newObj == null)
            {
                if (getRQ.GetComponent<Gezi>().IsOpen)
                {
                    //空格子 放上去
                    getRQ.GetComponent<Gezi>().GetInObj(choseObj);
                    XuanZhong.Play();
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
                XuanZhong.Play();
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
            HZs.Add(hz);
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
    public string HZSaveDate()
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
        
        //GlobalSetDate.instance.CurrentMapMsgDate.bagDate = saveDateStr;
        //GlobalDateControl.SaveMapDate();
        if (Globals.isDebug) print("保存 徽章信息---saveDateStr  " + GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
        return saveDateStr;
    }

    string[] getDateStrArr = { };
    //获取并分解数据
    public void HZGetDate()
    {
        //print("  获取徽章》>  ");
        if (GlobalSetDate.instance.CurrentMapMsgDate == null) return;
        //print("  游戏 开始 获取 背包 徽章数据！   "+ GlobalSetDate.instance.CurrentMapMsgDate.bagDate);
        print("  游戏 开始 获取 背包 徽章数据！CurrentUserDate  " + GlobalSetDate.instance.CurrentUserDate.bagDate);
        string tempDateStr = GlobalSetDate.instance.CurrentMapMsgDate.bagDate;// "huizhang1_0-huizhang2_2";
        print("tempDateStr>     " + tempDateStr);
        getDateStrArr = tempDateStr.Split('-');
        hzIdList.AddRange(getDateStrArr);
        //hzIdList.Add(getDateStrArr);
        if (Globals.isDebug) {
            //print("hzIdList list长度 " + hzIdList.Count);
        } 

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
                if (Mathf.Abs(rq.transform.position.x-kuang.transform.position.x)< wcjl && (int)rq.transform.position.y > (int)kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "down")
        {
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.x - kuang.transform.position.x) < wcjl && (int)rq.transform.position.y < (int)kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "right")
        {
            foreach (var rq in geziArr)
            {
                //Mathf.Abs(rq.transform.position.y - kuang.transform.position.y) < wcjl &&
                if ((int)rq.transform.position.x > (int)kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }
        else if (fx == "left")
        {
            foreach (var rq in geziArr)
            {
                if (Mathf.Abs(rq.transform.position.y - kuang.transform.position.y) < wcjl && (int)rq.transform.position.x < (int)kuang.transform.position.x)
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

        if (getRQ != null) {
            kuang.position = getRQ.position;
            GetHZInformation();
            GlobalTools.PlayAudio("chose", this);
        }
    }

    //获取徽章信息
    void GetHZInformation()
    {
        if (!getRQ) return; 
        if (getRQ.GetComponent<Gezi>().IsHasObj())
        {
            print("徽章名字： " + getRQ.GetComponent<Gezi>().IsHasObj().GetComponent<HZDate>().HZName);

            HZ_information.text = getRQ.GetComponent<Gezi>().IsHasObj().GetComponent<HZDate>().GetHZ_information_str();
            //HZ_img.overrideSprite = getRQ.GetComponent<Gezi>().IsHasObj().GetComponent<HZDate>().GetComponent<SpriteRenderer>().sprite;
            StartShowBar("img_"+getRQ.GetComponent<Gezi>().IsHasObj().GetComponent<HZDate>().objName);
        }
        else
        {
            HZ_information.text = "";
        }
    }

    void StartShowBar(string ImgName)
    {
        //Sprite sp = Resources.Load("i_huizhang12", typeof(Sprite)) as Sprite;  这个用不了 无法被加载 只能用 GameObject来加载

        GameObject sp = Resources.Load(ImgName) as GameObject;
        HZ_img.overrideSprite = sp.GetComponent<SpriteRenderer>().sprite;
    }

    public void ClickGetHZInformation(UEvent e)
    {
        HZDate _hzDate = e.eventParams as HZDate;
        HZ_information.text = _hzDate.GetHZ_information_str();

    }

    void ShowPlayerInformation(UEvent e = null)
    {
        Player_information.text = GlobalTools.FindObjByName("player").GetComponent<PlayerRoleDate>().GetPlayerMsg();
        print(" 调用了 背包数据 是什么   "+ Player_information.text);
    }


    public void PlaySoundByName(string sName)
    {
        GlobalTools.PlayAudio(sName, this);
    }

    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    float horizontalDirection;
    float verticalDirection;

    bool IsYD = false;


    void Update()
    {

        if (this.transform.parent.GetComponent<CanvasGroup>().alpha == 0) return;

        //左右
        horizontalDirection = Input.GetAxis(HORIZONTAL);
        //上下
        verticalDirection = Input.GetAxis(VERTICAL);

        if ((horizontalDirection>-0.6f&& horizontalDirection<0.6f)&& (verticalDirection > -0.6f && verticalDirection < 0.6f))
        {
            IsYD = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)||(!IsYD&& verticalDirection>0.6f))
        {
            IsYD = true;
            FindNearestQR("up");
        }else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (!IsYD && verticalDirection < -0.6f))
        {
            IsYD = true;
            FindNearestQR("down");
        }else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (!IsYD && horizontalDirection < - 0.6f))
        {
            IsYD = true;
            FindNearestQR("left");

        }else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (!IsYD && horizontalDirection > 0.6f))
        {
            IsYD = true;
            FindNearestQR("right");

        }else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            //print("enter");
            GetChoseObj();
        }


        //if (Input.anyKey)
        //{//得到按下什么键
        //    print("anyKey  " + Input.inputString);
        //}




    }
}
