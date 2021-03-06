﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMask : MonoBehaviour {
    public Transform hideObj;
    // Use this for initialization

    public bool IsOutShow = true;
	void Start () {
        Color t = this.GetComponent<SpriteRenderer>().color;

        this.GetComponent<SpriteRenderer>().color = new Color(t.r, t.g, t.b, 0f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player") {
            //t.gameObject.SetActive(false);
            hideObj.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
        if (Coll.tag == "Player")
        {
            if(IsOutShow) hideObj.gameObject.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");

    }
}
