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
    public List<RectTransform> geziArr = new List<RectTransform>();
    public List<string> hzIdList = new List<string>();
    void Start() {
        RectTransform[] t = { gezi1, gezi2 ,gezi3,gezi4,gezi5,gezi6, gezi7, gezi8, gezi9, gezi10 };
        geziArr.AddRange(t);
        //string[] t2 = { "huizhang1_0", "huizhang2_7" };
        //hzIdList.AddRange(t2);
        //print(geziArr.Count);
        getDate();
        GetInHZ();
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
