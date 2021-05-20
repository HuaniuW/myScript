using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class JN_Diandings : JN_SFBase
{
    //public void GetStart(GameObject gameObj)
    //{
    //    //检查 电钉 数组 移除 没活动的 电钉
    //    RemoveDieDiandingInList();

    //    ShowDiandings();
    //}

    protected override void Start()
    {
        if (IsGetBody)
        {
            print("  释放技能 ??  start! ");
            _gameBody = GetComponent<GameBody>();
            _gameBody.GetDB().AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
            DianPos = new List<UnityEngine.Transform>() { pos1, pos2, pos3, pos4 };
        }
    }


    public override void GetStart(GameObject obj)
    {
        _player = obj;

        if (IsHas4Dianding())
        {
            ReSetAll();
            return;
        }

        print(">>>>>>>diandings!!!!!");

        //RemoveDieDiandingInList();
        //ShowDiandings();

        //print("?????   " + obj);
        //进入 攻击距离
        IsStarting = true;
        if (SFTX)
        {
            //print(" 播放特效啊 ！！！！！！！！！！！！！！！！！！！！！！！！ ");
            SFTX.Play();
        }

    }

    protected override void ShowACTX(string type, EventObject eventObject)
    {
        testnums++;
        //print("type:  "+type);
        //print("eventObject  ????  " + eventObject);
        if (IsStarting) print(testnums + "  ___________________________________________________________________________________________________________________________name    " + eventObject.name);



        if (type == EventObject.FRAME_EVENT)
        {
            if (!IsStarting) return;
            if (eventObject.name == "ac")
            {
                print("?? 特效TXName " + TXName);
                //GetComponent<ShowOutSkill>().ShowOutSkillByName(TXName, true);
                if (TXName == "diandings")
                {
                    RemoveDieDiandingInList();
                    ShowDiandings();
                }
                OtherTX();


            }
        }

    }




    private void RemoveDieDiandingInList()
    {
        for (int i = DiandingList.Count - 1; i >= 0; i--)
        {
            if (DiandingList[i] == null||!DiandingList[i].activeSelf) DiandingList.Remove(DiandingList[i]);
        }
    }



    public bool IsHas4Dianding()
    {
        for (int i = DiandingList.Count - 1; i >= 0; i--)
        {
            if (DiandingList[i] && !DiandingList[i].activeSelf)
            {
                DiandingList.Remove(DiandingList[i]);
            }
        }


        if (DiandingList.Count < 3) return false;
        //for (int i = DiandingList.Count - 1; i >= 0; i--)
        //{
        //    if (DiandingList[i] == null) return false;
        //    if (!DiandingList[i].activeSelf)
        //    {
        //        DiandingList.Remove(DiandingList[i]);
        //        return false;
        //    }
        //}
        return true;
    }


   

  

    List<GameObject> DiandingList = new List<GameObject>() { };

    //4个点 位置
    List<UnityEngine.Transform> DianPos = new List<UnityEngine.Transform>() { };

    public UnityEngine.Transform pos1;
    public UnityEngine.Transform pos2;
    public UnityEngine.Transform pos3;
    public UnityEngine.Transform pos4;

    public string DiandingName = "TX_Dianding";

    void ShowDiandings()
    {

        Remove4Dianding();


        for (int i = 0;i<4;i++)
        {
            GameObject dianding = ObjectPools.GetInstance().SwpanObject2(Resources.Load(DiandingName) as GameObject);
            dianding.transform.position = new Vector2(DianPos[i].position.x, DianPos[i].position.y+GlobalTools.GetRandomDistanceNums(2));//    DianPos[i].position;
            dianding.GetComponent<JN_base>().atkObj = this.gameObject;
            dianding.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
            dianding.GetComponent<RoleDate>().Live = dianding.GetComponent<RoleDate>().maxLive;
            dianding.GetComponent<TX_Dianding>().GetStart();
            DiandingList.Add(dianding);


            //判断 该位置 是否 已经存在 电钉了
            //if (!IsHasDiandingInPos(i))
            //{
            //    GameObject dianding = ObjectPools.GetInstance().SwpanObject2(Resources.Load(DiandingName) as GameObject);
            //    dianding.transform.position = DianPos[i].position;
            //    dianding.GetComponent<JN_base>().atkObj = this.gameObject;
            //    dianding.GetComponent<JN_Date>().team = this.GetComponent<JN_Date>().team;
            //    dianding.GetComponent<RoleDate>().Live = dianding.GetComponent<RoleDate>().maxLive;
            //    dianding.GetComponent<TX_Dianding>().GetStart();
            //    DiandingList.Add(dianding);
            //}
        }
    }


    void Remove4Dianding()
    {
        for (int i = DiandingList.Count - 1; i >= 0; i--)
        {
            if (DiandingList[i])
            {
                if (DiandingList[i].activeSelf) DiandingList[i].GetComponent<TX_Dianding>().RemoveSelf();
                DiandingList.Remove(DiandingList[i]);
            }
           
        }
    }

    bool IsHasDiandingInPos(int s) {
        if (DiandingList.Count == 0) return false;
        for (int i = DiandingList.Count - 1; i >= 0; i--)
        {
            if (DiandingList[i] && DiandingList[i].activeSelf && (DiandingList[i].transform.position.x == DianPos[s].position.x)) {
                if (!DiandingList[i].activeSelf)
                {
                    DiandingList.Remove(DiandingList[i]);
                    return false;
                }
                print("i--------->   "+i);
                return true;
            } 
        }
        return true;
    }





}
