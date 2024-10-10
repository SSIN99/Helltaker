using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortLaser : MonoBehaviour
{
    private bool isOn = true;
    private Animator anim;
    [SerializeField] private GameObject ShortCircle;
    [SerializeField] private GameObject ShortCircleB;
    [SerializeField] private GameObject ShortBeam;
    [SerializeField] private GameObject ShortBeamB;
    [SerializeField] private GameObject PreBeam;
    [SerializeField] private WaitForSeconds hitTime = new WaitForSeconds(0.25f);
    [SerializeField] private WaitForSeconds preTime = new WaitForSeconds(0.45f);
    [SerializeField] private WaitForSeconds safeTime = new WaitForSeconds(0.3f);
    [SerializeField] private bool[] isSafe;
    void Start()
    {
        isOn = true;
        anim = GetComponent<Animator>();
        StartCoroutine(TurnOn_Co());
    }

    void Update()
    {
        if (GameManager.Instance.isWin && isOn)
            TurnOff();
    }

    public void TurnOff()
    {
        isOn = false;
        ShortBeam.SetActive(false);
        ShortBeamB.SetActive(false);
        ShortCircleB.SetActive(false);
        ShortCircle.GetComponent<Animator>().SetTrigger("off");
        anim.SetTrigger("off");
    }
    public IEnumerator TurnOn_Co()
    {
        yield return new WaitForSeconds(0.8f);
        ShortCircle.SetActive(true);
        ShortCircleB.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(Shot_Co());
    }
    public IEnumerator Shot_Co()
    {
        int safeIndex = 0;
        while (isOn)
        {
            if (safeIndex >= isSafe.Length)
                safeIndex = 0;
            if (isSafe[safeIndex])
            {
                safeIndex++;
                yield return preTime;
                yield return hitTime;
                yield return safeTime;
            }
            else
            {
                AudioManager.Instance.PlayEfx(Efx.ShortLaser);
                safeIndex++;
                PreBeam.SetActive(true);
                yield return preTime;
                PreBeam.SetActive(false);
                ShortCircle.GetComponent<Animator>().SetTrigger("pop");
                ShortCircleB.GetComponent<Animator>().SetTrigger("pop");
                ShortBeam.SetActive(true);
                ShortBeamB.SetActive(true);
                yield return hitTime;
                ShortBeam.SetActive(false);
                ShortBeamB.SetActive(false);
                yield return safeTime;
            }
        }
    }
}
