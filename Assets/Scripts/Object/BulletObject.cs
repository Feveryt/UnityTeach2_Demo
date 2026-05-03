using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo info;//用来寄存子弹数据的
    private float time;
    
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        Invoke("DealyDestroy", info.lifeTime);
    }
    private void DealyDestroy()
    {
        Destroy(this.gameObject);
    }
    public void Dead()
    {
        GameObject effObj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        effObj.transform.position = this.transform.position;
        Destroy(effObj, 1f);
        Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObject player = other.gameObject.GetComponent<PlayerObject>();
            player.Wound();
            this.Dead();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        //1直线
        //2曲线
        //3右抛物线
        //4左抛物线
        //5跟踪导弹

        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                this.transform.Translate(Vector3.right * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed * Time.deltaTime);
                break;
            case 3:
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                            Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position),
                                                            Time.deltaTime * info.roundSpeed);
                break;
        }
        Destroy(this.gameObject, info.lifeTime);
    }
}
