using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class RoleAudio : MonoBehaviour {
    public AudioSource run1;
    public AudioSource run2;
    public AudioSource run3;
    public AudioSource jumpUp;
    public AudioSource jumpUp2;
    public AudioSource downOnGround;
    public AudioSource dodge1;
    public AudioSource jumpHitWall;
    public AudioSource wallJump;
    public AudioSource hitWallDown;
    public AudioSource longhou;
    public AudioSource die_1;

    [Header("被攻击的 音效1名字")]
    public string AUDIOBEHIT_1 = "BeHit_1";
    [Header("被攻击的 音效2名字")]
    public string AUDIOBEHIT_2 = "BeHit_2";

    public AudioSource BeHit_1;
    public AudioSource BeHit_2;
   
    public AudioSource AudioAtk_1;
    public AudioSource AudioAtk_2;
    public AudioSource SkillAudio_1;


    [Header("技能不能释放的 提示音")]
    public AudioSource Alert_1;

    // Use this for initialization
    void Start () {
		
	}

    public void PlayAudio(string AudioName) {
        //(this[AudioName] as AudioSource)
        //print("-----------------------------------------------------------> diaoluode audio:  "+AudioName);
        AudioSource cAudio = GetDicSSByName(AudioName, this);
        if (cAudio)
        {
            cAudio.volume = 0.2f * GlobalSetDate.instance.GetSoundEffectValue();
            if (this.name!="B_Huolong" && (AudioName=="run1"|| AudioName == "run2"|| AudioName =="run3")) cAudio.volume =  0.1f*GlobalSetDate.instance.GetSoundEffectValue();
            cAudio.Play();
        }

    }

    public void PlayAudioYS(string AudioName)
    {
        //(this[AudioName] as AudioSource)
        //print("-----------------------------------------------------------> diaoluode audio:  "+AudioName);
        AudioSource cAudio = GetDicSSByName(AudioName, this);
        if (cAudio)
        {
            //cAudio.volume = GlobalSetDate.instance.GetSoundEffectValue();
            //if (AudioName == "run1" || AudioName == "run2" || AudioName == "run3") cAudio.volume = 0.3f * GlobalSetDate.instance.GetSoundEffectValue();
            cAudio.Play();
        }

    }

    public void PlayHitWallDown(bool isDown)
    {
        if (!hitWallDown) return;
        hitWallDown.volume = 0.2f * GlobalSetDate.instance.GetSoundEffectValue();
        if (isDown)
        {
            if (!hitWallDown.isPlaying) hitWallDown.Play();
        }
        else
        {
            if (hitWallDown.isPlaying) hitWallDown.Stop();
        }

    }

    public AudioSource GetDicSSByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        FieldInfo fieldInfo = type.GetField(_name);
        //print("  type " + type+ "    fieldInfo  "+ fieldInfo);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as AudioSource;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
