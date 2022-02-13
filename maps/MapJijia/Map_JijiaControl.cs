using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_JijiaControl : MonoBehaviour
{
    Map_AirJiguanGuais _jiguan;
    // Start is called before the first frame update
    void Start()
    {
        GlobalSetDate.instance.screenName = GlobalTools.GetCScreenName();
        print("  ??GlobalSetDate.instance.screenName     "+ GlobalSetDate.instance.screenName);
        //Air_DB_List.Clear();
        JingNamesList2 = GlobalTools.GetChildsNamesList(DBsParent1);
        _jiguan = GetComponent<Map_AirJiguanGuais>();
        print("机甲 自动地图 控制");
        GetPlayer();
        GetKuang();
        OpenCamera();
        CreateMaps();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DAOJISHI, "239"), this);
        CX = player_jijia.transform.position.x;
    }

    float CX = 0;

    List<string> JingNamesList = new List<string> { };

    private void CreateMaps()
    {
        //Duan(50, DBsParent1);
        //Qishi(DBsParent2_qs);
        //Duan(20, DBsParent2);
        //Duan(50, DBsParent1);
        //Duan(50, DBsParent1);

    }

    float __z = 10;

    void Qishi(GameObject parentObj)
    {
        JingNamesList = GlobalTools.GetChildsNamesList(parentObj);
        string JingName = JingNamesList[0];
        GameObject jing = GlobalTools.GetGameObjectInObjPoolByName(JingName);
        jing.name = JingName;
        float __x = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.x;
        float __y = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.y;
        jing.transform.position = new Vector3(__x, __y, -__z);
        Air_DB_List2.Add(jing);
    }


    void Duan(int nums,GameObject parentObj,float duanX = 0)
    {
        JingNamesList = GlobalTools.GetChildsNamesList(parentObj);
        for (int i = 0; i < nums; i++)
        {
            string JingName = JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)];
            GameObject jing = GlobalTools.GetGameObjectInObjPoolByName(JingName);
            jing.name = JingName;
            float __x = 0; 
            float __y = 0; 

            if (i == 0)
            {
                __x = Air_DB_List[Air_DB_List.Count - 1].GetComponent<DB_AirBase>().TRPos.position.x;
                __y = Air_DB_List[Air_DB_List.Count - 1].GetComponent<DB_AirBase>().TRPos.position.y;
            }
            else
            {
                __x = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.x;
                __y = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.y;
            }

            jing.transform.position = new Vector3(__x, __y, -__z);
            Air_DB_List2.Add(jing);
        }
        //duanX = Air_DB_List2[Air_DB_List.Count - 1].transform.position.x;
    }















    [Header("摄像机 框")]
    public GameObject Kuang;

    void GetKuang()
    {
        if(Kuang == null)
        {
            Kuang = GlobalTools.FindObjByName(GlobalTag.KUANG);
        }
    }

    GameObject player_jijia;
    void GetPlayer()
    {
        if(player_jijia == null)
        {
            player_jijia = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
        }
    }


    GameObject MainCamera;
    void OpenCamera()
    {
        //打开 摄像机 的 限制   不受限 kuang的X距离
        if(MainCamera == null)
        {
            MainCamera = GlobalTools.FindObjByName(GlobalTag.MAINCAMERA);
            MainCamera.GetComponent<CameraController>().DontSetX = true;
        }
    }



    [Header("空战地板 List")]
    public List<GameObject> Air_DB_List = new List<GameObject> { };

    List<GameObject> Air_DB_List2 = new List<GameObject> { };


    [Header("地板1 景s 父类  用于取景自动生成")]
    public GameObject DBsParent1;

    [Header("地板2 景s **起始 父类  用于取景自动生成")]
    public GameObject DBsParent2_qs;


    [Header("地板2 景s 父类  用于取景自动生成")]
    public GameObject DBsParent2;

    [Header("地板终点")]
    public GameObject DBZhongdian;


    [Header("左 点")]
    public Transform LeftPos;
    [Header("右* 点")]
    public Transform RightPos;

    List<string> JingNamesList2 = new List<string> { };
    void DBMoves()
    {
        
        if (Air_DB_List[0].transform.position.x <= LeftPos.position.x - Air_DB_List[0].GetComponent<DB_AirBase>().GetWidth() - 20)
        {
            ObjectPools.GetInstance().DestoryObject2(Air_DB_List[0]);
            //Air_DB_List[0].gameObject.SetActive(false);
            Air_DB_List.RemoveAt(0);
            
            GetJingAndPosByJingName(JingNamesList2[GlobalTools.GetRandomNum(JingNamesList2.Count)]);
        }
    }

    void GetJingAndPosByJingName(string JingName)
    {
        GameObject jing = GlobalTools.GetGameObjectInObjPoolByName(JingName);
        float __x = Air_DB_List[1].GetComponent<DB_AirBase>().TRPos.position.x;
        float __y = Air_DB_List[1].GetComponent<DB_AirBase>().TRPos.position.y;
        jing.transform.position = new Vector3(__x, __y,-__z);
        Air_DB_List.Add(jing);
    }

    // Update is called once per frame
    void Update()
    {
        if (player_jijia.GetComponent<PlayerRoleDate>().isDie) return;
        //DBMoves();
        DBMove2();
    }

    bool IsQishi = false;
    void DBMove2()
    {
        if (!IsQishi)
        {
            IsQishi = true;
            Duan(10, DBsParent1);
            return;
        }

        if (IsInEnd)
        {
            End();
            return;
        }


        if (Air_DB_List2[0].transform.position.x <= LeftPos.position.x - Air_DB_List2[0].GetComponent<DB_AirBase>().GetWidth() - 20)
        {
            GameObject jing = Air_DB_List2[0];
            //ObjectPools.GetInstance().DestoryObject2(Air_DB_List[0]);
            //Air_DB_List[0].gameObject.SetActive(false);
            Air_DB_List2.RemoveAt(0);

            //GetJingAndPosByJingName(JingNamesList2[GlobalTools.GetRandomNum(JingNamesList2.Count)]);

            float __x = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.x;
            float __y = Air_DB_List2[Air_DB_List2.Count - 1].GetComponent<DB_AirBase>().TRPos.position.y;
            jing.transform.position = new Vector3(__x, __y, -__z);
            Air_DB_List2.Add(jing);

        }

        //Jiguan();

    }


    bool IsInEnd = false;
    bool IsCreateMen = false;
    void End()
    {
        if (!IsCreateMen)
        {
            IsCreateMen = true;
            Qishi(DBZhongdian);
        }
        
    }


    float GuankaJishi = 0;

    void Jiguan()
    {

        print(player_jijia.transform.position.x - CX);

        GuankaJishi += Time.deltaTime;
        print("  关卡 进行时间  " + GuankaJishi);

        _jiguan.FasheHoufangDaodan();

        _jiguan.BianjieKongleiJishi();
        _jiguan.BianjieKonglei();

        //_jiguan.Feixingqi1Jishi();
        //_jiguan.Feixingqi1();

        //_jiguan.GZDDJishi();
        //_jiguan.GZDD();

        if (GuankaJishi >= 20)
        {
            //出现 前雷
            _jiguan.QianfangKonglei();
            //_jiguan.QianfangKonglei();

           

            if (GuankaJishi >= 40)
            {
                //前飞行器
                _jiguan.Feixingqi1Jishi();
                _jiguan.Feixingqi1();
            }


            //if (GuankaJishi >= 20)
            //{
            //    IsInEnd = true;
            //    return;
            //}



            if (GuankaJishi >= 90)
            {
                _jiguan.GZDDJishi();
                _jiguan.GZDD();
                //跟踪导弹
            }

            if (GuankaJishi >= 120)
            {
                //雷阵
                _jiguan.Feixingqi2Jishi();
                _jiguan.Feixingqi2();
            }


            if (GuankaJishi >= 150)
            {
                _jiguan.QianfangKonglei();
            }


            if (GuankaJishi >= 200)
            {
                if(player_jijia.transform.position.x - CX >= 14000)
                {
                    IsInEnd = true;
                }
                
            }


        }
    }


}
