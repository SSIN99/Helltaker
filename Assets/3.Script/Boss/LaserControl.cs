using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LaserControl : MonoBehaviour
{
    [Serializable]
    public class Order
    {
        public int[] num;
    }
    public Order[] orders;
    [SerializeField] BossLaser[] lasers;
    [SerializeField] float waveTime;
    public void Wave_1()
    {
        StartCoroutine(Wave1_Co());
    }
    public IEnumerator Wave1_Co()
    {
        for(int i = 0; i < orders[1].num.Length; i++)
        {
            lasers[orders[1].num[i]].Shot();
            lasers[orders[1].num[i] + 1].Shot();
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_2()
    {
        StartCoroutine(Wave2_Co());
    }
    public IEnumerator Wave2_Co()
    {
        for (int i = 0; i < orders[2].num.Length; i+=2)
        {
            lasers[orders[2].num[i]].Shot();
            lasers[orders[2].num[i] + 1].Shot();
            lasers[orders[2].num[i + 1]].Shot();
            lasers[orders[2].num[i + 1] + 1].Shot();
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_3_1()
    {
        StartCoroutine(Wave3_1_Co());
    }
    public IEnumerator Wave3_1_Co()
    {
        for (int i = 0; i < orders[3].num.Length; i++)
        {
            if (i < 4)
            {
                lasers[orders[3].num[i]].Shot();
                lasers[orders[3].num[i] + 1].Shot();
            }
            else
            {
                lasers[orders[3].num[i]].Shot();
                lasers[orders[3].num[i] + 1].Shot();
                lasers[orders[3].num[i + 1]].Shot();
                lasers[orders[3].num[i + 1] + 1].Shot();
                i++;
            }
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_3_2()
    {
        StartCoroutine(Wave3_2_Co());
    }
    public IEnumerator Wave3_2_Co()
    {
        for (int i = 0; i < orders[4].num.Length; i++)
        {
            lasers[orders[4].num[i]].Shot();
            lasers[orders[4].num[i] + 1].Shot();
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_3_3()
    {
        StartCoroutine(Wave3_3_Co());
    }
    public IEnumerator Wave3_3_Co()
    {
        for (int i = 0; i < orders[5].num.Length; i++)
        {
            lasers[orders[5].num[i]].Shot();
            lasers[orders[5].num[i] + 1].Shot();
            yield return new WaitForSeconds(waveTime);
        }
    }
}
