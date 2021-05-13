using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragonController : MonoBehaviour
{
    Animator animator;

    public GameObject effectPrefab;
    public GameObject textLifeNumber;
    public Vector3 effectRotation;

    int attackCounter;
    int life = 100;
    const int AttackStartSec = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackCounter <= 0)
        {
            StartCoroutine(AttackStart());
        }

        //体力表示を更新
        textLifeNumber.GetComponent<Text>().text = life.ToString();
        Debug.Log(life);

        if (life <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    IEnumerator AttackStart()
    {
        attackCounter = AttackStartSec;

        while (attackCounter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            attackCounter--;
        }
        animator.SetTrigger("Projectile Attack");
        Invoke("Attack", 0.6f);

    }

    void Attack()
    {
        //敵の口から攻撃エフェクト生成
        Instantiate(effectPrefab, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z - 4.0f),
                                              Quaternion.Euler(effectRotation));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            life -= 10;
        }
    }
}
