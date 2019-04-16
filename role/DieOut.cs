﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOut : MonoBehaviour {

    public string type = "1";

    public string diaoluowu = "";
	// Use this for initialization
	void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
	}

    void DieOutDo(UEvent e)
    {
        if (!IsDie && this.GetComponent<RoleDate>().isDie) {
            IsDie = true;
            if (IsBoss) {
                isBossDie = true;
                //隐藏UI血条
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_DIE, this.name), this);
                GetSlowAC();
            }
            else
            {
                Diaoluowu();
            }
        }
        //掉落几率 掉落的等级 ==  掉落多个物体
        //掉落 血  蓝  物品
    }

    //掉落物
    void Diaoluowu()
    {
        if (diaoluowu == "") return;
        int jv = Random.Range(0, 1000);
        int fx = this.transform.position.x > this.GetComponent<AIBase>().gameObj.transform.position.x ? 1 : -1;
        string[] diaoluowuArr = diaoluowu.Split('|');
        for (var i = 0; i < diaoluowuArr.Length; i++)
        {
            string objName = diaoluowuArr[i].Split('-')[0];
            //掉落几率
            int dljv = int.Parse(diaoluowuArr[i].Split('-')[1]);
            if (jv < dljv)
            {
                GameObject o = GlobalTools.GetGameObjectByName(objName);
                o.transform.position = this.transform.position;
                o.GetComponent<Wupinlan>().GetXFX(Random.Range(100, 300) * fx);
            }
        }
    }

    //是否是BOSS
    public bool IsBoss = false;
    //如果是boss die 慢动作计时
    int SlowTimesNum = 0;
    bool isBossDie = false;
    void GetSlowAC(float nums = 0.5f)
    {
        Time.timeScale = nums;
    }
    //其他要处理事件 比如 BOSS血条隐藏 开门关门 ===
    void SlowTime()
    {
        if (!isBossDie) return;
        if (IsBoss)
        {
            SlowTimesNum++;
            if (SlowTimesNum > 80)
            {
                isBossDie = false;
                SlowTimesNum = 0;
                //爆出掉落物
                Diaoluowu();
                //开门
                DoorDo();
                //结束慢动作
                GetSlowAC(1);
                //移除自己 
                DistorySelf();
                //派发地图改变事件 标注自己被移除
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, this.name), this);
            }
        }
    }

    public void DistorySelf()
    {
        StartCoroutine(IEDieDestory(2f, this.gameObject));
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    }
    public IEnumerator IEDieDestory(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj, true);
    }


    [Header("Die后需要处理的门的名字和动作")]
    public string DoorNames;//Men_1-0:Men_2-0
    void DoorDo()
    {
        if (DoorNames == null) return;
        string[] doorArr = DoorNames.Split(':');
        int Length = doorArr.Length;
        for(var i = 0; i < Length; i++)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, doorArr[i]), this);
        }
    }

    bool IsDie = false;

	// Update is called once per frame
	void Update () {
        SlowTime();
    }
}
