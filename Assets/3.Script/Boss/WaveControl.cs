using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    [SerializeField] BossControl bossControl;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] LaserControl laserControl;
    [SerializeField] SpikeControl spikeControl;
    [SerializeField] BoomControl boomControl;
    [SerializeField] Animator PompLeft, PompRight;
    [SerializeField] Animator[] Lasers;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void WaveStart(int wave)
    {
        switch (wave)
        {
            case 1:
                anim.SetTrigger("wave1");
                break;
            case 2:
                anim.SetTrigger("wave2");
                break;
            case 3:
                anim.SetTrigger("wave3");
                break;
            case 4:
                GameManager.Instance.generatorNum--;
                anim.SetTrigger("destroy");
                break;
        }
    }
    public void Wave_1_Spike()
    {
        spikeControl.Wave_1();
    }
    public void Wave1_Laser()
    {
        laserControl.Wave_1();
    }
    public void Wave2_Laser()
    {
        laserControl.Wave_2();
    }
    public void Wave2_Boom()
    {
        boomControl.Wave_2();
    }
    public void Wave3_1_Laser()
    {
        laserControl.Wave_3_1();
    }
    public void Wave3_2_Laser()
    {
        laserControl.Wave_3_2();
    }
    public void Wave3_3_Laser()
    {
        laserControl.Wave_3_3();
    }
    public void Wave3_1_Boom()
    {
        boomControl.Wave_3_1();
    }
    public void Wave3_2_Boom()
    {
        boomControl.Wave_3_2();
    }
    public void OverLoad()
    {
        PompLeft.SetTrigger("overload");
        PompRight.SetTrigger("overload");
    }
    public void Explosion()
    {
        PompLeft.SetTrigger("off");
        PompRight.SetTrigger("off");
    }
    public void LaserOff()
    {
        for(int i = 0; i < Lasers.Length; i++)
        {
            Lasers[i].SetTrigger("off");
        }
    }
    public void Shake(float time)
    {
        AudioManager.Instance.PlayBossEfx(BossEfx.BossHand);
        cameraShake.VibrateForTime(time);
    }
    public void Stun()
    {
        bossControl.Groggy();
    }
    public void Destroy()
    {
        AudioManager.Instance.PlayBossEfx(BossEfx.BossEnd1);
        GameManager.Instance.generatorNum--;
        anim.SetTrigger("destroy");
    }
}
