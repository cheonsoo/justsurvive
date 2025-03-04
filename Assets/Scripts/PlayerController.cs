using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCopntroller : MonoBehaviour
{
  public float speed = 5.0f;
  public GameObject bulletPrefab;

  public Material flashMaterial;
  public Material defaultMaterial;

  public AudioClip shotSound;
  public AudioClip hitSound;
  public AudioClip deadSound;

  Vector3 move;

  void Start()
  {

  }

  void Update()
  {
    move = Vector3.zero;

    // Left
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      move += new Vector3(-1, 0, 0);
    }

    // Right
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      move += new Vector3(1, 0, 0);
    }

    // Up
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      move += new Vector3(0, 1, 0);
    }

    // Down
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
      move += new Vector3(0, -1, 0);
    }

    move = move.normalized;

    if (move.x < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
    }
    else if (move.x > 0)
    {
      GetComponent<SpriteRenderer>().flipX = false;
    }

    if (move.magnitude > 0)
    {
      GetComponent<Animator>().SetTrigger("Move");
    }
    else
    {
      GetComponent<Animator>().SetTrigger("Stop");
    }

    if (Input.GetMouseButtonDown(0))
    {
      Shoot();
    }
  }

  void Shoot()
  {
    GetComponent<AudioSource>().PlayOneShot(shotSound);

    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Debug.Log("### WorldPosition: " + worldPosition);

    worldPosition.z = 0;
    worldPosition -= (transform.position + new Vector3(0, -0.5f, 0));

    // GameObject newBullet = Instantiate<GameObject>(bulletPrefab);
    GameObject newBullet = GetComponent<ObjectPool>().Get();
    if (newBullet != null)
    {
      newBullet.transform.position = transform.position + new Vector3(0, -0.5f);
      newBullet.GetComponent<Bullet>().Direction = worldPosition;
    }
  }

  private void FixedUpdate()
  {
    transform.Translate(move * speed * Time.fixedDeltaTime);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      Debug.Log("### Hit");
      if (GetComponent<Character>().Hit(1))
      {
        // GetComponent<Character>().flash();
        GetComponent<AudioSource>().PlayOneShot(hitSound);
        Flash();
      }
      else
      {
        // GetComponent<Character>().Die();
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
    // state = State.Dying;
    GetComponent<Animator>().SetTrigger("Die");
    Invoke("AfterDying", 0.875f);
  }

  void AfterDying()
  {
    // gameObject.SetActive(false);
    SceneManager.LoadScene("GameOverScene");
  }

}
