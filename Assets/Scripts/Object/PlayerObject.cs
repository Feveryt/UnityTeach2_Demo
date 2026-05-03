using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    public int nowHp;
    public int maxHp;
    public int speed;
    private int roundSpeed = 20;
    private Quaternion targetQ;
    public bool isDead;
    public Vector3 nowPos;
    public Vector3 frontPos;
    private void Awake()
    {
        instance = this;
    }
    public void Dead()
    {
        isDead = true;
        GameOverPanel.Instance.ShowMe();
    }
    public void Wound()
    {
        if (isDead)
            return;
        this.nowHp -= 1;
        GamePanel.Instance.ChangeHP(this.nowHp);
        if (nowHp <= 0)
        {
            this.Dead();
        }
    }

    private float hValue;
    private float vValue;
    void Update()
    {
        if (isDead)
        {
            return;
        }
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");

        if (hValue == 0)
            targetQ = Quaternion.identity;
        else
        {
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
            Debug.Log("ÇãÐ±");
        }

        // targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, Time.deltaTime * roundSpeed);

        frontPos = this.transform.position;

        this.transform.Translate(Vector3.forward * vValue * speed * Time.deltaTime);
        this.transform.Translate(Vector3.right * hValue * speed * Time.deltaTime, Space.World);

        nowPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if (nowPos.x <= 0 || nowPos.x >= Screen.width)
        {
            this.transform.position = new Vector3(frontPos.x, this.transform.position.y, this.transform.position.z);
        }
        if (nowPos.y <= 0 || nowPos.y >= Screen.height)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, frontPos.z);
        }
    }
}
