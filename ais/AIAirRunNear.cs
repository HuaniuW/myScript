using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIAirRunNear : MonoBehaviour
{
    
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

    public void ZJStop()
    {
        ResetAll();
    }


    //跑动作 和转向
    void zhuijiRun()
    {
        if (GetComponent<AIChongji>() && GetComponent<AIChongji>().isTanSheing) return;
        GetComponent<AirGameBody>().isRunYing = true;
        _airGameBody.Run();
        if (this.transform.position.x < _obj.transform.position.x)
        {
            _airGameBody.TurnRight();
        }
        else
        {
            _airGameBody.TurnLeft();
        }
    }


    bool isStartXY = false;
    bool isZhuijiY = true;
    int nums = 0;
    //靠XY来追击 不是寻路
    public bool ZhuijiXY(float atkdistance = 0) {
        //print("????? patk atkdistance     " + atkdistance + " isZhuijiY  "+ isZhuijiY+ "   --------------isStartXY  "+ isStartXY);
        if (_zjDistance ==0) _zjDistance = atkdistance;
        if (!isStartXY)
        {
            isStartXY = true;
            nums = GlobalTools.GetRandomNum();
        }

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

        setter.SetV2(v2);

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
