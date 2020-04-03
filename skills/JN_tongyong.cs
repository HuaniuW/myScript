using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_tongyong : MonoBehaviour
{

    //特效名字
    //特效动作
    //动作时间侦听
    //特效位置修正



    // Start is called before the first frame update
    void Start()
    {
        _gameBody = GetComponent<GameBody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 设置 释放动作名字 和特效名字
    /// </summary>
    /// <param name="acName">释放动作名字</param>
    /// <param name="TXName">特效名字</param>
    /// <param name="YCTime">延迟时间</param>
    public void SetACNameAndTX(string acName,string TXName,float YCTime = 0 )
    {

    }

    GameBody _gameBody;






}
