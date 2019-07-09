using System.Collections;
using System;
public class UEventListener
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public string eventType;

    public UEventListener(string eventType)
    {
        this.eventType = eventType;
    }

    public delegate void EventListenerDelegate(UEvent evt);
    public event EventListenerDelegate OnEvent;

    public void Excute(UEvent evt)
    {
        if (OnEvent != null)
        {
            //System.Diagnostics.Debug.Print(this+ "    "+evt);
            //Console.WriteLine(this + "  event>  " + evt);
            this.OnEvent(evt);
        }
    }
}