using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}

    private void OnEnable()
    {
        GetPlayer();
        GetPlayerUI();
        GetTargetPlayer();
    }

    // Update is called once per frame
    void Update () {
        //print(player.transform.position);
	}

    GameObject FindObjByName(string _name)
    {
        GameObject obj = GameObject.Find("/" + _name) as GameObject;
        if(obj == null)obj = GameObject.Find("/"+_name+"(Clone)") as GameObject;
        return obj;
    }


    GameObject InstancePrefabByName(string _name) {
        GameObject obj = Resources.Load(_name) as GameObject;
        GameObject.Instantiate(obj);
        return obj;
    }

    GameObject player;
    //找到主角 获取主角坐标
    void GetPlayer()
    {
        if (FindObjByName("player") == null)
        {
            player = InstancePrefabByName("player");
        }
        else
        {
            player = FindObjByName("player");
            player.GetComponent<DontDistoryObj>().ShowSelf();
        }
        //设置玩家进场位置
        if(GlobalSetDate.instance.playerPosition!="") player.transform.position = GlobalSetDate.instance.GetPlayerInScreenPosition();
        player.GetComponent<GameBody>().SetV0();
        GlobalSetDate.instance.IsChangeScreening = false;
        //print("p "+player.GetComponent<GameBody>().GetBodyScale());
        //player.transform.localScale = new Vector3(1, 1, 1);
    }
    //生成UI
    void GetPlayerUI()
    {
        GameObject playerUI;
        if (FindObjByName("PlayerUI") == null)
        {
            playerUI = InstancePrefabByName("PlayerUI");
        }
        else
        {
            playerUI = FindObjByName("PlayerUI");
        }
        playerUI.GetComponent<DontDistoryObj>().ShowSelf();
        playerUI.GetComponent<XueTiao>().GetTargetObj(FindObjByName("player"));
    }


    //摄像头定焦 和找到敌人
    void GetTargetPlayer()
    {
        //print("hi");
        this.GetComponent<CameraController>().GetTargetObj(FindObjByName("player").transform);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GET_ENEMY), null);
    }

}
