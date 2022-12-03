using TMPro; //Text Mesh Pro ile UI kullandığımız için.
using UnityEngine;
using UnityEngine.UI; //Text Mesh Pro içindeki Button buradanmış. O yüzden bunu da yazmalıyız.


//Basit bir SİNGLETON örneği..?
//singleton un mantığı yalnızca bir tane olması ve her yerden ulaşabilmen.
public class GameManager : MonoBehaviour
{


    //public getter, private setter..? herkes ulaşabilir ama sadece bu script değiştirebilir.
    public static GameManager Instance { get; private set; }

    //oyun başladığındaki ilerleme hızı
    public float initialGameSpeed = 80f;

    //oyun hızının artışı
    public float gameSpeedIncrease = 1.5f;

    //oyun devam ettikçe esas ilerleme hızı
    public float gameSpeed { get; private set; }


    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;
    public Button resetHiscore;




    private Player player;
    private Spawner spawner;

    public float score;

    private float hiscore;

    //instance yoksa instance bu..? Varsa yok et.



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    /*
        private void OnEnable()
        {
            Invoke()
        }

    */
    //yok edilen buysa boş bırak (ki yukardakiyle eşleşip tekrar bu olsun)
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    //ilk framei çağırarak oyunu başlatan gömülü fonksiyon.
    private void Start()
    {
        //değişkenleri yarattıktan sonra onları unityde buluyoruz.
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);


        NewGame();
    }


    public void NewGame()
    {

        //yeni oyunda önceki yanmadan ekranda kalan engelleri temizler.
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
            //sadece obstacle dersen (.gameObject demezsen) objelerin içindeki obstacle scriptini kaldırır, objeler ekranda kalır..?
            //ama bizim objenin tamamını, kendisini ekrandan kaldırmamız gerek
        }


        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }



    public void GameOver()
    {
        //oyun hızını sıfırlayacak
        gameSpeed = 0f;
        //yazılımı disable edecek ki oyun hızı artmaya devam etmesin
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }


    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += 20f * Time.deltaTime;
        //FloorToInt float'ı integer a çevirir ve alt değere yuvarlar.
        //ToString içindeki "D5" 5 decimal yani 5 basamak anlamında. Değer ne olursa olsun 5 basamak gözükecek. 00027 gibi.
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");


    }

    //PlayerPrefs means Player Preferences. Oyun dışında dahi oyuncunun ayarlarını tutan ve koruyan bir class. string float ve int saklayabiliyor.


    private void UpdateHiscore()
    {
        //PlayerPrefs means Player Preferences. Oyun dışında dahi oyuncunun ayarlarını tutan ve koruyan bir class. string float ve int saklayabiliyor.
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);


        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");




        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

    }


    public void ResetHiscore()
    {
        hiscore = 0f;
        PlayerPrefs.SetFloat("hiscore", hiscore);

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");


    }




}
