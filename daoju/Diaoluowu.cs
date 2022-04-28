using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Diaoluowu : MonoBehaviour {
    public string objName;

    public string id;
    
    public Transform up;
    public Transform down;
    /// <summary>
    /// 1徽章 2血 3蓝 4血瓶 5收集物
    /// </summary>
    public int type = 1;

    [Header("打过boss后 徽章获得 徽章 声音播放")]
    public bool IsHuzihangAudioPlay = true;
    // Use this for initialization
    void Start () {
        //thisY = this.transform.position.y;
       
    }

    //[Header("显示粒子 ")]
    //public ParticleSystem XSLizi;

    private void LateUpdate()
    {
        if (type == 1 && IsHuzihangAudioPlay)
        {
            IsHuzihangAudioPlay = false;
            GameObject TX_huoquhuizhang = GlobalTools.GetGameObjectByName("TX_HuizhangOut");
            TX_huoquhuizhang.transform.position = this.transform.position;

            //XSLizi.Play();
            string AudioName = "Audio_HuizhangJiangli";
            GameObject _audioObj = GlobalTools.GetGameObjectByName(AudioName);
            if (_audioObj != null) _audioObj.GetComponent<AudioSource>().Play();
        }
    }


    public bool IsCanBeRecord = false;
    bool IsOutRecorded = false;
    void GetOutRecord()
    {
        if (!IsCanBeRecord) return;
        if (type == 1&&!IsOutRecorded)
        {
            if (this.transform.parent.GetComponent<Wupinlan>().isChildCanBeHit)
            {
                IsOutRecorded = true;
                //print(" --------------------------------------------------------爆出 物品记录   ");
                string parentName = GlobalTools.GetNewStrQuDiaoClone(this.transform.parent.name);   //this.transform.parent.name.Replace("(Clone)", "");
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, parentName + "-1@" + this.transform.parent.transform.position.x + "#" + this.transform.parent.transform.position.y), this);
            }
        }
    }
    

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsCanTouch) return;
        if(Coll.tag == "Player")
        {
            //print("pengzhuang!!");
            //播放动画 什么的 消失啊  声音啊==
            //角色获得物品事件
            if(type == 1)
            {
                //徽章 等 装进背包
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_OBJ_NAME, this.objName), this);
                //print(this.transform.parent.name);
                //ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CLOSE_DOOR, GKDateChange);
                var parentName = GlobalTools.GetNewStrQuDiaoClone(this.transform.parent.name);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, parentName + "-0"), this);
                //显示 获取徽章的 提示UI
                GameObject ui_huizhangMsg = GlobalTools.FindObjByName("Canvas_HZMsg");
                if (!ui_huizhangMsg)
                {
                    ui_huizhangMsg = GlobalTools.GetGameObjectByName("Canvas_HZMsg");
                }
                GameObject HZ_obj = Resources.Load(objName) as GameObject;
                string HZ_Msg = HZ_obj.GetComponent<HZDate>().GetHZ_information_str();
                ui_huizhangMsg.GetComponent<UI_HZMsg>().StartShowBar("img_"+objName, HZ_Msg);
            }
            else if (type == 2) {
                //消耗的掉落物 吃了 直接加血
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_DIAOLUOWU, this.name), this);
            }else if (type == 3) {
                //血瓶
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_XP, 1), this);
            }else if (type == 4)
            {
                //存档测试点
               
                print("存档！！！" + this.name);
                //加血用
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_DIAOLUOWU, "C_cundangdian"), this);
                //显示存档提示
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_SAVEING, null), this);

                string JGMsg = "JG_"+ SceneManager.GetActiveScene().name + "-" + GlobalTools.FindObjByName("MainCamera").GetComponent<GuaiOutControl>().JGNum.ToString();
                print("------------------------------------------>>>>临时存档点    "+ JGMsg);
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, JGMsg), this);

                GlobalSetDate.instance.GetSave();
            }
            
          
            
            if (this.transform.parent != null) {
                this.transform.parent.GetComponent<Wupinlan>().DistorySelf();
            }
            else
            {
                DistorySelf();
            }
        }
    }

    public float moveSpeed = 0.00001f;
    public float thisY;

    [Header("延迟拾取 计时过了 才能开始拾取")]
    public bool YanChiTouchJiShi = true;

    float YCJiShi = 0;
    bool IsCanTouch = false;

    // Update is called once per frame
    void Update () {
        if (YanChiTouchJiShi)
        {
            YCJiShi += Time.deltaTime;
            if (YCJiShi >= 2)
            {
                IsCanTouch = true;
            }
        }
        else
        {
            IsCanTouch = true;
        }

        //return;
        if (this.up != null && this.down != null)
        {
            if (this.transform.position.y > this.up.transform.position.y|| this.transform.position.y < this.down.transform.position.y) {
                moveSpeed *= -1f;
            }
            //thisY += moveSpeed;
            float mY = this.transform.position.y + moveSpeed;
            //print(this.transform.position.y +"  -------------   "+ moveSpeed);
            this.transform.position = new Vector2(this.transform.position.x, mY);
        }

        GetOutRecord();

    }

    public void DistorySelf()
    {
        this.gameObject.SetActive(false);
    }


}

