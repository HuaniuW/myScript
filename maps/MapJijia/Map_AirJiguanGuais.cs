using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_AirJiguanGuais : MonoBehaviour
{
    [Header("顶部 点")]
    public Transform TopPos;
    [Header("底部 点")]
    public Transform DownPos;


    [Header("后方导弹名字")]
    public List<string> HoufangDaodan = new List<string> { };

   
    JiguanJishi _houfangDaodanJishi;
    bool IsStopFasheHoufangDaodan = false;
    public void StopFasheHoufangDaodan(bool IsTrue = true)
    {
        IsStopFasheHoufangDaodan = IsTrue;
    }
    //发射 后方 导弹
    public void FasheHoufangDaodan()
    {
        if (IsStopFasheHoufangDaodan) return;
        if (_houfangDaodanJishi != null)
        {
            if (_houfangDaodanJishi.IsJishiOver())
            {
                print(" 计时完成！！！！！！！ ");
                int rNums = GlobalTools.GetRandomNum();
                if (rNums < 30)
                {
                    FasheHoufangDaodan(5);
                }
                else if (rNums < 60)
                {
                    FasheHoufangDaodan(2+GlobalTools.GetRandomNum(4));
                }
                else
                {
                    FasheHoufangDaodan(2);
                }
            }
        }
    }

    protected GameObject palyerJijis;

    [Header("机关 飞机 导弹 Z位置")]
    float JiguanZ = 10;

    void FasheHoufangDaodan(int DDNums)
    {
        float DestanceY = Mathf.Abs(TopPos.position.y - DownPos.position.y) / (DDNums+1);
        for (int i =0;i<DDNums;i++)
        {
            string DDName = HoufangDaodan[GlobalTools.GetRandomNum(HoufangDaodan.Count)];
            GameObject daodan = GlobalTools.GetGameObjectInObjPoolByName(DDName);
            daodan.name = DDName;
            daodan.transform.position = new Vector3(palyerJijis.transform.position.x-36-GlobalTools.GetRandomDistanceNums(6),TopPos.position.y- DestanceY*(i+1)+5-GlobalTools.GetRandomDistanceNums(10), JiguanZ);
            daodan.transform.parent = this.transform.parent;
        }
    }


    bool IsStopQianfangKonglei = false;
    public void StopQianfangKonglei(bool IsTrue = true)
    {
        IsStopQianfangKonglei = IsTrue;
    }

    //空雷 出现 计时
    JiguanJishi _Kongleiishi;

    //前方 空中地雷
    public void QianfangKonglei()
    {
        if (IsStopQianfangKonglei) return;
        if (_Kongleiishi != null)
        {
            if (_Kongleiishi.IsJishiOver())
            {
                int rNums = GlobalTools.GetRandomNum();
                if (rNums < 20)
                {
                    ShowKonglei(5);
                    ShowKonglei(5,100);
                    ShowKonglei(5, 200);
                }
                else if (rNums < 40)
                {
                    ShowKonglei(2 + GlobalTools.GetRandomNum(4));
                }
                else
                {
                    ShowKonglei(2);
                }
            }
        }
    }


    void ShowKonglei(int DDNums,float dx = 0)
    {
        float DestanceY = Mathf.Abs(TopPos.position.y - DownPos.position.y) / (DDNums + 1);
        for (int i = 0; i < DDNums; i++)
        {
            string DDName = "JG_Konglei";
            GameObject lei = GlobalTools.GetGameObjectInObjPoolByName(DDName);
            lei.name = DDName;
            lei.transform.position = new Vector3(palyerJijis.transform.position.x + 166+dx + GlobalTools.GetRandomDistanceNums(36), TopPos.position.y - DestanceY * (i + 1) + 5 - GlobalTools.GetRandomDistanceNums(10), JiguanZ);
            lei.transform.parent = this.transform.parent;
        }
    }






    JiguanJishi _bianjieKonglei;
    //前方 飞行器1
    bool IsStopBianjieKonglei1 = false;
    public void StopBianjieKonglei(bool IsTrue = true)
    {
        IsStopBianjieKonglei1 = IsTrue;
    }


    bool IsBianjieKongleiJishi = false;
    public void BianjieKongleiJishi()
    {
        if (!IsBianjieKongleiJishi)
        {
            IsBianjieKongleiJishi = true;
            if (_bianjieKonglei == null) _bianjieKonglei = new JiguanJishi();
            _bianjieKonglei.SetJishiValue(5, 2);
            print("   边界 空雷！！！！！！！！ ");
        }
    }


    public void BianjieKonglei()
    {
        if (IsStopBianjieKonglei1) return;
        if (_bianjieKonglei != null)
        {
            if (_bianjieKonglei.IsJishiOver())
            {
                BianjieKongleis();
            }
        }
    }




    void BianjieKongleis()
    {
        GetQianlei(10 + GlobalTools.GetRandomDistanceNums(20), 1);
        GetQianlei(GlobalTools.GetRandomDistanceNums(20), 1);
        GetQianlei(10 + GlobalTools.GetRandomDistanceNums(20), 1,false);
        GetQianlei(GlobalTools.GetRandomDistanceNums(20), 1,false);
    }



    void GetQianlei(float dx,float dy,bool isUp = true)
    {
        string DDName = "JG_Konglei";
        GameObject lei = GlobalTools.GetGameObjectInObjPoolByName(DDName);
        lei.name = DDName;
        if (isUp)
        {
            lei.transform.position = new Vector3(palyerJijis.transform.position.x + 166 + dx + GlobalTools.GetRandomDistanceNums(dx), TopPos.position.y - GlobalTools.GetRandomDistanceNums(dy), JiguanZ);
        }
        else
        {
            lei.transform.position = new Vector3(palyerJijis.transform.position.x + 166 + dx + GlobalTools.GetRandomDistanceNums(dx), DownPos.position.y + GlobalTools.GetRandomDistanceNums(dy), JiguanZ);
        }
        
        lei.transform.parent = this.transform.parent;
    }



    JiguanJishi _feixingqi1Jishi1;
    //前方 飞行器1
    bool IsStopFeixingqi1 = false;
    public void StopFeixingqi1(bool IsTrue = true)
    {
        IsStopFeixingqi1 = IsTrue;
    }


    bool IsFeixingqi1Jishi = false;
    public void Feixingqi1Jishi()
    {
        if (!IsFeixingqi1Jishi)
        {
            IsFeixingqi1Jishi = true;
            if (_feixingqi1Jishi1 == null) _feixingqi1Jishi1 = new JiguanJishi();
            _feixingqi1Jishi1.SetJishiValue(10, 4);


            print("   出现飞行器！！！！！！！！ ");
        }
       
    }



    public void Feixingqi1()
    {
        if (IsStopFeixingqi1) return;
        if (_feixingqi1Jishi1 != null)
        {
            if (_feixingqi1Jishi1.IsJishiOver())
            {
                int rNums = GlobalTools.GetRandomNum();
                if (rNums < 20)
                {
                    ShowJiguanObj(1,0, Feixingqi1Name, true, true);
                }
                else if (rNums < 40)
                {
                    ShowJiguanObj(1,0, Feixingqi1Name, true, true);
                }
                else
                {
                    ShowJiguanObj(2,0, Feixingqi1Name, true, true);
                }
                print("     feixingqi a  a a a a a  "+ rNums);
            }
        }
    }


    string Feixingqi1Name = "G_Feixingqi_1";

    void ShowJiguanObj(int DDNums, float dx = 0,string ObjName = "",bool IsQian = true,bool IsInGuais = false)
    {
        float DestanceY = Mathf.Abs(TopPos.position.y - DownPos.position.y) / (DDNums + 1);
        for (int i = 0; i < DDNums; i++)
        {
            //string ObjName = "G_Feixingqi_1";
            GameObject o = GlobalTools.GetGameObjectInObjPoolByName(ObjName);
            o.name = ObjName;
            if (IsQian)
            {
                o.transform.position = new Vector3(palyerJijis.transform.position.x + 166 + dx + GlobalTools.GetRandomDistanceNums(36), TopPos.position.y - DestanceY * (i + 1) + 5 - GlobalTools.GetRandomDistanceNums(10), JiguanZ);
            }
            else
            {
                o.transform.position = new Vector3(palyerJijis.transform.position.x - 36 - GlobalTools.GetRandomDistanceNums(6), TopPos.position.y - DestanceY * (i + 1) + 5 - GlobalTools.GetRandomDistanceNums(10), JiguanZ);
            }

            if (IsInGuais)
            {
                o.transform.parent = Guais.transform;
            }
            else
            {
                o.transform.parent = this.transform.parent;
            }
            
        }
    }







    string Feixingqi2Name = "G_Feixingqi_2";

    JiguanJishi _feixingqi1Jishi2;
    //前方 飞行器1
    bool IsStopFeixingqi2 = false;
    public void StopFeixingqi2(bool IsTrue = true)
    {
        IsStopFeixingqi2 = IsTrue;
    }


    bool IsFeixingqi2Jishi = false;
    public void Feixingqi2Jishi()
    {
        if (!IsFeixingqi1Jishi)
        {
            IsFeixingqi1Jishi = true;
            if (_feixingqi1Jishi2 == null) _feixingqi1Jishi2 = new JiguanJishi();
            _feixingqi1Jishi2.SetJishiValue(10, 4);
        }
    }


    public void Feixingqi2()
    {
        if (IsStopFeixingqi2) return;
        if (_feixingqi1Jishi2 != null)
        {
            if (_feixingqi1Jishi2.IsJishiOver())
            {
                int rNums = GlobalTools.GetRandomNum();
                if (rNums < 20)
                {
                    ShowJiguanObj(1,0, Feixingqi2Name,true,true);
                }
                else if (rNums < 60)
                {
                    ShowJiguanObj(1,0, Feixingqi2Name, true, true);
                }
                else
                {
                    ShowJiguanObj(2,0, Feixingqi2Name, true, true);
                }
            }
        }
    }




    string GZDaodanName = "DD_GZDaodan";

    JiguanJishi _genzongDDJishi;
    //前方 飞行器1
    bool IsStopGZDD = false;
    public void StopGZDD(bool IsTrue = true)
    {
        IsStopGZDD = IsTrue;
    }



    bool IsGZDDJishi = false;
    public void GZDDJishi()
    {
        if (!IsGZDDJishi)
        {
            IsGZDDJishi = true;
            if (_genzongDDJishi == null) _genzongDDJishi = new JiguanJishi();
            _genzongDDJishi.SetJishiValue(10, 4);
        }
    }



    public void GZDD()
    {
        if (IsStopGZDD) return;
        if (_genzongDDJishi != null)
        {
            if (_genzongDDJishi.IsJishiOver())
            {
                int rNums = GlobalTools.GetRandomNum();
                if (rNums < 10)
                {
                    ShowJiguanObj(2, 0, GZDaodanName,false);
                }
                else if (rNums < 40)
                {
                    ShowJiguanObj(1, 0, GZDaodanName,false);
                }
                else
                {
                    ShowJiguanObj(1, 0, GZDaodanName,false);
                }
            }
        }
    }























    GameObject Guais;





    // Start is called before the first frame update
    void Start()
    {
        Guais = GlobalTools.FindObjByName(GlobalTag.GUAIS);

        _houfangDaodanJishi = new JiguanJishi();
        _houfangDaodanJishi.SetJishiValue(6);

        _Kongleiishi = new JiguanJishi();
        _Kongleiishi.SetJishiValue(10,4);


        palyerJijis = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
    }

    float timess = 0;
    // Update is called once per frame
    void Update()
    {
        //timess += Time.deltaTime;
        //print("timess  ---->  " + timess);
        //FasheHoufangDaodan();
        //QianfangKonglei();
    }
}
