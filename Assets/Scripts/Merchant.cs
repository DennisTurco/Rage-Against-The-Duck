using UnityEngine;

public class Merchant : MonoBehaviour
{
    public static bool IsTradeMenuOpen = false;  // Variabile statica per controllare se il trade menu è aperto

    public DialogManager dialogManager;  // Riferimento al DialogManager
    public GameObject tradeMenu;  // Riferimento al trade menu
    private bool playerInRange = false;  // Flag per controllare se il player è nel collider
    private bool dialogStarted = false;  // Flag per controllare se il dialogo è iniziato
    private bool tradeMenuOpen = false;  // Flag per controllare se il trade menu è aperto

    private void Start()
    {
        // Cerca il DialogManager nella scena se non è assegnato manualmente
        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }

        // Assicurati di iscriverti all'evento di fine dialogo
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd += OnDialogEnd;
        }
        else
        {
            Debug.LogError("Riferimento al DialogManager non assegnato in Merchant.");
        }
    }

    private void OnDestroy()
    {
        // Ricorda di annullare l'iscrizione all'evento quando l'oggetto viene distrutto per evitare memory leaks
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= OnDialogEnd;
        }
    }

    private void Update()
    {
        // Se il player è nel raggio d'azione e preme il tasto E, avvia il dialogo
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogStarted && !tradeMenuOpen)
        {
            StartDialog();
        }

        // Controlla se il trade menu è aperto e se viene premuto il tasto Esc, chiudilo
        if (tradeMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTradeMenu();
            Time.timeScale = 1f;
        }
    }

    private void StartDialog()
    {
        // Esegui il dialogo utilizzando il DialogManager
        string[] sentences = new string[] {
            "Benvenuto amico mio!",
            "Come stai? Io sono Marciello.",
            "Guarda cosa vendo o ti accoltello!"
        };
        string speakerName = "MARCIELLO";

        if (dialogManager != null)
        {
            dialogManager.StartDialog(sentences, speakerName);
            dialogStarted = true;
        }
        else
        {
            Debug.LogError("Riferimento al DialogManager non assegnato in Merchant.");
        }
    }

    // Metodo chiamato quando il dialogo finisce
    private void OnDialogEnd()
    {
        dialogStarted = false;

        if (playerInRange)  // Controlla se il giocatore è ancora nell'area del merchant
        {
            Time.timeScale = 0f;
            OpenTradeMenu();
        }
    }

    // Metodo per aprire il trade menu
    public void OpenTradeMenu()
    {
        if (tradeMenu != null)
        {
            tradeMenu.SetActive(true);
            tradeMenuOpen = true;
            IsTradeMenuOpen = true;  // Imposta la variabile statica a true
        }
    }

    // Metodo per chiudere il trade menu
    public void CloseTradeMenu()
    {
        if (tradeMenu != null)
        {
            tradeMenu.SetActive(false);
            tradeMenuOpen = false;
            IsTradeMenuOpen = false;  // Imposta la variabile statica a false
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
