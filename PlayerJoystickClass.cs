using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// 测试游戏手柄键值
/// </summary>
public class PlayerJoystickClass : MonoBehaviour
{
    private string currentButton;//当前按下的按键

    // Use this for initialization 
    void Start()
    {

    }
    // Update is called once per frame 
    void Update()
    {
        var values = Enum.GetValues(typeof(KeyCode));//存储所有的按键
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString();//遍历并获取当前按下的按键
            }
        }

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    // Show some data 
    void OnGUI()
    {
        //GUI.TextArea(new Rect(0, 0, 250, 40), "Current Button : " + currentButton);//使用GUI在屏幕上面实时打印当前按下的按键
        Zhenshu();
    }


    float deltaTime = 0.0f;


    void Zhenshu()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        //new Color (0.0f, 0.0f, 0.5f, 1.0f);
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
