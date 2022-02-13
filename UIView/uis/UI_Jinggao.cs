using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Jinggao : MonoBehaviour
{

    public Image Jinggao;

    public AudioSource Audio_Jinggao;

    // Start is called before the first frame update
    void Start()
    {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.LAIXI_JINGGAO, this.GetJinggao);
        //GetJinggao(null);
    }

    private void OnDisable()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.LAIXI_JINGGAO, this.GetJinggao);
    }

    float jinggaoTimes = 3;
    float jinggaoJishi = 0;

    bool IsInJinggao = false;
    private void GetJinggao(UEvent evt)
    {
        IsInJinggao = true;
        jinggaoJishi = 0;
        Jinggao.gameObject.SetActive(true);
        if(!Audio_Jinggao.isPlaying) Audio_Jinggao.Play();
    }

    float alphaChange = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (IsInJinggao)
        {
            Jinggao.GetComponent<CanvasGroup>().alpha += alphaChange;
            if (Jinggao.GetComponent<CanvasGroup>().alpha == 1|| Jinggao.GetComponent<CanvasGroup>().alpha == 0)
            {
                alphaChange *= -1;
            }

            jinggaoJishi += Time.deltaTime;
            if(jinggaoJishi>= jinggaoTimes)
            {
                IsInJinggao = false;
                jinggaoJishi = 0;
                Jinggao.gameObject.SetActive(false);
                Audio_Jinggao.Stop();
            }
        }
        //else
        //{
        //    if (!Audio_Jinggao.isActiveAndEnabled)
        //    {
        //        Audio_Jinggao.Stop();
        //    }
        //}
    }
}
