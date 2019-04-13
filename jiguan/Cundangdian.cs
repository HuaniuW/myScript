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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            print("存档！！！"+this.name);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_DIAOLUOWU, this.name), this);

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
