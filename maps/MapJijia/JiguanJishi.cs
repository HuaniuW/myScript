using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiguanJishi
{

    public void SetJishiValue(float GudingJiange,float JiangeWuchaShijian = 0,float Jiangeshijian = 2)
    {
        _HoufangDaodanJishi = 0;
        _Jiangeshijian = Jiangeshijian;
        _GudingJiange = GudingJiange;
        _JiangeWuchaShijian = JiangeWuchaShijian;
    }





    float _Jiangeshijian = 2;
    float _GudingJiange = 6;
    float _JiangeWuchaShijian = 3;
    float _HoufangDaodanJishi = 0;
    public bool IsJishiOver()
    {
        _HoufangDaodanJishi += Time.deltaTime;
        if (_HoufangDaodanJishi >= _Jiangeshijian)
        {
            _HoufangDaodanJishi = 0;
            _Jiangeshijian = _GudingJiange + GlobalTools.GetRandomDistanceNums(_JiangeWuchaShijian);
            return true;
        }
        return false;
    }
}
