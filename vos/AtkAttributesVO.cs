using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkAttributesVO : MonoBehaviour
{
    [Header("攻击力")]
    public float atkPower;
    [Header("相对位置x")]
    public float _xdx;
    [Header("相对位置y")]
    public float _xdy;
    [Header("宽")]
    public float _scaleW;
    [Header("高")]
    public float _scaleH;
    [Header("消失时间")]
    public float _disTime;

    public float team;

    

    // Use this for initialization
    void Start () {
		
	}

    public void GetValue(Dictionary<string, float> dict)
    {
        
        this.atkPower = dict["atkPower"];
        this._xdx = dict["_xdx"];
        this._xdy = dict["_xdy"];
        this._scaleW = dict["_scaleW"];
        this._scaleH = dict["_scaleH"];
        //消失时间
        this._disTime = dict["_disTime"];
        //print("shilihua     " + this._w);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
