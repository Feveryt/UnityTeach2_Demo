using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public TextMeshProUGUI txtRank;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtTime;
    public void InitInfo(int rank, string name, int time)
    {
        txtRank.text = rank.ToString();
        txtName.text = name;
        string str = "";
        if (time / 3600 > 0)
        {
            str += time / 3600 + "h";
        }
        if (time % 3600 / 60 > 0 || str != "")
        {
            str += time % 3600 / 60 + "m";
        }
        str += time % 60 + "s";
        txtTime.text = str;
    }
}
