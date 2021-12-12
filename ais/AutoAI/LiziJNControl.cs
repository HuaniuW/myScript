using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiziJNControl : MonoBehaviour
{
    //粒子技能控制 如果特效是粒子 这个要获取粒子的 gameObject



    public ParticleSystem lizis;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnEnable()
    {
        if (lizis) {
            lizis.GetComponent<ParticleSystem>().Simulate(0.0f);
            lizis.GetComponent<ParticleSystem>().Play();
        }
        
    }


    public ParticleSystem GetLiziObj()
    {
        return lizis;
    }

}
