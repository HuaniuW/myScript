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
        if (rx == 0 || rx == 180) rx = 1;
        print("rx1  "+rx);
        if (rx > 180) rx = 180 - rx;
        print("rx  "+rx);
        //rx = 130;
        //print("rx222  " + rx+(rx is float));
        //rx = 1;  rx不能=0；不然不能翻转
        //翻转修正 不知道为什么回翻转360度的差 这要减回来不然特效会不匹配方向
       // if (rx > 180) rx = 360 - rx;
        //float crx = 100; 这个是模拟带入固定的角度 来纠正
        //给出视觉差的比 来进一步修正 角度
        //获得主摄像头的 z位置 角度与主摄像头垂直
        //print(Mathf.Atan(3f/4f));
        //Mathf.Atan2()
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
            //print(">0  " + rx);
            if (rx < 0) {
                rx += 180;
            }
            rx -= jiaodu;
            //if (_z2 > z) rx += jiaodu;
            //if (_z2 < z) rx -= jiaodu;
            //rx -= jiaodu;
            //rx = crx;
            //print("rx>0   "+rx);
            transform.localEulerAngles = new Vector3(rx, 0, 0);
            //this.transform.localRotation = new Quaternion(80, 0, 0, 1);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            //print(">0  " + rx);
            if (rx > 0)
            {
                rx -= 180; 
            }
            rx -= jiaodu;
            //if (_z2 > z) rx += jiaodu;
            //if (_z2 < z) rx -= jiaodu;
            //rx = crx - 180;
            //rx -= jiaodu;
            //print("rx<0   " + rx);

            //改变 物体的 rotation  可以用localEulerAngles
            transform.localEulerAngles = new Vector3(rx, 0, 0);
            //this.transform.localRotation = new Quaternion(0, 0, 0, 1);
            this.transform.localScale = new Vector3(-1, -1, -1);
        }
        //print(rx);

        return;
        
        var v1 = this.transform.localScale;
        this.transform.localScale = new Vector3(_sx,1,1);

        
        for (int idx = 0; idx < particlesLength; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            var v3 = particle.transform.localScale;
            print(particle.rotationOverLifetime);
            //print(particle.main.randomizeRotationDirection.ToString());
            particle.transform.localScale = new Vector3(_sx,1  , 1);
            particle.startRotation *= _sx;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
