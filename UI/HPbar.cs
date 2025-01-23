using System;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    Camera cam;

    Transform worldPos_hp;
    RectTransform rt_hp;
    GameObject obj;

    public Action<int> onHit;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); ;

        var bar = Resources.Load<GameObject>("Hp");
        obj = Instantiate<GameObject>(bar, GameObject.Find("GameUI").transform);
        rt_hp = obj.GetComponent<RectTransform>();

        worldPos_hp = this.transform;
    }

    void Update()
    {
        FollowUI();
    }

    public void SetHp(int hp)
    {
        Slider sl = obj.GetComponent<Slider>();
        sl.maxValue = hp;
        sl.value = hp;

        this.onHit = (damage) => { sl.value -= damage; };
    }

    void FollowUI()
    {
        rt_hp.position = RectTransformUtility.WorldToScreenPoint(cam, worldPos_hp.position);
    }

    public void Death()
    {
        Destroy(obj);
    }

    public void Show(bool show)
    {
        rt_hp.gameObject.SetActive(show);
    }
}
