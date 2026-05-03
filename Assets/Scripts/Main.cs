using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelRoleInfo();

        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObject player = obj.AddComponent<PlayerObject>();
        obj.transform.localScale *= 40;
        obj.transform.rotation = Quaternion.identity;
        player.speed = info.speed * 20;
        player.maxHp = 10;
        player.nowHp = info.hp;

        GamePanel.Instance.ChangeHP(info.hp);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
