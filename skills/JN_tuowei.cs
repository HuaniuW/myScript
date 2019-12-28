using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JN_tuowei : MonoBehaviour {


    [Header("是否着地")]
    public bool grounded;


    [Header("感应地板的距离")]
    [Range(0, 1)]
    public float distance;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("烟幕粒子")]
    public ParticleSystem _yanmu;

    public virtual bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }

    // Use this for initialization
    void Start () {
        _yanmu.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        tuowei();
    }

    void tuowei()
    {
        if (IsGround)
        {
            _yanmu.Play();
        }
        else
        {
            _yanmu.Stop();
        }
    }
}
