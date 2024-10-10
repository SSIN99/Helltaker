using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bgm
{
    Abyss,
    Basic,
    Boss,
    Not
}
public enum Efx
{
    MenuMove,
    MenuSelect,
    Etc,
    StoneKick,
    StonMove,
    CharMove,
    Death,
    TransitionClose,
    TransitionOpen,
    Generator,
    Screen,
    DialogStart,
    DialogNext,
    DialogSuccess,
    ShortLaser
}
public enum BossEfx
{
    BossAwake,
    BossEnd1,
    BossEnd2,
    BossBeam,
    BossLongBeam,
    BossBoom,
    BossCannonShot,
    BossCannonShow,
    BossKick,
    BossHand
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Efxs;
    [SerializeField] AudioClip[] BossEfxs;
    [SerializeField] AudioClip[] Bgm;
    [SerializeField]AudioSource[] audioSource;
    [SerializeField]AudioSource[] bossAudioSource;
    [SerializeField] AudioSource BgmSource;
    int curSfx = 0;
    int curBoss = 0;
    private static AudioManager instance = null;
    public static AudioManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    public void PlayEfx(Efx type)
    {
        curSfx++;
        if (curSfx >= audioSource.Length)
            curSfx = 0;
        audioSource[curSfx].clip = Efxs[(int)type];
        audioSource[curSfx].Play();
    }
    public void PlayBossEfx(BossEfx type)
    {
        curBoss++;
        if (curBoss >= bossAudioSource.Length)
            curBoss = 0;
        bossAudioSource[curBoss].clip = BossEfxs[(int)type];
        bossAudioSource[curBoss].Play();
    }
    public void PlayBgm(Bgm bgm)
    {
        BgmSource.clip = Bgm[(int)bgm];
        if (!BgmSource.isPlaying)
            BgmSource.Play();
    }
    public void VolumeSet(int state, int type)
    {
        if(type == 2)
        {
            switch (state)
            {
                case 0:
                    BgmSource.volume = 0f;
                    break;
                case 1:
                    BgmSource.volume = 0.4f;
                    break;
                case 2:
                    BgmSource.volume = 0.7f;
                    break;
                case 3:
                    BgmSource.volume = 1f;
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    for(int i = 0; i < audioSource.Length; i++)
                    {
                        audioSource[i].volume = 0f;
                        bossAudioSource[i].volume = 0f;
                    }
                    break;
                case 1:
                    for (int i = 0; i < audioSource.Length; i++)
                    {
                        audioSource[i].volume = 0.4f;
                        bossAudioSource[i].volume = 0.4f;
                    }
                    break;
                case 2:
                    for (int i = 0; i < audioSource.Length; i++)
                    {
                        audioSource[i].volume = 0.7f;
                        bossAudioSource[i].volume = 0.7f;
                    }
                    break;
                case 3:
                    for (int i = 0; i < audioSource.Length; i++)
                    {
                        audioSource[i].volume = 1f;
                        bossAudioSource[i].volume = 1f;
                    }
                    break;
            }
        }
    }
}
