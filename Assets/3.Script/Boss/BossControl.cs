using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] Animator PompLeft,PompRight;
    [SerializeField] Animator[] BossLaser;
    [SerializeField] WaveControl waveControl;
    [SerializeField] GameObject lifeUI;
    [SerializeField] GameObject[] lifes;
    [SerializeField] CameraShake cameraShake;
    Transform hitBox;
    [SerializeField]bool isTurn = false;
    [SerializeField] static int life = 3;
    [SerializeField] static int wave = 1;

    private void Awake()
    {
        hitBox = GetComponent<Transform>();
        AudioManager.Instance.PlayBgm(Bgm.Not);
    }
    public void OnHit()
    {
        AudioManager.Instance.PlayBossEfx(BossEfx.BossKick);
        cameraShake.VibrateForTime(0.3f);
        if (!isTurn)
        {
            AudioManager.Instance.PlayBgm(Bgm.Boss);
            TurnOn();
        }
        else
            Break();
        waveControl.WaveStart(wave);
    }
    public void TurnOn()
    {
        AudioManager.Instance.PlayBossEfx(BossEfx.BossAwake);
        hitBox.tag = "Laser";
        isTurn = true;
        lifeUI.SetActive(true);
        for(int i = 0; i < life; i++)
        {
            lifes[i].SetActive(true);
        }
        PompLeft.SetTrigger("on");
        PompRight.SetTrigger("on");
        StartCoroutine(TurnOn_Co());
    }
    public IEnumerator TurnOn_Co()
    {
        for(int i = 0; i < BossLaser.Length; i += 2)
        {
            BossLaser[i].SetTrigger("on");
            BossLaser[i+1].SetTrigger("on");
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Break()
    {
        hitBox.tag = "Laser";
        life--;
        wave++;
        lifes[life].SetActive(false);
    }
    public void Groggy()
    {
        hitBox.tag = "Boss";
    }
}
