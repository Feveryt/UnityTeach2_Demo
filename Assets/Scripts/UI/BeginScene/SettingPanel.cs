using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingPanel : BasePanel<SettingPanel>
{
    // µ¥Àý


    [Header("ÒýÓÃ")]
    public Button btn_Close;
    public Slider slider_Music;
    public Toggle toggle_Music;

    public Slider slider_Voice;
    public Toggle toggle_Voice;


    public override void Init()
    {
        btn_Close.onClick.AddListener(() =>
        {
            HideMe();
        });
        slider_Music.onValueChanged.AddListener(value =>
        {
            GameDataMgr.Instance.SetMusicVolume(value);
        });
        toggle_Music.onValueChanged.AddListener(OnMusicToggleChanged);
        slider_Voice.onValueChanged.AddListener(OnVoiceVolumeChanged);
        toggle_Voice.onValueChanged.AddListener(OnVoiceToggleChanged);
        // HideMe();
    }

    // private void OnMusicVolumeChanged(float arg0)
    // {
    //     GameDataMgr.Instance.SetMusicVolume(arg0);
    // }
    private void OnMusicToggleChanged(bool isOn)
    {
        GameDataMgr.Instance.SetMusicIsOpen(isOn);
    }
    private void OnVoiceVolumeChanged(float value)
    {
        GameDataMgr.Instance.SetVoiceVolume(value);
    }
    private void OnVoiceToggleChanged(bool isOn)
    {
        GameDataMgr.Instance.SetVoiceIsOpen(isOn);
    }

    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.musicData;
        slider_Music.value = musicData.musicVolume;
        toggle_Music.isOn = musicData.isMusicOn;
        slider_Voice.value = musicData.voiceVolume;
        toggle_Voice.isOn = musicData.isVoiceOn;

    }

    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}