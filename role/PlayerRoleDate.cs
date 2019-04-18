using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoleDate : RoleDate
{
    float _atk;
    float _def;
    float _theMaxLive;
    float _yingzhi;
    float _live;
    float _lan;


    // Start is called before the first frame update
    void Start()
    {
        InitBaseDate();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HZ, changeHZ);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_DIAOLUOWU, this.GetDiaoLuo);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JIAXUE, this.JiaXue);
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HZ, changeHZ);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_DIAOLUOWU, this.GetDiaoLuo);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JIAXUE, this.JiaXue);
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }


    //void RemoveSelf(UEvent e)
    //{
    //    DestroyImmediate(this, true);
    //}



    void JiaXue(UEvent e)
    {
        this.live += 300;
        if(IsHasZZ) if (live > maxLive * 0.3f) live = maxLive * 0.3f;
        GetTX("jiaxue");
    }


    GameObject tx;
    void GetTX(string txName)
    {
        tx = null;
        tx = GlobalTools.GetGameObjectByName(txName);
        Vector2 v =  this.GetComponent<GameBody>().groundCheck.position;
        tx.transform.parent = this.transform;
        //print(this.transform.localScale);
        if (this.transform.localScale.x == -1)
        {
            tx.transform.position = new Vector2(v.x, v.y - 0.6f);
        }
        else {
            tx.transform.position = new Vector2(v.x, v.y);
        }
        ;//new Vector2(0, -3);
        //print("tx.transform.position     "+ tx.transform.position);
        

        //tx.transform.position = new Vector2(0,-3);
        //print("position  "+tx.transform.position);
        StartCoroutine(IEDestory2ByTime(tx, 0.8f));
    }

    public IEnumerator IEDestory2ByTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj, true);
    }



    //是否有诅咒
    public bool IsHasZZ = false;

    void GetDiaoLuo(UEvent e)
    {
        //print("------------------------------->>    "+e.eventParams.ToString());
        if(e.eventParams.ToString() == "XuehunXiao")
        {
            this.live += 100;
            GetTX("jiaxue");
        }
        else if (e.eventParams.ToString() == "XuehunDa")
        {
            this.live += 500;
            GetTX("jiaxue");
        }
        else if (e.eventParams.ToString() == "LanhunXiao")
        {
            this.lan += 100;
            GetTX("jialan");
        }
        else if (e.eventParams.ToString() == "LanhunDa")
        {
            this.lan += 500;
            GetTX("jialan");
        }else if (e.eventParams.ToString() == "C_cundangdian")
        {
            print("存档点加血");
            this.live += 5000;
            GetTX("jiaxue");
        }
        if (!IsHasZZ)
        {
            if (live > maxLive) live = maxLive;
        }
        else {
            //print(live+"  max  "+ maxLive);
            if (live > maxLive*0.3f) live = maxLive*0.3f;
            //print(live + "  max2222  " + maxLive);
        }
        
        if (lan > maxLan) lan = maxLan;
    }



    void InitBaseDate()
    {
        _atk = this.atk;
        _def = this.def;
        _theMaxLive = this.maxLive;
        _live = this.live;
        _lan = this.lan;
        _yingzhi = this.yingzhi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetCSDate()
    {
        this.atk = this._atk;
        this.def = _def;
        maxLive = _theMaxLive;
        yingzhi = _yingzhi;
    }

    void changeHZ(UEvent e)
    {
        List<RectTransform> t = (List<RectTransform>)e.eventParams;
        GetCSDate();
        if (t.Count != 0)
        {
            beishuArr.Clear();
            foreach (var hz in t)
            {
                GetHZDate(hz.GetComponent<HZDate>());
            }
            GetHZZZ(t);
            GetHZBeishu();
        }
        else
        {
            //徽章组没有徽章 没有诅咒徽章 清掉锁血
            IsHasZZ = false;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU, false), this);
        }
        //print("气血徽章- 事件发送！！！！！");
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANEG_LIVE,this.maxLive), this);
    }

    //徽章的倍数变化
    void GetHZBeishu()
    {
        if (beishuArr.Count != 0)
        {
            foreach (var s in beishuArr)
            {
                string s1 = s.Split('_')[0];
                float nums = float.Parse(s.Split('_')[1]);
                string _name = s.Split('_')[2];
                if (s1 == "atkP")
                {
                    this.atk *= nums;
                }
                else if (s1 == "defP")
                {
                    this.def *= nums;
                }
                else if (s1 == "liveP")
                {
                    if(_name!="诅咒宝石") this.maxLive *= nums;
                    if(IsHasZZ) if (live > maxLive * 0.3f) live = maxLive * 0.3f;
                }
            }
        }
    }

    List<string> beishuArr = new List<string>();

    //徽章的数据加成
    void GetHZDate(HZDate hzdate)
    {
        print(hzdate.HZName);
        
        if (hzdate.def != 0) this.def += hzdate.def;
        if (hzdate.atk != 0) this.atk += hzdate.atk;
        //print(this.atk + "  --   " + hzdate.atk);
        if (hzdate.live != 0)
        {
            //this.live = _live + hzdate.live;
            this.maxLive += hzdate.live;
            //print("装备最大气血装备  "+ this.maxLive);
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ADD_MAX_LIVE,maxLive), this);
        }

        if (hzdate.defP != 0) beishuArr.Add("defP_"+hzdate.defP+"_"+hzdate.HZName);
        if (hzdate.atkP != 0) beishuArr.Add("atkP_" + hzdate.atkP + "_" + hzdate.HZName);
        if (hzdate.liveP != 0) {
            beishuArr.Add("liveP_" + hzdate.liveP + "_" + hzdate.HZName);
        }

       
    }
    
    //徽章诅咒
    void GetHZZZ(List<RectTransform> t)
    {
        foreach (var hz in t)
        {
            if(hz.GetComponent<HZDate>().HZName == "诅咒宝石")
            {
                IsHasZZ = true;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU,true), this);
                return;
            }
        }
        IsHasZZ = false;
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ZUZHOU, false), this);
    }
}
