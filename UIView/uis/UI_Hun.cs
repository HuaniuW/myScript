using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Hun : MonoBehaviour {
    public Text hun_num_text;
    public AudioSource no_hun_prompt;
    // Use this for initialization
    void Start () {
        print("hun!!!");
        if (GlobalSetDate.instance.CurrentUserDate.curLan != null) {
            print(GlobalSetDate.instance.CurrentUserDate.curLan);
            GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan = float.Parse(GlobalSetDate.instance.CurrentUserDate.curLan);
        }
        
        hun_num_text.text = GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan.ToString();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.NO_HUN_PROMPT, this.No_hun_prompt);
        GetComponent<UIShake>().GetShakeObj(this.gameObject);

    }

    void OnDistory()
    {
        print("UI_hun  我被销魂了!");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.NO_HUN_PROMPT, this.No_hun_prompt);
        
    }

    public void OnDistory2()
    {
        OnDistory();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void ChangeHun(UEvent e)
    {
        hun_num_text.text = e.eventParams.ToString();
        GlobalSetDate.instance.CurrentUserDate.curLan = GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan.ToString();
        //GetComponent<UIShake>().GetShake();
    }

    public void InitHun()
    {

    }

    public void SetHun(int nums)
    {
        hun_num_text.text = nums.ToString();
    }

    void No_hun_prompt(UEvent e)
    {
        no_hun_prompt.Play();
        GetComponent<UIShake>().GetShake();
    }
}
