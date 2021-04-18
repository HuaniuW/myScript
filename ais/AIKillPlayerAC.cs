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


    [Header("击败玩家后的 声音")]
    public AudioSource AudioPlayerDie;

    [Header("击败玩家后的 嘲讽语句")]
    public string[] ChaoFengStrArr;
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

            if (IsHasChaofeng && ChaoFengStrArr != null)
            {
                if (ChaoFengStrArr.Length < 3)
                {
                    if (GlobalTools.GetRandomNum() < 50) return;
                }

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
        string _msg = ChaoFengStrArr[GlobalTools.GetRandomNum(ChaoFengStrArr.Length)];
        _cBar.GetComponent<UI_talkBar>().ShowTalkText(_msg, _talkPos, 1.5f);
    }






    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DIE_OUT, playerDie);
    }
}
