using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_JumpCut : MonoBehaviour, ISkill
{

    public float TempGravity = 6;
    float _recordGravity = 4.5f;
    bool IsStarting = false;

    [Header("起跳的 喊声")]
    public AudioSource JumpSound;


    GameObject _gameObj;
    float _playerX = 0;

    public void GetStart(GameObject gameObj)
    {
        //x方向的推力
        //_gameBody.GetPlayerRigidbody2D().velocity;
        //_recordGravity = _gameBody.GetPlayerRigidbody2D().gravityScale;
        //_gameBody.GetPlayerRigidbody2D().gravityScale = TempGravity;

        ReSetAll();


        if (_gameBody.IsHitTopWall)
        {
            print("头顶 顶墙了  停止跳跃");

            return;
        }


        
        _gameObj = gameObj;
        _playerX = _gameObj.transform.position.x;
        IsStartXV = true;
        //print("    --------------------------  >   "+ _playerX);
        if(!IsJumpToPlayerPostion)StartVX();

        //_gameBody.GetPlayerRigidbody2D().velocity = new Vector2(vx, _gameBody.GetPlayerRigidbody2D().velocity.y);
        _gameBody.GetJump();
        _gameBody.Jump();


        if (JumpSound != null) JumpSound.Play();
        //显示 hitkuai
        GetComponent<AI_hitkuaiShenTi>().ShowHitKuaiInAir();
        IsStarting = true;

    }

    [Header("跳跃 怪和玩家单位距离 ")]
    public float DistanceXDW = 2;
    public bool IsJumpToPlayerPostion = true;
    public float VXPower = 30;
    void StartVX()
    {
        float powerX = Mathf.Abs(this.transform.position.x - _playerX) / DistanceXDW * 80;
        float vx = this.transform.position.x > _playerX ? -VXPower : VXPower;
        if (IsJumpToPlayerPostion) vx = this.transform.position.x > _playerX ? -powerX : powerX;
        //_gameBody.GetPlayerRigidbody2D().AddForce(new Vector2(vx, 0));

       

        _gameBody.GetZongTuili(new Vector2(vx, 0));
    }


    bool IsStartXV = false;
    public bool IsGetOver()
    {
        if (!IsStarting) return true;
        if (_roleDate.isBeHiting || _roleDate.isDie || (_gameBody.IsGround&& _gameBody.IsGetStand())) {
            //_gameBody.GetPlayerRigidbody2D().gravityScale = _recordGravity;
            //if (GetComponent<AIBase>()) GetComponent<AIBase>().AIBeHit();
            print("   技能终止！！！！！ ");
            _gameBody.isJumping = false;
            ReSetAll();
            
            IsStarting = false;
            return true;
        }
        return false;
    }

    public void ReSetAll()
    {
        IsStartXV = false;
        IsStarting = false;
        IsKaqiang = false;
    }



    GameBody _gameBody;
    RoleDate _roleDate;

    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();
        _roleDate = GetComponent<RoleDate>();
    }

    bool IsKaqiang = false;
    
    // Update is called once per frame
    void Update()
    {
        if (_roleDate.isBeHiting || _roleDate.isDie || (_gameBody.IsGround && _gameBody.IsGetStand()))
        {
            //_gameBody.GetPlayerRigidbody2D().gravityScale = _recordGravity;
            _gameBody.isJumping = false;
            //print(" *******  技能终止！！！！！ ");
            ReSetAll();
            IsStarting = false;
        }


        if (!IsStarting) return;

        if (GetComponent<AIBase>().IsTuihuiFangshouquing)
        {
            ReSetAll();
            return;
        }


        //撞墙判断
        if (_gameBody.IsShentiHitWall&& !_roleDate.isDie)
        {
            
            //float vx = this.transform.localScale.x > 0 ? 0.2f : -0.2f;
            float vx = this.transform.localScale.x > 0 ? 400 : -400;
            print("  身体 前面撞墙了！！！！！ "+vx);
            _gameBody.GetZongTuili(new Vector2(vx,0));
            //GetZongTuili(Vector2.up * yForce);
            //_gameBody.GetPlayerRigidbody2D().velocity = new Vector2(vx, _gameBody.GetPlayerRigidbody2D().velocity.y);
            //print(_gameBody.GetPlayerRigidbody2D().velocity);
            //float __x = this.transform.position.x + vx;
            //print(vx+" ---  "+this.transform.position);
            //this.transform.position = new Vector2(__x, this.transform.position.y);
            //print(this.transform.position);
            //IsKaqiang = true;
            return;
        }

        //if(_gameBody.GetDB().animation.lastAnimationName == "jumpUp_1"&& _gameBody.GetPlayerRigidbody2D().velocity == Vector2.zero)
        //{
        //    if (IsKaqiang)
        //    {
        //        IsKaqiang = false;
        //        print("我靠 发什么神经！！！！！" + _gameBody.GetPlayerRigidbody2D().gravityScale);
        //        float vx = this.transform.localScale.x > 0 ? 100 : -100;
        //        _gameBody.GetZongTuili(new Vector2(vx, 100));
        //    }
            
        //}


        if (!_roleDate.isDie &&IsJumpToPlayerPostion &&IsStartXV && _gameBody.isInAiring&&_gameObj)
        {
            //print("我进来没、、、、、、、、、、、、、、///////////////////////////////////-------------->");
            StartVX();
        }
    }
}
