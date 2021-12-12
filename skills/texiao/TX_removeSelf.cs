using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_removeSelf : MonoBehaviour
{

    [Header("特效粒子1")]
    public ParticleSystem TX_lizi1;

    [Header("特效粒子2")]
    public ParticleSystem TX_lizi2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float removeTimes = 0.5f;

    private void OnEnable()
    {
        //if(GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().Play();
        //StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, removeTimes));

        RemoveSelftimes = 0;



    }

    public void GetStart()
    {
        RemoveSelftimes = 0;
    }




    float RemoveSelftimes = 0;


    void RemoveselfByTimes()
    {
        if (TX_lizi1 && TX_lizi1.isStopped)
        {
            if (TX_lizi1) TX_lizi1.loop = true;
            if (TX_lizi2) TX_lizi2.loop = true;
            ObjectPools.GetInstance().DestoryObject2(this.gameObject);
        }



        if (removeTimes != 0)
        {
            RemoveSelftimes += Time.deltaTime;
            if(RemoveSelftimes>= removeTimes)
            {
                RemoveSelftimes = 0;

                if (TX_lizi1) TX_lizi1.loop = false;
                if (TX_lizi2) TX_lizi2.loop = false;

                if (TX_lizi1 == null)
                {
                    ObjectPools.GetInstance().DestoryObject2(this.gameObject);
                }

                //ObjectPools.GetInstance().DestoryObject2(this.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        RemoveselfByTimes();
    }
}
