using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Hun : MonoBehaviour {
    public Text hun_num_text;
	// Use this for initialization
	void Start () {
        hun_num_text.text = GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan.ToString();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
        //GetComponent<UIShake>().GetShakeObj(hun_num_text.transform.gameObject);
    }

    void OnDistory()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeHun(UEvent e)
    {
        hun_num_text.text = e.eventParams.ToString();
        //GetComponent<UIShake>().GetShake();
    }
}
