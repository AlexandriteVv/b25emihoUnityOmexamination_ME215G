using UnityEngine;
using TMPro;

public class TalkPrompt : MonoBehaviour
{
    public float displayTime = 5f;

    void Start()
    {
        gameObject.SetActive(true);
        Invoke(nameof(HideText), displayTime);
    }

    void HideText()
    {
        gameObject.SetActive(false);
    }
}