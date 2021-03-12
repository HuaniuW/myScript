using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Yun : DB_TiaoYue
{
    [Header("背景墙的 显示位置")]
    public Transform BGWallPos;


    // Start is called before the first frame update
    void Start()
    {
        InitStart();
        if (!GlobalSetDate.instance.IsCMapHasCreated && !IsDangBan)
        {
            print("jinlaimie??");
            if (!IsShowDingDB)
            {
                //隐藏顶部地板
                HideDingDB();
                //显示 远背景
                GetYBJ();
            }
            else
            {
                print("&&&&&&&&&&&&&&&&&&&&&&&&&&  设置 顶地板位置");
                SetDingDBPos();
                GetBGWall();
            }


            OtherStart();
            SetDBPos();
            SetZiDanJG();
            GetJQJ();
        }

        print("?????????  地板云");
       
    }

    void GetBGWall()
    {
        string _bgWallName = "BGWall_1";
        GameObject bgWall = GlobalTools.GetGameObjectByName(_bgWallName);
        if (bgWall != null) bgWall.transform.position = BGWallPos.transform.position;
        if (maps != null) bgWall.transform.parent = maps.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //是否有 顶 和背墙
    public override void ShowDingDB(float __posY = 0, float __posX = 0)
    {

        if (!dibanDing.activeSelf)
        {
            dibanDing.SetActive(true);
        }

        if (dibanDing.activeSelf) IsShowDingDB = true;

        __dingDBPosX = __posX;
        float wuchaY = GlobalTools.GetRandomDistanceNums(2);
        __dingDBPosY = __posY - wuchaY;

        //SetDingDBPos();
        //print("顶部景控制*****   " + dibanDing.activeSelf);
        //生成 顶部时候 不许出树 或者 大概率不许出树
        //dibanDing.SetActive(true);

    }

    //顶部地板的 倒挂景
    protected override void DingDBJing()
    {
        //print("顶部景控制");
        //是否有什么背景？？？
        GetTopJ3();
        GetTopJ4();


        //if (IsShowDingDB)
        //{
        //    //生成喷火机关
        //    JiGuan_Penghuo();
        //}

    }

    protected override void GetTopJ3()
    {
        string qjuArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju");
        if (qjuArrName == "") return;
        int nums = 4 + GlobalTools.GetRandomNum(4);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //print("  ??>>>>>>>>>**qjuArrName   " + qjuArrName+"   pos  "+ DingDBPosL.transform.position);
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qjuArrName, nums, pos1, pos2, pos1.y - 2, 0, 0, 80, "d");
    }

    protected override void GetTopJ4()
    {
        string qju2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qju2");
        if (qju2ArrName == "") return;
        int nums = 4 + GlobalTools.GetRandomNum(2);
        //DingDBPosL.transform.parent = GlobalTools.FindObjByName("maps").transform;
        //DingDBPosR.transform.parent = GlobalTools.FindObjByName("maps").transform;
        Vector2 pos1 = DingDBPosL.position;
        Vector2 pos2 = DingDBPosR.position;
        SetJingByDistanceU(qju2ArrName, nums, pos1, pos2, pos1.y - 1.5f, -0.3f, 0, 90, "d");
    }

    //是否有远背景

    //前景

    //近前景
    protected override void GetJQJ()
    {
        int nums = 0;
        Vector2 pos1 = tl.position;
        Vector2 pos2 = new Vector2(rd.position.x, tl.position.y);

        if (Globals.mapTypeNums == 1)
        {
            nums = 8 + GlobalTools.GetRandomNum(4);
            //float _y = pos1.y - 1.5f;
            string qjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd");
            if (qjdArrName != "") SetJingByDistanceU(qjdArrName, nums, pos1, pos2, pos1.y - 3.9f - GlobalTools.GetRandomDistanceNums(1), -0.5f, 0.1f, 40, "u");
            //SetJingByDistanceU("qjd_1", nums, pos1, pos2, pos1.y, -4f, -1f, 40, "u");
            //string qjd2ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd2");
            //if (qjd2ArrName != "") SetJingByDistanceU(qjd2ArrName, nums, pos1, pos2, pos1.y - 4.6f, -0.3f, 0, 40, "u");

            //string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            //if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 3.6f, -0.4f, 0, 30, "u");

            //string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            //nums = 1 + GlobalTools.GetRandomNum(1);
            //if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 3.2f, -0.6f, 1, 40, "u", 2);
        }
        else if (Globals.mapTypeNums == 2)
        {
            nums = 1 + GlobalTools.GetRandomNum(1);
            string qjd3ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd3");
            if (qjd3ArrName != "") SetJingByDistanceU(qjd3ArrName, nums, pos1, pos2, pos1.y - 2f, -0.6f, 1.2f, 30, "u");

            string qjd5ArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd5");
            nums = 1 + GlobalTools.GetRandomNum(2);
            if (qjd5ArrName != "") SetJingByDistanceU(qjd5ArrName, nums, pos1, pos2, pos1.y - 2f, -0.2f, 0.6f, 35, "u", 2);

            string qyjdArrName = MapNames.GetInstance().GetJingArrNameByGKKey("qjd4");
            nums = 1 + GlobalTools.GetRandomNum(1);
            if (qyjdArrName != "") SetJingByDistanceU(qyjdArrName, nums, pos1, pos2, pos1.y - 2.9f, -1.6f, 1, 50, "u", 2);
        }
    }



    // 1-2个地板？ Y增高
    int DBNums = 1;

    protected override void SetDBPos()
    {
        if (IsHasSetDB) return;
        IsHasSetDB = true;

        if (maps == null) maps = GlobalTools.FindObjByName("maps");
        if (maps == null) return;
        //print("*******************************************跳跃地板");
        string tiaoyuediban = "tiaoyueDBD_" + Globals.mapTypeNums;
        List<string> tiaoyuedibanDArr = GetDateByName.GetInstance().GetListByName(tiaoyuediban, MapNames.GetInstance());
        GameObject dibanD = GlobalTools.GetGameObjectByName(tiaoyuedibanDArr[GlobalTools.GetRandomNum(tiaoyuedibanDArr.Count)]);
        float _x1 = tl.position.x + 5;
        float _x2 = rd.position.x - 5;

        float _x = _x1 + GlobalTools.GetRandomDistanceNums(Mathf.Abs(_x2 - _x1));
        float _y = tl.position.y-0.6f  + GlobalTools.GetRandomDistanceNums(3);

        //判断 是单地板 还是双地板
        DBNums = GlobalTools.GetRandomNum() > 50 ? 1 : 2;

        if(DBNums == 2)
        {
            _x =tl.position.x+ GetWidth()* 0.33f;
        }

        dibanD.transform.position = new Vector3(_x, _y, 0);
        dibanD.transform.parent = maps.transform;


        if(DBNums == 2)
        {
            GameObject dibanD2 = GlobalTools.GetGameObjectByName(tiaoyuedibanDArr[GlobalTools.GetRandomNum(tiaoyuedibanDArr.Count)]);
            _x = _x = tl.position.x + GetWidth() * 0.66f;
            _y = tl.position.y-1  + GlobalTools.GetRandomDistanceNums(3);
            dibanD2.transform.position = new Vector3(_x, _y, 0);
            dibanD2.transform.parent = maps.transform;
        }

        //if (IsPenSheZiDanJG) GetPenSheZiDanJG();

    }

    protected override void SetZiDanJG()
    {
        IsHasSetZDJG = true;
        if (IsPenSheZiDanJG) SetPenSheZiDan();
        //SetPenSheZiDan();
    }

    public override void SetPenSheZiDan()
    {
        //判断有几个 地板  有2个地板的话 放中间

        if (IsHasSetZiDan) return;
        IsHasSetZiDan = true;
        //print("************************************************************************************************************JG_zidanPenSheUP");
        GameObject JG_zidanUp = GlobalTools.GetGameObjectByName("JG_zidanPenSheUP");
        float __x = rd.position.x - 1;
        if(DBNums == 2)
        {
            __x = tl.position.x + GetWidth() * 0.5f;
        }

        float __y = tl.position.y - 8f;
        JG_zidanUp.transform.position = new Vector2(__x, __y);
        JG_zidanUp.transform.parent = maps.transform;
    }

}
