using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel<GamePanel>
{
    public Button btnBack;
    public TMP_Text labTime;
    public List<GameObject> hpObjs;
    public float nowTime;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            QuitPanel.Instance.ShowMe();
        });
        ChangeHP(PlayerObject.Instance.nowHp);
        // Invoke("TestFunc", 5f);

    }
    void TestFunc()
    {
        GameOverPanel.Instance.ShowMe();
    }
    public void ChangeHP(int hp)
    {
        for (int i = 0; i < hpObjs.Count; i++)
        {
            if (i < hp)
            {
                hpObjs[i].SetActive(true);
            }
            else
            {
                hpObjs[i].SetActive(false);
            }
        }
    }
    void Update()
    {
        nowTime += Time.deltaTime;
        labTime.text = "";
        if ((int)nowTime / 3600 > 0)
        {
            labTime.text += (int)nowTime / 3600 + "h";
        }
        if ((int)nowTime % 3600 / 60 > 0 || labTime.text != "")
        {
            labTime.text += (int)nowTime % 3600 / 60 + "m";
        }
        labTime.text += (int)nowTime % 60 + "s";
    }
}
