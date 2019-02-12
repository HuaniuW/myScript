using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetDate : MonoBehaviour {
    public static GlobalSetDate instance;
    
    // Use this for initialization
    static GlobalSetDate()
    {
        //这个好像只要调用就会触发 类同名函数
        if (instance) return;
        GameObject go = new GameObject("GlobalDates");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<GlobalSetDate>();
        //print("?????");
    }

    public string playerPosition = "";
    public string screenName = "";
    public bool IsChangeScreening = false;

    Vector3 playerInScreenPosition;

    public Vector3 GetPlayerInScreenPosition()
    {
        string[] sArray = playerPosition.Split('_');
        playerInScreenPosition = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), 0);
        //print("位置   "+ playerInScreenPosition);
        return playerInScreenPosition;
    }


    float SoundEffect = 1f;
    public float GetSoundEffectValue()
    {
        return SoundEffect;
    }
    public void GetUpSoundEffectValue(float v = 0.1f)
    {
        SoundEffect += v;
        SoundEffect = SoundEffect <= 1 ? SoundEffect: 1;
    }
    public void GetDownSoundEffectValue(float v = 0.1f)
    {
        SoundEffect -= v;
        SoundEffect = SoundEffect >= 0 ? SoundEffect : 0;
    }


    void Start()
    {
        //Debug.Log("Start");
    }

    public void DoSomeThings()
    {
        //Debug.Log("DoSomeThings");
    }

   

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetUpSoundEffectValue();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            GetDownSoundEffectValue();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.OPEN_DOOR,"3"),this);
        }

    }
}
