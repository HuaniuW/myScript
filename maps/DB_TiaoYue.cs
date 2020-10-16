using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_TiaoYue : DBBase
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GlobalSetDate.instance.IsCMapHasCreated) {

            if(Globals.mapType == "daogua")
            {
                //倒挂
            }
            else
            {
                SetDBPos();
                SetZiDanJG();
            }
            SetLightColor();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    bool IsHasSetDB = false;
    void SetDBPos()
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

        float _x = _x1 + GlobalTools.GetRandomDistanceNums(Mathf.Abs(_x2-_x1));
        float _y = tl.position.y-4+ GlobalTools.GetRandomDistanceNums(8);

        dibanD.transform.position = new Vector3(_x, _y, 0);
        dibanD.transform.parent = maps.transform;

        //if (IsPenSheZiDanJG) GetPenSheZiDanJG();

    }

    bool IsHasSetZDJG = false;
    void SetZiDanJG()
    {
        IsHasSetZDJG = true;
        if (IsPenSheZiDanJG) SetPenSheZiDan();
    }

    bool IsPenSheZiDanJG = false;
    public void JiGuan_PenSheZiDanJG()
    {
        if (GlobalTools.GetRandomNum() > 100)
        {
            IsPenSheZiDanJG = true;
            if (IsHasSetZDJG)
            {
                //说明已经先执行了
                SetPenSheZiDan();
            }
        }
    }


    bool IsHasSetZiDan = false;
    public void SetPenSheZiDan()
    {
        if (IsHasSetZiDan) return;
        IsHasSetZiDan = true;
        //print("************************************************************************************************************JG_zidanPenSheUP");
        GameObject JG_zidanUp = GlobalTools.GetGameObjectByName("JG_zidanPenSheUP");
        float __x = rd.position.x - 1;
        float __y = tl.position.y - 8f;
        JG_zidanUp.transform.position = new Vector2(__x,__y);
        JG_zidanUp.transform.parent = maps.transform;
    }


    //创建 一个地板

}
