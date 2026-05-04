using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;  // ★ Unity 内置对象池

/// <summary>
/// 全局对象池管理器（单例）
/// 使用 Unity 内置 API（ObjectPool<T>）管理子弹和特效
/// </summary>
public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    // 每个预制体路径对应一个 ObjectPool
    private Dictionary<string, ObjectPool<GameObject>> bulletPools = new();
    private Dictionary<string, ObjectPool<GameObject>> effectPools = new();

    // Hierarchy 下的父节点，保持场景整洁
    private Transform bulletParent;
    private Transform effectParent;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bulletParent = new GameObject("BulletPool").transform;
        bulletParent.SetParent(transform);
        effectParent = new GameObject("EffectPool").transform;
        effectParent.SetParent(transform);
    }

    /// <summary>
    /// 获取一颗子弹（若该类型池不存在，自动创建）
    /// </summary>
    /// <param name="resPath">如 "Bullet/1"</param>
    public GameObject GetBullet(string resPath, Vector3 position, Quaternion rotation)
    {
        // 如果池子不存在，创建新的 ObjectPool
        if (!bulletPools.ContainsKey(resPath))
        {
            GameObject prefab = Resources.Load<GameObject>(resPath);
            if (prefab == null)
            {
                Debug.LogError($"子弹预制体不存在: {resPath}");
                return null;
            }

            // ★ 使用 Unity 内置 ObjectPool 初始化
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                // ① 创建新对象时调用（仅池需要扩容时执行）
                createFunc: () =>
                {
                    GameObject obj = Instantiate(prefab, bulletParent);
                    if (obj.GetComponent<BulletObject>() == null)
                        obj.AddComponent<BulletObject>();
                    return obj;
                },
                // ② 从池中取出时调用（激活）
                actionOnGet: obj => obj.SetActive(true),
                // ③ 放回池中时调用（休眠）
                actionOnRelease: obj => obj.SetActive(false),
                // ④ 池子满了、真正销毁对象时调用
                actionOnDestroy: obj => Destroy(obj),
                // 是否开启集合检查（Unity 会检测同一个对象是否被重复释放，调试时开启）
                collectionCheck: true,
                // 默认容量
                defaultCapacity: 30,
                // 最大容量（超出后释放的对象会被直接销毁）
                maxSize: 100
            );

            bulletPools[resPath] = pool;
        }

        // 从池中取出
        GameObject bullet = bulletPools[resPath].Get();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        return bullet;
    }

    /// <summary>
    /// 回收子弹
    /// </summary>
    public void ReleaseBullet(string resPath, GameObject bullet)
    {
        if (bulletPools.TryGetValue(resPath, out var pool))
            pool.Release(bullet);
        else
            Destroy(bullet); // 容错
    }

    /// <summary>
    /// 获取一个特效
    /// </summary>
    public GameObject GetEffect(string resPath, Vector3 position, Quaternion rotation)
    {
        if (!effectPools.ContainsKey(resPath))
        {
            GameObject prefab = Resources.Load<GameObject>(resPath);
            if (prefab == null) return null;

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    GameObject obj = Instantiate(prefab, effectParent);
                    return obj;
                },
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: obj => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: 15,
                maxSize: 50
            );

            effectPools[resPath] = pool;
        }

        GameObject effect = effectPools[resPath].Get();
        effect.transform.position = position;
        effect.transform.rotation = rotation;
        return effect;
    }

    /// <summary>
    /// 回收特效
    /// </summary>
    public void ReleaseEffect(string resPath, GameObject effect)
    {
        if (effectPools.TryGetValue(resPath, out var pool))
            pool.Release(effect);
        else
            Destroy(effect);
    }
}