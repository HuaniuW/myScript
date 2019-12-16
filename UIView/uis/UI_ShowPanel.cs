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


    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.SKILL_UI_CHANGE, this.GetSkillChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.RELEASE_SKILL, this.ReleaseSkill);
        //GetObjByName("top",this.gameObject);

        // print("??  "+this.GetType().GetProperty("top").GetValue(this, null));
    }

    void OnDistory()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.SKILL_UI_CHANGE, this.GetSkillChange);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.RELEASE_SKILL, this.ReleaseSkill);
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
            if(o.GetComponent<UI_Skill>().GetSkillPos() == str)
            {
                print(o.GetComponent<UI_Skill>().GetHZDate().zd_skill_Name);
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
        print(">>>>>>>>>>>>>     "+t.Count);

        RemoveAllHZUI();

        foreach (Transform o in t)
        {
            print(o.GetComponent<HZDate>().name+"   容器名字  " +o.GetComponent<HZDate>().RQName);
            //匹配 HZlist


            string name = o.GetComponent<HZDate>().zd_skill_Name;
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


    }

    List<GameObject> HZList = new List<GameObject> { };


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
        hz.transform.position = img.transform.position;
        hz.transform.parent = GlobalTools.FindObjByName("PlayerUI").transform;
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
