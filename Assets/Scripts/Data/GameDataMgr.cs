using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    public MusicData musicData;
    public RankData rankData;
    public RoleData roleData;
    public BulletData bulletData;
    public FireData fireData;
    public int nowSelHeroIndex = 0;
    private GameDataMgr()
    {
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
    }
    #region 音乐音效相关

    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }
    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.isMusicOn = isOpen;
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);

    }
    public void SetVoiceIsOpen(bool isOpen)
    {
        musicData.isVoiceOn = isOpen;
    }
    public void SetMusicVolume(float volume)
    {
        musicData.musicVolume = volume;
        BKMusic.Instance.SetBKMusicVolume(volume);
    }
    public void SetVoiceVolume(float volume)
    {
        musicData.voiceVolume = volume;

    }
    #endregion
    #region 排行榜相关
    public void SaveRankData()
    {
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    public void AddRankInfo(string name, int time)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);
        rankData.rankList.Sort((a, b) =>
         {
             if (a.time > b.time)
                 return -1;
             return 1;
         });
        if (rankData.rankList.Count > 20)
        {
            // rankData.rankList.RemoveRange(20, rankData.rankList.Count - 20);
            rankData.rankList.RemoveAt(20);
        }
    }

    #endregion
    #region 玩家数据相关
    public RoleInfo GetNowSelRoleInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    #endregion
}
