using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RankData
{
    public List<RankInfo> rankList = new List<RankInfo>();
}
public class RankInfo
{
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public int time;
}
