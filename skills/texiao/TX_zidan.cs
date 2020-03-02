using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    GameObject _player;
    bool isFaShe = false;
    public float speeds = 20;
    Vector2 v2;


    private void OnEnable()
    {
        if (!_player) _player = GlobalTools.FindObjByName("player");
        //HitKuai
        //获取角速度
        isFaShe = true;
        //v2 = GlobalTools.GetVector2ByPostion(_player.transform.position,this.transform.position,speeds);
        /*  v2 = _player.transform.position - this.transform.position;
          print("_player.transform.position    " + _player.transform.position + "  this.transform.position   " + this.transform.position+  "-----------------------zd>  " +v2);
          GetComponent<Rigidbody2D>().velocity = v2;*/
    }


    void fire()
    {
        if (_player&& isFaShe) {
            isFaShe = false;
            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);
        } 
    }

    public int bzType = 1;

    void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.tag == "Player"||Coll.tag == "diban")
        {
            if (Coll.tag == "Player" && Coll.GetComponent<RoleDate>().isCanBeHit == false) return;
            //生成爆炸
            string bzName = "TX_zidan" + bzType + "_bz";
            GameObject baozha = ObjectPools.GetInstance().SwpanObject2(Resources.Load(bzName) as GameObject);
            baozha.transform.position = this.transform.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //移除自己 
            StartCoroutine(ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject,0.2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        fire();
    }
}
