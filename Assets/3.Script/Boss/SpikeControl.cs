using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeControl : MonoBehaviour
{
    [SerializeField] float waveTime;
    [SerializeField] SpikeSpawner[] spawners;

    public void Wave_1()
    {
        StartCoroutine(Wave_1_Co());
    }

    public IEnumerator Wave_1_Co()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
                spawners[0].WaveSpike();
            else
                spawners[i].WaveSpike();
            yield return new WaitForSeconds(waveTime);
        }
    }
}
