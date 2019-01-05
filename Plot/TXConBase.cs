using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TXConBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }

    void OnEnable()
    {
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 0.7f));
    }


    // Update is called once per frame
    void Update () {
		
	}
}
