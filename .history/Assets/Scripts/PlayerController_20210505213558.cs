using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    const float MinLaneX = -1.4f;
    const float MaxLaneX = 1.4f;
    const float MinLaneZ = -1.4f;
    const float MaxLaneZ = 1.4f;
    const float LaneWidth = 1.4f;
    const float StunDuration = 0.8f;

    CharacterController controller;
    Animator animator;
    Vector3 moveDirection = Vector3.zero;

    public GameObject punchEffect, destroyEffect;
    public GameObject textLifeNumber;
    public GameController gameController;
    public Vector3 effectRotation;

    public int gravity;
    public float speed;
    int life = 100;
    float recoverTime = 0.0f;
    float targetLaneX;
    float targetLaneZ;

    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("up")) MoveToUp();
        if (Input.GetKeyDown("down")) MoveToDown();

        if (IsStun())
        {
            //動きを止め、復帰カウントを進める
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            // 目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLaneX * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speed;

            float ratioZ = (targetLaneZ * LaneWidth - transform.position.z) / LaneWidth;
            moveDirection.z = ratioZ * speed;

        }

        // 重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        // 移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //体力表示を更新
        textLifeNumber.GetComponent<Text>().text = life.ToString();

        if(life <= 0)
        {
            life = 0;
            animator.SetTrigger("Down");
            Invoke("Destroy", 0.8f);
            gameController.GetComponent<GameController>().GameOver();

        }

        animator.SetBool("Idle", true);

    }

    public void MoveToLeft()
    {
        if (IsStun()) return;
        if (targetLaneZ > MinLaneZ) targetLaneZ -= MaxLaneZ;

    }

    public void MoveToRight()
    {
        if (IsStun()) return;
        if (targetLaneZ < MaxLaneZ) targetLaneZ += MaxLaneZ;
    }

    //X軸は下方向が正、上方向が負になる
    public void MoveToUp()
    {
        if (IsStun()) return;
        if (targetLaneX > MinLaneX) targetLaneX -= MaxLaneX;
    }

    public void MoveToDown()
    {
        if (IsStun()) return;
        if (targetLaneX < MaxLaneX) targetLaneX += MaxLaneX;
    }

    public void Attack()
    {
        if (IsStun()) return;

        animator.SetTrigger("Punch");
        Instantiate(punchEffect, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z + 2.0f),
                                              Quaternion.Euler(effectRotation));

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            life -= 50;
            recoverTime = StunDuration;

            if(life != 0)animator.SetTrigger("Damage");
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
        Instantiate(destroyEffect, new Vector3(transform.position.x,
                                              transform.position.y,
                                              transform.position.z),
                                              Quaternion.Euler(-90,0,0));
    }
}
