using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      

        //Audio1.GetComponent<AudioControl>().DisAutioObj();
        //Audio2 = GlobalTools.GetGameObjectByName(AudioName);
        //DontDestroyOnLoad(Audio2);
        //Audio2.GetComponent<AudioControl>().StopAudio();


        

    }

    private void Awake()
    {
        Audio1 = GlobalTools.GetGameObjectByName(AudioName);
        DontDestroyOnLoad(Audio1);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_TEMP_AUDIO, ChangeTempAudio);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_AUDIO, ChangeAudio);
    }

 

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_TEMP_AUDIO, ChangeTempAudio);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_AUDIO, ChangeAudio);
    }


    private void ChangeAudio(UEvent evt)
    {
        //throw new NotImplementedException();
        string str = evt.eventParams.ToString();
        if (str == AudioName) return;
        if (Audio1) DestroyImmediate(Audio1);
        Audio1 = GlobalTools.GetGameObjectByName(str);
        DontDestroyOnLoad(Audio1);
        AudioName = str;
    }


    private void ChangeTempAudio(UEvent evt)
    {
        //throw new NotImplementedException();
        string str = evt.eventParams.ToString();
        print("yy 事件--->  "+ str);
        if(str == "stop")
        {
            AudioStop();
            return;
        }else if (str == "ZJStop")
        {
            Audio1ZJStop();
            return;
        }

        PlayAudio(str);
    }

    [Header("默认 音乐名字")]
    public string AudioName = "Audio_BgYinyue1";

    GameObject Audio1;
    GameObject Audio2;
    //正在播放的 音乐ID  1就是Audio1  2就是Audio2
    int PlayingAudioID = 1;



    //播放音乐
    public void PlayAudio(string audioName = "")
    {
        print("  播放的 音乐名字 "+ audioName);
        if(audioName == "")
        {
            Audio1.GetComponent<AudioControl>().PlayAudio();
        }
        else
        {
            Audio1.GetComponent<AudioControl>().ZJStopAudio();
            //DestroyImmediate(Audio1.gameObject);
            Audio2 = GlobalTools.GetGameObjectByName(audioName);
        }
    }


    public void Audio1ZJStop()
    {
        Audio1.GetComponent<AudioControl>().ZJStopAudio();
    }



    //停止音乐 缓动停止
    public void AudioStop(int StopAudioID = 0)
    {
        if(StopAudioID == 1)
        {
            Audio1.GetComponent<AudioControl>().StopAudio();
        }
        else if(StopAudioID == 2)
        {
            Audio2.GetComponent<AudioControl>().StopAudio();
        }
        else
        {
            //2个音乐都停止
            Audio1.GetComponent<AudioControl>().StopAudio();
            if(Audio2)Audio2.GetComponent<AudioControl>().StopAudio();
        }
    }


    //切换音乐



    // Update is called once per frame
    void Update()
    {
        
    }
}
