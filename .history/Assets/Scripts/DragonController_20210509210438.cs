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

    const int MoveStartSec = 1;
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
        moveCounter = MoveStartSec;
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
        var m = RandomNumGenerate();
        //敵の口から攻撃エフェクト生成
        Instantiate(attackEffect, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z - 4.0f),
                                              Quaternion.Euler(attackEffectRotation));
        //プレイヤーの頭上からランダムな位置に降るメテオを生成
        Instantiate(meteorEffect, new Vector3(m.px, 5.0f, m.pz),Quaternion.Euler(90,-180,0));
        //攻撃後 移動開始
        StartCoroutine(MoveStart());

    }

    public void RandomMove()
    {
        var m = RandomNumGenerate();
        //各パネルの中心点に位置するように移動(最前列には移動しない)
        if (m.x % 2 == 0 && m.z % 2 == 0 && m.z >= 6)
        {
            gameObject.transform.position = new Vector3(m.x, 0, m.z);
        }
        Debug.Log(m);
    }

    (float x, float z, float px, float pz) RandomNumGenerate()
    {
        //敵の移動とメテオ攻撃の位置を乱数で決める
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);
        var px = Random.Range(-2, 3);
        var pz = Random.Range(-2, 3);

        return (x, z, px, pz);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
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
