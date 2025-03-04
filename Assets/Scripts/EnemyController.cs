using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Spawning,
        Moving,
        Dying
    }

    public float speed = 2.0f;
    public Material flashMaterial;
    public Material defaultMaterial;
    public AudioClip hitSound;
    public AudioClip deadSound;
    GameObject target;
    State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // target = GameObject.Find("Player"); // don't use Find in Update
        // state = State.Moving;
    }

    public void Spawn(GameObject target)
    {
        this.target = target;
        state = State.Spawning;
        GetComponent<Character>().Initialize();
        GetComponent<Animator>().SetTrigger("Spawn");
        Invoke("StartMoving", 1.0f);
        GetComponent<Collider2D>().enabled = false;
    }

    void StartMoving()
    {
        GetComponent<Collider2D>().enabled = true;
        state = State.Moving;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (state == State.Moving)
        {
            if (target != null)
            {
                Vector2 direction = target.transform.position - transform.position;

                transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);

                if (direction.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (direction.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("### Hit");

        if (collision.tag == "Bullet")
        {
            float d = collision.gameObject.GetComponent<Bullet>().damage;

            // 살아있을때
            if (GetComponent<Character>().Hit(d))
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                Flash();
                // Destroy(gameObject);
                // gameObject.SetActive(false);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(deadSound);
                Die();
            }
        }
    }

    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.5f);
    }

    void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    void Die()
    {
        state = State.Dying;
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.4f);
    }

    void AfterDying()
    {
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
