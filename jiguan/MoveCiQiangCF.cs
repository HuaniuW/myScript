using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCiQiangCF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Header("判断 是否 是横移动 刺墙")]
    public bool IsMoveHengCiQiang = false;
    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.tag == "Player")
        {
            print("机关 刺墙 触发！！！");
            if (IsMoveHengCiQiang)
            {
                this.transform.parent.GetComponent<JG_CiQiangMoveHeng>().StartMove();
            }
            else
            {
                this.transform.parent.GetComponent<MoveCiQian>().StartMove();
            }

            
        }
    }
}
