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
    public GameController gameController;
    public Vector3 effectRotation;

    int attackCounter;
    int life = 100;
    const int MoveStartSec = 1;
    [SerializeField]int moveCounter;
    Coroutine _moveStart;

    void Start()
    {
        animator = GetComponent<Animator>();
         _moveStart = StartCoroutine(MoveStart());
    }

    void Update()
    {
        /*if (moveCounter <= 0)
        {
            _moveStart = StartCoroutine(MoveStart());
        }*/

        //体力表示を更新
        textLifeNumber.GetComponent<Text>().text = life.ToString();

        if (life <= 0)
        {
            life = 0;
            enabled = false;
            animator.SetTrigger("Die");
            Invoke("Destroy", 1.2f);
            gameController.GetComponent<GameController>().GameClear();

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
        if (attackCounter % 2 == 0)
        {
            animator.SetTrigger("Projectile Attack");
            Invoke("Attack", 0.6f);

        }
        else
        {
            RandomMove();
        }
    }

    void Attack()
    {
        StopCoroutine(_moveStart);
        Instantiate(effectPrefab, new Vector3(transform.position.x,
                                              transform.position.y + 1.0f,
                                              transform.position.z - 4.0f),
                                              Quaternion.Euler(effectRotation));
                                              StartCoroutine(MoveStart());

    }

    public void RandomMove()
    {

        var m = RandomNumGenerate();

        if (m.x % 2 == 0 && m.z % 2 == 0)
        {
            gameObject.transform.position = new Vector3(m.x, 0, m.z);
        }
        else
        {
            RandomNumGenerate();
        }
    }

    (float x, float z) RandomNumGenerate()
    {
        var x = Random.Range(-2, 3);
        var z = Random.Range(4, 9);

        return (x, z);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            life -= 10;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
