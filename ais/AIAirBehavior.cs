using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIAirBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _airGameBody = GetComponent<AirGameBody>();
        setter = GetComponent<AIDestinationSetter>();
        _aiPath = GetComponent<AIPath>();
        _aiPath.canMove = false;
    }
    AirGameBody _airGameBody;
    AIDestinationSetter setter;
    AIPath _aiPath;


    public bool isJiansu = false;
    float jiansubili = 0.02f;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleDate>().isBeHiting) ReSetAll();
        if (isJiansu) AirJiansu();
    }

    public void ReSetAll()
    {
        _aiPath.canMove = false;
        setter.ReSetAll();
    }



    public void StartJS()
    {
        isJiansu = true;
    }

    public void StopJS()
    {
        isJiansu = false;
    }
    void AirJiansu()
    {
        //print("jiansu----------------------------------->  减速！！！！！！！！！！！");
        Vector2 sd = _airGameBody.GetPlayerRigidbody2D().velocity;
        float _x = sd.x; 
        float _y = sd.y; 
        _x += (0 - sd.x) * jiansubili; 
        _y+= (0 - sd.y) * jiansubili; 

        if (Mathf.Abs(_x) < 0.5f) _x = 0;
        if (Mathf.Abs(_y) < 0.5f) _y = 0;
        _airGameBody.GetPlayerRigidbody2D().velocity = new Vector2(_x,_y);
        if (_x == 0 && _y == 0) isJiansu = false;
    }

    public bool ZhuijiZuoBiao(Vector2 point, float inDistance = 1)
    {
        setter.SetV2(point);
        if (!_aiPath.canMove) _aiPath.canMove = true;
        if (((Vector2)transform.position - point).sqrMagnitude < inDistance)
        {
            _aiPath.canMove = false;
            return true;
        }
        return false;
    }


    


    //行为 类里面有啥  


    public bool MoveYInDistance(Transform targetObj,float distance, float speedY = 0)
    {
        if (Mathf.Abs(targetObj.position.y - this.transform.position.y) <= distance) return true;
        if (targetObj.position.y > this.transform.position.y)
        {
            //目标在上 向上移动
            _airGameBody.RunY(speedY);
        }
        else
        {
            //目标在下 向下移动
            _airGameBody.RunY(-speedY);
        }
        return false;
    }

    public bool MoveXInDistance(Transform targetObj, float distance, float speedX = 0) {
        if (Mathf.Abs(targetObj.position.x - this.transform.position.x) <= distance) return true;
        if (targetObj.position.x > this.transform.position.x)
        {
            _airGameBody.RunRight(speedX);
        }
        else
        {
            _airGameBody.RunLeft(speedX);
        }
        return false;
    }



    
}
