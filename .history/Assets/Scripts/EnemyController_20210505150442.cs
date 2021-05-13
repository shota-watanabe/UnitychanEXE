using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    const float MinLaneX = -1.4f;
    const float MaxLaneX = 1.4f;
    const float LaneWidth = 1.4f;
    const int MoveStartSec = 3;
    const int AttackStartSec = 2;

    CharacterController controller;
    Animator animator;
    Vector3 moveDirection = Vector3.zero;

    public GameObject effectPrefab;
    public GameObject textLifeNumber;
    public Vector3 effectRotation;
    public UnityEvent OnDestroyed = new UnityEvent();

    public int gravity;
    public float speed;

    int counter = 0;
    int life = 20;
    float targetLaneX;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (counter == 0)
        {
            StartCoroutine(RandomMove());
            StartCoroutine(AttackStart());
        }

        float ratioX = (targetLaneX * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speed;

        // 重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //体力表示を更新
        textLifeNumber.GetComponent<Text>().text = life.ToString();

        if (life <= 0)
        {
            animator.SetTrigger("Die");
            Invoke("Destroy", 1.0f);
        }
    }

    public void OnTriggerStay(Collider collider)
    {
        /*検知オブジェクトがPlayerなら攻撃する
        if (collider.CompareTag("Player") && counter <= 0)
        {
            StartCoroutine(AttackStart());
        }*/
    }

    IEnumerator AttackStart()
    {
        counter = AttackStartSec;

        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        animator.SetTrigger("Attack 02");
        Invoke("Attack", 0.3f);
    }

    IEnumerator RandomMove()
    {
        counter = MoveStartSec;

        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }

        //ランダムに上下移動
        switch (Random.Range(0, 2))
        {
            case 0: MoveToUp(); break;
            case 1: MoveToDown(); break;
        }
    }

    public void MoveToUp()
    {
        if (targetLaneX > MinLaneX) targetLaneX -= MaxLaneX;
    }

    public void MoveToDown()
    {
        if (targetLaneX < MaxLaneX) targetLaneX += MaxLaneX;
    }

    void Attack()
    {
        //敵の口から攻撃エフェクト生成
        Instantiate(effectPrefab, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z - 2.0f),
                                              Quaternion.Euler(effectRotation));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            life -= 10;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        OnDestroyed.Invoke();
    }
}