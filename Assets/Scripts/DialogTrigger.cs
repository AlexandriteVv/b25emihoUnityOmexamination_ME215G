using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button nextButton;

    [Header("Dialog")]
    [TextArea(2, 5)]
    public string[] dialogLines;
    public float typingSpeed = 0.03f;

    [Header("Player")]
    public GameObject player;

    private int currentLineIndex;
    private bool isTyping;
    private bool hasTalked = false;   // 🔒 Dialog kan bara ske en gång

    // 🔒 Spelarens rörelsescript
    private PlatformerMovement platformerMovement;

    void Start()
    {
        // Dölj dialog från start
        dialogPanel.SetActive(false);

        // Koppla Next-knappen
        nextButton.onClick.AddListener(NextLine);

        // Hämta spelarens movement-script
        platformerMovement = player.GetComponent<PlatformerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTalked)
        {
            StartDialog();
        }
    }

    void StartDialog()
    {
        dialogPanel.SetActive(true);
        currentLineIndex = 0;

        DisablePlayerMovement();

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in dialogLines[currentLineIndex])
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (isTyping)
            return;

        currentLineIndex++;

        if (currentLineIndex < dialogLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        dialogPanel.SetActive(false);
        dialogText.text = "";

        EnablePlayerMovement();

        hasTalked = true; // 🔒 NPC kan inte prata igen

        // Stäng av trigger helt (extra säkerhet)
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    void DisablePlayerMovement()
    {
        if (platformerMovement != null)
            platformerMovement.enabled = false;
    }

    void EnablePlayerMovement()
    {
        if (platformerMovement != null)
            platformerMovement.enabled = true;
    }
}
