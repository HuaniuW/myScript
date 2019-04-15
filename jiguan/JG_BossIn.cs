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
		
	}
    //关联boss
    public string BossName;
    [Header("关联门1的名字")]
    public string Door1Name;
    [Header("关联门2的名字")]
    public string Door2Name;
    [Header("遇见boss音效")]
    public AudioSource SeeBossAudio;
    [Header("遇见boss背景音乐")]
    public AudioSource SeeBossBGAudio;
    //防止重复碰撞播放
    bool IsPlayerIn = false;

    //关联boss是否存在 不存在就销毁自身
    void IsBeingBoss()
    {
        if (BossName == null)
        {
            DistorySelf();
        }
        else
        {
            if(GlobalTools.FindObjByName(BossName)==null) DistorySelf();
        }
        
    }

    //关联门 碰撞关门
    void CloseDoor()
    {
        if (Door1Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door1Name + "-0"), this);
        if (Door2Name != null) ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR, Door2Name + "-0"), this);
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
            SeeBossAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossAudio.Play();
        }

        if (SeeBossBGAudio)
        {
            SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Play();
        }
    }

    //战斗打完停止 播放音乐
    void SeeBossBGAudioStop()
    {
        if (SeeBossBGAudio)
        {
            SeeBossBGAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            SeeBossBGAudio.Stop();
        }
    }


    void ShowBossLiveBar()
    {
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, ShowSelf);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_OUT, BossName), this);
    }


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsPlayerIn&&Coll.tag == "Player")
        {
            IsPlayerIn = true;
            //碰撞了 关门 
            CloseDoor();
            //显示 字幕
            ShowSeeBossTxt();
            //遇Boss音效
            //Boss战音乐
            SeeBossAudioPlay();
            //显示Boss血条
            ShowBossLiveBar();

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
