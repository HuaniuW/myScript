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
        RectTransform _OldRQ = _obj.GetComponent<MyDrag>().OldRQ;
        if (_OldRQ != null&&!isChange)
        {
            _OldRQ.GetComponent<Gezi>().GetOutObj();
        }
        //_OldRO = this.GetComponent<RectTransform>();
        if (_OldRQ != null&&((this.tag == "zhuangbeilan" && _OldRQ.tag != "zhuangbeilan")|| (this.tag != "zhuangbeilan" && _OldRQ.tag == "zhuangbeilan")))
        {
            //&& (this.tag == "zhuangbeilan" && _OldRO.tag != "zhuangbeilan") || (this.tag != "zhuangbeilan" && _OldRO.tag == "zhuangbeilan")
            if (!isOldObjOut) {
                if (Globals.isDebug) print("zhuangbeilan  切换属性事件  " + _OldRQ.tag);
                List<RectTransform> HZs = this.transform.parent.GetComponent<Mianban1>().GetInHZListHZ();
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_HZ,HZs), this);
            }
        }

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

        _obj.GetComponent<MyDrag>().OldRQ = this.GetComponent<RectTransform>();
        _obj.transform.position = this.transform.position;
    }

   


}
