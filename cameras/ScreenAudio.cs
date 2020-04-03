using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //???
        AudioIn(bgm1);
        //_bgm = bgm1;
        //AudioOut();

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, ScreenChange);
    }

    //接受 切换场景事件 切换场景的时候 声音要渐渐变小


    //全局bgm
    [Header("背景音乐1")]
    public AudioSource bgm1;
    [Header("背景音乐2")]
    public AudioSource bgm2;



    //进入boss界面 战斗音乐开启 原音乐停止    使用场景
    //切换播放
    bool IsChangeAudio = false;
    void ChangeAudio(AudioSource bgm2) {
        //停止播放当前 音频
        //如果当前音频 没有播放 或者 是空  播放音频2
    }

    //播放

    //停止


    private void ResetAll()
    {
        IsAudioIn = false;
        IsAudioOut = false;
        audioValue = 0;
        audioValueB = 0;
    }

    AudioSource _bgm;
    bool IsAudioIn = false;
    float audioValue = 0;
    float audioValueB = 0;
    void AudioIn(AudioSource bgm)
    {
        if (bgm) {
            _bgm = bgm;
            _bgm.volume = audioValue;
            IsAudioIn = true;
        }   
    }

    void GetAudioIn()
    {
        audioValueB += (1 - audioValueB) * 0.01f;
        //print("音量变大    "+audioValueB);
        if (audioValueB >= 0.9f) {
            audioValueB = 1;
            IsAudioIn = false;
        }
        audioValue = audioValueB* GlobalSetDate.instance.GetSoundEffectValue();
        _bgm.volume = audioValue;
    }


    bool IsAudioOut = false;
    void AudioOut()
    {
        if (_bgm)
        {
            IsAudioOut = true;
        }
    }

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_SCREEN, ScreenChange);
    }

    void ScreenChange(UEvent e)
    {
        AudioOut();
    }

    void GetAudioOut()
    {
        audioValueB += (0 - audioValueB) * 0.01f;
        if (audioValueB <= 0.1f)
        {
            audioValueB = 0;
            IsAudioOut = false;
        }
        audioValue = audioValueB * GlobalSetDate.instance.GetSoundEffectValue();
        _bgm.volume = audioValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAudioIn) GetAudioIn();
        if (IsAudioOut) GetAudioOut();
    }
}
