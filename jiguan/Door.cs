using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("移动到的y位置的标点")]
    public UnityEngine.Transform moveToY;
    public GameObject men;

    [Header("开门声音")]
    public AudioSource openDoor;

    public string DoorNum = "";

    //使用的id记录 通过记录来查找是否使用
    public string useId = "";

    public bool IsToDown = true;

    // Use this for initialization
    void Start () {
        //这里做判断是否使用 如果使用了调用HsaOpen()
        chushiY = men.transform.position.y;
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CLOSE_DOOR, CloseDoor);
    }

    public void HasOpen()
    {
        //当门以及打开 返回场景时候调用
        men.transform.position = new Vector3(men.transform.position.x, moveToY.transform.position.y, men.transform.position.z);
    }
    float chushiY;
    public void Chushi()
    {
        men.transform.position = new Vector3(men.transform.position.x, chushiY, men.transform.position.z);
    }

    bool isClose = false;
    //关门
    public void Guanbi()
    {
        isClose = true;
        isOpen = false;
    }

    bool isOpen = false;
    public float moveSpeed = 0.1f;
    public void OpenDoor(UEvent uEvent)
    {
        if (DoorNum !=  uEvent.eventParams.ToString()) return;
        isOpen = true;
        isClose = false;
        //print(uEvent.eventParams);
        
        if (openDoor) openDoor.Play();
    }
    public void CloseDoor(UEvent uEvent)
    {
        if (DoorNum != uEvent.eventParams.ToString()) return;
        Guanbi();
        //print(uEvent.eventParams);
        
        if (openDoor) openDoor.Play();
    }

    private void OnDestroy()
    {
        //print("men");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CLOSE_DOOR, CloseDoor);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
    }

    // Update is called once per frame
    void Update () {

        if (isClose)
        {
            
            if (!IsToDown)
            {
                if (men && men.transform.position.y > chushiY)
                {
                    float moveY = men.transform.position.y - moveSpeed;
                    men.transform.position = new Vector3(men.transform.position.x, moveY, men.transform.position.z);
                    Gensui();
                }
                else
                {
                    string zt = this.name + "-0";
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CLOSE_DOOR, zt),this);
                    isClose = false;
                    if (openDoor) openDoor.Stop();
                }
                return;
            }
            if (men && men.transform.position.y > chushiY)
            {
                float moveY = men.transform.position.y + moveSpeed;
                men.transform.position = new Vector3(men.transform.position.x, moveY, men.transform.position.z);
                Gensui();
            }
            else
            {
                string zt = this.name + "-0";
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CLOSE_DOOR, zt), this);

                isClose = false;
                if (openDoor) openDoor.Stop();
            }
        }
        
        if (isOpen)
        {
            if (!IsToDown)
            {
                if (men && men.transform.position.y < moveToY.transform.position.y)
                {
                    float moveY = men.transform.position.y + moveSpeed;
                    men.transform.position = new Vector3(men.transform.position.x, moveY, men.transform.position.z);
                    Gensui();
                }
                else
                {
                    string zt = this.name + "-1";
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CLOSE_DOOR, zt), this);
                    isOpen = false;
                    if (openDoor) openDoor.Stop();
                }
                return;
            }
            if (men&&men.transform.position.y>moveToY.transform.position.y)
            {
                float moveY = men.transform.position.y - moveSpeed;
                men.transform.position = new Vector3(men.transform.position.x,moveY, men.transform.position.z);
                Gensui();
            }
            else
            {
                string zt = this.name + "-1";
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CLOSE_DOOR, zt), this);
                isOpen = false;
                if (openDoor) openDoor.Stop();
            }
        }

       

    }

    Transform[] tarr = { };
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        //print("Trigger - C  " + collision.collider.name);
        //float my = collision.collider.transform.position.x + mspeed;
        //collision.collider.transform.position = new Vector3(my, collision.collider.transform.position.y, collision.collider.transform.position.z);
        //collision.collider.transform.position = new Vector3(collision.collider.transform.position.x, my, collision.collider.transform.position.z);

    }

    void Gensui()
    {
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
                    float cmy = cubeF.transform.position.y + moveSpeed;
                    cubeF.transform.position = new Vector3(cubeF.transform.position.x, cmy, cubeF.transform.position.z);
                }
                else
                {
                    //cubeF.GetComponent<CameraController>().IsOutY2 = false;
                }

            }
            float cy = t.transform.position.y + moveSpeed;
            t.transform.position = new Vector3(t.transform.position.x, cy, t.transform.position.z);
        }
    }
}
