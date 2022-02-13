using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_AirBase : MonoBehaviour
{

    [Header("摄像机块 空中 终点")]
    public Transform CameraAirEnd;


    // Start is called before the first frame update
    void Start()
    {
        if (IsCreateJing) CreateJing();
        RanNeiyun3Y();
        if (CameraAirEnd)
        {
            GlobalTools.FindObjByName(GlobalTag.MAINCAMERA).GetComponent<CameraController>().CameraAirEnd = CameraAirEnd;
        }
    }

    public Transform TLPos;
    public Transform TRPos;

    public float GetWidth()
    {
        return Mathf.Abs(TLPos.position.x - TRPos.position.x);
    }

    [Header("是否 创建 景")]
    public bool IsCreateJing = false;

    public void CreateJing()
    {
        if (JingParent.transform.childCount == 0) return;
        List<string> JingNamesList = GlobalTools.GetChildsNamesList(JingParent);
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)]);
        //if (GlobalTools.GetRandomNum() >= 60)
        //{
        //    GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)]);
        //}

        //前景
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "qian");
        //背景
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");
        GetJingAndPosByJingName(JingNamesList[GlobalTools.GetRandomNum(JingNamesList.Count)], "bei");


    }


    void GetJingAndPosByJingName(string JingName,string type = "")
    {
        GameObject jing = GlobalTools.GetGameObjectByName(JingName);
        jing.name = JingName;
        float __x = TLPos.position.x + GlobalTools.GetRandomDistanceNums(GetWidth());
        float __y = TLPos.position.y -1 + GlobalTools.GetRandomDistanceNums(2);
        float __z = jing.transform.position.z;

        

        if (type == "qian")
        {
            if (jing.GetComponent<ParticleSystem>())
            {
                jing.GetComponent<ParticleSystemRenderer>().sortingOrder = 250;
                __z = -2 - GlobalTools.GetRandomDistanceNums(4);
                //__y += -6 + GlobalTools.GetRandomDistanceNums(10);
                __y = TLPos.position.y - GlobalTools.GetRandomDistanceNums(8);
            }
        }
        else if (type == "bei")
        {
            if (jing.GetComponent<ParticleSystem>())
            {
                jing.GetComponent<ParticleSystemRenderer>().sortingOrder = -160;
                __y = TLPos.position.y - 2 + GlobalTools.GetRandomDistanceNums(6);
                __z = 6 + GlobalTools.GetRandomDistanceNums(4);
            }
        }
        jing.transform.position = new Vector3(__x, __y, __z);

        jing.transform.parent = this.transform;
    }



    [Header("景资源的 父类")]
    public GameObject JingParent;
    public List<string> JingLists = new List<string> { };

    [Header("最里面的云层 ")]
    public GameObject Neiyun3;
    void RanNeiyun3Y()
    {
        if (Neiyun3)
        {
            Neiyun3.transform.position = new Vector3(Neiyun3.transform.position.x, Neiyun3.transform.position.y+GlobalTools.GetRandomDistanceNums(4), Neiyun3.transform.position.z+GlobalTools.GetRandomDistanceNums(5));
        }
    }

}
