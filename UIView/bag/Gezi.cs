using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gezi : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsOpen = true;

    RectTransform _obj;

    //是否有物品了
    public RectTransform IsHasObj()
    {
        return _obj;
    }

    public void GetOutObj()
    {
        _obj = null;
    }
    

    public void GetInObj(RectTransform obj,bool isChange = false,bool isOldObjOut = false)
    {
        _obj = obj;
        //print(" ---->    "+_obj.name);
        RectTransform _OldRQ = _obj.GetComponent<MyDrag>().OldRQ; 
        if (_OldRQ != null&&!isChange)
        {
            _OldRQ.GetComponent<Gezi>().GetOutObj();
        }

        string skillName = "";
        if (_obj.GetComponent<HZDate>().zd_skill_ui_Name == "")
        {
            skillName = "none";
        }
        else
        {
            skillName = _obj.GetComponent<HZDate>().zd_skill_ui_Name;
        }

        _obj.GetComponent<HZDate>().RQName = this.transform.name;
        //string tt = "hi";
        //string ui_change_msg = skillName + "|" + this.transform.name;


        //_OldRO = this.GetComponent<RectTransform>();
        if (_OldRQ != null&&((this.tag == "zhuangbeilan" && _OldRQ.tag != "zhuangbeilan")|| (this.tag != "zhuangbeilan" && _OldRQ.tag == "zhuangbeilan")))
        {
            //&& (this.tag == "zhuangbeilan" && _OldRO.tag != "zhuangbeilan") || (this.tag != "zhuangbeilan" && _OldRO.tag == "zhuangbeilan")
            if (!isOldObjOut) {
                //if (Globals.isDebug) print("zhuangbeilan  切换属性事件  " + _OldRQ.tag);
                List<RectTransform> HZs = this.transform.parent.GetComponent<Mianban1>().GetInHZListHZ();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ,HZs), this);

                //主动技能 显示 事件
                //print("name >>   "+this.transform.name);
                //print("徽章带的主动技能名字：  "+_obj.GetComponent<HZDate>().zd_skill_Name);

                //string skillName = _obj.GetComponent<HZDate>().zd_skill_Name == "" ? "n" : _obj.GetComponent<HZDate>().zd_skill_Name;
                
               
                //print("ui_change_msg  "+ ui_change_msg);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.SKILL_UI_CHANGE, HZs), this);
            }
        }

        if (_OldRQ != null && (this.tag == "zhuangbeilan" && _OldRQ.tag == "zhuangbeilan")) {
           

            //print("name >>22222222222222222222   " + this.transform.name);
            //print("徽章带的主动技能名字：  " + _obj.GetComponent<HZDate>().zd_skill_ui_Name);
            List<RectTransform> HZs = this.transform.parent.GetComponent<Mianban1>().GetInHZListHZ();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.SKILL_UI_CHANGE, HZs), this);
        }

        if (_OldRQ == null&& this.tag == "zhuangbeilan") {
            List<RectTransform> HZs = this.transform.parent.GetComponent<Mianban1>().GetInHZListHZ();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.SKILL_UI_CHANGE, HZs), this);
        }
        
        /**
        if (_OldRQ != null && ((this.tag == "JN_zhuangbeilan" && _OldRQ.tag != "JN_zhuangbeilan") || (this.tag != "JN_zhuangbeilan" && _OldRQ.tag == "JN_zhuangbeilan")))
        {
            //&& (this.tag == "zhuangbeilan" && _OldRO.tag != "zhuangbeilan") || (this.tag != "zhuangbeilan" && _OldRO.tag == "zhuangbeilan")
            if (!isOldObjOut)
            {
                if (Globals.isDebug) print("zhuangbeilan  切换主动技能事件   " + _OldRQ.tag);
                List<RectTransform> HZs = this.transform.parent.GetComponent<Mianban1>().GetInHZListHZ();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ, HZs), this);

                List<RectTransform> HZs2 = this.transform.parent.GetComponent<Mianban1>().GetZDJNList();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZD_SKILL, HZs2), this);
            }
        }

        if (_OldRQ != null && (this.tag == "JN_zhuangbeilan" && _OldRQ.tag == "JN_zhuangbeilan"))
        {
            if (!isOldObjOut)
            {
                List<RectTransform> HZs2 = this.transform.parent.GetComponent<Mianban1>().GetZDJNList();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZD_SKILL, HZs2), this);
            }
        }
        */

        _obj.GetComponent<MyDrag>().OldRQ = this.GetComponent<RectTransform>();
        _obj.transform.position = this.transform.position;
        //print("--------------------->   "+ this.GetComponent<RectTransform>().sizeDelta.x + "  -------------  "+ this.GetComponent<RectTransform>().sizeDelta.y);
        //改变徽章大小 与格子大小同步
        _obj.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x*0.8f, this.GetComponent<RectTransform>().sizeDelta.y * 0.8f);//this.GetComponent<RectTransform>().sizeDelta;

    }




}
