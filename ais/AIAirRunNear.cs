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
        setter.target = _obj.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        isStartXY = false;
        isZhuijiY = false;
        setter.ReSetAll();
        runAway.ReSetAll();
        _aiPath.canMove = false;
        this.GetComponent<GameBody>().IsJiasu = false;
        if (_csSpeed!=0) _aiPath.maxSpeed = _csSpeed;
        _csSpeed = 0;
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
    public bool Zhuiji(float inDistance = 0)
    {
        if (inDistance != 0) _zjDistance = inDistance;
        if(!_aiPath.canMove) _aiPath.canMove = true;
        zhuijiRun();
        float distances = (transform.position - _obj.transform.position).sqrMagnitude;
        //print(transform.position +"  -------------   "+ _obj.transform.position+ "   _zjDistance   "+ _zjDistance+ "   ??_aiPath.canMove   "+ _aiPath.canMove+ "    distances   "+ distances);
        if (distances < _zjDistance) {
            ResetAll();
            return true;
        } 
        return false;
    }

    public bool ZhuijiZuoBiao(Vector2 point,float inDistance = 0)
    {
        if (inDistance != 0) _zjDistance = inDistance;
        setter.SetV2(point);
        if (!_aiPath.canMove) _aiPath.canMove = true;
        zhuijiRun();
        if ((transform.position - _obj.transform.position).sqrMagnitude < _zjDistance)
        {
            ResetAll();
            return true;
        }
        return false;
    }

    //原来的速度
    float _csSpeed = 0;


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
        if ((thisV2 - point).sqrMagnitude < zuijiPosDisWC)
        {
            ResetAll();
            return true;
        }
        return false;
    }


    public float zhijieZhuijiTanCeDistance = 1;
    //直接 去到目标点
    public bool ZhijieMoveToPoint(Vector2 point, float inDistance = 0, float TempSpeed = 0)
    {
        if (inDistance != 0) zuijiPosDisWC = inDistance;
        zhuijiRun();

        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        //print("  v2-》  "+this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity);
        Vector2 v2 = (point - thisV2) * TempSpeed;// GlobalTools.GetVector2ByPostion(point, thisV2, TempSpeed);
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

        if(IsHitWallByFX(v2, zhijieZhuijiTanCeDistance, thisV2))
        {
            print("   撞墙了！！！！！！！！！！！！！！！！！！  ");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            ResetAll();
            return true;
        }

        //这里要做预判

       
        //距离小于 误差内 直接结束
        if ((thisV2 - point).sqrMagnitude < zuijiPosDisWC)
        {
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

        if (speed.x < 0)
        {
            tcPoint = new Vector2(pos.x - TCDistance, pos.y);
            endPoint1 = new Vector2(tcPoint.x,tcPoint.y+TCDistance);
            endPoint2 = new Vector2(tcPoint.x, tcPoint.y - TCDistance);
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) return true;
        }else if (speed.x > 0)
        {
            tcPoint = new Vector2(pos.x + TCDistance, pos.y);
            endPoint1 = new Vector2(tcPoint.x, tcPoint.y + TCDistance);
            endPoint2 = new Vector2(tcPoint.x, tcPoint.y - TCDistance);
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) return true;
        }

        //print("  speed ??????     "+speed);
        if (speed.y > 0)
        {
            tcPoint = new Vector2(pos.x , pos.y+TCDistance);
            endPoint1 = new Vector2(tcPoint.x + TCDistance, tcPoint.y);
            endPoint2 = new Vector2(tcPoint.x - TCDistance, tcPoint.y);
            //print("    上面 碰撞  ");
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) return true;
        }else if (speed.y < 0)
        {
            tcPoint = new Vector2(pos.x, pos.y - TCDistance);
            endPoint1 = new Vector2(tcPoint.x + TCDistance, tcPoint.y);
            endPoint2 = new Vector2(tcPoint.x - TCDistance, tcPoint.y);
            //print("    下面  碰撞----  ");
            if (IsHitDiBanByFX(tcPoint, endPoint1) || IsHitDiBanByFX(tcPoint, endPoint2)) return true;
        }

        return false;
    }


    public void ZJStop()
    {
        ResetAll();
    }


    //跑动作 和转向
    void zhuijiRun()
    {
        if (this.transform.position.x < _obj.transform.position.x)
        {
            _airGameBody.TurnRight();
        }
        else
        {
            _airGameBody.TurnLeft();
        }

        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) return;
        GetComponent<AirGameBody>().isRunYing = true;
        _airGameBody.Run();
      
    }


    public bool IsHitDiBanByFX(Vector2 pos1, Vector2 pos2)
    {
        Debug.DrawLine(pos1, pos2, Color.red);
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
   
    
    Vector2 FindAtkToPos(float atkdistance = 0, float atkdistanceY = 0)
    {
        Vector2 v2 = new Vector2(1000, 1000);
        Vector2 v3 = new Vector2(1000, 1000);
        //先左后右？    按朝向 来
        if (this.transform.position.x > _obj.transform.position.x)
        {
            //怪在左边

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

    //探测距离
    float tancejuli = 0.5f;


    bool isStartXY = false;
    bool isZhuijiY = true;
    int nums = 0;

   

    //靠XY来追击 不是寻路
    public bool ZhuijiXY(float atkdistance = 0,int type = 1,float atkDistanceY = 0) {
        //print("????? patk atkdistance     " + atkdistance + " isZhuijiY  "+ isZhuijiY+ "   --------------isStartXY  "+ isStartXY);
        if (_zjDistance ==0) _zjDistance = atkdistance;
        _zjDistanceY = 0;
        _zjDistanceY = atkDistanceY;

        //print(" _zjDistanceY >    " + _zjDistanceY);

        if (!isStartXY)
        {
            isStartXY = true;
            nums = GlobalTools.GetRandomNum();
        }


        //纯寻路的 追击
        //1.找到位置点  判断位置点和位置点周围 是否 碰到墙壁  找不到直接返回去   触发无法到达 取消 AI动作

        if(zuijiType == 2)
        {
            v2 = FindAtkToPos(atkdistance, _zjDistanceY);
            if (v2 == new Vector2(1000, 1000))
            {
                print(" 取消动作！！！！！！ ");
                //找不到 目标点 直接 取消动作
                GetComponent<AIAirBase>().QuXiaoAC();
                return false;
            }
            else
            {
                //print("  找到追击点    "+v2);
                return ZhuijiPointZuoBiao(v2);
                //_aiPath.canMove = true;
                //setter.SetV2(v2);

            }

        }

        //print("?????>>>>>>>>>>>>>>>>>!!!!!");

        //2.寻路 

        

        //-------各个方向 碰壁后 使用寻路-------------
        if (!isZhuijiY && (runAway.IsHitTop || runAway.IsHitDown || runAway.IsHitQianmain)) {
            isZhuijiY = true;
            //print("---------------------------------------------------------->   寻路Y");
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

    //Y靠近
    public bool GetMoveNearY(float _moveDistance, float speedY = 0)
    {
        if (speedY == 0) speedY = _airGameBody.moveSpeedY;
        if (_obj.transform.position.y - transform.position.y > _moveDistance)
        {
            //目标在上 向上移动

            _airGameBody.RunY(speedY);
            return false;
        }
        else if (_obj.transform.position.y - transform.position.y < -_moveDistance)
        {
            //目标在下 向下移动
            _airGameBody.RunY(-speedY);
            return false;
        }
        else
        {

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
