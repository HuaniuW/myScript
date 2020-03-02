using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xunlu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Vector2> roadList = new List<Vector2> { };

    List<List<Vector2>> HasCeck = new List<List<Vector2>> { };

    List<List<Vector2>> NewCeck = new List<List<Vector2>> { };

    void ClearAllList()
    {
        //List_ZB.Clear();
        HasCeck.Clear();
        NewCeck.Clear();
        _yuandian = Vector2.zero;
    }

    Vector2 _yuandian = Vector2.zero;

    GameObject _obj;
    Vector2 _targetZB;

    //根据目标坐标获取 寻路 坐标数组
    public List<Vector2> GetListZB(GameObject obj, Vector2 targetZB,Vector2 chosePos)
    {
        if (_yuandian == Vector2.zero)
        {
            //给出第一个点位置
            _yuandian = new Vector2((int)obj.transform.position.x, (int)obj.transform.position.y);
            //找到该点周围8个点 可以用的点 存入待计算列表  记录父节点
            List<List<Vector2>> Temp8zb = Get8FXZBInList(_yuandian);
            _obj = obj;
            _targetZB = targetZB;
        }
        else
        {
            Get8FXZBInList(chosePos);
        }

        //return null;


        // 代价排序 从代价最小的 开始查找 查找过的存入 已计算列表   遍历所有待计算数据中最小的优先开始
        List<Vector2> choseMinVec2 = new List<Vector2> { };
        float daijia = 0;
        //如果 待检测数组用完 表示 没有路径
        if (NewCeck.Count == 0) return null;
        for(int i=0;i<NewCeck.Count;i++) {
            
            if (i == 0)
            {
                choseMinVec2 = NewCeck[0];
                daijia = Daijiajisuan(choseMinVec2[0], _yuandian, targetZB);
            }
            else
            {
                if (daijia > Daijiajisuan(NewCeck[i][0], _yuandian, targetZB))
                {
                    daijia = Daijiajisuan(NewCeck[i][0], _yuandian, targetZB);
                    choseMinVec2 = NewCeck[i];
                }
            }
        }

        // 将最小点 从 待测列表清除
        NewCeck.Remove(choseMinVec2);
        //寻找的点 是否靠近终点 只要距离寻找点 小于 一个值就算找到了 路线中 最靠近终点的这个点 不加入路径list 保留parent+终点来替换
        if ((targetZB - choseMinVec2[0]).sqrMagnitude > checkDistance) {
            //将最小点 加入 已测列表
            HasCeck.Add(choseMinVec2);
            GetListZB(_obj, _targetZB, choseMinVec2[0]);
        }
        else
        {
            //将终点 存入数组
            choseMinVec2[0] = targetZB;
            HasCeck.Add(choseMinVec2);
        }

        //从终点 找到Parent 一直找到 parent=yuandian 存入 路径列表
        tnums++;
        print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>     "+ tnums);
        return GetRoadList(targetZB);
    }

    int tnums = 0;
    List<Vector2> GetRoadList(Vector2 parentPos)
    {
        if (HasCeck.Count == 0) return null;
        for (int i = HasCeck.Count-1;i>=0;i--)
        {
            print(i +"  ----------------------------------------------------->>>??? "+ HasCeck.Count);
            //if (i >= HasCeck.Count) break;
            if(HasCeck[i][0] == parentPos)
            {
                roadList.Add(HasCeck[i][0]);
                if (HasCeck[i][1] == _yuandian) {
                    //划线
                    roadList.Add(_yuandian);
                    GetLineByList();
                    return roadList;
                }
                else
                {
                    Vector2 zb = HasCeck[i][1];
                    print("HasCeck.Count>>>    " + HasCeck.Count);
                    HasCeck.Remove(HasCeck[i]);
                    print("HasCeck.Count>>>2222222    " + HasCeck.Count);
                    GetRoadList(zb);
                }
                
            }                    
        }
        return null;
    }

    void GetLineByList()
    {
        for(int i = 0;i< roadList.Count; i++)
        {
            if(i< roadList.Count-1) Debug.DrawLine(roadList[i], roadList[i+1],Color.green);
        }
    }

    float Daijiajisuan(Vector2 v0,Vector2 yuandian,Vector2 targetZB)
    {
        float daijia = 0;
        daijia = (v0 - yuandian).sqrMagnitude+(v0-targetZB).sqrMagnitude;
        return daijia;
    }


    int checkDistance = 2;

    int PointCheckDistance = 2;

    int zhuijiDistance = 20;

    //寻找周边8个 坐标 看是否 是可以进入 待计算列表
   /* List<List<Vector2>> Get8FXZBInList(Vector2 v0)
    {
        List<List<Vector2>> CanUsePointList = new List<List<Vector2>> { };
        List<Vector2> vl = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y), v0 };
        //if(!IsPointHitWall())
        if (!IsPointHitWall(vl[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vl[0], "l")) CanUsePointList.Add(vl);
        }
        List<Vector2> vlu = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y + checkDistance), v0 };
        if (!IsPointHitWall(vlu[0])) {
            if (!GetCheckDistancePointIsCanBeUse(vlu[0], "l") && !GetCheckDistancePointIsCanBeUse(vlu[0], "t")) CanUsePointList.Add(vlu);
        }
        
        List<Vector2> vu = new List<Vector2> { new Vector2(v0.x, v0.y + checkDistance), v0 };
        if (!IsPointHitWall(vu[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vl[0], "t")) CanUsePointList.Add(vu);
        }

        List<Vector2> vur = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y + checkDistance), v0 };
        if (!IsPointHitWall(vur[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vlu[0], "t") && !GetCheckDistancePointIsCanBeUse(vlu[0], "r")) CanUsePointList.Add(vur);
        }
       
        List<Vector2> vr = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y), v0 };
        if (!IsPointHitWall(vr[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vl[0], "r")) CanUsePointList.Add(vr);
        }
        
        List<Vector2> vrd = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y - checkDistance), v0 };
        if (!IsPointHitWall(vrd[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vrd[0], "r") && !GetCheckDistancePointIsCanBeUse(vrd[0], "d")) CanUsePointList.Add(vrd);
        }
        
        List<Vector2> vd = new List<Vector2> { new Vector2(v0.x, v0.y - checkDistance), v0 };
        if (!IsPointHitWall( vd[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vd[0], "d")) CanUsePointList.Add(vd);
        }
        
        List<Vector2> vld = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y - checkDistance), v0 };
        if (!IsPointHitWall(vld[0]))
        {
            if (!GetCheckDistancePointIsCanBeUse(vld[0], "d") && !GetCheckDistancePointIsCanBeUse(vld[0], "l")) CanUsePointList.Add(vld);
        }
        

        //CanUsePointList 数据列表 在已经加入 待检测列表对比  排除重复
        return CanUsePointGetInList(CanUsePointList);
    }*/





    bool IsPointHitWall(Vector2 point)
    {
        //Debug.DrawRay(point, Vector2.one, Color.yellow);
        //return Physics2D.BoxCast(point, Vector2.one,0, Vector2.zero, GroundLayer);
        return Physics2D.OverlapPoint(point, GroundLayer);
    }

    bool _IsHitWall(Vector2 start,Vector2 end)
    {
        Physics2D.OverlapPoint(start, GroundLayer);
        _isHitWall  = Physics2D.Linecast(start, end, GroundLayer);
        if (_isHitWall)
        {
            Debug.DrawLine(start, end, Color.cyan);
        }
        else
        {
            Debug.DrawLine(start, end, Color.yellow);
        }
        return _isHitWall;
    }


      List<List<Vector2>> Get8FXZBInList(Vector2 v0)
      {
          List<List<Vector2>> CanUsePointList = new List<List<Vector2>> { };
          List<Vector2> vl = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vl[0], "l") && !GetCheckDistancePointIsCanBeUse(vl[0], "r") && !GetCheckDistancePointIsCanBeUse(vl[0], "t") && !GetCheckDistancePointIsCanBeUse(vl[0], "d")) CanUsePointList.Add(vl);
          List<Vector2> vlu = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y + checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vlu[0], "l") && !GetCheckDistancePointIsCanBeUse(vlu[0], "t") && !GetCheckDistancePointIsCanBeUse(vlu[0], "r") && !GetCheckDistancePointIsCanBeUse(vlu[0], "d")) CanUsePointList.Add(vlu);
          List<Vector2> vu = new List<Vector2> { new Vector2(v0.x, v0.y + checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vl[0], "t") && !GetCheckDistancePointIsCanBeUse(vl[0], "d") && !GetCheckDistancePointIsCanBeUse(vl[0], "l") && !GetCheckDistancePointIsCanBeUse(vl[0], "r")) CanUsePointList.Add(vu);
          List<Vector2> vur = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y + checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vlu[0], "t") && !GetCheckDistancePointIsCanBeUse(vlu[0], "r") && !GetCheckDistancePointIsCanBeUse(vlu[0], "d") && !GetCheckDistancePointIsCanBeUse(vlu[0], "l")) CanUsePointList.Add(vur);
          List<Vector2> vr = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vl[0], "r") && !GetCheckDistancePointIsCanBeUse(vl[0], "l") && !GetCheckDistancePointIsCanBeUse(vl[0], "t") && !GetCheckDistancePointIsCanBeUse(vl[0], "d")) CanUsePointList.Add(vr);
          List<Vector2> vrd = new List<Vector2> { new Vector2(v0.x + checkDistance, v0.y - checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vrd[0], "r") && !GetCheckDistancePointIsCanBeUse(vrd[0], "d") && !GetCheckDistancePointIsCanBeUse(vrd[0], "t") && !GetCheckDistancePointIsCanBeUse(vrd[0], "l")) CanUsePointList.Add(vrd);
          List<Vector2> vd = new List<Vector2> { new Vector2(v0.x, v0.y - checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vd[0], "d") && !GetCheckDistancePointIsCanBeUse(vd[0], "t") && !GetCheckDistancePointIsCanBeUse(vd[0], "l") && !GetCheckDistancePointIsCanBeUse(vd[0], "r")) CanUsePointList.Add(vd);
          List<Vector2> vld = new List<Vector2> { new Vector2(v0.x - checkDistance, v0.y - checkDistance), v0 };
          if (!GetCheckDistancePointIsCanBeUse(vld[0], "d") && !GetCheckDistancePointIsCanBeUse(vld[0], "l") && !GetCheckDistancePointIsCanBeUse(vld[0], "r") && !GetCheckDistancePointIsCanBeUse(vld[0], "t")) CanUsePointList.Add(vld);
          //Time.timeScale = 0;
          //CanUsePointList 数据列表 在已经加入 待检测列表对比  排除重复
          return CanUsePointGetInList(CanUsePointList);
      }



    bool _isHitWall = false;
    //边缘检测 如果 边缘没有碰撞到 墙体 则为之可以用（怪物可以通过） 根据怪物体型判断
    bool GetCheckDistancePointIsCanBeUse(Vector2 vPoint, string direction) {
        Vector2 start = vPoint;
        Vector2 end = Vector2.zero;
        switch (direction)
        {
            case "l":
                end = new Vector2(start.x- PointCheckDistance, start.y);
                break;
            case "lt":
                end = new Vector2(start.x - PointCheckDistance, start.y+ PointCheckDistance);
                break;
            case "t":
                end = new Vector2(start.x, start.y + PointCheckDistance);
                break;
            case "tr":
                end = new Vector2(start.x + PointCheckDistance, start.y + PointCheckDistance);
                break;
            case "r":
                end = new Vector2(start.x + PointCheckDistance, start.y);
                break;
            case "rd":
                end = new Vector2(start.x + PointCheckDistance, start.y - PointCheckDistance);
                break;
            case "d":
                end = new Vector2(start.x , start.y - PointCheckDistance);
                break;
            case "dl":
                end = new Vector2(start.x - PointCheckDistance, start.y - PointCheckDistance);
                break;
        }

        print(start+" ---------------------------------------------------------------------------------------->>?   "+end);
        _isHitWall = Physics2D.Linecast(start, end, GroundLayer);
        if (_isHitWall)
        {
            Debug.DrawLine(start, end, Color.red);
        }
        else
        {
            Debug.DrawLine(start, end, Color.blue);
        }
        return _isHitWall;
    }


    [Header("探测地面图层")]
    public LayerMask GroundLayer;

    //加一个 已经判断过的 点 也要排除
    bool CompareInNewCeck(Vector2 vc)
    {
        //判断距离 如果距离超出 追击距离 也要排除
        if (Mathf.Abs(vc.x - _yuandian.x) >= zhuijiDistance || Mathf.Abs(vc.y - _yuandian.y) >= zhuijiDistance) return false;

        if (NewCeck.Count == 0&& HasCeck.Count == 0) return false;
        //判断 是否在 待判定列表 和已经检测列表
        foreach (List<Vector2> o in NewCeck)
        {
            if (o[0] == vc) return true;
        }

        foreach (List<Vector2> p in HasCeck)
        {
            if (p[0] == vc) return true;
        }

        return false;
    }

    //将可以用的 点 加入到 待check列表
    List<List<Vector2>> CanUsePointGetInList(List<List<Vector2>> Vlist)
    {
        List<List<Vector2>> CanUseList = new List<List<Vector2>> { };
        foreach(List<Vector2> p in Vlist)
        {
            if (!CompareInNewCeck(p[0])) CanUseList.Add(p);
        }

        if (CanUseList.Count == 0) return CanUseList;
        foreach(List<Vector2> canUsePosVector2 in CanUseList)
        {
            NewCeck.Add(canUsePosVector2);
        }

        return CanUseList;
    }
}
