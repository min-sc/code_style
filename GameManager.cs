using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public FrontUI frontUI;
    public TopUI topUI;
    public SkillPanel skillPanel;

    [SerializeField]
    TMP_Text msgTxt;

    [SerializeField]
    Animator blackAnim;

    Action<Action> OnCompleteLoad = null;
    public int currentTurn = 0;

    private bool isGaming = false;
    public bool IsGaming 
    { 
        get { return isGaming; } 
        set 
        { 
            isGaming = value; 

            if(value)    
            {
                StartCoroutine(StartRoutine());
                InvokeRepeating("SetGamePercentMsg", 0f, 0.5f);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(EndRoutine());
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        blackAnim.SetTrigger("white");

        OnCompleteLoad = (onLoaded) =>
        {
            onLoaded?.Invoke();
            IsGaming = true;
        };

        Invoke("Load", 1f);
    }

    IEnumerator StartRoutine()
    {
        frontUI.ShowFrontMsg("Start");

        yield return new WaitForSeconds(2f);

        EnemyManager.Instance.TurnStart();
    }

    IEnumerator EndRoutine()
    {
        blackAnim.SetTrigger("black");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    void Load()
    {
        OnCompleteLoad.Invoke(null);
    }

    int GetGamePercent()
    {
        return (int)(Mathf.Clamp(Player.Instance.GetLocalPosition().x, 0, 50) * 3f); ;
    }

    void SetGamePercentMsg()
    {
        if (!IsGaming) 
            return;

        int per = GetGamePercent();
        msgTxt.text = string.Format("{0}%", per);

        if (per >= 100)
            IsGaming = false;
    }
}
