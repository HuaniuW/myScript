using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAudios : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetAudioNameArrStr();
        GetScreenAudioByCustomsDate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //监听战斗音乐
    void AddFightBGAuido()
    {
        //战斗音乐开始
        //战斗音乐结束
    }

    void RemoveFightBGAudio()
    {

    }

    //public string TheScreenAudioName = "Audio_BgFengsheng_1-Audio_BgGangqing_1";
    string AudioNameArrStr = "Audio_BgFengsheng_1";
    void GetAudioNameArrStr()
    {
        //做一个 关卡 音乐数据 的对照表

        //AudioNameArrStr = TheScreenAudioName;
    }


    //根据关卡 获取 场景音乐
    void GetScreenAudioByCustomsDate()
    {
        //date 风声+钢琴 eg:   fengsheng1-gangqing1
        List<string> AudioNameStrList = new List<string>(AudioNameArrStr.Split('-'));
        if (AudioNameStrList.Count == 0) return;
        foreach (string AudioName in AudioNameStrList)
        {
            GameObject _audioObj = GlobalTools.GetGameObjectByName(AudioName);
            if (_audioObj != null) {
                _audioObj.GetComponent<AudioSource>().Play();
                _audioObj.GetComponent<AudioSource>().loop = true;
            }
            
        }
    }

    private void OnDisable()
    {
        print("ScreenAudio  音乐停播！！！！");
    }

}
