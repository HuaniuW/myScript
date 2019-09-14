using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luoshi : MonoBehaviour {

	// Use this for initialization
	void Start () {
        myY = this.transform.position.y;
	}

    float myY = 0;
    //下落距离
    float xialuoDistance = 0;

    bool IsDownBegin = true;

    void HideSelf()
    {
        if(Mathf.Abs(this.transform.position.y - myY) > 34)
        {
            IsDownBegin = false;
            Destroy(this.gameObject);
        }
    }
    
	// Update is called once per frame
	void Update () {
        if (IsDownBegin)
        {
            HideSelf();
        }
	}
}
