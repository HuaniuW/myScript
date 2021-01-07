using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuDong : MonoBehaviour
{
    float _thisX = 0;
    float _thisY = 0;
    float _thisZ = 0;
    public float MoveXDistance = 0;
    public float MoveYDistance = 0;
    public float MoveZDistance = 0;

    public bool IsRandom = true;
    public bool IsRandomZ = true;

    // Start is called before the first frame update
    void Start()
    {
        GetPostionReset();
        if (IsRandom) GetRandom();
    }

    void GetPostionReset()
    {
        _thisX = this.transform.position.x;
        _thisY = this.transform.position.y;
        _thisZ = this.transform.position.z;
    }

    void GetRandom()
    {
        MoveXDistance = 1 + GlobalTools.GetRandomDistanceNums(1);
        MoveYDistance = 1 + GlobalTools.GetRandomDistanceNums(1);
        MoveZDistance = 1 + GlobalTools.GetRandomDistanceNums(1);
        MoveSpeedX = 0.002f+ GlobalTools.GetRandomDistanceNums(0.008f);
        MoveSpeedY = 0.002f + GlobalTools.GetRandomDistanceNums(0.008f);
        if(IsRandomZ)MoveSpeedZ = 0.002f + GlobalTools.GetRandomDistanceNums(0.008f);

    }

    // Update is called once per frame
    void Update()
    {
        GetMove();
    }


    public float MoveSpeedX = 0;
    public float MoveSpeedY = 0;
    public float MoveSpeedZ = 0;
    void GetMove()
    {
       

        //if (IsRandomZ)
        //{
        //    if (this.transform.position.z > _thisZ + MoveZDistance || this.transform.position.z < _thisZ - MoveZDistance)
        //    {
        //        MoveSpeedZ *= -1;
        //    }
        //}

        float moveX = this.transform.position.x + MoveSpeedX;
        float moveY = this.transform.position.y + MoveSpeedY;

        this.transform.position = new Vector3(moveX, moveY, this.transform.position.z + MoveSpeedZ);

        if (!IsCanHitPlayer) return;
        var cubeF = GameObject.Find("/MainCamera");
        foreach (Transform t in objList)
        {
            //print(t.tag);
            if (t == null) continue;
            if (t.tag == "Player")
            {
                //print("????");    
                //print("cccc   "+cubeF);
                if (cubeF.GetComponent<CameraController>().IsOutY)
                {
                    //print("hiiiiiii");
                    //cubeF.GetComponent<CameraController>().IsOutY2 = true;\
                    float cmy = cubeF.transform.position.y + MoveSpeedY;
                    float cmx = cubeF.transform.position.x + MoveSpeedX;
                    cubeF.transform.position = new Vector3(cmx, cmy, cubeF.transform.position.z);
                    //return;
                }
                else
                {
                    //cubeF.GetComponent<CameraController>().IsOutY2 = false;
                }

            }
            //if (t == null) {
            //    //objList.Remove(t);
            //    continue;
            //}



            float cy = t.transform.position.y + MoveSpeedY;
            float cx = t.transform.position.x + MoveSpeedX;
            t.transform.position = new Vector3(cx, cy, t.transform.position.z);
        }


        if (this.transform.position.x > _thisX + MoveXDistance || this.transform.position.x < _thisX - MoveXDistance)
        {
            MoveSpeedX *= -1;
        }

        if (this.transform.position.y > _thisY + MoveYDistance || this.transform.position.y < _thisY - MoveYDistance)
        {
            MoveSpeedY *= -1;
        }


    }






    public bool IsCanHitPlayer = false;

    Transform[] tarr = { };
    List<Transform> objList = new List<Transform>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsCanHitPlayer) return;
        if (collision.collider.tag == "AirEnemy") return;
        print("Trigger - A  碰到角色！！ ");
        //obj2 = collision.collider.transform;
        if (!objList.Contains(collision.collider.transform)) objList.Add(collision.collider.transform);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsCanHitPlayer) return;
        if (collision.collider.tag == "AirEnemy") return;
        print("Trigger - B     ---- 角色离开 ");
        if (objList.Contains(collision.collider.transform)) objList.Remove(collision.collider.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //print("Trigger - C  " + collision.collider.name);
        //float my = collision.collider.transform.position.x + mspeed;
        //collision.collider.transform.position = new Vector3(my, collision.collider.transform.position.y, collision.collider.transform.position.z);
        //collision.collider.transform.position = new Vector3(collision.collider.transform.position.x, my, collision.collider.transform.position.z);

    }

}
