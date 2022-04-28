using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKillPlayerAC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void GetStart()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DIE_OUT, playerDie);
    }

    [Header("怪物 名字")]
    public string GuaiName = "";


    [Header("击败玩家后的 声音")]
    public AudioSource AudioPlayerDie;

    [Header("是否显示 击败玩家后的 嘲讽语句")]
    public bool IsHasChaofeng = false;

    private void playerDie(UEvent evt)
    {
        //这个 防止 别的怪  打败玩家  触发    只能是 boss打败玩家  才能触发

        if (GetComponent<RoleDate>().isDie)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.PLOT_KILL_PLAYER), this);
            return;
        }


        if (GetComponent<AIBase>().IsBossStop) return;

        

        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.PLOT_KILL_PLAYER), this);

      

        //if (evt.eventParams != null) print("dieout---------------------------------------------@@   "+evt.eventParams.ToString());
        if (evt.eventParams != null && evt.eventParams.ToString() == "Player")
        {
            GetComponent<AIBase>().isPlayerDie = true;

            //击败玩家  是否 就离开消失  存入数据中

            if (AudioPlayerDie) AudioPlayerDie.Play();

            if (IsHasChaofeng)
            {
                if (GlobalTools.GetRandomNum() < 50) return;
                StartCoroutine(ShowOutTxtBar(0.5f));
            }

            //Globals.isPlayerDie = true;
            //GetComponent<GameBody>().ResetAll();
            //if(GetComponent<GameBody>().GetDB().animation.lastAnimationName!="stand_1")GetComponent<GameBody>().GetStand();
            //print("palyer is die !!!!!!!!!");
            //GetComponent<GameBody>().GetStand();
        }

    }

    public IEnumerator ShowOutTxtBar(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject _cBar = ObjectPools.GetInstance().SwpanObject2(Resources.Load("TalkBar2") as GameObject);
        Vector2 _talkPos = GetComponent<GameBody>().GetTalkPos();
        CHAOFENGMSG = "";
        string _msg = CHAOFENGMSG;
        _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, 1.5f);
    }


    void OnGUI()
    {
        if (Globals.isDebug)
        {
            CHAOFENGMSG = "";
            GUI.TextArea(new Rect(0, 100, 450, 40), "GuaiName  ID : " + GuaiName + "    嘲讽：  " + CHAOFENGMSG);//使用GUI在屏幕上面实时打印当前按下的按键
            //Zhenshu();
        }

    }


    string ChaofengMsg = "";

    public string CHAOFENGMSG
    {
        //GET访问器，可以理解成另类的方法，返回已经被赋了值的私有变量a
        get { return ChaofengMsg; }
        //SET访问器，将我们打入的值赋给私有变量money
        set
        {
            if (GuaiName == "B_dlws")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        ChaofengMsg = "太弱";
                        break;
                    case Globals.JAPAN:
                        ChaofengMsg = "弱すぎる";
                        break;
                    case Globals.ENGLISH:
                        ChaofengMsg = "too weak";
                        break;
                    case Globals.Portugal:
                        ChaofengMsg = "muy debil";
                        break;
                    case Globals.KOREAN:
                        ChaofengMsg = "너무 약하다";
                        break;
                }
            }
            else if (GuaiName == "B_yg")
            {
                switch (Globals.language)
                {
                    case Globals.CHINESE:
                        ChaofengMsg = "又一个倒下的亡魂。";
                        break;
                    case Globals.JAPAN:
                        ChaofengMsg = "もう一つの堕落した魂。";
                        break;
                    case Globals.ENGLISH:
                        ChaofengMsg = "Another fallen soul.";
                        break;
                    case Globals.Portugal:
                        ChaofengMsg = "Otra alma caída.";
                        break;
                    case Globals.KOREAN:
                        ChaofengMsg = "또 다른 타락한 영혼.";
                        break;
                }
            }
            


        }
    }





    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, playerDie);
    }
}
