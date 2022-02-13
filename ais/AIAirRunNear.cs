using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIAirRunNear : MonoBehaviour
{

    [Header("追击类型 1是默认追踪 2是先找点")]
    public int zuijiType = 1;
    // Start is called before the first frame update
    void Start()
    {
        runAway = GetComponent<AIAirRunAway>();
        _airGameBody = GetComponent<AirGameBody>();
        setter = GetComponent<AIDestinationSetter>();
        
        _aiPath = GetComponent<AIPath>();
        _aiPath.canMove = false;
        _obj = GlobalTools.FindObjByName("player");
        if(_obj == null)_obj = GlobalTools.FindObjByName("player_jijia");
        setter.target = _obj.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isBeHiting)
        {
            ResetAll();
        }
    }


    //追击到的距离
    float _zjDistance = 0;
    float _zjDistanceY = 0;
    GameObject _obj;
    AirGameBody _airGameBody;
    AIDestinationSetter setter;
    AIPath _aiPath;

    public void SetZJDistanceAndObj(float zjdisatnce,GameObject obj) {
        _zjDistance = zjdisatnce;
        //_obj = obj;
        //setter.target = _obj.transform;
        //_aiPath.canMove = true;
    }


    public void ResetAll()
    {
        IsStartMoveY = false;
        CSDistanceY = 0;
        isStartXY = false;
        isZhuijiY = false;
        IsZhuijiPosing = false;
        _isZhuijiType3 = false;
        _isGetPos = false;
        setter.ReSetAll();
        runAway.ReSetAll();
        _aiPath.canMove = false;
        this.GetComponent<GameBody>().IsJiasu = false;
        if (_csSpeed!=0) _aiPath.maxSpeed = _csSpeed;
        _csSpeed = 0;
        IsQuXianToPoint = false;
    }

    public void TurnToPlayer()
    {
        if (this.transform.position.x > _obj.transform.position.x)
        {
            _airGameBody.TurnLeft();
        }
        else
        {
            _airGameBody.TurnRight();
        }
    }


    //public void ResetAll2()
    //{
    //    isStartXY = false;
    //    isZhuijiY = false;
    //    setter.ReSetAll();
    //    //runAway.ReSetAll();
    //    _aiPath.canMove = false;
    //}

    AIAirRunAway runAway;
    public bool Zhuiji(float inDistance = 0,bool IsCanTurnFace = true)
    {
        if (inDistance != 0) _zjDistance = inDistance;
        if(!_aiPath.canMove) _aiPath.canMove = true;
        zhuijiRun(IsCanTurnFace);
        float distances = (transform.position - _obj.transform.position).magnitude;
        //print(transform.position +" 追击中ing -------------   "+ _obj.transform.position+ "   _zjDistance   "+ _zjDistance+ "   ??_aiPath.canMove   "+ _aiPath.canMove+ "    distances   "+ distances);
        if (distances < _zjDistance) {
            ResetAll();
            return true;
        } 
        return false;
    }

    public bool ZhuijiZuoBiao(Vector2 point, float inDistance = 0, bool IsCanTurnFace = true)
    {
        if (inDistance != 0) _zjDistance = inDistance;
        setter.SetV2(point);
        if (!_aiPath.canMove) _aiPath.canMove = true;
        zhuijiRun(IsCanTurnFace);
        if (MoveToPoint(point, inDistance)) return true;
        if ((transform.position - _obj.transform.position).magnitude < _zjDistance)
        {
            ResetAll();
            return true;
        }
        return false;
    }

    ////去到 同Y的位置
    //public bool ZhuijiZuoBiaoTongY(Vector2 point, float inDistance = 0)
    //{
    //    if (inDistance != 0) _zjDistance = inDistance;
    //    setter.SetV2(point);
    //    if (!_aiPath.canMove) _aiPath.canMove = true;
    //    zhuijiRun();
    //    if ((transform.position - _obj.transform.position).sqrMagnitude < _zjDistance)
    //    {
    //        ResetAll();
    //        return true;
    //    }
    //    return false;
    //}



    //原来的速度
    float _csSpeed = 0;

    [Header("是否直接 判断 X的 攻击距离来行动")]
    public bool IsOnlyFindXDistance = false;

    [Header("追击的速度")]
    [Range(10,30)]
    public float zhuijiSpeed = 10;

    [Header("靠近目标点的 距离 误差")]
    public float zuijiPosDisWC = 0.8f;
    public bool ZhuijiPointZuoBiao(Vector2 point, float inDistance = 0,float TempSpeed = 0)
    {
        if (inDistance != 0) zuijiPosDisWC = inDistance;
        setter.SetV2(point);
        if (!_aiPath.canMove) _aiPath.canMove = true;

        if (TempSpeed != 0)
        {
            if (_csSpeed == 0) _csSpeed = _aiPath.maxSpeed;
            _aiPath.maxSpeed = TempSpeed;

        }
        else
        {
            _aiPath.maxSpeed = zhuijiSpeed;
        }

        //print(" _aiPath.maxSpeed不能是0  " + _aiPath.maxSpeed);
       

        zhuijiRun();
        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        //print("thisV2  " + thisV2 + "  point " + point + "  zuijiPosDisWC  "+ zuijiPosDisWC);
        //print(" ---->  "+ (thisV2 - point).sqrMagnitude);

        if (IsOnlyFindXDistance)
        {
            if (Mathf.Abs(thisV2.x - point.x) < zuijiPosDisWC)
            {
                ResetAll();
                return true;
            }
        }
        else
        {
            if ((thisV2 - point).sqrMagnitude < zuijiPosDisWC)
            {
                ResetAll();
                return true;
            }
        }
        return false;
    }


    public bool MoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0, bool IsCanTurnFace = true, bool IsTestHitWall = true, float MaxSpeedX = 18)
    {
        if (inDistance != 0) zuijiPosDisWC = inDistance;
        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        Vector2 v2 = (point - thisV2) * TempSpeed;
        if (MaxSpeedX != 0 && Mathf.Abs(v2.x) > MaxSpeedX)
        {
            v2.x = v2.x > 0 ? MaxSpeedX : -MaxSpeedX;
            v2.y *= MaxSpeedX / Mathf.Abs(v2.x);
        }

        this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = v2;
        this.GetComponent<GameBody>().IsJiasu = true;

        if (IsTestHitWall && IsHitWallByFX(v2, zhijieZhuijiTanCeDistance, thisV2))
        {
            print("   撞墙了！！！！！！！！！！！！！！！！！！  " + IsTestHitWall);
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        float _jinruDis = (thisV2 - point).sqrMagnitude;

        //print("两点间距离 " + _jinruDis+"   ------进入距离的 误差 内  "+ zuijiPosDisWC+ "  inDistance   " + inDistance+"    我的位置  "+thisV2+"    目标点 "+ point);
        //距离小于 误差内 直接结束
        if (_jinruDis < zuijiPosDisWC)
        {
            //print("进入 目标附近！！！");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        return false;
    }

    
    float __tanli = 2;
    float __vy = 0;
    float _upY = 0;
    float _downY = 0;
    float togPosY = 0;
    bool IsQuXianToPoint = false;

    public bool MoveQuXianToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0, bool IsCanTurnFace = true, bool IsTestHitWall = true, float MaxSpeedX = 18)
    {
        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        Vector2 v2 = (point - thisV2) * TempSpeed;

        if (!IsQuXianToPoint)
        {
            IsQuXianToPoint = true;
            __tanli = 0.2f;
            __vy = 10;
            _upY = thisV2.y + 1.5f;
            _downY = thisV2.y - 1.5f;
            togPosY = _upY;
            v2.x = v2.x > 0 ? 2 : -2;
        }
        


        //float __vx = v2.x;

        if (thisV2.y >= _upY)
        {
            //togPosY = _downY;
            __vy = -10;
        }

        if(thisV2.y <= _downY)
        {
            //togPosY = _upY;
            __vy = 10;
        }



        //__vy+= (togPosY - thisV2.y) * __tanli;

        if (MaxSpeedX != 0 && Mathf.Abs(v2.x) > MaxSpeedX)
        {
            v2.x = v2.x > 0 ? MaxSpeedX : -MaxSpeedX;
            //v2.y *= MaxSpeedX / Mathf.Abs(v2.x);
            
        }
        v2.y = __vy;

        this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = v2;
        this.GetComponent<GameBody>().IsJiasu = true;

        if (IsHitWallByFX(v2, zhijieZhuijiTanCeDistance, thisV2))
        {
            print("   撞墙了！！！！！！！！！！！！！！！！！！  ");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        float _jinruDis = (thisV2 - point).sqrMagnitude;
        if (_jinruDis < zuijiPosDisWC)
        {
            print("进入 目标附近！！！");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        return false;
    }



    public float zhijieZhuijiTanCeDistance = 1;
    //直接 去到目标点
    //inDistance 进入 范围直径
    public bool ZhijieMoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0, bool IsCanTurnFace = true, bool IsTestHitWall = true,float MaxSpeedX = 18)
    {

        //print("111***lastAnimationName>? " + _airGameBody.GetDB().animation.lastAnimationName);
        if (inDistance != 0) zuijiPosDisWC = inDistance;
        zhuijiRun(IsCanTurnFace);

        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        //print("  v2-》  "+this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity);
        Vector2 v2 = (point - thisV2) * TempSpeed;// GlobalTools.GetVector2ByPostion(point, thisV2, TempSpeed);

        //print("直接移动速度 v2 >>>>>>>>>>>>>>>>>>  " + v2);
        if (MaxSpeedX!=0&&Mathf.Abs(v2.x) > MaxSpeedX)
        {
            v2.x =v2.x>0? MaxSpeedX:-MaxSpeedX;
            v2.y *= MaxSpeedX / Mathf.Abs(v2.x);
        }

        //print("直接移动速度 v2   "+v2);
        

        //if (v2.sqrMagnitude > (thisV2 - point).sqrMagnitude)
        //{
        //    print("V2 --   " + v2);
        //    v2 = (point - thisV2)* TempSpeed;
        //    print(" 我的位置: " + thisV2 + "   目标坐标:  " + point);
        //    print("V@@@ --   " + v2);
        //}

        this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = v2;
        this.GetComponent<GameBody>().IsJiasu = true;
        //如果 行动中撞墙  直接返回true


        //判断 点 是否 在自己上方  左边 右边  下边  如果是上边 则 下边 不做撞击判断

        if (point.y > thisV2.y)
        {
            //目标点 在我上方
            //print("目标点 在我上方！！！！ ");

        }



        if (IsTestHitWall && IsHitWallByFX(v2, zhijieZhuijiTanCeDistance, thisV2))
        {
            print("   撞墙了！！！！！！！！！！！！！！！！！！  " + IsTestHitWall);
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        //这里要做预判

        float _jinruDis = (thisV2 - point).sqrMagnitude;

        //print("两点间距离 " + _jinruDis+"   ------进入距离的 误差 内  "+ zuijiPosDisWC+ "  inDistance   " + inDistance+"    我的位置  "+thisV2+"    目标点 "+ point);
        //距离小于 误差内 直接结束
        if (_jinruDis < zuijiPosDisWC)
        {
            //print(thisV2+ "  --point  "+ point);
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }
        return false;
    }


    //通过方向 判断是否在 方向上撞墙
    public bool IsHitWallByFX(Vector2 speed,float TCDistance,Vector2 pos)
    {
        Vector2 tcPoint = Vector2.zero;
        Vector2 endPoint1 = Vector2.zero;
        Vector2 endPoint2 = Vector2.zero;


        //print("speed   "+speed);


        float tcUp = speed.y < 0 ? 0 : TCDistance;
        float tcDown = speed.y > 0 ? 0 : TCDistance;

        float tcLeft = speed.x > 0 ? 0 : TCDistance;
        float tcRight = speed.x < 0 ? 0 : TCDistance;


        float _posYXiuzheng = speed.y > 0 ? 1.8f : 0;


        if (speed.x < 0)
        {
            tcPoint = new Vector2(pos.x - TCDistance, pos.y+ _posYXiuzheng);
            
            endPoint1 = new Vector2(tcPoint.x,tcPoint.y+ tcUp);
            endPoint2 = new Vector2(tcPoint.x, tcPoint.y - tcDown);
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) {
                //print( "  hit   x <0 ");
                //Time.timeScale = 0;
                return true;
            }
            
        }else if (speed.x > 0)
        {
            tcPoint = new Vector2(pos.x + TCDistance, pos.y+ _posYXiuzheng);
            endPoint1 = new Vector2(tcPoint.x, tcPoint.y + tcDown);
            endPoint2 = new Vector2(tcPoint.x, tcPoint.y - tcUp);
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) {
                //print("  hit   x >>>>0 ");
                return true;
            }
            
        }

        //print("  speed ??????     "+speed);
        if (speed.y > 0)
        {
            tcPoint = new Vector2(pos.x , pos.y+TCDistance);
            endPoint1 = new Vector2(tcPoint.x + tcRight, tcPoint.y);
            endPoint2 = new Vector2(tcPoint.x - tcLeft, tcPoint.y);
            //print("    上面 碰撞  ");
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) {
                //print("  hit   y*** >>>>0 ");
                return true;
            }
            
        }else if (speed.y < 0)
        {
            tcPoint = new Vector2(pos.x, pos.y - TCDistance);
            endPoint1 = new Vector2(tcPoint.x + tcRight, tcPoint.y);
            endPoint2 = new Vector2(tcPoint.x - tcLeft, tcPoint.y);
            //print("    下面  碰撞----  ");
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2))
            {
                //print("  hit   y*** <<<<<<<<<<<0 ");
                return true;
            }
        }

        return false;
    }


    public void ZJStop()
    {
        ResetAll();
    }


    //跑动作 和转向
    protected void zhuijiRun(bool IsCanTurnFace = true)
    {
        if (_obj!=null&&IsCanTurnFace)
        {
            if (this.transform.position.x < _obj.transform.position.x)
            {
                _airGameBody.TurnRight();
            }
            else
            {
                _airGameBody.TurnLeft();
            }
        }

        //print("****************lastAnimationName>? " + _airGameBody.GetDB().animation.lastAnimationName);
        //print("  #########  弹射 过程！！！！追击 ");
        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) {
            //print("弹射！！！！！！！！");
            return;
        }
        
        GetComponent<AirGameBody>().isRunYing = true;
        _airGameBody.Run();
        //print("****2222222222************lastAnimationName>? " + _airGameBody.GetDB().animation.lastAnimationName);

    }


    public bool IsHitDiBanByFX(Vector2 pos1, Vector2 pos2,bool tiaoshi = false)
    {
        Debug.DrawLine(pos1, pos2, Color.red);
        //if (tiaoshi && Physics2D.Linecast(pos1, pos2, GetComponent<AirGameBody>().groundLayer))
        //{
        //    Time.timeScale = 0;
        //}
        return Physics2D.Linecast(pos1, pos2, GetComponent<AirGameBody>().groundLayer);
    }


    public bool IsHitDiBan(Vector2 pos,float distance,string fx = "lr")
    {
        Vector2 start = pos;
        Vector2 end;
        if(fx == "lr")
        {
            end = new Vector2(start.x, start.y + distance);
            Debug.DrawLine(start, end, Color.blue);
        }
        else
        {
            end = new Vector2(start.x + distance, start.y);
            Debug.DrawLine(start, end, Color.red);
        }
        
        return Physics2D.Linecast(start, end, GetComponent<AirGameBody>().groundLayer);        
    }




    Vector2 FindAtkToPos2(float atkdistance = 0, float atkdistanceY = 0)
    {
        Vector2 v2 = new Vector2(1000, 1000);
        //只找左右点
        //Vector2 PotLeft = new Vector2(1000,1000);
        //Vector2 PotRight = new Vector2(1000, 1000);

        float __x = 0;
        float __y = 0;


        if (this.transform.position.x > _obj.transform.position.x)
        {
            //如果 我在 对象 右边  先找右点
            __x = _obj.transform.position.x + atkdistance;
            __y = _obj.transform.position.y + atkdistanceY;
            v2 = new Vector2(__x,__y);
            if (!IsHitDiBan(v2, tancejuli) && !IsHitDiBan(v2, -tancejuli) && !IsHitDiBan(v2, tancejuli, "") && !IsHitDiBan(v2, -tancejuli, ""))
            {
                return v2;
            }
            else
            {
                __x = _obj.transform.position.x - atkdistance;
                __y = _obj.transform.position.y + atkdistanceY;
                v2 = new Vector2(__x, __y);
                if (!IsHitDiBan(v2, tancejuli) && !IsHitDiBan(v2, -tancejuli) && !IsHitDiBan(v2, tancejuli, "") && !IsHitDiBan(v2, -tancejuli, ""))
                {
                    return v2;
                }
                else
                {
                    return new Vector2(1000,1000);
                }
            }


        }
        else
        {
            __x = _obj.transform.position.x - atkdistance;
            __y = _obj.transform.position.y + atkdistanceY;
            v2 = new Vector2(__x, __y);
            if (!IsHitDiBan(v2, tancejuli) && !IsHitDiBan(v2, -tancejuli) && !IsHitDiBan(v2, tancejuli, "") && !IsHitDiBan(v2, -tancejuli, ""))
            {
                return v2;
            }
            else
            {
                __x = _obj.transform.position.x + atkdistance;
                __y = _obj.transform.position.y + atkdistanceY;
                v2 = new Vector2(__x, __y);
                if (!IsHitDiBan(v2, tancejuli) && !IsHitDiBan(v2, -tancejuli) && !IsHitDiBan(v2, tancejuli, "") && !IsHitDiBan(v2, -tancejuli, ""))
                {
                    return v2;
                }
                else
                {
                    return new Vector2(1000, 1000);
                }
            }
        }
    }

   
    
    Vector2 FindAtkToPos(float atkdistance = 0, float atkdistanceY = 0)
    {
        Vector2 v2 = new Vector2(1000, 1000);
        Vector2 v3 = new Vector2(1000, 1000);
        //先左后右？    按朝向 来
        if (this.transform.position.x > _obj.transform.position.x)
        {
            //如果在右边
            if (Mathf.Abs(this.transform.position.x - _obj.transform.position.x) < atkdistance)
            {
                //在射程内  直接找上下位置就可以了
                v2 = new Vector2(this.transform.position.x, _obj.transform.position.y);
            }
            else
            {
                v2 = new Vector2(_obj.transform.position.x + atkdistance, _obj.transform.position.y);
                //v3 绕后了的点

            }

            v3 = new Vector2(_obj.transform.position.x - atkdistance, _obj.transform.position.y);

        }
        else
        {
            if (Mathf.Abs(this.transform.position.x - _obj.transform.position.x) < atkdistance)
            {
                //在射程内  直接找上下位置就可以了
                v2 = new Vector2(this.transform.position.x, _obj.transform.position.y);
            }
            else
            {
                v2 = new Vector2(_obj.transform.position.x - atkdistance, _obj.transform.position.y);
                //v3 绕后了的点
                
            }
            v3 = new Vector2(_obj.transform.position.x + atkdistance, _obj.transform.position.y);
            //判断  点周围 是否有碰撞
        }

        if (atkdistanceY != 0)
        {
            if (this.transform.position.y > _obj.transform.position.y)
            {
                //目标在 我  下方
                v2 = new Vector2(v2.x, _obj.transform.position.y + atkdistanceY);
            }
            else
            {
                v2 = new Vector2(v2.x, _obj.transform.position.y - atkdistanceY);
            }

            if (Mathf.Abs(this.transform.position.y - _obj.transform.position.y) < atkdistanceY)
            {
                v2 = new Vector2(v2.x, this.transform.position.y);
            }

            v3 = new Vector2(v3.x,v2.y);

        }



        if (IsHitDiBan(v2, tancejuli) || IsHitDiBan(v2, -tancejuli) || IsHitDiBan(v2, tancejuli, "") || IsHitDiBan(v2, -tancejuli, ""))
        {
            if (v3 != new Vector2(1000, 1000) && (IsHitDiBan(v3, tancejuli) || IsHitDiBan(v3, -tancejuli) || IsHitDiBan(v3, tancejuli, "") || IsHitDiBan(v3, -tancejuli, "")))
            {
                return new Vector2(1000, 1000);
            }
            else
            {
                return v3;
            }

        }

        return v2;
    }



    public bool GoToMoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0)
    {
        zhuijiRun();

        Vector2 thisV2 = this.transform.position;
        Vector2 v2 = (point - thisV2) * TempSpeed;
        if (v2.x < 1 || v2.y < 1)
        {
            v2 *= 2;
        }
        this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = v2;
        this.GetComponent<AirGameBody>().IsJiasu = true;

        if (GetComponent<AirGameBody>().IsHitWall)
        {
            print(" ??????   jinlaile ??  ");
            return true;
        }

        //这里要做预判

        print("------------------距离：   "+Vector2.Distance(thisV2, point) + "  ----范围    " + inDistance+"  速度：   "+v2+"   当前速度  " + this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity);
        //距离小于 误差内 直接结束
        //Vector2.Distance(thisV2,point)
        if ((thisV2 - point).sqrMagnitude < inDistance)
        {
            print(" thisV2  " + thisV2 + "    toPos   " + point);
            print(" inDistance "+ inDistance);
            this.GetComponent<AirGameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            return true;
        }
        return false;
    }


    bool IsInAtkDistance(float inDistance)
    {
        return Vector2.Distance(this.transform.position, _obj.transform.position) <= inDistance;
    }



    //探测距离
    float tancejuli = 0.5f;


    bool isStartXY = false;
    bool isZhuijiY = true;
    int nums = 0;

    bool IsZhuijiPosing = false;
    public Transform BiaoJiYPos;
    bool _isZhuijiType3 = false;
    bool _isGetPos = false;

    //靠XY来追击 不是寻路
    public bool ZhuijiXY(float atkdistance = 0,int type = 1,float atkDistanceY = 0) {
        //print("????? patk atkdistance     " + atkdistance + " isZhuijiY  "+ isZhuijiY+ "   --------------isStartXY  "+ isStartXY);
        if (_zjDistance ==0) _zjDistance = atkdistance;
        //_zjDistanceY = 0;
        _zjDistanceY = atkDistanceY;

        //print(" _zjDistanceY >    " + _zjDistanceY);

        if (!isStartXY)
        {
            isStartXY = true;
            nums = GlobalTools.GetRandomNum();
        }


        //纯寻路的 追击
        //1.找到位置点  判断位置点和位置点周围 是否 碰到墙壁  找不到直接返回去   触发无法到达 取消 AI动作



        //类型4
        if (zuijiType == 4)
        {
            //直接选点目标位置 判断 位置高度 能否容纳自己   判断 上和下 是否会碰撞 
            //判断 是否 中间 有障碍物 （用定点 和 底点连线判断）         有障碍物 启动自动寻路
            //如果 触地 目标点改为  (tag.x,self.y+0.2f)
            //如果 x1-x2<=atkD  攻击

            return false;
        }
        

        if (zuijiType == 3)
        {
            if(!_isZhuijiType3 &&this.transform.position.y< BiaoJiYPos.position.y) {
                v2 = new Vector2(this.transform.position.x, BiaoJiYPos.position.y+2);
                ZhuijiPointZuoBiao(v2);
            }
            else
            {
                if (this.transform.position.y > BiaoJiYPos.position.y)
                {
                    print("我已经 超过了Y轴的 位置");
                }
                else
                {
                    print("又 低于 Y了---->>>>????????");
                }


                _aiPath.canMove = false;
                if (!_isZhuijiType3)
                {
                    _isZhuijiType3 = true;
                }


                if (!_isGetPos)
                {
                    _isGetPos = true;
                    
                }


                print("atkdistance   "+ atkdistance+ "  _zjDistanceY  "+ _zjDistanceY);
                v2 = FindAtkToPos(atkdistance-2, _zjDistanceY-2);
                print("寻找点  " + v2);

                bool _isInPos = GoToMoveToPoint(v2, 0.2f, 10);

                print("攻击范围  " + atkdistance);
                print( "我的位置    "+ this.transform.position+" 敌人位置  "+ _obj.transform.position + " 距离  "+ Vector2.Distance(this.transform.position, _obj.transform.position));
                

                if (IsInAtkDistance(atkdistance))
                {
                    ResetAll();
                    print("进入了 攻击范围！");
                    return true;
                }
                else
                {
                    if (_isInPos)
                    {
                        print("没有进入 攻击范围 但是 进入了 寻找点  重新开始 寻找点");
                        _isGetPos = false;
                    }
                  
                }
            }
            return false;
        }


        //找点 追击
        if(zuijiType == 2)
        {
            //if (!IsZhuijiPosing)
            //{
            //    IsZhuijiPosing = true;
            //    v2 = FindAtkToPos(atkdistance, _zjDistanceY);
            //}
            v2 = FindAtkToPos2(atkdistance, _zjDistanceY);

            if (v2 == new Vector2(1000, 1000))
            {
                print(" 取消动作！！！！！！ ");
                IsZhuijiPosing = false;
                //找不到 目标点 直接 取消动作
                ResetAll();
                GetComponent<AIAirBase>().QuXiaoAC();
                GetComponent<AIAirBase>().ReSetAll2();
                return false;
            }
            else
            {
                

                bool _isToPos = ZhuijiPointZuoBiao(v2);

                //print("  找到追击点    " + v2+ "  _isToPos    "+ _isToPos);

                //if (_isToPos)
                //{
                //    if(Mathf.Abs(this.transform.position.x - _obj.transform.position.x)> _zjDistance)
                //    {
                //        _isToPos = false;
                //        IsZhuijiPosing = false;
                //    }
                //}

                return _isToPos;
                //_aiPath.canMove = true;
                //setter.SetV2(v2);

            }

        }

        //print("?????>>>>>>>>>>>>>>>>>!!!!!");

        //2.寻路 

        

        //-------各个方向 碰壁后 使用寻路-------------
        if (!isZhuijiY && (runAway.IsHitTop || runAway.IsHitDown || runAway.IsHitQianmain)) {
            isZhuijiY = true;
            print("---------------------------------------------------------->   寻路Y");
            _aiPath.canMove = true;
        } 

        if (isZhuijiY)
        {
            return ZhuijiY();
        }
        //----------------------------------------

        isZhuijiY = false;

        //普通移动 （非寻路）
        if (nums < 30)
        {
            return XianYhouX();
        }
        else if (nums < 60)
        {
            return XianXhouY();
        }
        else
        {
            return Tongshi();
        }
    }

    Vector2 v2;
    public bool ZhuijiY()
    {
        //print("------------------------------------------------进入 Y 寻路");
        //找到点
        if (this.transform.position.x < _obj.transform.position.x)
        {
            v2 = new Vector2(_obj.transform.position.x - _zjDistance + 0.5f, _obj.transform.position.y);
        }
        else
        {
            v2 = new Vector2(_obj.transform.position.x + _zjDistance - 0.5f, _obj.transform.position.y);
        }

        if (_zjDistanceY != 0) {
            if (this.transform.position.y < _obj.transform.position.y)
            {
                //在目标下面
                v2 = new Vector2(v2.x, _obj.transform.position.y - _zjDistanceY);
            }
            else
            {
                //在目标上面
                v2 = new Vector2(v2.x, _obj.transform.position.y + _zjDistanceY);
            }

            if (Mathf.Abs(this.transform.position.y - _obj.transform.position.y) < _zjDistanceY)
            {
                v2 = new Vector2(v2.x, this.transform.position.y);
            }
        }
      
        

        

        //这里还要判断 点位置 是否 碰到墙壁



        //启动寻路
        setter.SetV2(v2);

        //空中怪 跑的动作 和转向
        zhuijiRun();
        if (!_aiPath.canMove)_aiPath.canMove = true;
        //print(_obj.transform.position+"   ---??---- "+ transform.position);
        //print((_obj.transform.position - transform.position).magnitude + "  ? "+ _zjDistance+"-------------------------------------- "+ Mathf.Abs(transform.position.y - _obj.transform.position.y));
        if ((_obj.transform.position - transform.position).magnitude <= _zjDistance &&Mathf.Abs(transform.position.y - _obj.transform.position.y)<=0.6f)
        {
            //print("over 追击结束！！！！！！");
            setter.ReSetAll();
            ResetAll();
            return true;
        }

        return false;
    }

















    bool XianYhouX()
    {
        if (!GetMoveNearY(_zjDistanceY)) return false;
        if (!GetMoveNearX(_zjDistance)) return false;
        return true;
    }


    bool XianXhouY()
    {
        if (!GetMoveNearX(_zjDistance)) return false;
        if (!GetMoveNearY(_zjDistanceY)) return false;
        return true;
    }


    bool Tongshi()
    {
        if (!GetMoveNearX(_zjDistance) || !GetMoveNearY(GetComponent<AIAirBase>().flyYSpeed)) return false;
        return true;
    }


    bool IsStartMoveY = false;
    float CSDistanceY = 0;
    //Y靠近
    public bool GetMoveNearY(float _moveDistance, float speedY = 0)
    {
        if (speedY == 0) speedY = _airGameBody.moveSpeedY;

        if (!IsStartMoveY)
        {
            IsStartMoveY = true;
            CSDistanceY = Mathf.Abs(_obj.transform.position.y - transform.position.y);
        }

        float __speedY = speedY;
        __speedY *= Mathf.Abs(_obj.transform.position.y - transform.position.y) / CSDistanceY;
        if (__speedY > speedY) __speedY = speedY;

        if (_obj.transform.position.y - transform.position.y > _moveDistance)
        {
            //目标在上 向上移动
            //print("   上移动 ");
            _airGameBody.RunY(__speedY);
            return false;
        }
        else if (_obj.transform.position.y - transform.position.y < -_moveDistance)
        {
            //目标在下 向下移动
            //print("   下移动 ！！！！！");
            _airGameBody.RunY(-__speedY);
            return false;
        }
        else
        {
            IsStartMoveY = false;
            CSDistanceY = 0;
            return true;
        }
    }



    public  bool GetMoveNearX(float distance)
    {

        if (_obj.transform.position.x - transform.position.x > distance)
        {
            //目标在右
            _airGameBody.RunRight(_airGameBody.moveSpeedX);
            return false;
        }
        else if (_obj.transform.position.x - transform.position.x < -distance)
        {
            //目标在左
            _airGameBody.RunLeft(_airGameBody.moveSpeedX);
            return false;
        }
        else
        {

            return true;
        }
    }

}
