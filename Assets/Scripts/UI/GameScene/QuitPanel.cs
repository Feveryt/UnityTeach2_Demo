using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitPanel : BasePanel<QuitPanel>
{
    public Button btnSure;
    public Button btnCancel;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BeginScene");
        });
        btnCancel.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
    }
}
