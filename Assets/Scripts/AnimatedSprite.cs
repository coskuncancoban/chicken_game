using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{

    //animasyon olacak görsellerin olduğu array
    public Sprite[] sprites;

    //Sprite Renderer ile ilişkilendireceğimiz variable
    private SpriteRenderer spriteRenderer;

    //hangi framede olduğumuzu tutacak variable
    private int frame;

    private bool pressed;
    

    //ilişkilendirme
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    //oyun başlamadan önce bir oyun hızı oluşmadığı için animate fonksiyonu oyun başlar başlamaz çağırılamaz. oyun hızı 0 döneceği için aşağıda hata oluşur.
    //Bu yüzden bu kodla oyunun başlamasından hemen sonra çağırılacak.
    //oyun hızını animasyonu etkileyen bir değişken olarak ayarladık.
    // 0f kadar bekleme başlangıçtan hemen sonraya denk geliyo..? bizim için yetiyo..?
    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }


    //Animasyonu durduracak, yazılım aktifliğini kaybedince.
    private void OnDisable()
    {
        CancelInvoke();
    }


    //animasyonu yapan fonksiyon
    private void Animate()
    {

    

        frame++;

        //frame (sahne) animasyon olacak görsel sayısını geçtiğinde başa dön, sahne değerini artır.
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        //hata vermemesi için if içerisinde..?
        //frame istenen değerlerdeyse frame'e göre sprites arrayinden görsel kullan.
        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        //gameSpeed (oyun hızı)na göre Animate fonksiyonunu tekrar tekrar çalıştır.
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed * 24f);

    

    }





}
