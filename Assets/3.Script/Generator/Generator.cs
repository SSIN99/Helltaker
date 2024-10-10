using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private Animator anim;
    private bool isOn = true;
    [SerializeField] private KickEfxSpawner kickEfx;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject particle;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Break()
    {
        if (!isOn) return;
        isOn = false;
        kickEfx.OnBigEfx(transform.position);
        GameManager.Instance.generatorNum--;
        anim.SetTrigger("Break");
        particle.SetActive(true);
        cameraShake.VibrateForTime(0.3f);
        AudioManager.Instance.PlayEfx(Efx.Generator);
    }
}
