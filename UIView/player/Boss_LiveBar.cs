using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_LiveBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //this.gameObject.SetActive(false);
        this.GetComponent<CanvasGroup>().alpha = 0;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_OUT, ShowSelf);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_DIE, HideSelf);
    }

    bool isShowSelf = false;

    //初始化 隐藏自己
    //接受到出现boss缓动显示 并且获取boss信息 与其关联
    void ShowSelf(UEvent e)
    {
        //print("show bar!!!");
        //this.gameObject.SetActive(true);
        string bossName = e.eventParams.ToString();
        isShowSelf = true;
        this.GetComponent<EnemyXueTiao>().gameObj = GlobalTools.FindObjByName(bossName);
        this.GetComponent<EnemyXueTiao>().GetGameObj();
    }
    //boss die事件 隐藏自己
    void HideSelf(UEvent e)
    {
        //this.gameObject.SetActive(true);
        //string bossName = e.eventParams.ToString();  预留给后面 需要做判断用
        if(this == null)
        {
            OnDisable();
            return;
        }

        //this.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(IEDestory2ByTime(this.GetComponent<CanvasGroup>(), 1f));
    }

    IEnumerator IEDestory2ByTime(CanvasGroup obj, float time)
    {
        yield return new WaitForSeconds(time);
        //yield return new WaitForFixedUpdate();
        //Debug.Log(">>   "+ gameObject);
        obj.alpha = 0;
    }

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_OUT, ShowSelf);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_DIE, HideSelf);
    }

    // Update is called once per frame
    void Update () {
        if (isShowSelf)
        {
            if (this.GetComponent<CanvasGroup>().alpha < 1)
            {
                this.GetComponent<CanvasGroup>().alpha += (1 - this.GetComponent<CanvasGroup>().alpha) * 0.02f;
                if (this.GetComponent<CanvasGroup>().alpha >= 0.94)
                {
                    this.GetComponent<CanvasGroup>().alpha = 1;
                    isShowSelf = false;
                }
            }
        }
	}
}
