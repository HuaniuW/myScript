using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_LianDan : MonoBehaviour,ISkill
{
    //连续 子弹攻击目标

    [Header("发射子弹数量")]
    public int BulletNums = 10;

    [Header("子弹发射间隔时间")]
    public float ShotEveryTimes = 0.5f;

    [Header("定高点Y位置")]
    public Transform PointHight;

    //Y是固定的 X距离范围
    float _atkDistanceX = 6;

    
    Vector2 _atkPot = Vector2.zero;

    bool IsGetAtkPot = false;
    Vector2 GetAtkPot()
    {
        if (!IsGetAtkPot)
        {
            IsGetAtkPot = true;
            float potY = PointHight.position.y;
            //这里判断 高点是否存在 不在的话 要自己设计找


            if (this.transform.position.x < _playerObj.transform.position.x)
            {
                _atkPot = new Vector2(_playerObj.transform.position.x - _atkDistanceX, potY);
            }
            else
            {
                _atkPot = new Vector2(_playerObj.transform.position.x + _atkDistanceX, potY);
            }
        }
        return _atkPot;
    }

    AirGameBody _airGameBody;
    RoleDate _roleDate;
    AIAirRunNear _aiAirRunNear;

    bool IsGoInAtkPoint = false;
    void GoInAtkPoint()
    {
        //如果前面 碰到地板 就结束了
        GetAtkPot();
        if(_aiAirRunNear.ZhijieMoveToPoint(_atkPot, 0.6f, 10))
        {
            IsShoting = true;
            IsGoInAtkPoint = false;
        }

    }

    bool IsShoting = false;
    float theFireTimes = 0;
    //已经射击的次数
    float fireTimes = 0;
    void StartFire()
    {
        if (fireTimes >= BulletNums) {
            IsShoting = false;
            return;
        }
        
        //开火
        if(fireTimes == 0)
        {
            fireTimes++;
            Fire();
        }
        theFireTimes += Time.deltaTime;
        if (theFireTimes >= ShotEveryTimes)
        {
            theFireTimes = 0;
            fireTimes++;
            Fire();
        }
    }

    [Header("子弹发射声音")]
    public AudioSource FireAudio;


    [Header("子弹发射点")]
    public Transform BulletShotPot;

    [Header("子弹名字")]
    public string BulletName = "TX_zidan1";
    void Fire()
    {
        if (FireAudio) FireAudio.Play();
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(BulletName) as GameObject);
        zidan.transform.position = BulletShotPot.position;
        zidan.GetComponent<TX_zidan>().IsAtkAuto = true;
    }


    GameObject _playerObj;

    public void GetStart(GameObject gameObj)
    {
        ReSetAll();
        _playerObj = gameObj;
        IsGoInAtkPoint = true;
    }

    public bool IsGetOver()
    {
        if (_airGameBody.IsHitWall||_roleDate.isBeHiting||_roleDate.isDie)
        {
            ReSetAll();
            return true;
        }
        


        if (fireTimes >= BulletNums) {
            return true;
        }

        if (IsGoInAtkPoint) GoInAtkPoint();
        if(IsShoting)StartFire();
        return false;
    }

    public void ReSetAll()
    {
        IsGoInAtkPoint = false;
        IsGetAtkPot = false;
        fireTimes = 0;
        theFireTimes = 0;
        IsGoInAtkPoint = false;
        IsShoting = false;
        _aiAirRunNear.ResetAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        _airGameBody = GetComponent<AirGameBody>();
        _roleDate = GetComponent<RoleDate>();
        _aiAirRunNear = GetComponent<AIAirRunNear>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
