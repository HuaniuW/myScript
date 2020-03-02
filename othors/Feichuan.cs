using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feichuan : MonoBehaviour {
    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float distanceMQ = 0.13f;


    [Header("侦测地板的射线起点2")]
    public UnityEngine.Transform groundCheck2;
    [Header("地面图层")]
    public LayerMask groundLayer;


    bool isHitWall = false;
    public bool IsHitWall
    {
        get
        {
            if (groundCheck2 == null) return false;
            Vector2 start = groundCheck2.position;
            Vector2 end = new Vector2(start.x+distanceMQ, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            isHitWall = Physics2D.Linecast(start, end, groundLayer);
            return isHitWall;
        }
    }

    // Use this for initialization
    void Start () {
		
	}


    public bool IsMoveX = true;
	// Update is called once per frame
	void Update () {
        if (IsMoveY) MoveY();
        if (IsHitWall) return;
        if (IsMoveX) MoveX();
        
    }

    public float speedX = 0.1f;

    void MoveSpace()
    {
        this.transform.position = new Vector2(this.transform.position.x + speedX, this.transform.position.y);
    }

    public bool IsMoveY = false;
    float speedY = 0.005f;
    float moveYDistance = 0;
 

    List<Transform> objList = new List<Transform>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("Trigger - A");
        //obj2 = collision.collider.transform;
        if (!objList.Contains(collision.collider.transform)) objList.Add(collision.collider.transform);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("Trigger - B");
        if (objList.Contains(collision.collider.transform)) objList.Remove(collision.collider.transform);
    }

    private void MoveY()
    {
        moveYDistance += speedY;
        if (moveYDistance >= 0.2f)
        {
            speedY = -0.003f;
        }
        else if (moveYDistance <= -0.2f)
        {
            speedY = 0.005f;
        }
        //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + speedY);

        float moveY = transform.position.y + speedY;
        transform.position = new Vector3(transform.position.x, moveY, transform.position.z);
        foreach (Transform t in objList)
        {
            //print(t.tag);
            if (t.tag == "Player")
            {
                var cubeF = GameObject.Find("/MainCamera");
                //print("cccc   "+cubeF);
                if (cubeF.GetComponent<CameraController>().IsOutY)
                {
                    //print("hiiiiiii");
                    //cubeF.GetComponent<CameraController>().IsOutY2 = true;
                    float cmy = cubeF.transform.position.y + speedY;
                    cubeF.transform.position = new Vector3(cubeF.transform.position.x, cmy, cubeF.transform.position.z);
                }
                else
                {
                    //cubeF.GetComponent<CameraController>().IsOutY2 = false;
                }

            }
            float cy = t.transform.position.y + speedY;
            t.transform.position = new Vector3(t.transform.position.x, cy, t.transform.position.z);
        }

    }

    private void MoveX()
    {
        float moveX = this.transform.position.x + speedX;
        this.transform.position = new Vector3(moveX, this.transform.position.y, this.transform.position.z);
        foreach (Transform t in objList)
        {
            float cx = t.transform.position.x + speedX;
            t.transform.position = new Vector3(cx, t.transform.position.y, t.transform.position.z);
        }

        //print(objList.Count);

    }

}
