using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cundangdian : MonoBehaviour {
    public AudioSource saveAudio;
    // Use this for initialization
    public ParticleSystem huixuetexiao;



    

    void Start () {
        if (huixuetexiao != null)
        {
            huixuetexiao.Stop();
        }
        GetOtherCheckRemoveSelf();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    [Header("是否被点亮 判定")]
    public string RemoveSelfOtherCheckStr = "";

    [Header("神树 点亮的 特效")]
    public ParticleSystem TX_Dianliang;

    //是否能存档
    bool IsCanCundang = true;


    void GetOtherCheckRemoveSelf()
    {
        //print(" 存档点 自我检测---------------***************：  "+ RemoveSelfOtherCheckStr);
        if (RemoveSelfOtherCheckStr == "") return;
        string value = GlobalSetDate.instance.GetOtherDateValueByKey(RemoveSelfOtherCheckStr);
        //print("  value值是多少？  "+ value);
        if (value == "1")
        {
            if(TX_Dianliang) TX_Dianliang.Stop();
            //RemoveSelf();
            IsCanCundang = false;
        }
    }

    void RemoveSelf()
    {
        DestroyImmediate(this.gameObject, true);
    }


    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsCanCundang) return;
        if (Coll.tag == "Player")
        {
            //print("存档！！！"+this.name);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_DIAOLUOWU, "C_cundangdian"), this);

            //播放存档声音
            if (saveAudio != null)
            {
                saveAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
                saveAudio.Play();
            }
            //显示回血特效
            if (huixuetexiao != null)
            {
                huixuetexiao.Play();
            }


            //显示存档提示
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_SAVEING, null), this);

            GlobalSetDate.instance.GetSave();


        }
    }
}
