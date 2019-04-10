using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("获得角色")]
    public Transform player;//获得角色
    [Header("相机与角色的相对范围")]
    public Vector2 Margin;//相机与角色的相对范围
    [Header("相机移动的平滑度")]
    public Vector2 smoothing;//相机移动的平滑度
    [Header("背景的边界")]
    public BoxCollider2D Bounds;//背景的边界

    [Header("下降时摄像机的角色最大Y距离")]
    public float maxPlayerCameraYDistanceDown = 4;

    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值

    public bool IsFollowing { get; set; }//用来判断是否跟随

    private float cameraZ = 0;
    private float yuanCameraZ = 0;



    protected bool isXLocked = false;
    protected bool isYLocked = false;

    void Start()
    {
        if (Bounds)
        {
            getBoundsMinMax();
        }
        else
        {
            Bounds = GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>();
            getBoundsMinMax();
        }
        
        IsFollowing = true;//默认为跟随
        cameraZ = transform.position.z;
        yuanCameraZ = cameraZ - 5;
        Application.targetFrameRate = 60;
        oldPosition = transform.position;
        //print(obj.transform.position);
    }

    public void GetBounds(BoxCollider2D bounds) {
        if (bounds)
        {
            Bounds = bounds;
            getBoundsMinMax();
        }
    }

    void getBoundsMinMax()
    {
        _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
        _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
    }

    void GetPlayer()
    {
        if (player)
        {
            transform.position = new Vector3(player.position.x, player.position.x, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
    }
    
    //获取焦点角色
    public void GetTargetObj(Transform obj) {
        if (obj) player = obj;
    }

    public bool IsOutY = false;
    public bool IsOutY2 = false;

    public void GetHitCameraKuaiY(float _y) {
        IsHitCameraKuai = true;
        CameraKuaiY = _y;
    }
    public void OutHitCameraKuaiY()
    {
        IsHitCameraKuai = false;
    }

    float CameraKuaiY = 0;
    bool IsHitCameraKuai = false;

    void Update()
    {
        if (!player) return;
        var x = transform.position.x;
        var y = transform.position.y;
        if (IsFollowing)
        {
            float xNew = transform.position.x;
            if (!isXLocked)
            {
                xNew = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * smoothing.x);
            }

            float yNew = transform.position.y;
            if (IsHitCameraKuai)
            {
                //yNew = CameraKuaiY;
                //print("??????????");
                yNew = Mathf.Lerp(transform.position.y, CameraKuaiY, Time.deltaTime * smoothing.y);


                
                //if (Mathf.Abs(CameraKuaiY - yNew) < 0.02f)
                //{
                    
                //    yNew = CameraKuaiY;
                //}
                //else
                //{
                //    yNew += (CameraKuaiY - transform.position.y) * 0.1f;
                //    if (yNew != CameraKuaiY) print("CameraKuaiY  " + CameraKuaiY + "  ---  " + yNew);
                //}
            }
            else
            {
                if (!isYLocked)
                {
                    yNew = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * smoothing.y);
                }
            }
            
           

            //print(">>>>>>???");
            if (Mathf.Abs(x - player.position.x) > Margin.x)
            {
                //如果相机与角色的x轴距离超过了最大范围则将x平滑的移动到目标点的x
                //用插值会抖动 不知道是不是计算过快 来回找不到值？ 让在范围内=角色位置
                //小于0.3会抖动 估计是角色 跑过摄像机 场景移动慢导致的视觉差 避免抖动 不用这种缓动跟随
                //x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
                //防止摄像机抖动
                //if(Mathf.Abs(x - player.position.x)<0.3)

                x = player.position.x;
                //x = (player.position.x-x)*(float)0.5;
            }
            /**
            if (Mathf.Abs(y - player.position.y) > Margin.y)
            {//如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的y
                y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
                if (Mathf.Abs(y - player.position.y) < 0.3) y = player.position.y;
                //y = player.position.y;
            }*/

            if (!IsHitCameraKuai)
            {
                if (y - player.position.y > Margin.y || y - player.position.y < -Margin.y)
                {
                    //IsOutY = false;
                    //if (IsOutY2)return;
                    if (y - player.position.y > maxPlayerCameraYDistanceDown)
                    {
                        y = player.position.y + maxPlayerCameraYDistanceDown;
                        IsOutY = false;

                    }
                    else
                    {
                        IsOutY = true;
                        //print("抖");
                        //if (IsOutY2) return;
                        y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
                        if (Mathf.Abs(y - player.position.y) < 0.3) y = player.position.y;
                    }
                }
            }
            else
            {
                y = Mathf.Lerp(y, CameraKuaiY, smoothing.y * Time.deltaTime);
                if (Mathf.Abs(y - CameraKuaiY) < 0.1) y = CameraKuaiY;
            }
           
            
        }
        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        //print("   orthographicSize   "+ orthographicSize+"   s摄像机位置  "+ transform.position+"  角色的位置 "+ player.position);
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
        //做个限制x的 数组  超过数组中块的x 就设为最小限制 否则设置初始的位置
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);//限定x值
        if(!IsHitCameraKuai) y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);//限定y值
        transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置


        if (setNewPosition) SetCameraNewPosition();
    }

   
    Vector3 newPositon;
    bool setNewPosition = false;
    public void SetNewPosition(Vector3 pos)
    {
        newPositon = pos;
        setNewPosition = true;
    }


    void SetCameraNewPosition()
    {
        if (transform.position.z == newPositon.z)
        {
            //transform.position = newPositon;
            setNewPosition = false;
            return;
        }

        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;
        //x = Mathf.Abs(transform.position.x - newPositon.x) < 0.5 ? newPositon.x : Mathf.Lerp(x, newPositon.x, 2 * Time.deltaTime);
        //print("y>  "+y);
        //print("---- "+ Mathf.Abs(transform.position.y - newPositon.y));
        //y = Mathf.Abs(transform.position.y - newPositon.y) < 0.02 ? newPositon.y: y+= (newPositon.y - transform.position.y) * 0.02f;

        //拉扯超出 和角色的 摄像头跟随的范围导致不停抖动
        y = Mathf.Abs(transform.position.y - newPositon.y) < 0.02 ? newPositon.y : Mathf.Lerp(y, newPositon.y, 2 * Time.deltaTime);
        z = Mathf.Abs(transform.position.z - newPositon.z) < 0.02 ? newPositon.z : Mathf.Lerp(z, newPositon.z, 2 * Time.deltaTime);
       
        transform.position = new Vector3(x, y, z);
        
        
    }

    Vector3 oldPosition;

    public void BackOldPosition()
    {
        SetNewPosition(oldPosition);
    }


}