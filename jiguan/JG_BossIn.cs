using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_BossIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IsBeingBoss();
    }
	
	// Update is called once per frame
	void Update () {
        YanChiStart();
    }
    //关联boss
    public string BossName;
    [Header("关联门1的名字  发送状态0")]
    public string Door1Name;
    [Header("关联门2的名字  发送状态0")]
    public string Door2Name;
    [Header("遇见boss音效")]
    public AudioSource SeeBossAudio;
    [Header("遇见boss背景音乐")]
    public AudioSource SeeBossBGAudio;
    [Header("改变摄像机边界")]
    public string ChangeKuaiName = "kuang1";


    [Header("特殊 遇见boss龙  吼声")]
    public AudioSource LongHou;


    [Header("摄像机是否跟随玩家和boss距离改变Z")]
    public bool IsChangeZByBossAndPlayerDistance = false;
    //防止重复碰撞播放
    bool IsPlayerIn = false;

    [Header("是否是在BOSS中使用  如果不是 就是没有boss 防止自己删除自己")]
    public bool IsInBoss = true;
    //关联boss是否存在 不存在就销毁自身



    [Header("boss 机关门1")]
    public GameObject Door1;
    [Header("boss 机关门2")]
    public GameObject Door2;

    void IsBeingBoss()
    {
        if (Boss && !Boss.activeSelf) return;
        if (!IsInBoss) return;
        if (BossName == null)
        {
            DistorySelf();
        }
        else
        {
            if (GlobalTools.FindObjByName(BossName) == null) {
                DistorySelf();
            }
            else
            {
                //关门
               
                //延迟几秒 boss开始行动
                //IsSatrtYC = true;
            }
        }
        
    }

    bool IsSatrtYC = false;
    public float YCTimes = 2.5f;
    float YCJiShi = 0;
    void YanChiStart()
    {
        if (IsSatrtYC)
        {
            YCJiShi += Time.deltaTime;
            if(YCJiShi>= YCTimes)
            {
                IsSatrtYC = false;
                CloseDoor();
                //显示 字幕
                ShowSeeBossTxt();
                //遇Boss音效
                //Boss战音乐
                SeeBossAudioPlay();
                //显示Boss血条
                ShowBossLiveBar();

                DistorySelf();
            }
        }
    }


   



    private void OnDisable()
    {
        //print("我被消除了！？？？？？？");
    }

    [Header("1 下 0上")]
    public int OpenOrClose = 0;
    //关联门 碰撞关门
    void CloseDoor()
    {
        if (Door1Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door1Name + "-"+ OpenOrClose), this);
        if (Door2Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door2Name + "-"+ OpenOrClose), this);


        if (Door1)
        {
            //Door1.
        }

    }

    //遇见boss显示的字幕
    void ShowSeeBossTxt()
    {

    }

    //播放遇到Boss的音效
    void SeeBossAudioPlay()
    {
        if (SeeBossAudio)
        {
            //SeeBossAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossAudio.Play();
        }

        if (SeeBossBGAudio)
        {
            //SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Play();
        }


        if (LongHou)
        {
            LongHou.Play();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.4"), this);
        }
    }

    //战斗打完停止 播放音乐
    void SeeBossBGAudioStop()
    {
        if (SeeBossBGAudio)
        {
            //SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Stop();
        }
    }


    void ShowBossLiveBar()
    {
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, ShowSelf);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_OUT, BossName), this);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_RUN_AC_2, ""), this);
        //if(IsChangeZByBossAndPlayerDistance) GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBossAndMaxZPos(GlobalTools.FindObjByName(BossName));
        if (ChangeKuaiName!="kuang1"&&ChangeKuaiName!="")GlobalTools.FindObjByName("MainCamera").GetComponent<CameraController>().GetBounds(GlobalTools.FindObjByName(ChangeKuaiName).GetComponent<BoxCollider2D>(),true);
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "y"), this);
    }


    [Header("boss")]
    public GameObject Boss;

    [Header("boss 出现位置")]
    public Transform BossOutPos;

    public bool IsBossInPos = false;

    public string otherEvent;
    [Header("boss出现时候 显示特效")]
    public ParticleSystem TX_ShowBossTX;


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (IsSatrtYC) return;
        if(BossOutPos && Boss && !IsBossInPos)
        {
            IsBossInPos = true;
            TX_ShowBossTX.Play();
            Boss.transform.position = BossOutPos.position;

        }


        //return;
        if (!IsPlayerIn&&Coll.tag == "Player")
        {
            IsPlayerIn = true;
            //碰撞了 关门 
            CloseDoor();

            if (otherEvent != "")
            {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.JG_OTHER_EVENT, otherEvent), this);
            }

            if (IsInBoss)
            {
                //显示 字幕
                ShowSeeBossTxt();
                //遇Boss音效
                //Boss战音乐
                SeeBossAudioPlay();
                //显示Boss血条
                ShowBossLiveBar();

               

                if(!IsSatrtYC)DistorySelf();
            }
            

        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {

        if (Coll.tag == "Player")
        {
        }
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {

        //print("Trigger - C");
    }

    public void DistorySelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }
}
