using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BulletData
{
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();
}
public class BulletInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;//1-5种移动方式
    [XmlAttribute]
    public float forwardSpeed;
    [XmlAttribute]
    public float rightSpeed;
    [XmlAttribute]
    public float roundSpeed;
    [XmlAttribute]
    public string resName;
    [XmlAttribute]
    public string deadEffRes;
    [XmlAttribute]
    public float lifeTime;
}