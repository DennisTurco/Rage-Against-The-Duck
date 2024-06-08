using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;  // Riferimento al componente Text della dialog box
    public TextMeshProUGUI nameText;  // Riferimento al componente Text per il nome del personaggio
    public GameObject dialogBox;  // Riferimento al GameObject della dialog box
    public float typingSpeed = 0.02f;  // Velocità di digitazione del testo

    private Queue<string> sentences;  // Coda di frasi da mostrare
    private string currentSpeaker;  // Nome dell'attuale parlante

    // Evento per notificare quando il dialogo è terminato
    public event System.Action OnDialogEnd;

    private bool isDialogActive = false; // Flag per verificare se il dialogo è attivo

    private void Start()
    {
        sentences = new Queue<string>();
        dialogBox.SetActive(false);  // Assicurati che la dialog box sia nascosta all'inizio
    }

    private void Update()
    {
        // Controlla se il mouse è stato cliccato o se c'è un tocco sullo schermo
        if (isDialogActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)))
        {
            DisplayNextSentence();
        }
    }

    // Metodo per iniziare il dialogo
    public void StartDialog(string[] dialogSentences, string speaker)
    {
        dialogBox.SetActive(true);  // Mostra la dialog box
        sentences.Clear();

        currentSpeaker = speaker;
        nameText.text = currentSpeaker;  // Imposta il nome del parlante

        foreach (string sentence in dialogSentences)
        {
            sentences.Enqueue(sentence);
        }

        isDialogActive = true; // Imposta la flag come attiva
        DisplayNextSentence();
    }

    // Metodo per mostrare la frase successiva
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Coroutine per mostrare il testo con effetto di digitazione
    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Metodo per terminare il dialogo
    private void EndDialog()
    {
        dialogBox.SetActive(false);  // Nascondi la dialog box
        isDialogActive = false; // Imposta la flag come inattiva
        OnDialogEnd?.Invoke();  // Notifica che il dialogo è terminato
    }
}