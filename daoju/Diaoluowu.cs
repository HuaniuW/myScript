using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_OBJ_NAME, this.objName), this);
            }
            else if (type == 2) {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_DIAOLUOWU, this.name), this);
            }else if (type == 3) {
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_XP, 1), this);
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

