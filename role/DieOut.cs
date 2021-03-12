using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOut : MonoBehaviour {

    public string type = "1";

    public string diaoluowu = "";

    [Header("boss Die后 奖励物品出现位置")]
    public Transform bossDieOutPos;
	// Use this for initialization
	void Start () {
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
      
    }

   


    void OnDistory()
    {
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
        
    }

    void OnRemove()
    {
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    }

    //标记开门
    public bool IsBiaojiOpenDoor = false;

    [Header("自身的碰撞块")]
    public GameObject HitKuai;

    [Header("自身的碰撞块")]
    public GameObject HitKuai2;



    [Header("标记 检查 是否 怪都die了")]
    public bool IsBiaojiAllDieStart = true;
    void DieOutDo()
    {
        if (!IsDie && this.GetComponent<RoleDate>().isDie) {
            IsDie = true;
            if (IsOrter1) DieBeBlack();

            if (HitKuai) HitKuai.SetActive(false);
            if (HitKuai2) HitKuai2.SetActive(false);
            if (IsNeedDieSlowAC) DieSlowAC();

            //判断是否 在自动地图里面
            if (GlobalTools.FindObjByName("maps") != null)
            {
                if (GlobalTools.FindObjByName("maps").GetComponent<GetReMap2>()) {
                    GlobalTools.FindObjByName("maps").GetComponent<GetReMap2>().GuaiList.Remove(this.gameObject);
                }
                else
                {
                    if (GlobalTools.FindObjByName("MainCamera") != null)
                    {
                        GlobalTools.FindObjByName("MainCamera").GetComponent<GameControl>().GuaiList.Remove(this.gameObject);
                    }
                }

               


                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ALLDIE_OPEN_DOOR, "allDie"), this);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.All_DIE_OPEN_DOOR, "allDie"), this);
            }
            else
            {
                if (IsBiaojiAllDieStart) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "allDie"), this);
                if (IsBiaojiOpenDoor) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, "open"), this);
            }

           
            if (IsBoss) {
                isBossDie = true;
                //隐藏UI血条
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_DIE, this.name), this);
                //运行帧速变慢  慢动作
                GetSlowAC();
            }
            else
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GUAI_DIE, this.gameObject), this);
                Diaoluowu();

                if (IsJingying)
                {
                    //开门
                    DoorDo();
                }

                DistorySelf();
            }
        }
        //掉落几率 掉落的等级 ==  掉落多个物体
        //掉落 血  蓝  物品
    }

    //在关卡数据 和存档数据匹配的 时候  是怪物的 话 如果 消失 要 调用一下 开门
    public void GetMenPiPei()
    {
        if (GlobalTools.FindObjByName("maps") != null)
        {
            GlobalTools.FindObjByName("maps").GetComponent<GetReMap2>().GuaiList.Remove(this.gameObject);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ALLDIE_OPEN_DOOR, "allDie"), this);
        }
    }


    public bool IsOrter1 = false;
    void DieBeBlack()
    {
        GetComponent<GameBody>().SetInitColor(Color.black);
        GetComponent<GameBody>().GetBoneColorChange(Color.black);
    }



    //掉落物
    void Diaoluowu()
    {
        if (diaoluowu == ""|| !this.GetComponent<AIBase>().thePlayer) return;
        int jv = Random.Range(0, 100);
        int fx = this.transform.position.x > this.GetComponent<AIBase>().thePlayer.transform.position.x ? 1 : -1;
        string[] diaoluowuArr = diaoluowu.Split('|');
        //print("掉落物  "+ diaoluowuArr.Length);
        for (var i = 0; i < diaoluowuArr.Length; i++) 
        {
            string objName = diaoluowuArr[i].Split('-')[0];
            //掉落几率
            int dljv = int.Parse(diaoluowuArr[i].Split('-')[1]);
            if (jv < dljv)
            {
                GameObject o = GlobalTools.GetGameObjectByName(objName);
                if (IsBoss)
                {
                    //BOSS掉落物 掉落到指定位置  出现在指定位置
                    //魂 和和徽章
                    //这个 bossDieOutPosition 位置 必须在场景中放置 一个点位置
                    if(bossDieOutPos == null&& GlobalTools.FindObjByName("bossDieOutPosition")) bossDieOutPos = GlobalTools.FindObjByName("bossDieOutPosition").transform;
                    if (bossDieOutPos)
                    {
                        o.transform.position = new Vector2(bossDieOutPos.position.x+i*2,bossDieOutPos.position.y);
                    }
                    else
                    {
                        //如果没有 boss出现点 就是开门 后面 继续  掉落物在 后面的场景

                    }
                }else
                {
                    o.transform.position = this.transform.position;
                    o.GetComponent<Wupinlan>().GetXFX(Random.Range(100, 300) * fx);
                    if(IsDieRecord) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, this.name), this);
                    if(IsNeedReSetCameraKuai) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_KUAI_REDUCTION, null), this);
                }
                
            }
        }
    }

    [Header("是否需要 还原 摄像机块")]
    public bool IsNeedReSetCameraKuai = false;



    [Header("是否是BOSS")]
    public bool IsBoss = false;
    [Header("是否是精英")]
    public bool IsJingying = false;
    //如果是boss die 慢动作计时
    int SlowTimesNum = 0;
    bool isBossDie = false;
    void GetSlowAC(float nums = 0.5f)
    {
        Time.timeScale = nums;
    }

    [Header("是否需要  在die的时候 减慢动作")]
    public bool IsNeedDieSlowAC = false;
    void DieSlowAC()
    {
        GetComponent<GameBody>().GetPause(0.5f, 0.2f);
    }


    //其他要处理事件 比如 BOSS血条隐藏 开门关门 ===
    void SlowTime()
    {

        if (!isBossDie) {
            return;
        } 
        if (IsBoss)
        {
            SlowTimesNum++;
            //print("SlowTimesNum   " + SlowTimesNum);
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
                //还原摄像机的 边界块
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_KUAI_REDUCTION, null), this);
            }
        }
    }

    public bool IsDieRecord = false;
    [Header("die 后多久 销毁自己")]
    public float DieDisSelfTime = 2;

    public void DistorySelf()
    {
        StartCoroutine(IEDieDestory(DieDisSelfTime, this.gameObject));
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, this.DieOutDo);
    }
    public IEnumerator IEDieDestory(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(obj, true);
    }


    [Header("特殊 单独的 门处理")]
    public bool IsSpciDoorOpen = false;
    // 加入的 是 里面有 JG_Door2 的 obj
    public GameObject Door1;
    public GameObject Door2;

    [Header("Die后需要处理的门的名字和动作")]
    public string DoorNames;//Men_1-0|Men_2-0
    void DoorDo()
    {
        if (IsSpciDoorOpen)
        {
            if (Door1 && Door1.GetComponent<JG_Door2>()) Door1.GetComponent<JG_Door2>().IsCloseDoor = false;
            if (Door2 && Door2.GetComponent<JG_Door2>()) Door2.GetComponent<JG_Door2>().IsCloseDoor = false;
        }


        if (DoorNames == null|| DoorNames == "") return;
        string[] doorArr = DoorNames.Split('|');
        int Length = doorArr.Length;
        //print("Length    " + Length);
        if (Length == 0) return;
        for(var i = 0; i < Length; i++)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, doorArr[i]), this);
        }
    }

    bool IsDie = false;

	// Update is called once per frame
	void Update () {
        DieOutDo();
        SlowTime();
    }
}
