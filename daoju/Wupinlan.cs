using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wupinlan : MonoBehaviour {
    protected Rigidbody2D playerRigidbody2D;
    // Use this for initialization
    void Start () {
       
        //随机方向扩散
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetXFX(float num)
    {
        if (!playerRigidbody2D) playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.AddForce(Vector2.right * num);
        playerRigidbody2D.AddForce(Vector2.up * 200);
    }

    public void DistorySelf()
    {
        //this.gameObject.SetActive(false);
        //var collider = gameObject.GetComponent<Collider>();
        //if (collider)
        //{
        //    Debug.Log("collider exists");
        //    Destroy(collider);
        //}
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }

}
