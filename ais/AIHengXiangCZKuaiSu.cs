using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHengXiangCZKuaiSu : AIHengXiangChongZhuang
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    


    protected override Vector2 GetCanGoToPos()
    {
        //GetComponent<AIAirRunNear>()
        //print(">>>>>>>>>>>>>>>>>>>>>> 快速 横向 冲击  去到 指定 起始冲击点  ");

        //判断左右 角色是在 中心点 左边 还是我右边  然后 找到同Y点 如果过去 底部碰撞 就算到达位置
        if (!_player)
        {
            return new Vector2(1000, 1000);
        }



        if((_player.transform.position.x< CenterPos.position.x && this.transform.position.x< CenterPos.position.x)||(_player.transform.position.x > CenterPos.position.x && this.transform.position.x > CenterPos.position.x))
        {
            //print("1 在同一边！！！！ ");
            return new Vector2(CenterPos.position.x, CenterPos.position.y+0.6f);
        }
        else
        {
            //print("2 原地开始！！！！ ");
            return new Vector2(this.transform.position.x, CenterPos.position.y+0.6f);
        }
    }


    protected override void GoToPos()
    {

        if (_isChongJiQiShou)
        {
            ChongJiQiShou();
            return;
        }


        //这里要加判断 是否 碰到 地面 或者机关
        if (GetComponent<AirGameBody>().IsHitDown || GoToMoveToPoint(startPos, 0.5f, 2))
        {
            //print("中心点位置  "+CenterPos.position);
            //print("我到达目的地了!!!startPos    "+ startPos+"  我的 位置是？ "+ this.transform.position+"  是否撞墙  "+ GetComponent<AirGameBody>().IsHitDown);
            //_isGetOver = true;


            if (this.transform.position.x < _player.transform.position.x)
            {
                //print(" turn y !!");
                _airGameBody.TurnRight();
                ChongJiPos = new Vector2(CenterPos.position.x + CJDistance, this.transform.position.y);
            }
            else
            {
                //print(" turn z !!");
                _airGameBody.TurnLeft();
                ChongJiPos = new Vector2(CenterPos.position.x - CJDistance, this.transform.position.y);
            }
            //print("  冲击 点位置！！ "+ ChongJiPos+"   我的位置   "+this.transform.position);
            _isChongJiQiShou = true;
        }
    }




}
