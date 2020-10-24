using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class UI_ShowPanel : MonoBehaviour {

    public Image top;
    public Image down;
    public Image right;
    public Image center;

    GameObject _player;

    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.SKILL_UI_CHANGE, this.GetSkillChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.RELEASE_SKILL, this.ReleaseSkill);
        if (!_player) _player = GlobalTools.FindObjByName("player");
        //GetObjByName("top",this.gameObject);
        //print(" 9999999999999!  ");
        //_skillUseDate = GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date;
        //print(" _skillUseDate  "+ _skillUseDate);
        //对比 技能使用情况
        //ComparedSkillUse(_skillUseDate);
        //print(" UI_ShowPanel  启动 新建的是否 新进来了   top "+top);
        //print("??  "+this.GetType().GetProperty("top").GetValue(this, null));
    }


    //将 徽章UI存入对象池
    public void RemoveAllSkillHZUI()
    {
        foreach(GameObject o in HZList)
        {
            ObjectPools.GetInstance().DestoryObject2(o);
        }
        HZList.Clear();
    }

    //存档时 所有CD直接变满
    public void AllSkillCDFull()
    {
        foreach (GameObject o in HZList)
        {
            o.GetComponent<UI_Skill>().CDFull();
        }
        GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date = "";
    }



    //存入 所有 徽章数据 
    public void SaveAllHZDate()
    {
        //print("HZList是不是技能槽？    " + HZList.Count);
        foreach (GameObject o in HZList)
        {
            o.GetComponent<UI_Skill>().GetDateInGlobalSkillDate();
        }
        //GlobalSetDate.instance.CurrentMapMsgDate.skill_use_date = GlobalSetDate.instance.CurrentUserDate.skill_use_date;
        //GlobalSetDate.instance.TempSkillUseRecord = GlobalSetDate.instance.CurrentUserDate.skill_use_date;

        //GlobalSetDate.instance.CurrentUserDate.skill_use_date = GlobalSetDate.instance.CurrentUserDate.skill_use_date;
        //print("  徽章数据 "+ GlobalSetDate.instance.CurrentUserDate.skill_use_date);
    }


    //读取所有徽章使用数据
    public void GetAllHZDate()
    {
        //print("--------------------------------------------------------------->   读取所有徽章使用数据 "+HZList.Count);
        foreach (GameObject o in HZList)
        {
            o.GetComponent<UI_Skill>().GetGlobalSkillDate();
        }

        ShowHZTXinPlayer();

    }


    //显示 玩家身上要显示那些 徽章特效  切换徽章和使用徽章 都要调用这里
    public void ShowHZTXinPlayer()
    {
        //移除所有徽章特效
        PlayerGameBody playerGameBody = _player.GetComponent<PlayerGameBody>();
        //总共显示 哪些徽章 特效
        //1.花防 2.电刀 -生命上限 3.火刀 -生命上限  4.神佑  5.电盾  6.毒刃 -生命上限  7.龙盾 金色光   8.
        playerGameBody.StopAllHZInTX();

        foreach (GameObject o in HZList)
        {
            print("  看看装备了 哪些徽章：    "+o.GetComponent<UI_Skill>().GetHZDate().HZName+"   是否可用 "+ o.GetComponent<UI_Skill>().IsCDSkillCanBeUse());
            if(o.GetComponent<UI_Skill>().IsCDSkillCanBeUse()) playerGameBody.PlayHZInTXByTXName(o.GetComponent<UI_Skill>().GetHZDate().HZZBTXName);
        }

        //显示徽章特效   怎么显示

    }


    //对比技能使用数据
    public void ComparedSkillUse(string skillUseDate)
    {
        string[] useDateList = skillUseDate.Split('|');
        if (useDateList.Length == 0) return;
        List<GameObject> tempHZList = HZList;

        for (int i=0;i<useDateList.Length;i++)
        {
            for(int j = tempHZList.Count-1;j>=0;j--)
            {
               
                //剩余使用次数
                int synums = int.Parse(useDateList[i].Split('_')[1]);
                if(tempHZList[j].GetComponent<UI_Skill>().GetHZDate().usenums <= synums)
                {
                    //如果事最大使用次数  就直接跳过
                    tempHZList.Remove(tempHZList[j]);
                    break;
                }

                string hzName = useDateList[i].Split('_')[0];
                //剩余CD宽高
                float cd = float.Parse(useDateList[i].Split('_')[2]);
                //剩余读秒
                float dumiaoNums = float.Parse(useDateList[i].Split('_')[3]);
                if (tempHZList[j].GetComponent<UI_Skill>().GetHZDate().HZName == hzName)
                {
                    tempHZList[j].GetComponent<UI_Skill>().SetSkillDate(synums, cd, dumiaoNums);
                    tempHZList.Remove(tempHZList[j]);
                }
            }
        }
    }

    public string GetSaveSkillDate()
    {
        string str = "";
        for (int i=0;i<HZList.Count;i++)
        {
            /**if(i == HZList.Count - 1)
            {
                str += HZList[i].GetComponent<UI_Skill>().GetSkillDate();
            }
            else
            {
                str += HZList[i].GetComponent<UI_Skill>().GetSkillDate() + "|";
            }*/

            str += i == HZList.Count - 1 ? str += HZList[i].GetComponent<UI_Skill>().GetSkillDate() : HZList[i].GetComponent<UI_Skill>().GetSkillDate() + "|";
        }
        return str;
    }


    void OnDistory()
    {
        //print("我被销毁了？？？？@、@");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.SKILL_UI_CHANGE, this.GetSkillChange);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.RELEASE_SKILL, this.ReleaseSkill);
    }

    string _skillUseDate = "";
    public string SaveSkillUseDate() {

        return "";
    }

    public void OnDistory2() {
        OnDistory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ReleaseSkill(UEvent e)
    {
        string str = e.eventParams.ToString();
        //print("释放技能  "+str);
        foreach(GameObject o in HZList)
        {
            //上下左右 4个位置的节能 如果和 str 传来的位置一致就释放
            if(o.GetComponent<UI_Skill>().GetSkillPos() == str)
            {
                if (o.GetComponent<UI_Skill>().GetHZDate().type == "bd") return;
                GlobalTools.FindObjByName("player").GetComponent<GameBody>().ShowSkill(o);
                break;
            }
        }
    }



    


    //name_(0/1)  1是装配 0是卸下
    void GetSkillChange(UEvent e)
    {
        //print(e.eventParams.ToString());
        //gezi23  上
        //22 中
        //26 下
        //21 右
        //return;
        List<RectTransform> t = (List<RectTransform>)e.eventParams;
        //print(">>>>>>>>>>>>>     "+t.Count);

        RemoveAllHZUI();
        
        foreach (Transform o in t)
        {
            //print(o.GetComponent<HZDate>().name+"   容器名字  " +o.GetComponent<HZDate>().RQName);
            //匹配 HZlist


            string name = o.GetComponent<HZDate>().zd_skill_ui_Name;
            if (name == "") continue;
            int posNum = 0;// int.Parse(e.eventParams.ToString().Split('_')[1]);
            string str = o.GetComponent<HZDate>().RQName;

            if (str == "gezi23")
            {
                posNum = 1;
            }
            else if (str == "gezi26")
            {
                posNum = 2;
            }
            else if (str == "gezi22")
            {
                posNum = 3;
            }
            else if (str == "gezi21")
            {
                posNum = 4;
            }

            //print(" posNum "+ posNum);

            switch (posNum)
            {
                case 1:
                    //print("topimg   "+ top);
                    //if (top == null) top = this.transform.Find("top");
                    if (posNum == 1) GetInObj(top, name, o.GetComponent<HZDate>(),"up");
                    break;
                case 2:
                    if (posNum == 2) GetInObj(down, name, o.GetComponent<HZDate>(),"down");
                    break;
                case 3:
                    if (posNum == 3) GetInObj(center, name, o.GetComponent<HZDate>(),"center");
                    break;
                case 4:
                    if (posNum == 4) GetInObj(right, name, o.GetComponent<HZDate>(),"right");
                    break;

            }
        }

        //print(e.eventParams.);


        //1.对比是否跟换了 新的徽章  1.移除 位置上的 徽章   2.替换新徽章 
        GetAllHZDate();

    }

    public List<GameObject> HZList = new List<GameObject> { };


    void RemoveAllHZUI()
    {
        if (HZList.Count == 0) return;
        foreach (GameObject o in HZList)
        {
            ObjectPools.GetInstance().DestoryObject2(o);
        }
        HZList.Clear();
    }

    void RemoveHZ(Image img)
    {
        foreach(GameObject o in HZList)
        {
            if(o.transform.position == img.transform.position)
            {
                HZList.Remove(o);
                //Destroy(o);
                ObjectPools.GetInstance().DestoryObject2(o);
            } 
        }   
    }

    void GetInObj(Image img,string objName, HZDate hzDate,string hzPos)
    {
        //print("-------------->   "+objName);
        GameObject hz = ObjectPools.GetInstance().SwpanObject2(Resources.Load(objName) as GameObject);
        //print("hz ---------------> "+ hz);
        //print("img-------------->  " + img);
        hz.transform.position = img.transform.position;
        hz.transform.parent = this.transform; //GlobalTools.FindObjByName("PlayerUI").transform;
        hz.GetComponent<UI_Skill>().SetHZDate(hzDate);
        hz.GetComponent<UI_Skill>().SetSkillPos(hzPos);
        //print("hz   "+ hz+"   hz pos  "+hz.transform.position);
        HZList.Add(hz);
    }



    /** public Image GetObjByName(string _name, System.Object obj)
     {
         Type type = obj.GetType();
         FieldInfo fieldInfo = type.GetField(_name);
         if (fieldInfo == null) return null;
         return fieldInfo.GetValue(obj) as Image;
     }*/

    public void GetObjByName(string _name, System.Object obj) {

        Type t = obj.GetType();
        print(t.Name);
        print("获取 属性  "+ t.GetProperty(_name));

        
    }

  
}
