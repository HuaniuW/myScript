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

    // Use this for initialization
    void Start () {
       //thisY = this.transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
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
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, this.transform.parent.name),this);
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

    // Update is called once per frame
    void Update () {
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
	}

    public void DistorySelf()
    {
        this.gameObject.SetActive(false);
    }


}

