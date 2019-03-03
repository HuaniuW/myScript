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
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HZ, changeHZ);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_DIAOLUOWU, this.GetDiaoLuo);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JIAXUE, this.JiaXue);
    }

    void JiaXue(UEvent e)
    {
        this.live += 300;
        GetTX("jiaxue");
    }


    GameObject tx;
    void GetTX(string txName)
    {
        tx = null;
        tx = GlobalTools.GetGameObjectByName(txName);
        tx.transform.position = new Vector2(this.transform.position.x, this.transform.position.y-1);//new Vector2(0, -3);
        tx.transform.parent = this.transform;

        //tx.transform.position = new Vector2(0,-3);
        //print("position  "+tx.transform.position);
        StartCoroutine(IEDestory2ByTime(tx, 0.8f));
    }

    public IEnumerator IEDestory2ByTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj, true);
    }

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
        }
        if (live > maxLive) live = maxLive;
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

            GetHZBeishu();
        }
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANEG_LIVE), this);
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
                    this.maxLive *= nums;
                }
            }
        }
    }

    List<string> beishuArr = new List<string>();

    //徽章的数据加成
    void GetHZDate(HZDate hzdate)
    {
        //print(hzdate.HZName);
        
        if (hzdate.def != 0) this.def += hzdate.def;
        if (hzdate.atk != 0) this.atk += hzdate.atk;
        //print(this.atk + "  --   " + hzdate.atk);
        if (hzdate.live != 0)
        {
            //this.live = _live + hzdate.live;
            this.maxLive += hzdate.live;
        }

        if (hzdate.defP != 0) beishuArr.Add("defP_"+hzdate.defP);
        if (hzdate.atkP != 0) beishuArr.Add("atkP_" + hzdate.atkP);
        if (hzdate.liveP != 0) {
            beishuArr.Add("liveP_" + hzdate.liveP);
        }

        if (hzdate.skill!=null)
        {

        }
    }
}
