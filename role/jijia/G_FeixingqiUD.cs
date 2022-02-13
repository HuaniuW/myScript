using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FeixingqiUD : G_Feixingqi
{


    protected override void GetUpdate()
    {
        FlyUpDown();
    }

    bool IsFlyUp = true;
    bool IsFlyDown = false;
    void FlyUpDown()
    {
        if (IsFlyUp)
        {
            if (this.transform.position.y > UpPos.transform.position.y - 5)
            {
                IsFlyUp = false;
                IsFlyDown = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,5));

        }

        if (IsFlyDown)
        {
            if (this.transform.position.y < DownPos.transform.position.y + 5)
            {
                IsFlyUp = true;
                IsFlyDown = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5));
        }
    }


}
