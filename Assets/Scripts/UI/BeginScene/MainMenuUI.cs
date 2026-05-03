using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : BasePanel<MainMenuUI>
{
    // 单例（方便外部调用，也可以不用）


    [Header("按钮")]
    public Button btnStart;
    public Button btnRank;
    public Button btnSetting;
    public Button btnExit;

    [Header("面板")]
    public GameObject rankPanel;    // 排行榜面板
    public GameObject settingPanel; // 设置面板
    public GameObject choosePanel;

    public override void Init()
    {
        // 一开始隐藏所有面板
        // rankPanel.SetActive(false);
        settingPanel.SetActive(false);
        choosePanel.SetActive(false);

        // 绑定按钮事件
        btnStart.onClick.AddListener(OnStartGame);
        btnRank.onClick.AddListener(OnOpenRank);
        btnSetting.onClick.AddListener(OnOpenSetting);
        btnExit.onClick.AddListener(OnExitGame);
    }
    // 开始游戏 → 切换场景
    public void OnStartGame()
    {
        // Debug.Log("开始游戏，切换场景");
        // SceneManager.LoadScene("GameScene");
        ChoosePanel.Instance.ShowMe();
        HideMe();

    }

    // 打开排行榜
    public void OnOpenRank()
    {
        Debug.Log("打开排行榜");
        // rankPanel.SetActive(true);
        // settingPanel.SetActive(false); // 避免同时打开
        RankPanel.Instance.ShowMe();
    }

    // 打开设置
    public void OnOpenSetting()
    {
        Debug.Log("打开设置");
        // settingPanel.SetActive(true);
        // rankPanel.SetActive(false);
        SettingPanel.Instance.ShowMe();
    }

    // 关闭所有面板（给关闭按钮用）
    public void CloseAllPanels()
    {

        // rankPanel.SetActive(false);
        // settingPanel.SetActive(false);
    }

    // 退出游戏
    public void OnExitGame()
    {

        // Application.Quit();
        Debug.Log("游戏已退出"); // 编辑器里不会真退出，但会输出这句话
    }
}