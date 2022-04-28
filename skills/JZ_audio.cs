using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class JZ_audio : MonoBehaviour {
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource hit3;
    // Use this for initialization
    void Start () {
        
	}
    
    public void PlayAudio(string AudioName,string name = "")
    {
        //(this[AudioName] as AudioSource)
        //AudioName = "hit1";
        AudioSource cAudio = GetDicSSByName(AudioName, this);
        //print(AudioName + "       " + cAudio);
        print(" yyyyyyyyy thisposition " + this.transform.position+"   behit name "+ name);
        if (cAudio)
        {
			cAudio.volume = 0.8f * GlobalSetDate.instance.GetSoundEffectValue();
            //cAudio.PlayOneShot();
            //cAudio.PlayOneShot(cAudio.clip);
            //cAudio.GetComponent<AudioSource>().Play();
            //print("-------->   "+cAudio.name);
            //cAudio.GetComponent<SoundTest>().APlay();
            //print("------------------>    "+cAudio.GetComponent<AudioSource>());
            //cAudio.GetComponent<AudioSource>().Play();
            //cAudio.Stop();
            cAudio.Play();
        }

    }

    public AudioSource GetDicSSByName(string _name, System.Object obj)
    {
        Type type = obj.GetType();
        //FieldInfo 映射 动态获取属性
        FieldInfo fieldInfo = type.GetField(_name);
        //print("  type " + type+ "    fieldInfo  "+ fieldInfo);
        if (fieldInfo == null) return null;
        return fieldInfo.GetValue(obj) as AudioSource;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
