﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
    private AudioSource m_AudioSource;
    public bool isValueByPlayerDistance = false;
    public float distance = 10;
    GameObject player;
    public float TheVolume = 1;
    public float YuanshiYinLian = 1;

    public float MaxVolume = 0;
    float _cVolume = 0;

    float CVolume
    {
        get
        {
            return _cVolume;
        }

        set
        {
            _cVolume = value;
            if (MaxVolume != 0)
            {
                if (_cVolume > MaxVolume)
                {
                    _cVolume = MaxVolume;
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        //print("m_AudioSource> " + m_AudioSource);
        CVolume = GlobalSetDate.instance.GetSoundEffectValue();
        m_AudioSource.volume = CVolume;
        YuanshiYinLian = m_AudioSource.volume;
        if (IsGradualValue) m_AudioSource.volume = 0;
    }

    [Header("是否在 距离内 音量不变 超出 就为0")]
    public bool IsInDistanceDontChange = false;

    void GetValueByPlayerDistance()
    {
        
        if (isValueByPlayerDistance)
        {
            if (player == null)
            {
                player = GlobalTools.FindObjByName("player");
                if (player == null)
                {
                    player = GlobalTools.FindObjByName(GlobalTag.PlayerJijiaObj);
                }
            }

            if(player&&Mathf.Abs(player.transform.position.x - this.transform.position.x)> distance)
            {
                m_AudioSource.volume = 0;
                return;
            }

            if (IsInDistanceDontChange)
            {
                return;
            }
            
            if (player) {
                TheVolume = (1 - Mathf.Abs(player.transform.position.x - this.transform.position.x) / distance) * CVolume * YuanshiYinLian;
                m_AudioSource.volume = TheVolume;
                if (GuDingValue != 0 && GlobalSetDate.instance.GetSoundEffectValue() != 0) m_AudioSource.volume = GuDingValue;
            }
           
        }

    }

    [Header("---固定音量大小")]
    public float GuDingValue = 0;

    private void OnEnable()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.AUDIO_VALUE,audioValue);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_SCREEN, ScreenChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, DieOut);

        PlayAudio();
    }






    [Header("音乐是否 切换场景时候 停止播放")]
    public bool IsChangeScreenStop = true;
    void ScreenChange(UEvent e)
    {
        //print("??????????? 切换场景！！！！ ");
        if(!IsChangeScreenStop)return;
        if(m_AudioSource) m_AudioSource.Stop();
    }


    public void audioValue(UEvent e)
    {
        if (!IsGradualValue&& m_AudioSource) {
            CVolume = GlobalSetDate.instance.GetSoundEffectValue();
            m_AudioSource.volume = CVolume;
        }
        
    }

    void Update()
    {
        GetValueByPlayerDistance();
        GradualValue();
        if (IsPlayerDieGraduaMining) AudioValueGraduaMin();
    }


    private void OnDisable()
    {
        //print(" 声音销毁！！！！！ ");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.AUDIO_VALUE, audioValue);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_SCREEN, ScreenChange);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, DieOut);
    }

    [Header("音量 是否 渐进")]
    public bool IsGradualValue = false;
    [Header("音量 渐进 速度值")]
    public float GraduaNums = 0.02f;


    void GradualValue()
    {
        //if (!m_AudioSource.isPlaying) {
        //    return;
        //}
        //print("  当前音量 CVolume  " + CVolume + "   GuDingValue " + GuDingValue);
        if (IsZJStop) {
            if (m_AudioSource && m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
            return;
        }
        
        if (m_AudioSource.isPlaying &&IsGradualValue)
        {
            if (GuDingValue != 0) {
                if (GlobalSetDate.instance.GetSoundEffectValue() > GuDingValue)
                {
                    CVolume = GuDingValue;
                }
            }
            
            m_AudioSource.volume += (CVolume - m_AudioSource.volume) * GraduaNums;
            if (m_AudioSource.volume == CVolume - 0.05f)
            {
                m_AudioSource.volume = CVolume;
                IsGradualValue = false;
            }
        }
    }


    [Header("玩家die 音量 是否 减小到0")]
    public bool IsPlayerDieGraduaMin = false;

    bool IsPlayerDieGraduaMining = false;
    void AudioValueGraduaMin()
    {
        if (m_AudioSource.isPlaying && (IsPlayerDieGraduaMin||IsBossFIGHTAudioDown))
        {
            IsGradualValue = false;
            //m_AudioSource.volume += (CVolume - m_AudioSource.volume) * GraduaNums;
            CVolume += (0 - CVolume) * GraduaNums;
            if (CVolume <= 0.02f)
            {
                CVolume = 0;
                //IsPlayerDieGraduaMin = false;
                IsPlayerDieGraduaMining = false;
                m_AudioSource.Stop();
                //IsBossFIGHTAudioDown = false;
            }
            m_AudioSource.volume = CVolume;
            //print(" *****************m_AudioSource.volume "+ m_AudioSource.volume);
        }
    }

    //渐隐关掉音乐
    public void GetIsPlayerDieGraduaMining()
    {
        IsPlayerDieGraduaMining = true;
    }

    [Header("boss战 关闭音乐")]
    public bool IsBossFIGHTAudioDown = false;



    public void PlayAudio()
    {
        IsZJStop = false;
        CVolume = GlobalSetDate.instance.GetSoundEffectValue();
        if (GuDingValue != 0 && GlobalSetDate.instance.GetSoundEffectValue() != 0) CVolume = GuDingValue;
        if (m_AudioSource) m_AudioSource.volume = CVolume;
        if (IsGradualValue && m_AudioSource&&!m_AudioSource.isPlaying) m_AudioSource.volume = 0;
        if (m_AudioSource && !m_AudioSource.isPlaying) m_AudioSource.Play();
    }

    public void StopAudio()
    {
        IsZJStop = false;
        IsPlayerDieGraduaMining = true;
    }


    bool IsZJStop = false;


    public void ZJStopAudio()
    {
        IsZJStop = true;
    }


    public void DisAutioObj()
    {
        DestroyImmediate(this.gameObject);
    }





    void DieOut(UEvent e)
    {
        if (e.eventParams == null) return;
        //print(e.eventParams.ToString());
        //print((e.target as GameObject).name);
        //print(" yy  audio -------- "+ e.eventParams.ToString());
        if(e.eventParams.ToString() == GlobalTag.Player)
        {
            if (IsPlayerDieGraduaMin)
            {
                //玩家die音量 渐隐
                IsPlayerDieGraduaMining = true;
            }
        }

        if (e.eventParams.ToString() == GlobalTag.BOSS&& IsBossFIGHTAudioDown)
        {
            IsPlayerDieGraduaMining = true;
            return;
        }


        if (e.eventParams.ToString() == GlobalTag.JINGYING)
        {
            IsPlayerDieGraduaMining = true;
        }
    }



}
