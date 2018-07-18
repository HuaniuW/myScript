using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_base : MonoBehaviour
{

    GameObject hitKuai;
    JN_Date jn_date;

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
	
    }

	public void GetPositionAndTeam(Vector3 _position, float team,float _sacaleX)
    {
        jn_date = GetComponent<JN_Date>();
        AtkAttributesVO _atkVVo = GetComponent<AtkAttributesVO>();
        //偷懒 兼容 如果给予了 全局旧的 Dic 就用旧的 否则用新的
        if (GetComponent<JN_Date>().atk_date != null)
        {
            _atkVVo.GetValue(GetComponent<JN_Date>().atk_date);
        }
        else {
            _atkVVo.GetValue(DataZS.atk_1_v);
        }
        _atkVVo.team = team;
        //根据数据 获取新的位置 
        _position = new Vector3(_position.x + _atkVVo._xdx * _sacaleX, _position.y + _atkVVo._xdy, _position.z);
        //指定特效位置
        this.transform.position = _position;
        gameObject.transform.localScale = new Vector3(-_sacaleX, transform.localScale.y, transform.localScale.z);

        

        if (jn_date._type == "2"|| jn_date._type == "3") {
            //2 持续型
            GetKuai(_atkVVo, _position);
            jn_date.moveXSpeed = Mathf.Abs(jn_date.moveXSpeed);
            jn_date.moveXSpeed *= gameObject.transform.localScale.x;
            StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(hitKuai,jn_date._disTime));
        }
        //GetKuai(_atkVVo, _position);
        //StartCoroutine(ObjectPools.GetInstance().IEDestory2(hitKuai));
        StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(gameObject, jn_date.TXDisTime));
    }

    public void disObj()
    {
        ObjectPools.GetInstance().DestoryObject2(hitKuai);
        ObjectPools.GetInstance().DestoryObject2(gameObject);
    }

    void GetKuai(AtkAttributesVO _atkVVo,Vector3 _position)
    {
        hitKuai = Resources.Load("jn_fk") as GameObject;
        hitKuai = ObjectPools.GetInstance().SwpanObject2(hitKuai);
        hitKuai.transform.parent = this.transform;
        //指定 碰撞块位置
        hitKuai.transform.position = _position;
        //指定 特效方向
        hitKuai.transform.localScale = new Vector3(_atkVVo._scaleW, _atkVVo._scaleH, hitKuai.transform.localScale.z);
    }

    private void Update()
    {
        if (jn_date!=null &&(jn_date._type == "2"|| jn_date._type == "3"))
        {
            //2 持续型
            //3 持续并且碰到就消失
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(jn_date.moveXSpeed,jn_date.moveYSpeed);
            print(transform.GetComponent<Rigidbody2D>().velocity);
        }
    }



}
