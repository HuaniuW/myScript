using System.Collections;
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
    }
    float sacaleX;

    public void GetPositionAndTeam(Vector3 _position, float team,float _sacaleX,GameObject obj,bool isSkill = false)
    {
        //print(obj.transform.position.y + "  -  " + GameObject.Find("/MainCamera").transform.position.y);

        atkObj = obj;
        jn_date = GetComponent<JN_Date>();
        jn_date.team = team;
        sacaleX = _sacaleX;
        //根据数据 获取新的位置 
        _position = new Vector3(_position.x + jn_date._xdx * _sacaleX, _position.y + jn_date._xdy, _position.z);
        //指定特效位置
        this.transform.position = _position;
        //TX_weizhiyupan();
        if (!isSkill)
        {
            ShowHitFK();
        }
        else {
            if(_hitKuai!=null) _hitKuai.GetComponent<HitKuai>().GetTXObj(this.gameObject,true);
        }
        


        if (_sacaleX > 0) {
            GetComponent<MyParticlesScale>().SetParticlesScale(1);
        }
        else
        {
            GetComponent<MyParticlesScale>().SetParticlesScale(-1);
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
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, jn_date.TXDisTime));
    }

    //显示碰撞方块
    void ShowHitFK()
    {
        GameObject hitFK = ObjectPools.GetInstance().SwpanObject2(Resources.Load("hit_fk") as GameObject);
        hitFK.GetComponent<HitKuai>().GetTXObj(this.gameObject);
        Vector3 nv3 = new Vector3(this.transform.position.x - 0.8f * sacaleX, this.transform.position.y, this.transform.position.z);
        hitFK.transform.position = nv3;

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
        hitKuai.transform.localScale = new Vector3(jn_date._scaleW, jn_date._scaleH, hitKuai.transform.localScale.z);
    }

    private void Update()
    {
        //先判断是否是普通攻击  是的话才跟随  还是加快特效做 待定
        //TX_gensui();
        if (jn_date!=null &&(jn_date._type == "2"|| jn_date._type == "3"))
        {
            //2 持续型
            //3 持续并且碰到就消失
            if(gameObject.GetComponent<Rigidbody2D>()!=null) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(jn_date.moveXSpeed,jn_date.moveYSpeed);
            //print(transform.GetComponent<Rigidbody2D>().velocity);
        }
        else
        {
            TX_gensui();
        }
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
            Vector3 np = atkObj.transform.position;
            //根据数据 获取新的位置 
            Vector3 _position = new Vector3(np.x + jn_date._xdx * sacaleX, np.y + jn_date._xdy, np.z);
            //指定特效位置
            this.transform.position = _position;
        }
    }



}
