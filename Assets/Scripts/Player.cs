using UnityEngine;

public class Player : MonoBehaviour
{

    //character adında, CharacterController komponıntının bir değişkeni.
    private CharacterController character;

    //playerın gittiği yönün değişkeni.
    private Vector3 direction;

    //Yerçekimi için değişken -gerçek hayattaki 9.81.
    //Editördeki değer esas değer. Editörden değiştirirsen ordaki kabul edilir.
    public float gravity = 90f;

    public float jumpForce = 40f;

    private bool ju = false;

    public Vector3 screenStart;

    private Vector3 forvard = new Vector3(30f, 30f, 0f);


    //script okunduğunda ilk çalışacak bir gömülü fonksiyon.
    private void Awake()
    {


        screenStart = Camera.main.ScreenToWorldPoint(Vector3.zero);

        if (transform.position.x != screenStart.x + 10f)
        {
            transform.position = new Vector3(screenStart.x + 10f, 0f, 0f);
        }





        //character değişkeni için CharacterController komponıntını buluyor.
        character = GetComponent<CharacterController>();
    }

    //yazılım aktif olduğun çağırılacak gömülü fonksiyon.
    private void OnEnable()
    {
        //Player her yeniden başladığında yön değerini sıfırlar.
        direction = Vector3.zero;
    }

    public void jumping()
    {
        ju = true;
        Update();
        ju = false;
    }


    //Unitynin her frame çağırdığı update fonksiyonu. Inputu kontrol edeceğin yer.
    private void Update()
    {


        //Aşağı (down) yönlü gravity katsayısı kadar tüm zaman boyunca uygulanacak kuvvet = yerçekimi işlevi görecek.
        direction += Vector3.down * gravity * Time.deltaTime;

        //karakter yerdeyse zıplayabilme
        if (character.isGrounded)
        {
            //karakter yerdeyse aşağıya itme(yerçekimi işlevini uygulama), constant -sabit- kuvvet uygula...?
            direction = Vector3.down;

            //zıplama tuşuna basınca (space)
            if ((Input.GetButton("Jump")) || ju == true)
            {
                //yukarı yönlü jumpForce kadar kuvvet uygula.
                direction = Vector3.up * jumpForce;
            }




        }

        //karakteri tüm zaman boyunca direction yönünde hareket ettirir.
        character.Move(direction * Time.deltaTime);
    }


    //Game over için trigger (değme) algılama fonksiyonu

    private void OnTriggerEnter(Collider other)
    {
        //algıladığında GameManager'daki game over fonksiyonunu çağırıyoruz.
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }





}
