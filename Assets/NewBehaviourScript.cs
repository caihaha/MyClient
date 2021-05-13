using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;

public class NewBehaviourScript : CELLTcpClient
{
    private string _ip = "127.0.0.1";
    private UInt16 _port = 4567;

    [DllImport("CppNet100")]
    public static extern int Add(int a, int b);

    public delegate void CallBack1(string s);
    [MonoPInvokeCallback(typeof(CallBack1))]
    public void CallBackFun1(string s)
    {
        Debug.Log(s);
    }

    [DllImport("CppNet100")]
    public static extern int TestCall1(string s, CallBack1 cb);

    // Start is called before the first frame update
    void Start()
    {
        Creat();
        Connect(_ip, _port);
    }

    // Update is called once per frame
    void Update()
    {
        OnRun();
    }

    void OnDestroy()
    {
        Close();
    }
}
