using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 对象池管理脚本，无需挂载
/// </summary>
public class ObjectPools
{
    #region 对象池

    /// <summary>
    /// 对象池1，创建单个对象
    /// </summary>
    List<GameObject> pools1;

    /// <summary>
    /// 以字典的方式储存对象到池中，int类型key，集合类型的value
    /// </summary>
    Dictionary<int, List<GameObject>> pools2;
    Dictionary<int, Vector3> poolsV3;

    //单例
    private static ObjectPools instance;

    /// <summary>
    /// 获取对象，单例
    /// </summary>
    /// <returns></returns>
    public static ObjectPools GetInstance()
    {
        if (instance == null)
        {
            instance = new ObjectPools();
        }
        return instance;
    }

    //构造函数初始化对象池
    public ObjectPools()
    {
        pools1 = new List<GameObject>();
        pools2 = new Dictionary<int, List<GameObject>>();
        poolsV3 = new Dictionary<int, Vector3>();
    }

    #endregion

    #region 储存单个游戏对象到池中

    /// <summary>
    /// 储存单个游戏对象到池中
    /// </summary>
    /// <param name="gameObject">创建的游戏对象</param>
    /// <param name="pos">位置</param>
    /// <param name="rotation">旋转</param>
    /// <returns>返回的对象</returns>
    public GameObject SwpanObject(GameObject gameObject)
    {
        //Debug.Log("pools1.Count>" + pools1.Count);
        //如果池中的对象数量大于0
        if (pools1.Count > 0)
        {
            GameObject result =  pools1[0]; //提取第一个对象
            pools1.Remove(result); //从集合中清除提取的对象
            result.SetActive(true); //设置当前对象激活
            return result; //返回游戏对象
        }
        //否则，直接实例化游戏对象并返回
        return GameObject.Instantiate(gameObject) as GameObject;
    }


    /// <summary>
    /// 隐藏当前游戏对象并添加到池中
    /// </summary>
    /// <param name="gameObejct">游戏对象</param>
    public void DestoryObject(GameObject gameObejct)
    {
        //关闭当前游戏对象显示
        gameObejct.SetActive(false);
        //添加到集合中
        pools1.Add(gameObejct);
    }



    #endregion

    #region 储存多个游戏对象到池中

    /// <summary>
    /// 实例化并储存游戏对象到池中
    /// </summary>
    /// <param name="gameObject">被创建的游戏对象</param>
    /// <param name="pos">位置</param>
    /// <param name="rotation">旋转</param>
    /// <returns>返回当前游戏对象</returns>
    public GameObject SwpanObject2(GameObject gameObject)
    {
        //获取到当前对象的ID（每个游戏对象都有各自的ID）
        int key = gameObject.GetInstanceID();
        //Debug.Log("key "+key);
        //如果池中包含key当前游戏对象的key值（游戏对象的ID）
        if (pools2.ContainsKey(key))
        {
            //如果池中的对象数量大于0
            if (pools2[key].Count > 0)
            {
                //提取第一个对象（key就是ID（ID就是相对应的游戏对象），0代表的是这个key中第一个对象）
                GameObject result = pools2[key][0];
                result.SetActive(true); //激活显示当前对象
                pools2[key].Remove(result); //从池中清除对象游戏对象
                result.transform.position = Vector3.zero; //设置初始位置
                result.transform.localEulerAngles = poolsV3[key];
                //Debug.Log("result.transform.localScale  "+ result.transform.localScale);
                //result.transform.rotation = rotation; //设置初始旋转
                return result; //返回提取的游戏对象
            }
        }
        //否则，实例化对象并接收
        GameObject res = GameObject.Instantiate(gameObject) as GameObject;
        //res.transform.localScale = new Vector3(-1, 1, 1);
        //储存游戏对象的ID，转换成字符串
        res.name = gameObject.GetInstanceID().ToString();
        if (!poolsV3.ContainsKey(key))
        {
            //几率特效初始角度
            poolsV3.Add(key, res.transform.localEulerAngles);
        }
        //返回场景的游戏对象
        return res;
    }

   


    /// <summary>
    /// 隐藏当前游戏对象并添加到池中
    /// </summary>
    /// <param name="gameObject">游戏对象</param>
    public void DestoryObject2(GameObject gameObject)
    {
        //当前游戏对象不激活
        gameObject.SetActive(false);
        //获取当前游戏对象的key值，转换成int类型
        int key = int.Parse(gameObject.name);
        //Debug.Log("回收id  "+key);
        //如果池中不包含key值对应的游戏对象
        if (!pools2.ContainsKey(key))
        {
            //添加对象到池中，并开辟value空间
            pools2.Add(key, new List<GameObject>());
        }
        //否则直接添加key对应的游戏对象到池中
        pools2[key].Add(gameObject);
    }

    #endregion

    #region 协程，延迟关闭游戏对象的显示

    /// <summary>
    /// 协程，延迟隐藏游戏对象，单个游戏对象
    /// </summary>
    /// <param name="gameObject">隐藏的游戏对象</param>
    /// <param name="time">隐藏的时间间隔</param>
    /// <returns></returns>
    public IEnumerator IEDestory(GameObject gameObject, float time)
    {
        //Debug.Log("time   "+time);
        yield return new WaitForFixedUpdate();
        //yield return new WaitForSeconds(time);
        GetInstance().DestoryObject(gameObject);
    }


    /// <summary>
    /// 协程，延迟隐藏游戏对象，游戏对象数组
    /// </summary>
    /// <param name="gameObject">隐藏的游戏对象</param>
    /// <param name="time">隐藏的时间间隔</param>
    /// <returns></returns>
    public IEnumerator IEDestory2(GameObject gameObject)
    {
        yield return new WaitForFixedUpdate();
        GetInstance().DestoryObject2(gameObject);
    }

    public IEnumerator IEDestory2ByTime(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        //yield return new WaitForFixedUpdate();
        //Debug.Log(">>   "+ gameObject);
        GetInstance().DestoryObject2(gameObject);
    }
    #endregion
}
