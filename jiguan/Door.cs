using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("down标点")]
    public UnityEngine.Transform downPos;

    [Header("down标点")]
    public UnityEngine.Transform upPos;

    public GameObject men;

    [Header("开门声音")]
    public AudioSource openDoorAudio;

    // Use this for initialization
    void Start () {
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
        if (Mathf.Abs(men.transform.position.y - downPos.position.y) < 1) _zt = "1";
        if (Mathf.Abs(men.transform.position.y - upPos.position.y) < 1) _zt = "0";
    }

    //是否自动记录---单独记录   men_1-0 位置在上  men_1-1 位置在下   0上1下
    public bool isSaveBySelf = false;

    //初始化属性
    //要不要带存档属性？
    //存档内有记录 门是开着的
    //进入特定场景（boss战） 关门碰撞
    //boss战胜利 开门
    //如果是单独存档 门怪打掉后 还刷不刷？（不刷门怪 怎么记录  刷们怪的画 位置给远一点 不要再存档地点开战）

    //状态-已经是开的门
    string _zt = "0";
    public void HasOpen()
    {
        //当门以及打开 返回场景时候调用
        men.transform.position = downPos.transform.position;
        _zt = "1";
    }
    
    public void Chushi()
    {
        //print("?????????");
        men.transform.position = upPos.transform.position;
        _zt = "0";
    }

    bool isClose = false;
    bool isOpen = false;
    public float moveSpeed = 0.1f;
    public void OpenDoor(UEvent e)
    {
        string str = e.eventParams.ToString();
        string _name = str.Split('-')[0];
        string zt = str.Split('-')[1];
        //print(e.eventParams.ToString() + " -zt  " + _zt + "  thisName  " + this.name + "   name " + _name);
        //如果传来的状态一样就返回 没必要改变
        if (_zt == zt) return;
        //开门用1 关门用0
        if (this.name != _name) return;
        isOpen = false;
        isClose = false;
        if (zt == "1")
        {
            isOpen = true;
            
        }else if (zt == "0")
        {
            isClose = true;
            //print("关门  "+this.name);
        }
        //声音播放
        if (openDoorAudio) openDoorAudio.Play();
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.OPEN_DOOR, OpenDoor);
    }

    // Update is called once per frame
    void Update () {

        if (isClose)
        {
            if (men && men.transform.position.y < upPos.transform.position.y)
            {
                float moveY = men.transform.position.y + moveSpeed;
                men.transform.position = new Vector3(men.transform.position.x, moveY, men.transform.position.z);
                Gensui();
            }
            else
            {
                string zt = this.name + "-0";
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, zt), this);
                isClose = false;
                if (isSaveBySelf) GlobalSetDate.instance.SaveDoor();
                if (openDoorAudio) openDoorAudio.Stop();
                _zt = "0";
            }
        }
        
        if (isOpen)
        {
            if (men&&men.transform.position.y> downPos.transform.position.y)
            {
                float moveY = men.transform.position.y - moveSpeed;
                men.transform.position = new Vector2(men.transform.position.x,moveY);
                Gensui();
            }
            else
            {
                string zt = this.name + "-1";
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.RECORDOBJ_CHANGE, zt), this);
                isOpen = false;
                if (isSaveBySelf) GlobalSetDate.instance.SaveDoor();
                if (openDoorAudio) openDoorAudio.Stop();
                _zt = "1";
            }
        }
        
    }



    //---------------------------跟随--------------------------------------------------------------------------


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
