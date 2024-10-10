using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{ 
    int seletedMenu = 0;
    static int volumeStateBgm = 2;
    static int volumeStateSfx = 2;
    [SerializeField] Menu[] menu;
    [SerializeField] Generator[] generators;
    [SerializeField] bool isBoss;
    [SerializeField] WaveControl boss;

    private void Start()
    {
        seletedMenu = 0;
        menu[seletedMenu].Seleted();
        menu[2].VolumeSet(volumeStateBgm);
        menu[3].VolumeSet(volumeStateSfx);
        if (isBoss)
            boss = GameObject.FindObjectOfType<WaveControl>();
        else
            generators = GameObject.FindObjectsOfType<Generator>();
    }
    private void Update()
    {
        if (GameManager.Instance.isPause)
        {
            if (Input.GetKeyDown(KeyCode.W) && seletedMenu > 0)
            {
                menu[seletedMenu].NotSelected();
                seletedMenu--;
                if (GameManager.Instance.isWin && seletedMenu == 1)
                    seletedMenu--;
                menu[seletedMenu].Seleted();
            }
            if (Input.GetKeyDown(KeyCode.S) && seletedMenu < 4)
            {
                menu[seletedMenu].NotSelected();
                seletedMenu++;
                if (GameManager.Instance.isWin && seletedMenu == 1)
                    seletedMenu++;
                menu[seletedMenu].Seleted();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Operate();
            }
            if(Input.GetKeyDown(KeyCode.A) && (seletedMenu == 2 || seletedMenu == 3))
            {
                if(seletedMenu == 2)
                {
                    if (volumeStateBgm == 0)
                        volumeStateBgm = 3;
                    else
                        volumeStateBgm--;
                    menu[seletedMenu].VolumeSet(volumeStateBgm);
                    AudioManager.Instance.VolumeSet(volumeStateBgm, seletedMenu);
                }
                else
                {
                    if (volumeStateSfx == 0)
                        volumeStateSfx = 3;
                    else
                        volumeStateSfx--;
                    menu[seletedMenu].VolumeSet(volumeStateSfx);
                    AudioManager.Instance.VolumeSet(volumeStateSfx, seletedMenu);
                }
            }
            if (Input.GetKeyDown(KeyCode.D) && (seletedMenu == 2 || seletedMenu == 3))
            {
                if(seletedMenu == 2)
                {
                    if (volumeStateBgm == 3)
                        volumeStateBgm = 0;
                    else
                        volumeStateBgm++;
                    menu[seletedMenu].VolumeSet(volumeStateBgm);
                    AudioManager.Instance.VolumeSet(volumeStateBgm, seletedMenu);
                }
                else
                {
                    if (volumeStateSfx == 3)
                        volumeStateSfx = 0;
                    else
                        volumeStateSfx++;
                    menu[seletedMenu].VolumeSet(volumeStateSfx);
                    AudioManager.Instance.VolumeSet(volumeStateSfx, seletedMenu);
                }
           
            }
        }
    }
    public void Operate()
    {
        switch (menu[seletedMenu].type)
        {
            case Type.Start:
                GameManager.Instance.Continue();
                break;
            case Type.Skip:
                GameManager.Instance.Continue();
                if (isBoss)
                {
                    boss.Destroy();
                }
                else
                {
                    for (int i = 0; i < generators.Length; i++)
                    {
                        generators[i].Break();
                    }
                }
                break;
            case Type.Main:
                GameManager.Instance.Continue();
                GameManager.Instance.GoMain();
                break;
            default:
                return;
        }
    }
    public void Reset()
    {
        menu[seletedMenu].NotSelected();
        seletedMenu = 0;
        menu[seletedMenu].Seleted();
    }
}
