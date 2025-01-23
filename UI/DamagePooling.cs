using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePooling : Pooling
{
    private static DamagePooling _instance;
    public static DamagePooling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<DamagePooling>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(DamagePooling).Name);
                    _instance = singleton.AddComponent<DamagePooling>();
                }
            }
            return _instance;
        }
    }

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        base.Awake();
    }

    public void SetDamage(int damage, Vector2 pos)
    {
        var obj = GetObject();
        Debug.Log(obj.name);
        obj.transform.Find("Number").GetComponent<TextMeshProUGUI>().text = damage.ToString();
        obj.transform.position = RectTransformUtility.WorldToScreenPoint(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), pos);

        StartCoroutine(ReturnAfterTime(1f, obj));
    }
}
