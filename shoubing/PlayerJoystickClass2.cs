using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// 测试游戏手柄键值
/// </summary>
public class PlayerJoystickClass2 : MonoBehaviour
{
    private string currentButton;//当前按下的按键
    private string currentAxis;//当前移动的轴向
    private float axisInput;//当前轴向的值

    void Update()
    {
        getAxis();
        getButtons();
    }

    /// <summary>
    /// Get Button data of the joysick
    /// </summary>
    void getButtons()
    {
        var values = Enum.GetValues(typeof(KeyCode));//存储所有的按键
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString();//遍历并获取当前按下的按键
            }
        }
    }

    /// <summary>
    /// Get Axis data of the joysick
    /// </summary>
    void getAxis()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.3 || Input.GetAxisRaw("Horizontal") < -0.3)
        {
            currentAxis = "Horizontal";
            axisInput = Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetAxisRaw("Vertical") > 0.3 || Input.GetAxisRaw("Vertical") < -0.3)
        {
            currentAxis = "Vertical";
            axisInput = Input.GetAxisRaw("Vertical");
        }


        //PS4手柄 十字键  对应 7 8
        if (Input.GetAxisRaw("ShiziX") > 0.3 || Input.GetAxisRaw("ShiziX") < -0.3)
        {
            currentAxis = "ShiziX";
            axisInput = Input.GetAxisRaw("ShiziX");
        }

        if (Input.GetAxisRaw("ShiziY") > 0.3 || Input.GetAxisRaw("ShiziY") < -0.3)
        {
            currentAxis = "ShiziY";
            axisInput = Input.GetAxisRaw("ShiziY");
        }


        //PS4 右手柄 对应 3 4
        if (Input.GetAxisRaw("Horizontal2") > 0.3 || Input.GetAxisRaw("Horizontal2") < -0.3)
        {
            currentAxis = "Horizontal2";
            axisInput = Input.GetAxisRaw("Horizontal2");
        }

        if (Input.GetAxisRaw("Vertical2") > 0.3 || Input.GetAxisRaw("Vertical2") < -0.3)
        {
            currentAxis = "Vertical2";
            axisInput = Input.GetAxisRaw("Vertical2");
        }

        //if (Input.GetAxisRaw("7th axis") > 0.3 || Input.GetAxisRaw("7th axis") < -0.3)
        //{
        //    currentAxis = "7th axis";
        //    axisInput = Input.GetAxisRaw("7th axis");
        //}

        //if (Input.GetAxisRaw("8th axis") > 0.3 || Input.GetAxisRaw("8th axis") < -0.3)
        //{
        //    currentAxis = "8th axis";
        //    axisInput = Input.GetAxisRaw("8th axis");
        //}

        //if (Input.GetAxisRaw("9th axis") > 0.3 || Input.GetAxisRaw("9th axis") < -0.3)
        //{
        //    currentAxis = "9th axis";
        //    axisInput = Input.GetAxisRaw("9th axis");
        //}

        //if (Input.GetAxisRaw("10th axis") > 0.3 || Input.GetAxisRaw("10th axis") < -0.3)
        //{
        //    currentAxis = "10th axis";
        //    axisInput = Input.GetAxisRaw("10th axis");
        //}

        //if (Input.GetAxisRaw("11th axis") > 0.3 || Input.GetAxisRaw("11th axis") < -0.3)
        //{
        //    currentAxis = "11th axis";
        //    axisInput = Input.GetAxisRaw("11th axis");
        //}

        //if (Input.GetAxisRaw("12th axis") > 0.3 || Input.GetAxisRaw("12th axis") < -0.3)
        //{
        //    currentAxis = "12th axis";
        //    axisInput = Input.GetAxisRaw("12th axis");
        //}

        //if (Input.GetAxisRaw("13th axis") > 0.3 || Input.GetAxisRaw("13th axis") < -0.3)
        //{
        //    currentAxis = "13th axis";
        //    axisInput = Input.GetAxisRaw("13th axis");
        //}

        //if (Input.GetAxisRaw("14th axis") > 0.3 || Input.GetAxisRaw("14th axis") < -0.3)
        //{
        //    currentAxis = "14th axis";
        //    axisInput = Input.GetAxisRaw("14th axis");
        //}
    }

    /// <summary>
    /// show the data onGUI
    /// </summary>
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 250, 50), "Current Button : " + currentButton);//使用GUI在屏幕上面实时打印当前按下的按键
        GUI.TextArea(new Rect(0, 100, 250, 50), "Current Axis : " + currentAxis);//使用GUI在屏幕上面实时打印当前按下的轴
        GUI.TextArea(new Rect(0, 200, 250, 50), "Axis Value : " + axisInput);//使用GUI在屏幕上面实时打印当前按下的轴的量
    }
}