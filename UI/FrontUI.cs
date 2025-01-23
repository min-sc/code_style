using System;
using UnityEngine;


public class FrontUI : Pooling
{
    Animator anim;
    TMPro.TMP_Text msgTex;

    [SerializeField]
    Transform goldTr;

    [SerializeField]
    GameObject skillNumberObj;
    GameObject hasSkillObj;

    [SerializeField]
    Transform skillNumberTr;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        msgTex = transform.Find("Msg").GetComponent<TMPro.TMP_Text>();
    }

    public void ShowFrontMsg(string msg)
    {
        msgTex.text = msg;
        anim.SetTrigger("frontmsg");
    }

    public void ShowAddEvent(int coin, Vector2 pos)
    {
        //TODO make coin count rule
        int coinCount = UnityEngine.Random.Range(3, 10);

        for(int i = 0; i < coinCount; i++)
        {
            GameObject obj = GetObject();
            obj.transform.position = RectTransformUtility.WorldToScreenPoint(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), pos);;

            int ind = i;

            obj.GetComponent<CoinMover>().Move(goldTr.position, () =>
            {
                ReturnObject(obj);
                
                if(ind == 0)
                    GameManager.Instance.topUI.AddGold(coin);
            });
        }

        ShowAddSkill(pos);
    }

    public void ShowAddSkill(Vector2 pos, Action onAddSkill = null)
    {
        if(UnityEngine.Random.Range(0, 2) != 0)
            return;

        if (hasSkillObj == null)
            hasSkillObj = Instantiate<GameObject>(skillNumberObj, objectParent, false);

        hasSkillObj.SetActive(true);
        hasSkillObj.transform.position = RectTransformUtility.WorldToScreenPoint(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), pos); ;

        hasSkillObj.GetComponent<CoinMover>().Move(skillNumberTr.position, () => { hasSkillObj.SetActive(false); GameManager.Instance.skillPanel.AddRanCount(); });
    }
}
