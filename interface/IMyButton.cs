using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyButton{
    /// <summary>
    /// 显示位置类型 PC 或者 移动
    /// </summary>
    /// <param name="n"></param>
    void BtnPositionType(bool isPC);


    void BtnHideShow(bool isShow = true);
}
