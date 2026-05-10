using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Pos_Type
{
    TopLeft,
    Top,
    TopRight,

    Left,
    Right,

    BottomLeft,
    Bottom,
    BottomRight
}
public class FireObject : MonoBehaviour
{

    public E_Pos_Type posType;

    private FireInfo fireInfo;
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    private BulletInfo nowBulletInfo;

    private float changeAngle;

    private Vector3 screenPos;
    private Vector3 initDir;
    private Vector3 nowDir;

    void Update()
    {
        // print(Camera.main.WorldToScreenPoint(PlayerObject.Instance.transform.position));
        UpdataPos();
        ResetFireInfo();
        UpdataFire();
    }
    private void UpdataPos()

    {
        screenPos.z = 240;
        switch (posType)
        {
            case E_Pos_Type.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;

                initDir = Vector3.left;
                break;
            case E_Pos_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.left;
                break;
            case E_Pos_Type.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_Pos_Type.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;

                initDir = Vector3.left;
                break;
        }
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    private void ResetFireInfo()
    {
        if (nowCD != 0 && nowNum != 0)
        {
            return;
        }
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0)
            {
                return;
            }
        }

        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];
        nowNum = fireInfo.num;
        nowCD = fireInfo.cd;
        nowDelay = fireInfo.delay;

        string[] strs = fireInfo.ids.Split(",");
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletID - 1];

        if (fireInfo.type == 2)
        {
            switch (posType)
            {
                case E_Pos_Type.TopLeft:
                case E_Pos_Type.TopRight:
                case E_Pos_Type.BottomLeft:
                case E_Pos_Type.BottomRight:
                    changeAngle = 90f / (nowNum + 1);
                    break;
                case E_Pos_Type.Top:
                case E_Pos_Type.Bottom:
                case E_Pos_Type.Left:
                case E_Pos_Type.Right:
                    changeAngle = 180f / (nowNum + 1);
                    break;

            }
        }
    }
    private void UpdataFire()
    {
        if (nowCD == 0 && nowNum == 0)
        {
            return;
        }
        nowCD -= Time.deltaTime;
        if (nowCD > 0)
        {
            return;
        }
        GameObject bullet;
        // BulletObject bulletObj;
        switch (fireInfo.type)
        {
            case 1:
                
                bullet = BulletPoolManager.Instance.GetBullet(nowBulletInfo.resName, this.transform.position, Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position));
                bullet.GetComponent<BulletObject>().InitInfo(nowBulletInfo, nowBulletInfo.resName);
                
                --nowNum;
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case 2:
                if (nowCD == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                        bullet = BulletPoolManager.Instance.GetBullet(nowBulletInfo.resName, this.transform.position, Quaternion.LookRotation(nowDir));
                        bullet.GetComponent<BulletObject>().InitInfo(nowBulletInfo, nowBulletInfo.resName);
                        

                    }
                    nowCD = nowNum = 0;
                }
                else
                {
                    
                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num - nowNum), Vector3.up) * initDir;
                    //对象池获取子弹
                    bullet = BulletPoolManager.Instance.GetBullet(nowBulletInfo.resName, this.transform.position, Quaternion.LookRotation(nowDir));
                    bullet.GetComponent<BulletObject>().InitInfo(nowBulletInfo, nowBulletInfo.resName);
                    
                    --nowNum;
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
    }
}
