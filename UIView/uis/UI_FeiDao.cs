using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FeiDao : MonoBehaviour {

    // Use this for initialization
    UI_Skill _uiskill;
    HZDate _hzdate;
    GameObject _player;
	void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIU_FEIDAO, DiuFeidao);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DISTORY_FEIDAO, HasShouDao);
        if (!_uiskill) _uiskill = GetComponent<UI_Skill>();
        if(!_hzdate) _hzdate = GetComponent<HZDate>();
        if (!_player) _player = GlobalTools.FindObjByName("player");
        _uiskill.SetHZDate(_hzdate);
        print(">>>>>>");
    }

    void OnDistory()
    {

    }

    public void GetDistory()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIU_FEIDAO, DiuFeidao);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DISTORY_FEIDAO, HasShouDao);
    }

    //bool isShouFD = false;

    public void HasShouDao(UEvent e)
    {
        //isShouFD = false;
        
    }


    void SanXian()
    {
        GetComponent<UI_Skill>().CDFull();
        //调用角色 闪现到 飞刀地方  事件
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.SHOU_FEIDAO, null), this);
    }


    void DiuFeidao(UEvent e)
    {
        //print("飞刀-----------------------------------------------------------> "+e.eventParams.ToString());

        print("isShouFD     "+ Globals.isShouFD);
        if (Globals.isShouFD&&Globals.feidao)
        {
            //收飞刀
            SanXian();
            return;
        }
        GetComponent<UI_Skill>().CDFull();

        //没有飞刀 瞬移 飞刀消失
        Globals.feidaoFX = e.eventParams.ToString();

        //查看是否有飞刀  
        //if (!_uiskill.isCanBeUseSkill()) return;
        //print("2");
        //丢飞刀技能 获取飞刀技能date 

        //根据方向 判断 动作
        if (_player.GetComponent<GameBody>().isInAiring)
        {
            if (e.eventParams.ToString() == "up")
            {
                _hzdate.skillACName = "jumpAtk_dfd2";
            }
            else if (e.eventParams.ToString() == "down")
            {
                _hzdate.skillACName = "jumpAtk_dfd3";
            }
            else
            {
                _hzdate.skillACName = "jumpAtk_dfd1";
            }
        }
        else
        {
            if (e.eventParams.ToString() == "up")
            {
                _hzdate.skillACName = "atk_dfd2";
            }
            else if (e.eventParams.ToString() == "down")
            {
                _hzdate.skillACName = "";
                //放地上
            }
            else
            {
                _hzdate.skillACName = "atk_dfd1";
            }
        }

        print("_hzdate.skillACName     "+ _hzdate.skillACName);

        _player.GetComponent<GameBody>().ShowSkill(this.gameObject);
        Globals.isShouFD = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
