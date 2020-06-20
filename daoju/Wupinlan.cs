using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wupinlan : MonoBehaviour {
    protected Rigidbody2D playerRigidbody2D;
    // Use this for initialization

    public bool isChildCanBeHit = false;     
    void Start () {
       
        //随机方向扩散
    }
	
	// Update is called once per frame
	void Update () {
        if (!isChildCanBeHit && this.transform.GetComponent<Rigidbody2D>().velocity.y == 0) isChildCanBeHit = true;
    }

    public void GetXFX(float num)
    {
        if (!playerRigidbody2D) playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.AddForce(Vector2.right * num);
        playerRigidbody2D.AddForce(Vector2.up * 200);
    }

    public void DistorySelf()
    {
        StartCoroutine(IEDestoryByEnd(this.gameObject));
    }
    public IEnumerator IEDestoryByEnd(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(obj, true);
    }

}
