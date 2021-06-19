using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Process: ICloneable
{
    public int id;

    /// <summary>
    /// 用于控制此流程最多可被执行的次数
    /// </summary>
    public int count;

    public BaseFragment fragment;

    public List<Process> NextProcesses;

    
    public Process targetProcess;

   public Process(int id,BaseFragment fragment,int count=1)
    {
        this.id = id;
        this.fragment = fragment;
        fragment.process = this;
    }

    public object Clone()
    {
        using (Stream objectStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as Process;
        }
    }
}
