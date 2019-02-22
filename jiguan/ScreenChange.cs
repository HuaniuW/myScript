using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour {
    [Header("去的场景名字")]
    public string GoScreenName = "";
    [Header("位置")]
    public string PlayerPosition = "";
    [Header("是否碰撞触发")]
    public bool IsHitGo = false;
    [Header("朝向")]
    public float PlayerScaleX = 1;

	// Use this for initialization
	void Start () {
		
	}

    void SetPlayerPositionAndScreen()
    {
        GlobalSetDate.instance.playerPosition = PlayerPosition;
        GlobalSetDate.instance.screenName = GoScreenName;
        GlobalSetDate.instance.IsChangeScreening = true;
    }
	
    void ChangeScreen()
    {
        GameObject playerUI = GlobalTools.FindObjByName("PlayerUI");
        playerUI.GetComponent<DontDistoryObj>().HideSelf();
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CHANGE_SCREEN, null), this);
        //ObjectPools.GetInstance().delAll();//解决切换场景时候特效没回收被销毁导致再取取不出来的问题  但是这样销毁会导致卡顿很长的切换速度 已用重新创建解决
        SetPlayerPositionAndScreen();
        SceneManager.LoadScene("loads");
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(Coll.tag + "  --  " + Coll.transform.tag);
        if(Coll.tag == "Player")
        {
            if (Coll.transform.GetComponent<RoleDate>().isDie) return;
            ChangeScreen();
        }
    }
}
