using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yishan : MonoBehaviour {
    AtkAttributesVO _atkVVo;


	// Use this for initialization
	void Start () {
        _atkVVo = GetComponent<AtkAttributesVO>();
        _atkVVo.GetValue(DataZS.atk_1_v);
	}

    void OnEnable() {
        StartCoroutine(ObjectPools.GetInstance().IEDestory2(gameObject));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
