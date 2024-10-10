using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    Animator anim;

    [SerializeField] float preTime = 0.45f;
    [SerializeField] BossBeam bossBeam;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Shot()
    {
        StartCoroutine(Shot_Co());
    }
    public IEnumerator Shot_Co()
    {
        AudioManager.Instance.PlayEfx(Efx.ShortLaser);
        anim.SetTrigger("pop");
        yield return new WaitForSeconds(preTime);
        bossBeam.HitTime();
        
    }
}
