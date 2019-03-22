using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameSetBar : MonoBehaviour {
    public Button btn_close;
    public RectTransform mianban1;
    public RectTransform mianban2;
    //移动版
    public Toggle tog1;
    //非移动版
    public Toggle tog2;
    public Slider sl1;

    // Use this for initialization
    void Start () {
        AddEvenr();
        this.sl1.value = GlobalSetDate.instance.GetSoundEffectValue();
    }

    void AddEvenr()
    {
        tog1.onValueChanged.AddListener(GetTog1);
        tog2.onValueChanged.AddListener(GetTog2);
        sl1.onValueChanged.AddListener(SLChange);
        btn_close.onClick.AddListener(RemoveSelf);
    }
	void RemoveSelf()
    {
        GlobalSetDate.instance.IsChangeScreening = false;
        DestroyImmediate(this.gameObject, true);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void SLChange(float value)
    {
        //print(value);
        GlobalSetDate.instance.SoundEffect = value;
    }

    void GetTog1(bool check)
    {
        //print(check);
    }

    void GetTog2(bool check)
    {
        //print("hello!");
    } 
}
