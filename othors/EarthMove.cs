using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveSpace();

    }

    float speedX = -0.0007f;

    void MoveSpace()
    {
        this.transform.position = new Vector2(this.transform.position.x + speedX, this.transform.position.y);
    }
}
