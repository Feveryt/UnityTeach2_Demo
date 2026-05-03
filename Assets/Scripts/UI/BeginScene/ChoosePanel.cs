using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public Button btnClose;
    public Button btnStart;
    public Button btnLeft;
    public Button btnRight;
    public Transform heroPos;

    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;
    private GameObject airPlaneObj;

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
        btnLeft.onClick.AddListener(() =>
        {
            --GameDataMgr.Instance.nowSelHeroIndex;
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
            ChangeNowHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            ++GameDataMgr.Instance.nowSelHeroIndex;
            if (GameDataMgr.Instance.nowSelHeroIndex >= GameDataMgr.Instance.roleData.roleList.Count)
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            ChangeNowHero();
        });
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
            MainMenuUI.Instance.ShowMe();
        });
        // HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();

        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero();

    }
    public override void HideMe()
    {
        base.HideMe();
        DestroyObj();
    }
    private void ChangeNowHero()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelRoleInfo();
        DestroyObj();
        airPlaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        airPlaneObj.transform.SetParent(heroPos, false);


        airPlaneObj.transform.localScale = Vector3.one * info.scale;

        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumeObjs[i].SetActive(i < info.volume);
        }
    }

    private void DestroyObj()
    {
        if (airPlaneObj != null)
        {
            Destroy(airPlaneObj);
            airPlaneObj = null;
        }
    }
    private float time;
    private bool isSel;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 2;
        heroPos.Translate(Vector3.up * Mathf.Sin(time) * 0.0004f, Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                                1000,
                                1 << LayerMask.NameToLayer("UI")))

            {
                isSel = true;
                Debug.Log("点击了飞机");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }
        if (Input.GetMouseButton(0) && isSel)
        {
            Debug.Log("拖动了飞机");
            // heroPos.position += new Vector3(0, 0, -2);
            heroPos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 20, Vector3.up);
        }
    }
}
