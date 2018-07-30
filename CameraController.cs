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

    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值

    public bool IsFollowing { get; set; }//用来判断是否跟随

    private float cameraZ = 0;
    private float yuanCameraZ = 0;

    void Start()
    {
        _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
        _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
        IsFollowing = true;//默认为跟随
        cameraZ = transform.position.z;
        yuanCameraZ = cameraZ - 5;
        Application.targetFrameRate = 60;



        //print(obj.transform.position);
    }

    void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        if (IsFollowing)
        {
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
            if (Mathf.Abs(y - player.position.y) > Margin.y)
            {//如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的y
                y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
                if (Mathf.Abs(y - player.position.y) < 0.3) y = player.position.y;
                //y = player.position.y;
            }
        }
        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        //print("   orthographicSize   "+ orthographicSize+"   s摄像机位置  "+ transform.position+"  角色的位置 "+ player.position);
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);//限定x值
        y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);//限定y值
        transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置

       
        isYuan = player.position.x >= 10 ? true : false;
        GetCameraZ();
    }

    bool isYuan = false;
    void GetCameraZ()
    {
        var z = transform.position.z;
        if (isYuan)
        {
            if(z> yuanCameraZ)
            {
                z = Mathf.Lerp(z, yuanCameraZ, 2 * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
            }
        }
        else
        {
            if (z < cameraZ)
            {
                z = Mathf.Lerp(z, cameraZ, 2 * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
            }
        }

    }
}