using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot_enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!_roleDate) _roleDate = GetComponent<RoleDate>();
        
    }

    RoleDate _roleDate;
    bool IsStart = true;


    [Header("战斗音乐音效")]
    public AudioSource Audio_Zhandou;



    [Header("被攻击时候 需要 移除的 物体")]
    public GameObject BeHitDisObj;

    protected virtual void AIBeHit()
    {
        if (_roleDate.isBeHiting)
        {
            if (BeHitDisObj) Destroy(BeHitDisObj);
            IsStart = false;
            CloseDoor();
            //播放战斗音乐
            //Audio_Zhandou.Play();
        }

    }


    //bool IsStartPlot2 = false;
    protected void HalfLiveInpLOT()
    {
        if(_roleDate.live<= _roleDate.maxLive*0.5f&&!Globals.isInPlot)
        {
            //IsStartPlot2 = true;
            Globals.isInPlot = true;
            GetComponent<AIBase>().AIReSet();
            GetComponent<GameBody>().ResetAll();
            //GetComponent<GameBody>().isAcing = false;
            //GetComponent<GameBody>().GetAcMsg(GetComponent<GameBody>().GetStandACName());
            ////GetComponent<GameBody>().GetStand(); 
            GetComponent<AIBase>().IsBossStop = true;
            _gamebody = GetComponent<GameBody>();
            STAND = _gamebody.GetStandACName();



            player = GlobalTools.FindObjByName("player");
            player.GetComponent<GameBody>()._yanmu.Stop();
            player.GetComponent<GameBody>()._yanmu2.Stop();
            player.GetComponent<GameBody>().ResetAll();
            //player.GetComponent<GameBody>().isAcing = false;

            PLAYER_STAND = player.GetComponent<GameBody>().GetStandACName();
            IsGetStand = true;
            //player.GetComponent<GameBody>().GetAcMsg(GetComponent<GameBody>().GetStandACName());
          

            //相互朝向？
            Dubais();
            IsStartDisSelfByTime = true;
            //GlobalTools.FindObjByName("player").GetComponent<GameBody>().GetStand();

        }
    }


    GameObject player;
    GameBody _gamebody;// = GetComponent<GameBody>();
    string STAND = "";// _gamebody.GetStandACName();
    bool IsGetStand = false;

    string PLAYER_STAND = "";

    void GetStands()
    {
        if (!IsGetStand) return;
        if (_gamebody.GetDB().animation.lastAnimationName!= STAND)
        {
            _gamebody.GetDB().animation.GotoAndPlayByFrame(STAND);
            _gamebody.isAcing = true;
        }

        if (player.GetComponent<GameBody>().GetDB().animation.lastAnimationName != PLAYER_STAND)
        {
            player.GetComponent<GameBody>().GetDB().animation.GotoAndPlayByFrame(PLAYER_STAND);
            player.GetComponent<GameBody>().isAcing = true;
        }
    }



    //一秒后 出 独白

    void Dubais()
    {
        string _msg ="很好！你表现令人惊喜";
        GameObject _cBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar2") as GameObject);
        Vector2 _talkPos = GetComponent<GameBody>().GetTalkPos();
        //_cBar.GetComponent<UI_talkBar>().
        //print("我是boss！！！！  boss 的 独白  是什么？？       " + _msg);
        _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, 4);
       
    }



    [Header("隐身特效粒子")]
    public ParticleSystem Lizi_Yinshen;

    float DisTimes = 4;
    float DisTimesNun = 0;
    bool IsStartDisSelfByTime = false;
    void DisSelfByTimes()
    {
        if (!IsStartDisSelfByTime) return;
        DisTimesNun += Time.deltaTime;
        if (DisTimesNun >= DisTimes)
        {
            DisTimesNun = 0;
            Lizi_Yinshen.transform.parent = this.gameObject.transform.parent;
            Lizi_Yinshen.Play();
            Lizi_Yinshen.GetComponent<AudioSource>().Play();
            Globals.isInPlot = false;
            IsGetStand = false;
            //开门
            OpenDoor();

            //Audio_Zhandou.Stop();

            //出徽章

            //记录
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.PLOT_KILL_PLAYER), this);
            player.GetComponent<GameBody>().isAcing = false;
            DestroyImmediate(this.gameObject, true);
        }
    }


    //给出奖励 护盾




    protected virtual void CloseDoor()
    {
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR, "close"), this);
    }


    protected virtual void OpenDoor()
    {
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NEW_OPEN_DOOR, "open"), this);
    }



    //记录 门 和 自身 移除 都在  对话剧情类里面

    // Update is called once per frame
    void Update()
    {
        if (IsStart) {
            HalfLiveInpLOT();
            DisSelfByTimes();
            GetStands();
        }
        

        if (!IsStart) return;
        AIBeHit();
        
    }
}
