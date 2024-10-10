using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoomControl : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public Vector2[] pos;
    }
    public Wave[] waves;



    [SerializeField] GameObject boomPrefeb;
    [SerializeField] float waveTime;
    Queue<GameObject> pool;
    int poolCount = 16;


    private void Start()
    {
        GameObject temp;
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolCount; i++)
        {
            temp = Instantiate(boomPrefeb, transform.position, Quaternion.identity);
            temp.SetActive(false);
            temp.transform.SetParent(gameObject.transform);
            pool.Enqueue(temp);
        }
    }
    public void Wave_2()
    {
        StartCoroutine(Wave_2_Co());
    }
    public IEnumerator Wave_2_Co()
    {
        GameObject temp;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 0; j < waves[i].pos.Length; j++)
            {
                temp = pool.Dequeue();
                temp.transform.position = waves[i].pos[j];
                temp.SetActive(true);
                pool.Enqueue(temp);
            }
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_3_1()
    {
        StartCoroutine(Wave_3_1_Co());
    }
    public IEnumerator Wave_3_1_Co()
    {
        GameObject temp;
        for (int i = 5; i < 9; i++)
        {
            for (int j = 0; j < waves[i].pos.Length; j++)
            {
                temp = pool.Dequeue();
                temp.transform.position = waves[i].pos[j];
                temp.SetActive(true);
                pool.Enqueue(temp);
            }
            yield return new WaitForSeconds(waveTime);
        }
    }
    public void Wave_3_2()
    {
        StartCoroutine(Wave_3_2_Co());
    }
    public IEnumerator Wave_3_2_Co()
    {
        GameObject temp1, temp2;
        for (int i = 0; i < 15; i++)
        {
            if (i < 8)
            {
                temp1 = pool.Dequeue();
                temp2 = pool.Dequeue();
                temp1.transform.position = new Vector2((-3.5f + (1f * i)), -1.3f);
                temp2.transform.position = new Vector2((-3.5f + (1f * i)), -3.3f);
                temp1.SetActive(true);
                temp2.SetActive(true);
                pool.Enqueue(temp1);
                pool.Enqueue(temp2);
            }
            else
            {
                temp1 = pool.Dequeue();
                temp2 = pool.Dequeue();
                temp1.transform.position = new Vector2((3.5f - (1f * (i - 7))), -1.3f);
                temp2.transform.position = new Vector2((3.5f - (1f * (i - 7))), -3.3f);
                temp1.SetActive(true);
                temp2.SetActive(true);
                pool.Enqueue(temp1);
                pool.Enqueue(temp2);
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

}
