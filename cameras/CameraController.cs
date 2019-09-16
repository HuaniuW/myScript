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
       
        if(!Bounds) Bounds = GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>();
        getBoundsMinMax();

        IsFollowing = true;//默认为跟随
        cameraZ = transform.position.z;
        yuanCameraZ = cameraZ - 5;
        Application.targetFrameRate = 60;
        oldPosition = transform.position;
        //print(obj.transform.position);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CAMERA_KUAI_REDUCTION, this.KiaoReduction);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CAMERA_SHOCK, this.GetShock);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CAMERA_KUAI_REDUCTION, this.KiaoReduction);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CAMERA_SHOCK, this.GetShock);
    }


    float theStillNums = 0;
    void GetShock(UEvent e)
    {

        string str = e.eventParams.ToString();
        string bt = str.Split('-')[0];
        if (str.Split('-').Length>1&& str.Split('-')[1] != null) theStillNums = float.Parse(str.Split('-')[1]);


        if (bt == "y")
        {
            GetShockY();
        } else if (bt == "z") {
            GetShockZ();
        }else if (bt == "z2")
        {
            GetShockZ2(theStillNums);
        }else if (bt == "end")
        {
            Shock2Stop();
        }
    }

    bool _isChangeKuang = false;
    public void GetBounds(BoxCollider2D bounds, bool isChangeKuang = false) {
        if (bounds)
        {
            Bounds = bounds;
            getBoundsMinMax();
            _isChangeKuang = isChangeKuang;
        }
    }


    void KiaoReduction(UEvent e)
    {
        if (Bounds.name != "kuang")
        {
            Bounds = GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>();
            _isChangeKuang = false;
            getBoundsMinMax();
        }
        if (isChangeByBossAndPlayer) RemoveBoss();
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


    //private void LateUpdate()
    //{

    //}


    float distanceX = 0;

    void Update()
    {
        if (!player) return;
        var x = transform.position.x;
        var y = transform.position.y;
        
        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        //print("   orthographicSize   "+ orthographicSize+"   s摄像机位置  "+ transform.position+"  角色的位置 "+ player.position);
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小

        //print(cameraHalfWidth + "   wwwwwwwwww   "+ Bounds.size);


        if (IsFollowing)
        {
            //float xNew = transform.position.x;
            //if (!isXLocked)
            //{
            //    xNew = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * smoothing.x);
            //}

            float yNew = transform.position.y;
            if (IsHitCameraKuai)
            {
                yNew = Mathf.Lerp(transform.position.y, CameraKuaiY, Time.deltaTime * smoothing.y);
            }
            else
            {
                if (!isYLocked)
                {
                    yNew = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * smoothing.y);
                }
            }
            //x = player.position.x;
            bool isInEdge = false;
            if (!_isChangeKuang)
            {
                if (player.position.x <= _min.x + cameraHalfWidth || player.position.x >= _max.x - cameraHalfWidth)
                {
                    //if (player.position.x <= _min.x + cameraHalfWidth) {
                    //    x = _min.x + cameraHalfWidth;
                    //} else if(player.position.x >= _max.x - cameraHalfWidth)
                    //{
                    //    x = _max.x - cameraHalfWidth;
                    //}

                    //distanceX = 0;
                    isInEdge = true;
                }
                else
                {
                    var vx = player.GetComponent<Rigidbody2D>().velocity.x;
                    var xzX = 3f;//值小会出现边缘卡顿 原因未理清

                    if (Mathf.Abs(vx) >= xzX)
                    {
                        if (isInEdge)
                        {
                            distanceX = 0;
                        }
                        else
                        {
                            x = player.position.x - distanceX;
                        }
                    }
                    else
                    {
                        isInEdge = false;
                        distanceX = player.position.x - x;
                        if (!player.GetComponent<GameBody>().isInAiring)
                        {
                            if (player.transform.localScale.x > 0)
                            {
                                x = Mathf.Lerp(x, player.position.x - Margin.x, 1 * Time.deltaTime);
                            }
                            else
                            {
                                x = Mathf.Lerp(x, player.position.x + Margin.x, 1 * Time.deltaTime);
                            }
                        }
                    }
                }

                //print("vx   " + player.GetComponent<Rigidbody2D>().velocity.x+ "      distanceX    "+ distanceX);
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

        ChangeByBossAndPlayer();
        if (setNewPosition) SetCameraNewPosition();
        //float targetX;
        if (_isChangeKuang)
        {
            x = player.position.x;
            if (x < _min.x + cameraHalfWidth)
            {
                if (Mathf.Abs(_min.x + cameraHalfWidth - x) < 0.2f)
                {
                    _isChangeKuang = false;
                }

            }
            else if (x > _min.x + cameraHalfWidth)
            {
                if (Mathf.Abs(_max.x - cameraHalfWidth - x) < 0.2f)
                {
                    _isChangeKuang = false;
                }
            }
        }
        else
        {
            //做个限制x的 数组  超过数组中块的x 就设为最小限制 否则设置初始的位置
            x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);//限定x值
        }

        

        if (!IsHitCameraKuai) y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);//限定y值

        if (isShockYing) y = GetShockYing();

        var z = transform.position.z;
        if (isShockZing) z = GetShockZing();

        if(isShockZing2) z = GetShockZing2();


        transform.position = new Vector3(x, y, z);//改变相机的位置
        //print("2  " + transform.position.y + "  ---  " + CameraKuaiY + "  >> " + y + "   IsHitCameraKuai  " + IsHitCameraKuai + "    IsFollowing   " + IsFollowing);

        
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

    //***************************************************************************Z轴根据玩家和BOSS距离变化

    //是否开始自动变距
    bool isChangeByBossAndPlayer = false;
    //第一次触发相机变z距 当玩家和boss接近多少触发
    bool isFirstChange = false;
    //最大Z的距离点  
    float MaxMoveToZPos = 20;
    //Z轴边距距离 需要和初始Z计算
    float ChangeZDistance;
    //Y按比例像上移动
    float MaxYUpDistance = 3;
    //最小点距离
    float minPosDistance = 12;
    //最大点距离
    float maxPosDistance = 25;
    //点距离比例限制
    float PosDistanceLimit;
    //玩家和BOSS点距离
    float BossAndPlayerPosDistance;



    GameObject boss;
    //获取boss
    public void GetBossAndMaxZPos(GameObject obj,float MaxZPos = 20) {
        boss = obj;
        MaxMoveToZPos = MaxZPos;
        ChangeZDistance = MaxMoveToZPos - this.transform.position.z;
        if (ChangeZDistance < 0||boss == null) return;
        isChangeByBossAndPlayer = true;
        PosDistanceLimit = maxPosDistance - minPosDistance;
    }

   
    //根据玩家与boss x距离 来调整z轴
    void ChangeByBossAndPlayer()
    {
        if (!isChangeByBossAndPlayer) return;
        if (!isFirstChange)
        {
            if (Mathf.Abs(boss.transform.position.x - player.position.x) < 12) {
                isFirstChange = true;
            } 
            return;
        }

        //计算点距离
        BossAndPlayerPosDistance = Vector2.Distance(player.position, boss.transform.position);
        //CameraKuaiZ  = (BossAndPlayerPosDistance - minPosDistance) / PosDistanceLimit* ChangeZDistance;
        float newZ = oldPosition.z - (BossAndPlayerPosDistance - minPosDistance) / PosDistanceLimit * ChangeZDistance;
        //float newY;
        //if (IsHitCameraKuai)
        //{
        //    print("??----->      "+(BossAndPlayerPosDistance - minPosDistance) / PosDistanceLimit * MaxYUpDistance);
        //    newY = CameraKuaiY + (BossAndPlayerPosDistance - minPosDistance) / PosDistanceLimit * MaxYUpDistance;
        //}
        //else
        //{
        //    newY = oldPosition.y + (BossAndPlayerPosDistance - minPosDistance) / PosDistanceLimit * MaxYUpDistance;
        //}
        if (newZ < -20) newZ = -20;
        if (newZ > oldPosition.z) newZ = oldPosition.z;
        float _x;
        if (player.position.x > boss.transform.position.x)
        {
            _x = player.position.x - (Mathf.Abs(boss.transform.position.x- player.position.x) * 0.5f)-15;
        }
        else
        {
            _x = player.position.x + (Mathf.Abs(boss.transform.position.x - player.position.x) * 0.5f)+15;
        }
        //_x = boss.transform.position.x;
        //print("_x    "+_x);
        Vector3 newPos = new Vector3(_x, this.transform.position.y, newZ);
        SetNewPosition(newPos);
       

    }

    //移除boss
    public void RemoveBoss()
    {
        boss = null;
        isChangeByBossAndPlayer = false;
        isFirstChange = false;
        BackOldPosition();
    }



    //**********************************************************震动 Y  和 Z

    bool isShockY = false;
    bool isShockYing = false;
    float targetY;
    //弹力系数
    float spring = 1f;
    //摩擦力  摩擦力与弹力系数越接近 持续时间越长
    float friction = 0.9f;
    float vy = 0;

    bool IsStillShock = true;

    float newY2;
    void GetShockY()
    {
        newY2 = this.transform.position.y;
        if (!isShockY)
        {
            isShockY = true;
            isShockYing = true;
            targetY = newY2 + 0.2f;
        }
    }



    float GetShockYing()
    {
        newY2 = this.transform.position.y;
        if (isShockYing)
        {
            vy += (targetY - newY2) * spring;
            newY2 += (vy *= friction);

            if(decimal.Round(decimal.Parse(newY2.ToString()), 2) == decimal.Round(decimal.Parse(targetY.ToString()), 2))
            {
                isShockYing = false;
                isShockY = false;
            }
        }
        return newY2;
    }

    float targetZ;
    bool isShockZ = false;
    bool isShockZing = false;
    float vz = 0;
    float newZ2;
    void GetShockZ()
    {
        newZ2 = this.transform.position.z;
        if (!isShockZ)
        {
            isShockZ = true;
            isShockZing = true;
            targetZ = newZ2 + 2f;
        }
    }

    float GetShockZing()
    {
        newZ2 = this.transform.position.z;
        if (isShockZing)
        {
            vz += (targetZ - newZ2) * spring;
            newZ2 += (vz *= friction);
            //if (IsStillShock) newZ2 = vz;

            if (decimal.Round(decimal.Parse(newZ2.ToString()), 2) == decimal.Round(decimal.Parse(targetZ.ToString()), 2))
            {
                isShockZing = false;
                isShockZ = false;
            }
        }
        return newZ2;
    }



    //持续震动z

    float targetZ2;
    float targetZ21;
    float theTargetZ;

    float yuanshiZ;

    bool isShockZ2 = false;
    bool isShockZing2 = false;
    float vz2 = 0;
    float newZ22;

    TheTimer stillTimes = new TheTimer();
    void GetShockZ2(float stillNums)
    {
        newZ22 = this.transform.position.z;
        if (!isShockZ2)
        {
            isShockZ2 = true;
            isShockZing2 = true;
            targetZ2 = newZ22 + 0.4f;
            targetZ21 = newZ22 - 0.4f;
            theTargetZ = targetZ2;

            yuanshiZ = this.transform.position.z;
            stillTimes.GetStopByTime(theStillNums);
        }
    }

    



    float GetShockZing2()
    {
        newZ22 = this.transform.position.z;
        if (isShockZing2)
        {
            newZ22+= (theTargetZ - newZ22) * 0.4f;

            if (Mathf.Abs(targetZ2 - newZ22)<0.2f)
            {
                newZ22 = targetZ2 - 0.22f;
                theTargetZ = targetZ21;
            }else if (Mathf.Abs(targetZ21 - newZ22)<0.2f)
            {
                newZ22 = targetZ21 + 0.22f;
                theTargetZ = targetZ2;
            }

            if (stillTimes.isStart&& stillTimes.IsPauseTimeOver())
            {
                isShockZing2 = false;
                isShockZ2 = false;
                this.transform.position =new Vector3(this.transform.position.x,this.transform.position.y,yuanshiZ);
            }
        }
        return newZ22;
    }


    void Shock2Stop()
    {
        stillTimes.End();
        isShockZing2 = false;
        isShockZ2 = false;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, yuanshiZ);
    }


}