using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOutObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BeHitFlyOut(80);
    }

    protected virtual void BeHitFlyOut(float power)
    {
        //print("@@@@@@@@@---------------------------------------->!!!!!!!  ");
        float __x =  GlobalTools.GetRandomNum()>50?this.transform.position.x - GlobalTools.GetRandomDistanceNums(4): this.transform.position.x + GlobalTools.GetRandomDistanceNums(4);
        float __y = this.transform.position.y - GlobalTools.GetRandomDistanceNums(4);

        Vector2 SuiJiTuili = new Vector2(__x,__y);

        GetZongTuili(GlobalTools.GetVector2ByPostion(this.transform.position, SuiJiTuili,10) * GlobalTools.GetRandomDistanceNums(power));
        //if (GlobalTools.FindObjByName("player"))
        //{
        //    //playerRigidbody2D.AddForce(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
        //    GetZongTuili(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
        //}
        StartCoroutine(IEDieDestory(3f));
    }

    public IEnumerator IEDieDestory(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }




    public virtual void GetZongTuili(Vector2 v2, bool IsSetZero = false)
    {
        //print(this.name+"  看看谁给的 力 "+v2);
        if (IsSetZero) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //print("推力是多少？？？？？  "+v2);
        GetComponent<Rigidbody2D>().AddForce(v2);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
