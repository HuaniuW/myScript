using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLiziHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        ShowTXFudai();
    }

    public void ShowTXFudai()
    {
        print("stopLiziHit!!!!!!!!!!!!!!!!!!!");
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(IEDestory2());

    }

    //这里解决了 特效出现后跟着玩家跑  依然可以 点燃敌人的 问题*********注意  一定在下一帧 取消掉碰撞机
    public IEnumerator IEDestory2()
    {
        yield return new WaitForFixedUpdate();
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
