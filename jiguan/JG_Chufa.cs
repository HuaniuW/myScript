using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_Chufa : MonoBehaviour
{

    //主要是 触发 战斗音效  


    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}


    bool IsPlayerIn = false;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //return;
        if (!IsPlayerIn && Coll.tag == GlobalTag.Player)
        {
            IsPlayerIn = true;

            //Boss战音乐
            SeeBossAudioPlay();

            DistorySelf();


        }
    }

    [Header("战斗背景 音效")]
    public AudioSource ZhandouBGAudio;

    //播放遇到Boss的音效
    void SeeBossAudioPlay()
    {
        if (ZhandouBGAudio)
        {
            ZhandouBGAudio.Play();
        }

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
