using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo info;
    private float time;
    private string resPath;      // 资源路径，回收时用来定位所属的池
    private string deadEffRes;   // 死亡特效路径

    /// <summary>初始化（替代原来的 InitInfo）</summary>
    public void InitInfo(BulletInfo info, string resPath)
    {
        this.info = info;
        this.resPath = resPath;
        this.deadEffRes = info.deadEffRes;
        time = 0;

        CancelInvoke();
        Invoke(nameof(DelayReturn), info.lifeTime);
    }

    private void DelayReturn() => ReturnToPool();

    /// <summary>子弹死亡：生成特效并回收自身</summary>
    public void Dead()
    {
        // 从特效池获取死亡特效
        GameObject eff = BulletPoolManager.Instance.GetEffect(deadEffRes, transform.position, Quaternion.identity);

        // 挂载自动回收组件（如果已有则直接 Init）
        if (!eff.TryGetComponent<EffectAutoRecycler>(out var recycler))
            recycler = eff.AddComponent<EffectAutoRecycler>();
        recycler.Init(deadEffRes, 1f);

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        CancelInvoke();
        BulletPoolManager.Instance.ReleaseBullet(resPath, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerObject>()?.Wound();
            Dead();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);

        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                transform.Translate(Vector3.right * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed * Time.deltaTime);
                break;
            case 3:
                transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                if (PlayerObject.Instance != null)
                {
                    Vector3 dir = PlayerObject.Instance.transform.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(dir), Time.deltaTime * info.roundSpeed);
                }
                break;
        }
    }

    void OnDisable()
    {
        CancelInvoke();
        time = 0;
    }
}