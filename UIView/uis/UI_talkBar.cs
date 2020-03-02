using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_talkBar  : MonoBehaviour
{
    public Text WaiText;
    public Image barImg;
    public Text talkText;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTalkText(string txts,Vector2 pos,float disTime = 1,bool isJishi = true)
    {
        this.transform.position = pos;
        WaiText.text = talkText.text = txts;
        GetComponent<TheTimer>().TimesAdd(disTime, TimeCallBack);
    }

    public void TimeCallBack(float nums)
    {
        RemoveSelf();
    }

    public void RemoveSelf()
    {
        ObjectPools.GetInstance().DestoryObject2(this.gameObject);
    }
}
