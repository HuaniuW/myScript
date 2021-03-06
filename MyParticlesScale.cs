﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticlesScale : MonoBehaviour {

    // Use this for initialization
    ParticleSystem[] particles;
    int particlesLength;
    [Header("是否使用此类 用的话 会改变朝向")]
    public bool IsUseThis = true;

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


    public void SetParticlesScale2(float _sx)
    {
        if (!IsUseThis) return;
        //print(transform.localEulerAngles);
        //print(transform.localRotation);
        //切换场景的时候 特效还在 这里判断摄像机
        if (GameObject.Find("/MainCamera") == null) return;

        var rx = transform.localEulerAngles.x;
        var ry = transform.localEulerAngles.y;
        ry = 0;
        if (transform.localRotation.x >= 0.7) rx = 180 - rx;
        //rx = 1;  rx不能=0；不然不能翻转
        if (rx == 0 || rx == 180) rx = 1;
        if (rx > 180) rx = 180 - rx;
        //rx = 130;
        //print("rx222  " + rx+(rx is float));

        //特效 对摄像机的角度修正
        var y = GameObject.Find("/MainCamera").transform.position.y;
        var z = GameObject.Find("/MainCamera").transform.position.z;
        var x = GameObject.Find("/MainCamera").transform.position.x;
        var _y2 = this.transform.position.y;
        var _z2 = this.transform.position.z;
        var _x2 = this.transform.position.x;
        var du = Mathf.Atan2((_y2 - y), (_z2 - z));
        var jiaodu = du * 180 / Mathf.PI;

        var duy = Mathf.Atan2((_x2 - x), (_z2 - z));
        var jiaoduy = duy * 180 / Mathf.PI;

        //if (Globals.isDebug) print(">  "+jiaoduy);
        //return;
        float __scaleX = GetScaleXByJL(x, _x2);
        float __scaleY = GetScaleYByJL(y, _y2);
        if (_sx > 0)
        {
            if (rx < 0)
            {
                rx += 180;
            }
            //rx -= jiaodu;
            //ry += jiaoduy;
            //ry = 0;
            //print(ry + "  >  " + jiaoduy);
            transform.localEulerAngles = new Vector3(rx, ry, 0);
            //this.transform.localRotation = new Quaternion(80, 0, 0, 1);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            if (rx > 0)
            {
                rx -= 180;
            }
            //rx -= jiaodu;
            //ry += jiaoduy;
            //ry = -40;
            //print(ry+"    "+jiaoduy);
            //改变 物体的 rotation  可以用localEulerAngles
            transform.localEulerAngles = new Vector3(rx, ry, 0);
            //this.transform.localRotation = new Quaternion(0, 0, 0, 1);
            this.transform.localScale = new Vector3(-1, -1, -1);
        }
        //if (Globals.isDebug) print("角度   "+ transform.localEulerAngles);
    }



    public void SetParticlesScale(float _sx)
    {
        if (!IsUseThis) return;
        //print(transform.localEulerAngles);
        //print(transform.localRotation);
        //切换场景的时候 特效还在 这里判断摄像机
        if (GameObject.Find("/MainCamera") == null) return;

        var rx = transform.localEulerAngles.x;
        var ry = transform.localEulerAngles.y;
        ry = 0;
        if (transform.localRotation.x >= 0.7) rx = 180 - rx;
        //rx = 1;  rx不能=0；不然不能翻转
        if (rx == 0 || rx == 180) rx = 1;
        if (rx > 180) rx = 180 - rx;
        //rx = 130;
        //print("rx222  " + rx+(rx is float));
        
        //特效 对摄像机的角度修正
        var y = GameObject.Find("/MainCamera").transform.position.y;
        var z = GameObject.Find("/MainCamera").transform.position.z;
        var x = GameObject.Find("/MainCamera").transform.position.x;
        var _y2 = this.transform.position.y;
        var _z2 = this.transform.position.z;
        var _x2 = this.transform.position.x;
        var du = Mathf.Atan2((_y2 - y), (_z2 - z));
        var jiaodu = du * 180 / Mathf.PI;

        var duy = Mathf.Atan2((_x2 - x), (_z2 - z));
        var jiaoduy = duy * 180 / Mathf.PI;

        //if (Globals.isDebug) print(">  "+jiaoduy);
        //return;
        float __scaleX = GetScaleXByJL(x,_x2);
        float __scaleY = GetScaleYByJL(y, _y2);
        if (_sx>0)
        {
            if (rx < 0) {
                rx += 180;
            }
            rx -= jiaodu;
            ry += jiaoduy;
            //ry = 0;
            //print(ry + "  >  " + jiaoduy);
            transform.localEulerAngles = new Vector3(rx, ry, 0);
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
            ry += jiaoduy;
            //ry = -40;
            //print(ry+"    "+jiaoduy);
            //改变 物体的 rotation  可以用localEulerAngles
            transform.localEulerAngles = new Vector3(rx, ry, 0);
            //this.transform.localRotation = new Quaternion(0, 0, 0, 1);
            this.transform.localScale = new Vector3(-1, -1, -1);
        }
        //if (Globals.isDebug) print("角度   "+ transform.localEulerAngles);
    }


    float GetScaleXByJL(float creamX,float thisX)
    {
        float jl = Mathf.Abs(creamX - thisX);
        jl = jl >= 8 ? 8 : jl;
        float sj = jl / 8 * 0.2f;
        return 1 - sj;
    }


    float GetScaleYByJL(float creamY, float thisY)
    {
        float jl = Mathf.Abs(creamY - thisY);
        jl = jl >= 8 ? 8 : jl;
        float sj = jl / 8 * 0.2f;
        return 1 - sj;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
