using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_TigaoYinzhi : MonoBehaviour
{
    //当生命 降低到一定比例时候 开始提高硬直
    // Start is called before the first frame update

    [Header("当生命降低到多少比例的时候 开始 增加硬直(0-100)")]
    public float LiveBili = 50;

    RoleDate _roleDate;
    void Start()
    {
        if (_roleDate == null) _roleDate = GetComponent<RoleDate>();
    }

    bool IsHasChangeYZ = false;
    // Update is called once per frame
    void Update()
    {
        if (IsHasChangeYZ) return;
        if (_roleDate.live <= _roleDate.maxLive* LiveBili*0.01f)
        {
            IsHasChangeYZ = true;
            _roleDate.yingzhi += 1000;
        }
    }
}
