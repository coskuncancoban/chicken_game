using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //objelerin kaybolacağı kooordinat değişkeni
    public float leftEdge;



    //objelerin kaybolacağı yerin kordinatlarını (kameraya göre) hesaplar
    private void Start()
    {


        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 10f;
    }




    //yaratılan engellerin konumlarını belirleyen (tavuğa doğru ilerlemelerini sağlayan) fonksiyon.
    private void Update()

    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime / 4;

        //objeler koordinatı geçti mi kontrol edip yok eden döngü.
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }

    }




}
