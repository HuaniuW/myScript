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
    public RectTransform kuang;
    public RectTransform xuanzhong;
    public List<RectTransform> geziArr = new List<RectTransform>();
    public List<string> hzIdList = new List<string>();
    List<RectTransform> HZzhuangbeizu = new List<RectTransform>();

    //被选中的物品
    RectTransform beChoseWP = null;
    void Start() {
        RectTransform[] HZzbz = { gezi7, gezi8, gezi9, gezi10 };
        HZzhuangbeizu.AddRange(HZzbz);

        RectTransform[] t = { gezi1, gezi2 ,gezi3,gezi4,gezi5,gezi6, gezi7, gezi8, gezi9, gezi10 };
        geziArr.AddRange(t);
        //string[] t2 = { "huizhang1_0", "huizhang2_7" };
        //hzIdList.AddRange(t2);
        //print(geziArr.Count);
        getDate();
        GetInHZ();
        xuanzhong.GetComponent<CanvasGroup>().alpha = 0;
        kuang.position = gezi1.position;
        getRQ = gezi1;
    }

   
    void GetInHZ()
    {
        for(var i = 0; i < hzIdList.Count; i++)
        {
            if (hzIdList[i] != "")
            {
                string hzName = hzIdList[i].Split('_')[0];
                int geziNum = int.Parse(hzIdList[i].Split('_')[1]);
                GameObject obj = Resources.Load(hzName) as GameObject;
                obj = Instantiate(obj);
                obj.transform.parent = this.transform;
                RectTransform hz = obj.GetComponent<RectTransform>();
                geziArr[geziNum].GetComponent<Gezi>().GetInObj(hz);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
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

    RectTransform choseObj = null;
    void GetChoseObj()
    {
        //查找该格子里面是否有物品 有的话就被选中了
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


    RectTransform getRQ = null;
    void FindNearestQR(string fx)
    {
        if (geziArr.Count == 0) return;
        float jl = 0;
        bool hasValue = false;
        getRQ = null;
        List<RectTransform> tempList = new List<RectTransform>();

        if (fx == "up")
        {
            //获取在我上方的容器list
            foreach(var rq in geziArr)
            {
                if (rq.transform.position.y > kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }else if (fx == "down")
        {
            foreach (var rq in geziArr)
            {
                if (rq.transform.position.y < kuang.transform.position.y)
                {
                    tempList.Add(rq);
                }
            }
        }else if (fx == "right")
        {
            foreach (var rq in geziArr)
            {
                if (rq.transform.position.x > kuang.transform.position.x)
                {
                    tempList.Add(rq);
                }
            }
        }else if (fx == "left")
        {
            foreach (var rq in geziArr)
            {
                if (rq.transform.position.x < kuang.transform.position.x)
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

    //获取到新的徽章
    public void GetNewHZ(string hzName)
    {
        GameObject obj = Resources.Load(hzName) as GameObject;
        obj = Instantiate(obj);
        obj.transform.parent = this.transform;
        RectTransform hz = obj.GetComponent<RectTransform>();
        for(var i = 0; i < geziArr.Count; i++)
        {
            if (geziArr[i].tag!="zhuangbeilan"&& geziArr[i].GetComponent<Gezi>().IsHasObj() == null)
            {
                geziArr[i].GetComponent<Gezi>().GetInObj(hz);
                return;
            }
        }
    }

    string saveDateStr;
    //存储数据
    public string saveDate()
    {
        saveDateStr = "";
        for (var i = 0; i < geziArr.Count; i++)
        {
            if (geziArr[i].GetComponent<Gezi>().IsHasObj() != null) {
                saveDateStr += (geziArr[i].GetComponent<Gezi>().IsHasObj().name + "_" + i);
                if (i != geziArr.Count - 1) saveDateStr += "-";
            }
        }
        return saveDateStr;
    }

    string[] getDateStrArr = { };
    //获取并分解数据
    public void getDate()
    {
        string tempDateStr = "huizhang1_0-huizhang2_2";
        getDateStrArr = tempDateStr.Split('-');
        hzIdList.AddRange(getDateStrArr);
    }
}
