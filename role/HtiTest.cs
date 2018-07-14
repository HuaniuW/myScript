using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HtiTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D Coll)
    {
        //print("A"); //碰到碰撞器時印出"A"
       // print("A:" + Coll.gameObject.name); //印出A:碰撞對象的名字
    }
    void OnCollisionExit2D(Collision2D Coll)
    {
       // print("B"); //離開碰撞器時印出"B"
        //print("B:" + Coll.gameObject.name); //印出B:碰撞對象的名字
    }
    void OnCollisionStay2D(Collision2D Coll)
    {
        //print("C"); //接觸著碰撞器時印出"C"
        //print("C:" + Coll.gameObject.name); //印出C:碰撞對象的名字
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print("Trigger - A");
    }
    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");
    }
}
