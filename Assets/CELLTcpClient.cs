using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;

public class CELLTcpClient : MonoBehaviour
{
    public CELLTcpClient()
    {
        _thisObj = IntPtr.Zero;
        _cppClientObj = IntPtr.Zero;
    }
    private GCHandle _handle;
    IntPtr _thisObj;
    IntPtr _cppClientObj;

    public delegate void OnNetMsgCallBack(IntPtr obj, IntPtr data, int len);
    [MonoPInvokeCallback(typeof(OnNetMsgCallBack))]
    public void DellOnNetMsgCallBack(IntPtr csObj, IntPtr data, int len)
    {
        Debug.Log(len);
    }

    [DllImport("CppNet100")]
    public static extern IntPtr CELLClient_Create(IntPtr obj, OnNetMsgCallBack cb, int sendSize, int recvSize);

    [DllImport("CppNet100")]
    public static extern bool CELLClient_Connect(IntPtr cppClientObj, string ip, UInt16 port);

    [DllImport("CppNet100")]
    public static extern bool CELLClient_OnRun(IntPtr cppClientObj);

    [DllImport("CppNet100")]
    public static extern void CELLClient_Close(IntPtr cppClientObj);

    [DllImport("CppNet100")]
    public static extern int CELLClient_SendData(IntPtr cppClientObj, byte[] data, int len);


    public void Creat()
    {
        _handle = GCHandle.Alloc(this);
        _thisObj = GCHandle.ToIntPtr(_handle);

        _cppClientObj = CELLClient_Create(_thisObj, DellOnNetMsgCallBack, 1024, 1024);
    }

    public bool Connect(string ip, UInt16 port)
    {
        if (_cppClientObj == IntPtr.Zero)
        {
            return false;
        }

        return CELLClient_Connect(_cppClientObj, ip, port);
    }

    public bool OnRun()
    {
        if(_cppClientObj == IntPtr.Zero)
        {
            return false;
        }

        return CELLClient_OnRun(_cppClientObj);
    }

    public void Close()
    {
        if (_cppClientObj == IntPtr.Zero)
        {
            return;
        }

        CELLClient_Close(_cppClientObj);
    }

    public int SendData(byte[] data, int len)
    {
        if (_cppClientObj == IntPtr.Zero)
        {
            return 0;
        }

        return CELLClient_SendData(_cppClientObj, data, len);
    }
}
