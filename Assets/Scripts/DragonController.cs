using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{
    public GameObject attackEffect;
    public GameObject meteorEffect;
    public GameObject textLifeNumber;
    public Vector3 attackEffectRotation;
    public Vector3 meteorEffectRotation;
    public GameController gameController;

    private AudioSource audioSource;
    public AudioClip damageSE;

    const int MoveStartSecond = 1;
    int moveCounter;
    int attackCounter;
    int life = 100;

    Coroutine _moveStart;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(moveCounter <= 0)_moveStart = StartCoroutine(MoveStart());

        textLifeNumber.GetComponent<Text>().text = life.ToString();

        if (life <= 0)
        {
            life = 0;
            enabled = false;
            animator.SetTrigger("Die");
            Invoke("Destroy", 1.2f);
        }
    }

    IEnumerator MoveStart()
    {
        moveCounter = MoveStartSecond;
        while (moveCounter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            moveCounter--;
            attackCounter++;
        }

        RandomMove();

        if (attackCounter % 2 == 0)
        {
            animator.SetTrigger("Projectile Attack");
            Invoke("Attack", 0.6f);

        }
    }


    void Attack()
    {
        //攻撃時は移動停止
        StopCoroutine(_moveStart);
        var randomPosition = RandomNumberGenerate();
        //敵の口から攻撃エフェクト生成
        Instantiate(attackEffect, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z - 3.0f),
                                              Quaternion.Euler(attackEffectRotation));
        //プレイヤーの頭上からランダムな位置に降るメテオを生成
        Instantiate(meteorEffect, new Vector3(randomPosition.playerX, 5.0f, randomPosition.playerZ),Quaternion.Euler(90,-180,0));
        //攻撃後 移動開始
        StartCoroutine(MoveStart());

    }

    public void RandomMove()
    {
        var randomPosition = RandomNumberGenerate();
        //各パネルの中心点に位置するように移動(最前列には移動しない)
        if (randomPosition.x % 2 == 0 && randomPosition.z % 2 == 0 && randomPosition.z >= 6)
        {
            gameObject.transform.position = new Vector3(randomPosition.x, 0, randomPosition.z);
        }
    }

    (float x, float z, float playerX, float playerZ) RandomNumberGenerate()
    {
        //敵の移動とメテオ攻撃の位置を乱数で決める
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);
        var playerX = Random.Range(-2, 3);
        var playerZ = Random.Range(-2, 3);

        return (x, z, playerX, playerZ);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(damageSE);
            life -= 10;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
        gameController.GetComponent<GameController>().GameClear();
    }
}
