using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TX_LeiTXs : MonoBehaviour
{
    // Start is called before the first frame update
    public Light2D LeiLight1;
    public Light2D LeiLight2;
    public Light2D LeiLight3;


    public AudioSource LeiAudio1;
    public AudioSource LeiAudio2;
    public AudioSource LeiAudio3;
    public AudioSource LeiAudio4;
    public AudioSource LeiAudio5;

    // Start is called before the first frame update
    void Start()
    {
        AudioList = new List<AudioSource>() {LeiAudio1, LeiAudio2, LeiAudio3, LeiAudio4, LeiAudio5 };
    }

    List<AudioSource> AudioList;


    float StartTimes = 0;
    float JiangeTimes = 3;
    void Leishan()
    {
        StartTimes += Time.deltaTime;
        if (StartTimes >= JiangeTimes)
        {
            if (GlobalTools.GetRandomNum() >= 30)
            {
                print("ls---------------- ");
                if (LeiLight1) LeiLight1.GetComponent<TX_LeiLight>().GetLeishan(AudioList[GlobalTools.GetRandomNum(AudioList.Count-1)]);
                if (LeiLight2) LeiLight2.GetComponent<TX_LeiLight>().GetLeishan(AudioList[GlobalTools.GetRandomNum(AudioList.Count - 1)]);
                if (LeiLight3) LeiLight3.GetComponent<TX_LeiLight>().GetLeishan(AudioList[GlobalTools.GetRandomNum(AudioList.Count - 1)]);
            }
            StartTimes = 0;
            JiangeTimes = 3 + GlobalTools.GetRandomNum(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Leishan();
    }
}
