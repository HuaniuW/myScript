using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_JiguangSaoshe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("激光蓄能 声音")]
    public AudioSource JiguangXuneng;
    [Header("激光持续 扫射声音")]
    public AudioSource JiguangChixuSaoshe;




    [Header("激光引线")]
    public ParticleSystem JiguangYinxian;

    [Header("激光")]
    public ParticleSystem Jiguang;


    public void JiguangYinxianStart()
    {
        //JiguangYinxian.gameObject.SetActive(true);
        JiguangYinxian.Play();
        if (JiguangXuneng && !JiguangXuneng.isPlaying) {
            JiguangXuneng.Play();
        }
        
    }

    public void JiguangYinxianStop()
    {
        //JiguangYinxian.gameObject.SetActive(false);
        JiguangYinxian.Stop();
        if (JiguangXuneng) JiguangXuneng.Stop();
    }


    public void JiguangStart()
    {
        Jiguang.gameObject.SetActive(true);
        Jiguang.Play();
        if (JiguangChixuSaoshe) JiguangChixuSaoshe.Play();
    }

    public void JiguangStop()
    {
        Jiguang.gameObject.SetActive(false);
        Jiguang.Stop();
        if (JiguangChixuSaoshe) JiguangChixuSaoshe.Stop();
    }


   
}
