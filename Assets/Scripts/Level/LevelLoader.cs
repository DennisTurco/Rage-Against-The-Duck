using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas;

    [Header("Text Settings")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private float minLoadingTime = 2f;

    [Header("Icon Settings")]
    [SerializeField] private Image loadingIcon;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float frameDelay = 0.2f;

    private AsyncOperation loadOperation;
    private bool readyToContinue = false;

    private void Start()
    {
        loadingCanvas.gameObject.SetActive(false);
    }

    public void LoadLevel(string sceneName, string sceneTitle)
    {
        loadingCanvas.gameObject.SetActive(true);
        loadingText.text = "Loading";
        titleText.text = sceneTitle;
        readyToContinue = false;
        StartCoroutine(LoadAsynchronously(sceneName));
        StartCoroutine(Animate());
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        float timer = 0f;
        float dotTimer = 0f;

        loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while (loadOperation.progress < 0.9f || timer < minLoadingTime)
        {
            timer += Time.deltaTime;
            dotTimer += Time.deltaTime;

            if (dotTimer >= 0.5f)
            {
                dotTimer = 0f;
                if (loadingText.text.EndsWith("..."))
                    loadingText.text = "Loading";
                else
                    loadingText.text += ".";
            }

            yield return null;
        }

        LoadingComplete();
    }

    private void LoadingComplete()
    {
        loadingText.text = "Press any key to start";
        readyToContinue = true;
    }

    private void Update()
    {
        if (readyToContinue && Input.anyKeyDown)
        {
            loadingCanvas.gameObject.SetActive(false);
            loadOperation.allowSceneActivation = true;
        }
    }

    //TODO: We should avoid to duplicate this code with PlayerAnimation.cs - maybe create a common AnimationHandler class?
    private IEnumerator Animate()
    {
        if (sprites.Length == 0)
            yield break;

        int currentFrame = 0;

        while (!readyToContinue)
        {
            loadingIcon.sprite = sprites[currentFrame];
            currentFrame = (currentFrame + 1) % sprites.Length;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}
