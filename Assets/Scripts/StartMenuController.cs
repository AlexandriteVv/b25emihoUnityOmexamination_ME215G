using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartMenuController : MonoBehaviour
{
    public Button startButton;
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    public string mainSceneName = "MainScene";

    private void Start()
    {
        // Koppla knapp och slider
        startButton.onClick.AddListener(StartGame);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Hämta tidigare sparad volym
        if (PlayerPrefs.HasKey("MasterVolume"))
            volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        else
            volumeSlider.value = 0.75f;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    private void SetVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}