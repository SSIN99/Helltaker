using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    public bool isWin = false;
    public bool isPause = false;
    public int generatorNum;
    private string nextScene;
    [SerializeField] GameObject transitionOpen;
    [SerializeField] GameObject transitionClose;
    [SerializeField] StageData stage;
    [SerializeField] GameObject restart, dialogUI;
    [SerializeField] GameObject lifeUI, pauseUI;
    [SerializeField] DialogManager dialogManager;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        generatorNum = stage.GeneratorNum;
        nextScene = stage.NextScene;
        isWin = false;
        AudioManager.Instance.PlayEfx(Efx.TransitionOpen);
        transitionOpen.SetActive(true);
        StartCoroutine(Load_Co());
    }
    public IEnumerator Load_Co()
    {
        yield return new WaitForSeconds(0.55f);
        transitionOpen.SetActive(false);
        restart.SetActive(true);
    }
    void Update()
    {
        if (!isPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = true;
                pauseUI.SetActive(true);
                Time.timeScale = 0;
                return;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
            if (generatorNum <= 0 && !isWin)
            {
                isWin = true;
                dialogManager.OpenDialog();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Continue();
            }
        }
    }
    public void Continue()
    {
        isPause = false;
        Time.timeScale = 1;
        pauseUI.GetComponent<PauseManager>().Reset();
        pauseUI.SetActive(false);
    }
    public void Restart()
    {
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        transitionClose.SetActive(true);
        restart.SetActive(false);
        dialogUI.SetActive(false);
        if(lifeUI)
            lifeUI.SetActive(false);
        StartCoroutine(Restart_Co());
    }
    public IEnumerator Restart_Co()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        transitionClose.SetActive(true);
        StartCoroutine(NextScene_Co());
    }
    public IEnumerator NextScene_Co()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextScene);
    }
    public void GoMain()
    {
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        transitionClose.SetActive(true);
        StartCoroutine(GoMain_Co());
    }
    public IEnumerator GoMain_Co()
    {
        if (restart)
            restart.SetActive(false);
        if (dialogUI)
            dialogUI.SetActive(false);
        if (lifeUI)
            lifeUI.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
