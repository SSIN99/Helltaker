using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] private GameObject deadBG;
    [SerializeField] private Tilemap wallTile;
    [SerializeField] private Canvas canvas;
    [SerializeField] private MoveEfxSpawner moveEfx;
    [SerializeField] private Vector3 cur, des, dir;
    [SerializeField] private bool isAction = false;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject transitionClose;
    private bool isDead = false;
    private Box box;
    private Generator gen;
    private BossControl boss;
    private Animator anim;
    private Counter counter;
    private SpriteRenderer rend;

    void Start()
    {
        TryGetComponent<Animator>(out anim);
        TryGetComponent<SpriteRenderer>(out rend);
        GameObject.FindObjectOfType<Counter>().TryGetComponent(out counter);
        cur = transform.position;
        isDead = false;
    }
    void Update()
    {
        if (isDead || GameManager.Instance.isWin || GameManager.Instance.isPause) return;

        if (Input.GetKey(KeyCode.W) && !isAction)
        {
            dir = Vector3.up;
            Action();
        }
        if (Input.GetKey(KeyCode.S) && !isAction)
        {
            dir = Vector3.down;
            Action();
        }
        if (Input.GetKey(KeyCode.A) && !isAction)
        {
            dir = Vector3.left;
            Action();
        }
        if (Input.GetKey(KeyCode.D) && !isAction)
        {
            dir = Vector3.right;
            Action();
        }
    }
    public void Action()
    {
        if (counter.Count == 0) Dead();
        else
        {
            des = cur + dir;
            if (!isWall())
            {
                isAction = true;
                if (isObject())
                    Kick();
                else
                    Move();
                counter.Down_Count();
            }
        }
    }
    public void Move()
    {
        Flip();
        moveEfx.OnEfx(cur);
        anim.SetTrigger("dash");
        AudioManager.Instance.PlayEfx(Efx.CharMove);
        StartCoroutine(Move_Co());
    }
    public IEnumerator Move_Co()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1.0f)
        {
            transform.position = Vector3.Lerp(cur, des, elapsedTime);
            elapsedTime += 0.01f * moveSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        Vector3 roundPosition = new Vector3(Mathf.Round(des.x * 2) / 2.0f, Mathf.Round(des.y * 2) / 2.0f, 0);
        transform.position = roundPosition;
        cur = transform.position;
        yield return new WaitForSeconds(0.07f);
        isAction = false;
    }
    public void Kick()
    {
        Flip();
        if(box)
            box.Move(dir);
        if (gen)
            gen.Break();
        if (boss)
            boss.OnHit();
        anim.SetTrigger("Kick");
        StartCoroutine(Kick_Co());
    }
    public IEnumerator Kick_Co()
    {
        yield return new WaitForSeconds(0.15f);
        isAction = false;
    }
    public void Dead()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
        canvas.gameObject.SetActive(false);
        anim.SetTrigger("Dead");
        deadBG.SetActive(true);
        AudioManager.Instance.PlayEfx(Efx.Death);
        cameraShake.VibrateForTime(0.85f);
        StartCoroutine(Dead_Co());
    }
    public IEnumerator Dead_Co()
    {
        yield return new WaitForSeconds(0.75f);
        AudioManager.Instance.PlayEfx(Efx.TransitionClose);
        transitionClose.SetActive(true);
        canvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public bool isWall()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(des, 0.4f))
        {
            if (col.CompareTag("Laser"))
                return true;
        }
        if (wallTile.HasTile(wallTile.WorldToCell(des)))
            return true;
        else
            return false;
    }
    public bool isObject()
    {
        foreach(Collider2D col in Physics2D.OverlapCircleAll(des, 0.4f))
        {
            if (col.CompareTag("Box") ||
                col.CompareTag("Gen") ||
                col.CompareTag("Boss"))
            {
                col.TryGetComponent<Box>(out box);
                col.TryGetComponent<Generator>(out gen);
                col.TryGetComponent<BossControl>(out boss);
                return true;
            }
        }
        return false;
    }
    public void Flip()
    {
        if (des.x < cur.x)
            rend.flipX = true;
        if (des.x > cur.x)
            rend.flipX = false;
    }
}
