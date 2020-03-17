using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int vidas = 100;

    public float velocidad = 2;

    public float rangoDeDisp = 0.5F;

    public Rigidbody2D rigidBody;

    public Sprite ImagenInicial;

    public Sprite ImagenMovimiento;

    private SpriteRenderer renderizador;

    public float TiempoAnDeCambiarImagen = 0.7F;

    public GameObject Disparo_ene;

    public float TiempoMinimoDeDisparo = 3.0F;

    public float TiempoMaximoDeDisparo = 10.0F;

    public float TiempoBaseDeDisparo = 3F;

    public Sprite ExplosionNave1;

    public Sprite ExplosionNave2;

    private float fireRate = 5.0f;

    void Start()
    {
        

        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = new Vector2(1, 0) * velocidad;

        renderizador = GetComponent<SpriteRenderer>();

        StartCoroutine(CambiarImagen());

        TiempoBaseDeDisparo += Random.Range(TiempoMinimoDeDisparo, TiempoMaximoDeDisparo);

    }

    void Cambiar(int direccion)
    {
        Vector2 nuevaVelocidad = rigidBody.velocity;
        nuevaVelocidad.x = velocidad * direccion;
        rigidBody.velocity = nuevaVelocidad;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //soundManager.Instance.PlayOneShot(SoundManager.Instance.Explosion);
            col.gameObject.GetComponent<SpriteRenderer>().sprite = ExplosionNave1;
            Destroy(gameObject);
            Destroy(col.gameObject, 2F);
            col.gameObject.GetComponent<SpriteRenderer>().sprite = ExplosionNave2;
            Destroy(col.gameObject, 1.5F);
        }

        if (col.gameObject.name == "Muro_izq")
        {
            Cambiar(1);//se llama a la funcion cambiar y se le manda el entero 1
        }

        if (col.gameObject.name == "Muro_der")
        {
            Cambiar(-1);
        }

        if (col.gameObject.tag == "balas")
        {
            vidas--;
        }
        if (vidas <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
            Disparo.puntajeNum = 0;
        }
    }

    public IEnumerator CambiarImagen()//funcion para cambiar la imagen
    {
        while (true)
        {
            if (renderizador.sprite == ImagenInicial)
                renderizador.sprite = ImagenMovimiento;
            else
                renderizador.sprite = ImagenInicial;
            yield return new WaitForSeconds(TiempoAnDeCambiarImagen);
        }

    }

    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > TiempoBaseDeDisparo)
        {
            TiempoBaseDeDisparo += Random.Range(TiempoMinimoDeDisparo, TiempoMaximoDeDisparo);
            Debug.Log("Awa");
            Instantiate(Disparo_ene, this.transform.position, this.transform.rotation);
        }
    }
}