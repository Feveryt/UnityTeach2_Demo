using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

using System.Collections.Generic;


public class RankPanel : BasePanel<RankPanel>
{
    public Button btnClose;

    public ScrollRect rankScroll; // 用于放置排行榜项的父物体
    // public GameObject rankItemPrefab; // 排行榜项的预制体
    private List<RankItem> itemList = new List<RankItem>(); // 用于存储排行榜项的列表
    public override void Init()
    {
        btnClose.onClick.AddListener(() =>
                {
                    HideMe();
                });
        HideMe();


    }
    public override void ShowMe()
    {
        base.ShowMe();
        // 刷新排行榜数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        for (int i = 0; i < list.Count; i++)
        {
            if (itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(rankScroll.content, false);
                obj.transform.localPosition = new Vector3(0, -20 - i * 40, 0);
                RankItem item = obj.GetComponent<RankItem>();
                item.InitInfo(i + 1, list[i].name, list[i].time);
                itemList.Add(item);
            }
            // RankInfo info = list[i];
            // RankItem item;
            // if (i < itemList.Count)
            // {
            //     item = itemList[i];
            // }
            // else
            // {
            //     GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/RankItem"), content);
            //     item = obj.GetComponent<RankItem>();
            //     itemList.Add(item);
            // }
            // item.InitInfo(i + 1, info.name, info.time);
        }
        // 隐藏多余的排行榜项
        // for (int i = list.Count; i < itemList.Count; i++)
        // {
        //     itemList[i].gameObject.SetActive(false);
        // }
    }
}

