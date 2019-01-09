using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticlesScale : MonoBehaviour {

    // Use this for initialization
    ParticleSystem[] particles;
    int particlesLength;
    float _rx;
    void Start () {
        particles = GetComponentsInChildren<ParticleSystem>(true);
        particlesLength = particles.Length;
        _rx = transform.localEulerAngles.x;
        //print("---------------------> rx "+_rx);
        //print("length   "+ particlesLength);
        if (particlesLength == 0)
        {
            return;
        }

    }
	

    public void SetParticlesScale(float _sx)
    {
        //print(transform.localEulerAngles);
        //print(transform.localRotation);
        
        var rx = transform.localEulerAngles.x;
        if (transform.localRotation.x >= 0.7) rx = 180 - rx;
        //rx = 1;  rx不能=0；不然不能翻转
        if (rx == 0 || rx == 180) rx = 1;
        if (rx > 180) rx = 180 - rx;
        //rx = 130;
        //print("rx222  " + rx+(rx is float));
        
        //特效 对摄像机的角度修正
        var y = GameObject.Find("/MainCamera").transform.position.y;
        var z = GameObject.Find("/MainCamera").transform.position.z;
        var _y2 = this.transform.position.y;
        var _z2 = this.transform.position.z;
        var du = Mathf.Atan2((_y2 - y), (_z2 - z));
        var jiaodu = du * 180 / Mathf.PI;

        //print(">  "+jiaodu);
        //return;
        if (_sx>0)
        {
            if (rx < 0) {
                rx += 180;
            }
            rx -= jiaodu;
            transform.localEulerAngles = new Vector3(rx, 0, 0);
            //this.transform.localRotation = new Quaternion(80, 0, 0, 1);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            if (rx > 0)
            {
                rx -= 180; 
            }
            rx -= jiaodu;
            //改变 物体的 rotation  可以用localEulerAngles
            transform.localEulerAngles = new Vector3(rx, 0, 0);
            //this.transform.localRotation = new Quaternion(0, 0, 0, 1);
            this.transform.localScale = new Vector3(-1, -1, -1);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
