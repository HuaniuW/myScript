using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_FeiDao : MonoBehaviour {
    public float fly_distance = 10;
    public ParticleSystem tuowei;
    float fly_speed = -100;
    bool _isStartFly = false;
    Vector2 startPos = Vector2.zero;
	// Use this for initialization
	void Start () {
        ShowFeiDao();

        
    }

    public void ShowFeiDao() {
        _isStartFly = true;
        startPos = this.transform.position;
        GetComponent<JN_base>()._hitKuai.SetActive(true);


       

    }
	
	// Update is called once per frame
	void Update () {
        if (_isStartFly)
        {

            /**
             if (Mathf.Abs(this.transform.position.x - startPos.x) >= fly_distance)
             {
                 _isStartFly = false;
                 Globals.feidaoPos = this.transform.position;
                 //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                 GetComponent<JN_base>()._hitKuai.SetActive(false);
                 //ObjectPools.GetInstance().DestoryObject2(gameObject);
             }
             else
             {
                 this.transform.position = new Vector2(this.transform.position.x + fly_speed, this.transform.position.y);
             }*/



            if (Mathf.Abs(this.transform.position.x - startPos.x) >= fly_distance)
            {
                _isStartFly = false;
                //Globals.feidaoPos = this.transform.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<JN_base>()._hitKuai.SetActive(false);
                //ObjectPools.GetInstance().DestoryObject2(gameObject);
            }
            else
            {
                if (gameObject.GetComponent<Rigidbody2D>() != null) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(fly_speed, 0);
                //if (gameObject.GetComponent<Rigidbody2D>() != null) gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left*10);
            }

        }
    }
}
