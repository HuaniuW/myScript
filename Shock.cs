using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour {

    Rigidbody2D skillRigidbody2D;
    public float speedX = 2f;
    // Use this for initialization
    void Start () {
        skillRigidbody2D = this.GetComponent<Rigidbody2D>();
        
        speedX *= this.transform.localScale.x;
        //print("skillRigidbody2D   " + skillRigidbody2D+"   speedX  "+ speedX+"   id "+this.GetInstanceID());
       
    }

   

    void OnEnable()
    {
        //Debug.Log("enable");
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, 2f));
    }

    // Update is called once per frame
    void Update () {
        if (skillRigidbody2D) skillRigidbody2D.velocity = new Vector2(speedX, 0);
    }
}
