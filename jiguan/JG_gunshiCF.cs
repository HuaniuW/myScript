using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_gunshiCF : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
        if (Coll.tag == "Player")
        {
            this.transform.parent.GetComponent<JG_gunshi>().GetStart();
        }
    }
}
