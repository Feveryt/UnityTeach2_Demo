using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource bkAudio;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        bkAudio = this.GetComponent<AudioSource>();
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.isMusicOn);
        SetBKMusicVolume(GameDataMgr.Instance.musicData.musicVolume);
    }
    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }
    public void SetBKMusicVolume(float volume)
    {
        bkAudio.volume = volume;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
