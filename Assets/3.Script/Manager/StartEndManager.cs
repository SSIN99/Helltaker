using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartEndManager : MonoBehaviour
{

    [SerializeField] GameObject transitionClose, transitionOpen;
    [SerializeField] bool isStart;
    [SerializeField] int dialogIndex, cutseenIndex;
    [SerializeField] DialogData dialogData;
    [SerializeField] SpriteRenderer charImage;
    [SerializeField] SpriteRenderer cutseenImage;
    [SerializeField] Text charName;
    [SerializeField] Text charScript;
    [SerializeField] GameObject booper;
    [SerializeField] GameObject labBG, dialogUI;
    void Start()
    {
        AudioManager.Instance.PlayBgm(Bgm.Not);
        AudioManager.Instance.PlayEfx(Efx.TransitionOpen);
        transitionOpen.SetActive(true);
        StartCoroutine(Load_Co());
        OpenDialog();
    }
    public IEnumerator Load_Co()
    {
        yield return new WaitForSeconds(0.55f);
        transitionOpen.SetActive(false);
        dialogUI.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Space))  
        {
            dialogIndex++;
            if (dialogIndex >= dialogData.DialogCount)
            {
                OpenCutseen();
            }
            else
            {
                NextDialog();
            }
        }
    }
    public void OpenCutseen()
    {
        if (cutseenIndex >= dialogData.Cutseen.Length)
        {
            NextScene();
        }
        else
        {
            booper.GetComponent<Animator>().SetTrigger("pop");
            charImage.gameObject.SetActive(false);
            charName.text = null;
            labBG.GetComponent<SpriteRenderer>().sprite = null;
            if (dialogData.Cutseen[cutseenIndex])
            {
                cutseenImage.gameObject.SetActive(false);
                cutseenImage.sprite = dialogData.Cutseen[cutseenIndex];
                cutseenImage.gameObject.SetActive(true);
            }
            cutseenIndex++;
            charScript.gameObject.SetActive(false);
            charScript.text = dialogData.ScriptList[dialogIndex];
            charScript.gameObject.SetActive(true);
            AudioManager.Instance.PlayEfx(Efx.DialogNext);
        }
    }
    public void NextDialog()
    {
        booper.GetComponent<Animator>().SetTrigger("pop");

        if (dialogData.CharList[dialogIndex])
        {
            charImage.gameObject.SetActive(false);
            charImage.sprite = dialogData.CharList[dialogIndex];
        }

        charScript.gameObject.SetActive(false);
        charScript.text = dialogData.ScriptList[dialogIndex];
        charName.text = dialogData.NameList[dialogIndex];

        charImage.gameObject.SetActive(true);
        charScript.gameObject.SetActive(true);
        AudioManager.Instance.PlayEfx(Efx.DialogNext);
    }
    public void OpenDialog()
    {
        dialogIndex = 0;
        cutseenIndex = 0;
        labBG.SetActive(true);
        if (isStart)
        {
            labBG.GetComponent<SpriteRenderer>().sprite = null;
            charName.gameObject.SetActive(false);
            charScript.text = dialogData.ScriptList[dialogIndex];
        }
        else
        {
            charImage.sprite = dialogData.CharList[dialogIndex];
            charName.text = dialogData.NameList[dialogIndex];
            charScript.text = dialogData.ScriptList[dialogIndex];
            charImage.gameObject.SetActive(true);
        }
        booper.SetActive(true);
        AudioManager.Instance.PlayEfx(Efx.DialogStart);
    }
    public void NextScene()
    {
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        transitionClose.SetActive(true);
        dialogUI.SetActive(false);
        StartCoroutine(NextScene_Co());
    }
    public IEnumerator NextScene_Co()
    {
        yield return new WaitForSeconds(1.0f);
        if (isStart)
        {
            AudioManager.Instance.PlayBgm(Bgm.Basic);
            SceneManager.LoadScene("Chapter_1");
        }
        else
        {
            AudioManager.Instance.PlayBgm(Bgm.Abyss);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
