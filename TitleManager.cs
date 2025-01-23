using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    float slidePos = 0f;
    bool isLoading = true;

    void Start()
    {
        slider.value = slidePos;
    }

    void Update()
    {
        if (isLoading)
        {
            slidePos += 0.003f;
            slider.value = slidePos;

            if (slidePos >= 1f)
            {
                SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
                isLoading = false;
            }
        }
    }
}