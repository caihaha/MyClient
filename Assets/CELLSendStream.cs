using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class CELLSendStream
{
    List<byte> byteList;

    public List<byte> ByteList => byteList;

    public CELLSendStream(int nSize = 128)
    {
        byteList = new List<byte>(nSize);
    }

    public byte[] DataArray 
    { 
        get 
        {
            return byteList.ToArray();
        } 
    }

    public int DataSize
    {
        get
        {
            return byteList.Count;
        }
    }

    public void SetNetCMD(CMD cmd)
    {
        WriteUInt16((UInt16)cmd);
    }

    public void Write(byte[] data)
    {
        byteList.AddRange(data);
    }

    public void WriteInt8(sbyte n)
    {
        byteList.Add((byte)n);
    }

    public void WriteInt16(Int16 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteInt32(Int32 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteInt64(Int64 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteUInt8(byte n)
    {
        byteList.Add(n);
    }

    public void WriteUInt16(UInt16 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteUInt32(UInt32 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteUInt64(UInt64 n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteFloat(float n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteDouble(double n)
    {
        Write(BitConverter.GetBytes(n));
    }

    public void WriteString(string s)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(s);
        WriteUInt32((UInt32)buffer.Length + 1);
        Write(buffer);
        WriteUInt8(0);
    }

    public void WriteBytes(byte[] data)
    {
        WriteUInt32((UInt32)data.Length);
        Write(data);
    }

    public void WriteInts(int[] data)
    {
        WriteUInt32((UInt32)data.Length);
        for(int i = 0; i < data.Length; ++i)
        {
            WriteInt32(data[i]);
        }
    }

    public void Finsh()
    {
        UInt16 len = (UInt16)byteList.Count;
        len += 2;
        byteList.InsertRange(0, BitConverter.GetBytes(len));
    }
}
