using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] Canvas canvas, Dialog;
    [SerializeField] GameObject LabBG;
    [SerializeField] GameObject mask;
    [SerializeField] SpriteRenderer mainScreen;

    [SerializeField] DialogData dialogData;
    [SerializeField] SpriteRenderer charImage;
    [SerializeField] SpriteRenderer mainCharImage;
    [SerializeField] SpriteRenderer subCharImage;
    [SerializeField] SpriteRenderer cutseenImage;
    [SerializeField] Text charName;
    [SerializeField] Text charScript;
    [SerializeField] GameObject booper;
    [SerializeField] GameObject success;
    [SerializeField] bool isLast;
    [SerializeField] private int dialogIndex = 0;
    int cutseenIndex = 0;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || 
            Input.GetKeyDown(KeyCode.Space)) && 
            GameManager.Instance.isWin &&
            !GameManager.Instance.isPause)
        {
            dialogIndex++;
            if (dialogIndex >= dialogData.DialogCount)
            {
                if (isLast)
                {
                    CloseDialog();
                }
                else
                {
                    Dialog.gameObject.SetActive(false);
                    GameManager.Instance.NextScene();
                }
            }
            else
            {
                if (isLast && dialogIndex == 2)
                {
                    AudioManager.Instance.PlayEfx(Efx.DialogSuccess);
                    success.SetActive(true);
                }
                if(isLast && dialogIndex != 2)
                    success.SetActive(false);

                if (dialogData.IsDouble[dialogIndex])
                    NextDialog_Double();
                else
                    NextDialog();
            }
        }
    }
    public void NextDialog()
    {
        booper.GetComponent<Animator>().SetTrigger("pop");

        mainCharImage.gameObject.SetActive(false);
        subCharImage.gameObject.SetActive(false);

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
    public void NextDialog_Double()
    {
        booper.GetComponent<Animator>().SetTrigger("pop");
        AudioManager.Instance.PlayEfx(Efx.DialogNext);
        charImage.gameObject.SetActive(false);

        if (dialogData.CharList[dialogIndex])
        {
            mainCharImage.gameObject.SetActive(false);
            mainCharImage.sprite = dialogData.CharList[dialogIndex];
        }
        if (dialogData.SubCharList[dialogIndex])
        {
            subCharImage.gameObject.SetActive(false);
            subCharImage.sprite = dialogData.SubCharList[dialogIndex];
        }
            
        charScript.gameObject.SetActive(false);
        charScript.text = dialogData.ScriptList[dialogIndex];
        charName.text = dialogData.NameList[dialogIndex];

        mainCharImage.gameObject.SetActive(true);
        subCharImage.gameObject.SetActive(true);
        charScript.gameObject.SetActive(true);
        AudioManager.Instance.PlayEfx(Efx.DialogNext);
    }
    public void OpenDialog()
    {
        canvas.gameObject.SetActive(false);
        dialogIndex = 0;
        charImage.sprite = dialogData.CharList[dialogIndex];
        charName.text = dialogData.NameList[dialogIndex];
        charScript.text = dialogData.ScriptList[dialogIndex];
        StartCoroutine(OpenDialog_Co());
    }
    public IEnumerator OpenDialog_Co()
    {
        if (isLast)
        {
            yield return new WaitForSeconds(5.5f);
        }
        else
        {
            yield return new WaitForSeconds(1.1f);
            AudioManager.Instance.PlayEfx(Efx.Screen);
            screen.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            mask.SetActive(false);
            mainScreen.maskInteraction = SpriteMaskInteraction.None;
        }
        AudioManager.Instance.PlayEfx(Efx.DialogStart);
        LabBG.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        charImage.gameObject.SetActive(true);
        Dialog.gameObject.SetActive(true);
        booper.SetActive(true);
    }
    public void CloseDialog()
    {
        if(cutseenIndex >= dialogData.Cutseen.Length)
        {
            Dialog.gameObject.SetActive(false);
            GameManager.Instance.NextScene();
        }
        else
        {
            booper.GetComponent<Animator>().SetTrigger("pop");
            charImage.gameObject.SetActive(false);
            mainCharImage.gameObject.SetActive(false);
            subCharImage.gameObject.SetActive(false);
            LabBG.GetComponent<SpriteRenderer>().sprite = null;
            if (dialogData.Cutseen[cutseenIndex])
            {
                cutseenImage.gameObject.SetActive(false);
                cutseenImage.sprite = dialogData.Cutseen[cutseenIndex];
                cutseenImage.gameObject.SetActive(true);
            }
            cutseenIndex++;
            charScript.gameObject.SetActive(false);
            charScript.text = dialogData.ScriptList[dialogIndex];
            charName.text = null;

            charScript.gameObject.SetActive(true);
        }
        AudioManager.Instance.PlayEfx(Efx.DialogNext);
    }
}
