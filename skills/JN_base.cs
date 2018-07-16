using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_base : MonoBehaviour
{

    public GameObject hitKuai;


    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        ObjectPools.GetInstance().SwpanObject2(hitKuai);
        hitKuai.GetComponent<AtkAttributesVO>().getValue(DataZS.atk_1_v);
    }



}
