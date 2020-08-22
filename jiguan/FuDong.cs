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
        if(this.transform.position.x>_thisX+MoveXDistance|| this.transform.position.x < _thisX - MoveXDistance)
        {
            MoveSpeedX *= -1;
        }

        if (this.transform.position.y > _thisY + MoveYDistance || this.transform.position.y < _thisY - MoveYDistance)
        {
            MoveSpeedY *= -1;
        }

        if (IsRandomZ)
        {
            if (this.transform.position.z > _thisZ + MoveZDistance || this.transform.position.z < _thisZ - MoveZDistance)
            {
                MoveSpeedZ *= -1;
            }
        }
       


        this.transform.position = new Vector3(this.transform.position.x+MoveSpeedX, this.transform.position.y + MoveSpeedY, this.transform.position.z + MoveSpeedZ);
    }

}
