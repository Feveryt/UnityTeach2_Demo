using UnityEngine;

/// <summary>
/// 挂在特效预制体上（或运行时添加），让特效在指定时间后自动回池
/// </summary>
public class EffectAutoRecycler : MonoBehaviour
{
    private string resPath;
    private float delay;

    public void Init(string resPath, float delay)
    {
        this.resPath = resPath;
        this.delay = delay;
        CancelInvoke();
        Invoke(nameof(DoRelease), delay);
    }

    private void DoRelease()
    {
        BulletPoolManager.Instance.ReleaseEffect(resPath, gameObject);
    }

    void OnDisable() => CancelInvoke();
}