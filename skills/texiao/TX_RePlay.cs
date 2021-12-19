using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_RePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("显示 特效")]
    public ParticleSystem TX;

    private void OnEnable()
    {
        if (TX && TX.isStopped) TX.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
