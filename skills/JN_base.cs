﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_base : MonoBehaviour
{

    GameObject hitKuai;
    JN_Date jn_date;
    public GameObject atkObj;
    public GameObject _hitKuai;

        // Use this for initialization
    void Start()
    {
        
        //绕过 位置无效的bug
       // hitKuai = ObjectPools.GetInstance().SwpanObject2(Resources.Load("fk") as GameObject);
        //hitKuai.transform.position = new Vector3(0, 200, 0);
        //StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitKuai));
    }





    private void OnEnable()
    {
        //transform.Find("jn_fk").GetComponent<HitKuai>().CanHit();
        IsCanMove = true;
    }


    float sacaleX;
    public void GetPositionAndTeam(Vector3 _position, float team,float _sacaleX,GameObject obj,bool isSkill = false)
    {
        //print(obj.transform.position.y + "  -  " + GameObject.Find("/MainCamera").transform.position.y);
        atkObj = obj;
        jn_date = GetComponent<JN_Date>();
        if (jn_date == null)return;
        jn_date.team = team;
        sacaleX = _sacaleX;
        //根据数据 获取新的位置 
        _position = new Vector3(_position.x + jn_date._xdx * _sacaleX, _position.y + jn_date._xdy, _position.z);
        //print( jn_date.name+"    xdx "+  jn_date._xdx     + "   、、、、///////////////////----------------------------------_position     "+ _position);
        //指定特效位置
        this.transform.position = _position;
		//print("jn_date._scaleW     "+jn_date._scaleW);
		//print();
		//this.transform.localScale = new Vector3(jn_date._scaleW,jn_date._scaleH,1);
        //TX_weizhiyupan();
        if (!isSkill)
        {
            //print("-------------------------->>>>????   "+obj.name+ "  _sacaleX   " + _sacaleX);
            ShowHitFK();
        }
        else {
            if (_hitKuai != null) {
                //print("   ----------->  2222222  _hitKuai   "+_hitKuai+"   name "+_hitKuai.name  + "  ?  "+ _hitKuai.GetComponent<HitKuai>());
                _hitKuai.GetComponent<HitKuai>().GetTXObj(this.gameObject, true);
            }
            else
            {
                //print("   3333333 ");
                ShowHitFK();
            }
            
        }

        //特效缩放大小
        //this.transform.localScale = new Vector3(jn_date._scaleW, jn_date._scaleH, 1);


        //特效方向
        if (GetComponent<MyParticlesScale>() != null && GetComponent<MyParticlesScale>().IsUseThis) {
            if (_sacaleX > 0)
            {
                GetComponent<MyParticlesScale>().SetParticlesScale(1);
                this.transform.localScale = new Vector3(jn_date._scaleW, jn_date._scaleW, jn_date._scaleW);
            }
            else
            {
                GetComponent<MyParticlesScale>().SetParticlesScale(-1);
                this.transform.localScale = new Vector3(-jn_date._scaleW, -jn_date._scaleW, -jn_date._scaleW);
            }
        }
        


        if (jn_date._type == "2"|| jn_date._type == "3") {
            //2 持续型
            //GetKuai(jn_date, _position);
            jn_date.moveXSpeed = Mathf.Abs(jn_date.moveXSpeed);
            jn_date.moveXSpeed *= -_sacaleX;
            //StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(hitKuai,jn_date._disTime));
        }

        //GetKuai(_atkVVo, _position);
        //StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitKuai));
        if(!IsControlDis) StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, jn_date.TXDisTime));
    }

    [Header("是否控制消失 如果是false 这开发者自己控制 什么时候销毁")]
    public bool IsControlDis = false;


    //显示碰撞方块  开始是将碰撞块放进特效 和特效一起 但是移动会导致 碰撞快 连续撞击 所以动态调用
    void ShowHitFK()
    {
        GameObject hitFK = ObjectPools.GetInstance().SwpanObject2(Resources.Load("hit_fk") as GameObject);
        hitFK.GetComponent<HitKuai>().GetTXObj(this.gameObject,false, sacaleX);
        hitFK.transform.localScale = new Vector3(jn_date.hitKuaiSW, jn_date.hitKuaiSH, 1);
        Vector3 nv3 = new Vector3(this.transform.position.x - sacaleX*jn_date.hitKuai_xdx, this.transform.position.y+jn_date.hitKuai_xdy, this.transform.position.z);
        hitFK.transform.position = nv3;
        //print("动态创建碰撞快------------->hitFK pos  "+nv3);
    }


    public void DisObj()
    {
        ObjectPools.GetInstance().DestoryObject2(gameObject);
    }

    void GetKuai(JN_Date jn_date, Vector3 _position)
    {
        hitKuai = Resources.Load("jn_fk") as GameObject;
        hitKuai = ObjectPools.GetInstance().SwpanObject2(hitKuai);
        hitKuai.transform.parent = this.transform;
        //指定 碰撞块位置
        hitKuai.transform.position = _position;
        //指定 特效方向
        hitKuai.transform.localScale = new Vector3(jn_date._scaleW, jn_date._scaleW, hitKuai.transform.localScale.z);
    }

    private void Update()
    {
        //先判断是否是普通攻击  是的话才跟随  还是加快特效做 待定
        //TX_gensui();
        //print("jn_date._type      "+ jn_date._type);
        if (!jn_date) return;
        if (jn_date!=null &&(jn_date._type == "2"|| jn_date._type == "3"|| jn_date._type == "4"))
        {
            //2 持续型
            //3 持续并且碰到就消失
            //4 不跟随 攻击者会被反弹
            //print("2222222222222222222222222222222222222     "+jn_date.moveXSpeed);
            if (gameObject.GetComponent<Rigidbody2D>() != null && IsCanMove) {
                //print(">>>>>>>>>>>>>>>>>   "+ jn_date.moveXSpeed);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(jn_date.moveXSpeed, jn_date.moveYSpeed);
            }
            
            //print(transform.GetComponent<Rigidbody2D>().velocity);
        }
        else
        {
            TX_gensui();
        }
    }

    bool IsCanMove = true;
    //停止速度运行
    public void StopSD() {
        IsCanMove = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //print("//////////////////////////////////////////////////////////////////////禁止 速度");   
    }


    public bool isYuPan = true;
    void TX_weizhiyupan()
    {

        print(atkObj.GetComponent<Rigidbody2D>().velocity);
        //return;
        if (isYuPan&&atkObj)
        {
            Vector3 np = atkObj.transform.position;
            double vx = atkObj.GetComponent<Rigidbody2D>().velocity.x*0.01;
            double vy = atkObj.GetComponent<Rigidbody2D>().velocity.y * 0.3;
            Vector3 _position = new Vector3(np.x + jn_date._xdx * sacaleX + (float)vx, np.y + jn_date._xdy+ (float)vy, np.z);
            //指定特效位置
            this.transform.position = _position;
        }
    }

    void TX_gensui()
    {
        if (atkObj)
        {
            //print("np     atkObj   " + atkObj+"   jn_date "+jn_date);
            Vector3 np = atkObj.transform.position;
            //根据数据 获取新的位置 
            Vector3 _position = new Vector3(np.x + jn_date._xdx * sacaleX, np.y + jn_date._xdy, np.z);
            //指定特效位置
            this.transform.position = _position;
        }
    }



}
