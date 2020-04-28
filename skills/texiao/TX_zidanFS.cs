using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidanFS : MonoBehaviour
{

    [Header("发射子弹的名字")]
    public string ZDName = "";

    // Start is called before the first frame update
    void Start()
    {
        print("  子弹发射！ ");
    }



    void FaSheZiDan()
    {
        if (ZDName == "") return;
        //找到发射点
        print("  ATKoBJ "+GetComponent<JN_base>().atkObj);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
