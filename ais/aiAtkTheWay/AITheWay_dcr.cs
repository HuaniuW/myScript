using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AITheWay_dcr : MonoBehaviour {
    float theLive;
    // Use this for initialization
    void Start () {
        theLive = this.GetComponent<RoleDate>().live;
        GetInitAtkZsArr();
        //atkArrs = new Array[] { zsArr1, zsArr2, zsArr3 };
        //print("way----->   "+GetDicSFByName("zsArr1",this).Length);
    }
	
    void GetInitAtkZsArr()
    {
        if (zs_way.Count == 0|| zs_way.Count == 1)
        {
            atkArrs = new Array[] { zsArr1 };
            return;
        }
        GetNewZSListByCLive();

       // 特殊触发 强行 变招  如果 小于3 就等于3
    }

    public string[] zsArr1 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr2 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr3 = { "atk_1", "atk_2", "atk_3" };
    public string[] zsArr4 = { "atk_1", "atk_1", "atk_1" };
    public string[] zsArr5 = { "atk_1", "atk_1", "atk_1" };
    public string[] zsArr6 = { "atk_1", "atk_1", "atk_1" };

    Array[] atkArrs = new Array[] { };

   
    //做成动态的   不可逆 当回血后 不要返回之前的攻击
    int n = 0;//记录 不可以返回
    string _zsList = "";
    public List<string> zs_way = new List<string> { };

    public string[] GetZSArrays(int lie = 0)
    {
        if (zs_way.Count == 0 || zs_way.Count == 1) return zsArr1;
        //print("lie   "+lie+ "  atkArrs   "+ atkArrs.Length);

        string[] zss = (string[])atkArrs[lie];
        return zss;
    }

    [Header("血少于一半时候 显示的 特效")]
    public ParticleSystem LiveHalfShow;
    void ShowHalfTX()
    {
        if (LiveHalfShow&& LiveHalfShow.isStopped && theLive / this.GetComponent<RoleDate>().live >= 2)
        {
            LiveHalfShow.Play();
        }
    }

    void GetNewZSListByCLive()
    {
        float _xueliangN = theLive / zs_way.Count;

        //print("zs_way.Count    "+ zs_way.Count);

        ShowHalfTX();

        for (var i = 0; i < zs_way.Count; i++)
        {
            
            if (this.GetComponent<RoleDate>().live-1 <= _xueliangN * (zs_way.Count - i))
            {
                //print(this.GetComponent<RoleDate>().live - 1 + " i ---" + i + "-n- " + n + " ------  " + _xueliangN * (zs_way.Count - i));
                //print("jinlaimei");
                if (n <= i) n = i;
                //获取 招式数组列表
                _zsList = zs_way[n];
                //print("_zsList ******************************************  "+ _zsList);
            }
        }
        //通过招式数组列表 获取 招式数组
        if (_zsList != "")
        {
            List<string[]> TempList = new List<string[]> { };
            string[] tempArr = _zsList.Split('_');
            for (var s = 0; s < tempArr.Length; s++)
            {
                TempList.Add(this.GetDicSFByName("zsArr" + tempArr[s], this));
            }
            atkArrs = TempList.ToArray();
        }
        //print("atkArrs      "+ atkArrs.Length);
    }

   

    public int GetZSArrLength()
    {
        GetNewZSListByCLive();
        return atkArrs.Length;
    }

    // Update is called once per frame
    void Update () {
        //GetZSArrLength();
    }

    public string[] GetDicSFByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as string[];
    }
}
