using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_MoveFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_OUT, BossName), this);

        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, this.DBStartMove);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_DIE, this.DBStartMove);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsStartUp) CheckIsHasBoss();
        StartMove();
    }

    [Header("判断 boss是否还存在")]
    public string BossName = "";


    bool IsCheckTheBoss = false;
    void CheckIsHasBoss()
    {
        if(!IsCheckTheBoss && GlobalTools.FindObjByName(BossName) == null)
        {
            IsCheckTheBoss = true;
            print("boss已过！！！");
            YSPos();
        }
    }


    private void OnDisable()
    {
        //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_OUT, this.DBStartMove);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_DIE, this.DBStartMove);
    }

    void DBStartMove(UEvent e)
    {
        IsStartUp = true;
    }


    [Header("要移动到的 目标位置点 多出的 偏移误差")]
    public float ToPosMoveDis = 0;

    [Header("要移动到的 目标位置点")]
    public Transform ToPos;

    public float MoveSpeed = 0.6f;

    [Header("地板移动 声音")]
    public AudioSource DBMoveAudio;

    public bool IsUp = true;
    bool IsStartUp = false;
    float JSTimes = 0;
    void StartMove()
    {
        if (!IsStartUp) return;
        if (!ToPos) return;
        if (IsUp)
        {
            if (this.transform.position.y>= ToPos.position.y+ ToPosMoveDis)
            {
                if (DBMoveAudio) DBMoveAudio.Stop();
                return;
            }
            if (!DBMoveAudio.isPlaying) DBMoveAudio.Play();
            JSTimes += Time.deltaTime;
            //if (JSTimes >= 0.5f)
            //{
            //    JSTimes = 0;
               
            //}

            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + MoveSpeed);

        }
    }

    //原始 位置 在改变状态后 记录点力面取 不要在生成一次
    void YSPos()
    {
        this.transform.position = new Vector2(this.transform.position.x, ToPos.position.y + ToPosMoveDis);  
    }


}
