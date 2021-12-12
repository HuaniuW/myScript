using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_ZidanWeirao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetCenterObj();
    }

    // Update is called once per frame
    void Update()
    {
        Weirao();
    }

    GameObject _centerObj;
    [Header("围绕中心obj 的名字")]
    public string WeiRaoName = "";

    [Header("围绕的 移动速度")]
    public float MoveSpeed = 45;



    Vector3 dir = Vector3.zero;
    float distance = 0;
    //增加 和 减小 distance
    public void ChangeDistance(float add,float MaxDis)
    {
        distance += add;
        if (distance >= MaxDis) distance = MaxDis;
        if (distance <= 1) distance = 1;
    }



    void GetCenterObj()
    {
        if (WeiRaoName != "")
        {
            _centerObj = GlobalTools.FindObjByName(WeiRaoName);
            if (_centerObj == null) return;

            dir = this.transform.position - _centerObj.transform.position;

            distance = Vector3.Distance(transform.position, _centerObj.transform.position);


        }
    }


    public void GetTheCenterObj(GameObject centerObj)
    {
        _centerObj = centerObj;
        if (_centerObj == null) return;
        dir = this.transform.position - _centerObj.transform.position;

        distance = Vector3.Distance(transform.position, _centerObj.transform.position);
    }


   


    public void StartWeirao() { }
    public void StopWeirao() { }


    void Weirao()
    {
        if (IsCanFire)
        {
            FireTimesJishi += Time.deltaTime;
            if(FireTimesJishi>= _FireTimes)
            {
                Fire();
                StartCoroutine(IECanHitWall(1));
                return;
            }
        }


        if (_centerObj == null) return;
        transform.position = _centerObj.transform.position + dir.normalized * distance;
        //Obj2.transform.RotateAround(Obj.transform.position, Obj2.transform.right, 45 * Time.deltaTime);

        //停止 转动 只要停止这个行动就行
        this.transform.RotateAround(_centerObj.transform.position, _centerObj.transform.forward, MoveSpeed * Time.deltaTime);
        dir = transform.position - _centerObj.transform.position;
    }


    public IEnumerator IECanHitWall(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<TX_zidan>().IsCanHitDiban = true;
    }


    


    bool IsFire = false;
    private void Fire()
    {
        if (!IsFire)
        {
            IsFire = true;
            if (!_targetObj) return;



            Vector2 _speed = (_targetObj.transform.position - this.transform.position) * 0.7f;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = _speed;
        }
    }





    void ReSetAll()
    {
        FireTimesJishi = 0;
        IsFire = false;
    }


    bool IsCanFire = false;
    float _FireTimes = 1;
    float FireTimesJishi = 0;

    GameObject _targetObj;
    public void GetTargetObj(GameObject targetObj)
    {
        IsCanFire = true;
        _targetObj = targetObj;
        _FireTimes =  1+ GlobalTools.GetRandomDistanceNums(_FireTimes);


    }

}
