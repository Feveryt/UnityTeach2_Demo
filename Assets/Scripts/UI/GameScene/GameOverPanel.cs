using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public TMP_Text labTime;
    public TMP_InputField inputName;
    public Button btnOK;
    private string inputNameValue;
    private int endTime;
    public override void Init()
    {
        btnOK.onClick.AddListener(() =>
        {

            inputNameValue = inputName.text;
            if (inputNameValue == "")
            {
                inputNameValue = "Íæ¼ÒÒ»Æß";
            }
            GameDataMgr.Instance.AddRankInfo(inputNameValue, endTime);
            SceneManager.LoadScene("BeginScene");

        });
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        endTime = (int)GamePanel.Instance.nowTime;
        labTime.text = GamePanel.Instance.labTime.text;
    }
}
