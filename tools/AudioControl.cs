using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
    private AudioSource m_AudioSource;
    // Use this for initialization
    void Start () {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        //print("m_AudioSource> " + m_AudioSource);
        m_AudioSource.volume = GlobalSetDate.instance.GetSoundEffectValue();
    }

    private void OnEnable()
    {
        if(m_AudioSource) m_AudioSource.volume = GlobalSetDate.instance.GetSoundEffectValue();
        //print(GlobalSetDate.instance.GetSoundEffectValue());
    }

    
}
