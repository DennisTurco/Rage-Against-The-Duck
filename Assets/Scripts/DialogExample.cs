using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    public DialogManager dialogManager;  // Riferimento al DialogManager
    public bool enableDialogOnHKey = true;  // Variabile pubblica per abilitare/disabilitare la funzione dal Inspector
    private bool dialogStarted = false;  // Per assicurarsi che il dialogo non inizi più volte

    private void Start()
    {
        // Iscriviti all'evento OnDialogEnd del DialogManager
        dialogManager.OnDialogEnd += OnDialogEnd;
    }

    private void Update()
    {
        if (enableDialogOnHKey && Input.GetKeyDown(KeyCode.H) && !dialogStarted)
        {
            dialogStarted = true;  // Impedisce l'avvio ripetuto del dialogo
            StartDialog();
        }
    }

    private void StartDialog()
    {
        string[] sentences = new string[] {
            "Benvenuto nel nostro gioco!",
            "LE PAPEREEEEEEEEE.",
            "Buona fortuna e divertiti!"
        };

        string speakerName = "Narratore";

        dialogManager.StartDialog(sentences, speakerName);
    }

    // Metodo chiamato quando il dialogo termina
    private void OnDialogEnd()
    {
        dialogStarted = false;  // Resetta il flag per permettere di riavviare il dialogo
    }
}
