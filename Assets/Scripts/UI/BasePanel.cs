using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance => instance;
    protected virtual void Awake()
    {
        instance = this as T;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();

    }
    public abstract void Init();
    // Update is called once per frame
    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
