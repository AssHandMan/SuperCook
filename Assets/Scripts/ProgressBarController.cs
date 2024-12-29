using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public float duration;
    public bool inProgress;
    public Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 0;

    }

    private void Update()
    {
        if (!inProgress && image.fillAmount > 0)
        {
            image.fillAmount = 0;
        }
    }

    public IEnumerator ChangeProgressBar()
    {
        inProgress = true;
        float timer = 0f;
        float startFillAmount = 0f;
        float endFillAmount = 1f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, timer / duration);
            image.fillAmount = fillAmount;
            yield return null;
        }

        image.fillAmount = 0f;
        inProgress = false;
    }
}