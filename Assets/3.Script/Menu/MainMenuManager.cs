using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject TransitionOpen, TransitionClose;
    [SerializeField] GameObject Canvas;
    int seletedMenu;
    [SerializeField] MainMenu[] menu;
    [SerializeField] static bool isFirst = true;

    private void Start()
    {
        if (isFirst)
        {
            isFirst = false;
            Canvas.SetActive(true);
        }
        else
        {
            StartCoroutine(ReturnMain_Co());
        }
        seletedMenu = 0;
        menu[seletedMenu].Seleted();
        AudioManager.Instance.PlayBgm(Bgm.Abyss);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && seletedMenu == 1)
        {
            menu[seletedMenu].NotSelected();
            seletedMenu--;
            menu[seletedMenu].Seleted();
            AudioManager.Instance.PlayEfx(Efx.MenuMove);
        }
        if (Input.GetKeyDown(KeyCode.S) && seletedMenu == 0)
        {
            menu[seletedMenu].NotSelected();
            seletedMenu++;
            menu[seletedMenu].Seleted();
            AudioManager.Instance.PlayEfx(Efx.MenuMove);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlayEfx(Efx.MenuSelect);
            OperateMenu();
        }
    }
    public void OperateMenu()
    {
        switch (menu[seletedMenu].type)
        {
            case MenuType.Start:
                StartCoroutine(Start_Co());
                break;
            case MenuType.Exit:
                Application.Quit();
                break;
            
        }
    }
    public IEnumerator Start_Co()
    {
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        TransitionClose.SetActive(true);
        Canvas.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Prologue");
    }
    public IEnumerator ReturnMain_Co()
    {
        TransitionOpen.SetActive(true);
        AudioManager.Instance.PlayEfx(Efx.TransitionOpen);
        yield return new WaitForSeconds(0.6f);
        TransitionOpen.SetActive(false);
        Canvas.SetActive(true);
        AudioManager.Instance.PlayBgm(Bgm.Abyss);
    }
    
}
