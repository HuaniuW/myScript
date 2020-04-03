using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
    private AudioSource m_AudioSource;
    public bool isValueByPlayerDistance = false;
    public float distance = 10;
    GameObject player;
    // Use this for initialization
    void Start () {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        //print("m_AudioSource> " + m_AudioSource);
        m_AudioSource.volume = GlobalSetDate.instance.GetSoundEffectValue();
    }

    void GetValueByPlayerDistance()
    {
        
        if (isValueByPlayerDistance)
        {
            if (player == null)
            {
                player = GlobalTools.FindObjByName("player");
            }

            if(Mathf.Abs(player.transform.position.x - this.transform.position.x)> distance)
            {
                m_AudioSource.volume = 0;
                return;
            }
            m_AudioSource.volume = (1 -Mathf.Abs(player.transform.position.x - this.transform.position.x)/distance)* GlobalSetDate.instance.GetSoundEffectValue();
        }

    }

    private void OnEnable()
    {
        if(m_AudioSource) m_AudioSource.volume = GlobalSetDate.instance.GetSoundEffectValue();
        //print(GlobalSetDate.instance.GetSoundEffectValue());
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.AUDIO_VALUE,audioValue);
    }
        
    public void audioValue(UEvent e)
    {
        m_AudioSource.volume = GlobalSetDate.instance.GetSoundEffectValue();
    }


    void Update()
    {
        GetValueByPlayerDistance();
    }


    private void OnDisable()
    {
        //print(" 声音销毁！！！！！ ");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.AUDIO_VALUE, audioValue);
    }

}
