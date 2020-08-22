using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_JumpCut : MonoBehaviour, ISkill
{

    public float TempGravity = 6;
    float _recordGravity = 4.5f;
    bool IsStarting = false;

    
   

    GameObject _gameObj;
    float _playerX = 0;

    public void GetStart(GameObject gameObj)
    {
        //x方向的推力
        //_gameBody.GetPlayerRigidbody2D().velocity;
        //_recordGravity = _gameBody.GetPlayerRigidbody2D().gravityScale;
        //_gameBody.GetPlayerRigidbody2D().gravityScale = TempGravity;

        ReSetAll();
        
        _gameObj = gameObj;
        _playerX = _gameObj.transform.position.x;
        IsStartXV = true;
        print("    --------------------------  >   "+ _playerX);
        if(!IsJumpToPlayerPostion)StartVX();

        //_gameBody.GetPlayerRigidbody2D().velocity = new Vector2(vx, _gameBody.GetPlayerRigidbody2D().velocity.y);
        _gameBody.GetJump();
        _gameBody.Jump();
        //显示 hitkuai
        GetComponent<AI_hitkuaiShenTi>().ShowHitKuaiInAir();

    }

    [Header("跳跃 怪和玩家单位距离 ")]
    public float DistanceXDW = 2;
    public bool IsJumpToPlayerPostion = true;
    public float VXPower = 30;
    void StartVX()
    {
        float powerX = Mathf.Abs(this.transform.position.x - _playerX) / DistanceXDW * 80;
        float vx = this.transform.position.x > _playerX ? -VXPower : VXPower;
        if(IsJumpToPlayerPostion) vx = this.transform.position.x > _playerX ? -powerX : powerX;
        _gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(vx, 0));
    }


    bool IsStartXV = false;
    public bool IsGetOver()
    {
        
        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie || (_gameBody.IsGround&& _gameBody.IsGetStand())) {
            //_gameBody.GetPlayerRigidbody2D().gravityScale = _recordGravity;
            //ReSetAll();
            return true;
        }

        
       


        return false;
    }

    public void ReSetAll()
    {
        IsStartXV = false;
    }



    GameBody _gameBody;

    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
    }


    
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AIBase>().IsTuihuiFangshouquing)
        {
            ReSetAll();
            return;
        }

        if (!GetComponent<RoleDate>().isDie &&IsJumpToPlayerPostion &&IsStartXV && _gameBody.isInAiring&&_gameObj)
        {
            //print("我进来没、、、、、、、、、、、、、、///////////////////////////////////-------------->");
            StartVX();
        }
    }
}
