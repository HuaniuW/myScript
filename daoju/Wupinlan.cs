using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wupinlan : MonoBehaviour {
    protected Rigidbody2D playerRigidbody2D;
    // Use this for initialization
    void Start () {
        if (!playerRigidbody2D) playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.AddForce(Vector2.up * 200);
        //随机方向扩散
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DistorySelf()
    {
        this.gameObject.SetActive(false);
        //var collider = gameObject.GetComponent<Collider>();
        //if (collider)
        //{
        //    Debug.Log("collider exists");
        //    Destroy(collider);
        //}
    }


}
