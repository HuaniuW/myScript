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
    }

    public void ResetAll2()
    {
        isStartXY = false;
        isZhuijiY = false;
        setter.ReSetAll();
        //runAway.ReSetAll();
        _aiPath.canMove = false;
    }

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

    [Header("靠近目标点的 距离 误差")]
    public float zuijiPosDisWC = 0.8f;
    public bool ZhuijiPointZuoBiao(Vector2 point, float inDistance = 0)
    {
        if (inDistance != 0) zuijiPosDisWC = inDistance;
        setter.SetV2(point);
        if (!_aiPath.canMove) _aiPath.canMove = true;
        zhuijiRun();
        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        if ((thisV2 - point).sqrMagnitude < zuijiPosDisWC)
        {
            ResetAll();
            return true;
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
   
    
    Vector2 FindAtkToPos(float atkdistance = 0)
    {
        Vector2 v2 = new Vector2(1000, 1000);
        Vector2 v3 = new Vector2(1000,1000);
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
    public bool ZhuijiXY(float atkdistance = 0,int type = 1) {
        //print("????? patk atkdistance     " + atkdistance + " isZhuijiY  "+ isZhuijiY+ "   --------------isStartXY  "+ isStartXY);
        if (_zjDistance ==0) _zjDistance = atkdistance;
        if (!isStartXY)
        {
            isStartXY = true;
            nums = GlobalTools.GetRandomNum();
        }


        //纯寻路的 追击
        //1.找到位置点  判断位置点和位置点周围 是否 碰到墙壁  找不到直接返回去   触发无法到达 取消 AI动作

        if(zuijiType == 2)
        {
            v2 = FindAtkToPos(atkdistance);
            if (v2 == new Vector2(1000, 1000))
            {
                print(" 取消动作！！！！！！ ");
                //找不到 目标点 直接 取消动作
                GetComponent<AIAirBase>().QuXiaoAC();
                return false;
            }
            else
            {
                print("  找到追击点    "+v2);
                return ZhuijiPointZuoBiao(v2);
                //_aiPath.canMove = true;
                //setter.SetV2(v2);

            }

        }

        

        //2.寻路 



        //各个方向 碰壁后 使用寻路
        if (!isZhuijiY && (runAway.IsHitTop || runAway.IsHitDown || runAway.IsHitQianmain)) {
            isZhuijiY = true;
            print("---------------------------------------------------------->   寻路Y");
            _aiPath.canMove = true;
        } 

        if (isZhuijiY)
        {
            return ZhuijiY();
        }
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
            v2 = new Vector2(_obj.transform.position.x-_zjDistance+0.5f, _obj.transform.position.y);
        }
        else
        {
            v2 = new Vector2(_obj.transform.position.x + _zjDistance-0.5f, _obj.transform.position.y);
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
        if (!GetMoveNearY(GetComponent<AIAirBase>().flyYSpeed)) return false;
        if (!GetMoveNearX(_zjDistance)) return false;
        return true;
    }


    bool XianXhouY()
    {
        if (!GetMoveNearX(_zjDistance)) return false;
        if (!GetMoveNearY(GetComponent<AIAirBase>().flyYSpeed)) return false;
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
