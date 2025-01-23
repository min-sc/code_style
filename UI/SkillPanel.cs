using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public enum SkillType
    {
        Bomb, Electric, Heal, Defend, Gold, AttackBuff
    }

    [SerializeField]
    GameObject[] boxes;
    [SerializeField]
    TextMeshProUGUI txt;
    [SerializeField]
    Image gauge;

    public Func<bool> OnGameState;
    public Action<SkillType> OnPickSkill;

    private int pickCount;
    private int lastCount;
    private bool processing;

    private float startValue = 0.0f;
    private float endValue = 1.0f;
    private float waitValue = 3.0f;

    private SkillType[] currentTypes;
    private SkillType pickType;
    private SkillFactory skillFactory;


    void Start()
    {
        pickCount = 0;
        processing = false;
        skillFactory = new SkillFactory();

        //temp
        currentTypes = new SkillType[]{ SkillType.Bomb,
                                        SkillType.AttackBuff,
                                        SkillType.Gold,
                                        SkillType.Defend,
                                        SkillType.Heal,
                                        SkillType.Electric,
                                        SkillType.Bomb,
                                        SkillType.AttackBuff,
                                        SkillType.Gold,
                                        SkillType.Defend,
                                        SkillType.Heal,
                                        SkillType.Electric, };

        OnPickSkill = (pick) => 
        {
            switch(pick)
            {
                case SkillType.Bomb:
                    skillFactory.CreateSkill(Skill.Bomb).Apply(Player.Instance);
                    break;
                case SkillType.AttackBuff:
                    skillFactory.CreateSkill(Skill.AttackBuff).Apply(Player.Instance);
                    break;
                case SkillType.Gold:
                    skillFactory.CreateSkill(Skill.Gold).Apply(Player.Instance);
                    break;
                case SkillType.Defend:
                    skillFactory.CreateSkill(Skill.Defend).Apply(Player.Instance);
                    break;
                case SkillType.Heal:
                    skillFactory.CreateSkill(Skill.Heal).Apply(Player.Instance);
                    break;
                case SkillType.Electric:
                    skillFactory.CreateSkill(Skill.Electric).Apply(Player.Instance);
                    break;
            }
        };
    }

    void Update()
    {
        if (processing) 
            return;

        if (pickCount <= 0)
            return;

        processing = true;

        StartCoroutine(PickStart());
    }

    public void AddRanCount(int add = 1)
    {
        pickCount += add;

        txt.text = string.Format("{0}", pickCount);
    }

    void Reset()
    {
        if(boxes is null) 
            return;

            foreach(var box in boxes)
                box.SetActive(false);
    }

    void ShowEffBox(int count)
    {
        boxes[count == 0 ? boxes.Length - 1 : count - 1].SetActive(false);
        boxes[count].SetActive(true);
    }

    void HideEffBox(int count)
    {
        boxes[count].SetActive(false);
    }

    IEnumerator PickStart()
    {
        Reset();

        lastCount %= boxes.Length;

        float delayTime = 0.03f;
        int ranCount = UnityEngine.Random.Range(36, 60);//Temp
        int curSeq = 0;
        for(int i = lastCount; i < lastCount + ranCount; i++)
        {
            curSeq = i % boxes.Length;

            ShowEffBox(curSeq);
            yield return new WaitForSeconds(GetDelay((float)i, (float)(lastCount + ranCount)));
            delayTime += 0.01f;
        }

        yield return new WaitForSeconds(1f);

        pickType = currentTypes[curSeq];

        for (int i = 0; i < 2; i++)
        {
            HideEffBox(curSeq);
            yield return new WaitForSeconds(0.3f);
            ShowEffBox(curSeq);
            yield return new WaitForSeconds(0.3f);
        }

        OnPickSkill(pickType);

        lastCount = curSeq % boxes.Length;

        AddRanCount(-1);

        if (pickCount > 0)
            StartCoroutine(PickEnd());
        else
            processing = false;
    }

    IEnumerator PickEnd()
    {
        float currentValue = startValue;
        while (currentValue <= endValue)
        {
            currentValue += Time.deltaTime / waitValue;
            gauge.fillAmount = currentValue;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        gauge.fillAmount = 0f;

        StartCoroutine(PickStart());
    }

    float GetDelay(float count, float all)
    {
        float delay = 0f;

        if (count < all * 0.6f)
            delay = 0.03f;
        else if (count < all * 0.8f)
            delay = 0.07f;
        else if (count < all * 0.9f)
            delay = 0.2f;
        else if (count < all * 0.95f)
            delay = 0.3f;
        else
            delay = 0.5f;

        return delay;
    }
}
