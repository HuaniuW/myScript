using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_screenChangeZZ : MonoBehaviour
{

    public RectTransform screenChangeZZ;
    public Text screenLoadTxt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetScreenChange();
    }


    void GetScreenChange()
    {
        if (!IsScreenChangeing) return;
        if (this.screenChangeZZ != null)
        {
            this.screenChangeZZ.GetComponent<CanvasGroup>().alpha += (ZZAlphaNums - this.screenChangeZZ.GetComponent<CanvasGroup>().alpha) * 0.1f;
            if (ZZAlphaNums == 0 && this.screenChangeZZ.GetComponent<CanvasGroup>().alpha <= 0.2)
            {
                this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
                IsScreenChangeing = false;
                //防止遮挡鼠标事件
                screenChangeZZ.gameObject.SetActive(false);
            }
        }
    }

    float ZZAlphaNums = 0;
    bool IsScreenChangeing = false;
    public void Show_UIZZ()
    {
        screenChangeZZ.gameObject.SetActive(true);
        ZZAlphaNums = 1;
        IsScreenChangeing = true;
    }


    public void Hide_UIZZ()
    {
        IsScreenChangeing = false;
        this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 1;
        screenLoadTxt.text = "";
        StartCoroutine(IHideZZByTimes(0.3f));

    }

    public IEnumerator IHideZZByTimes(float time)
    {
        yield return new WaitForSeconds(time);
        this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
        //不禁止 会阻挡 鼠标点击事件
        screenChangeZZ.gameObject.SetActive(false);
    }

    public void ShowLoadProgressNums(float nums, bool isHasChangeScreen = false)
    {
        if (nums == 100)
        {
            //IsScreenChangeing = false;
            //this.screenChangeZZ.GetComponent<CanvasGroup>().alpha = 0;
            //ZZAlphaNums = 0;

        }
        if (screenLoadTxt != null) screenLoadTxt.text = nums + "%";
    }
}
