using UnityEngine;

public class Spawner : MonoBehaviour
{

    //bu attribute'ü vermeden unity bu kodu nasıl işleyeceğini anlamadı..?
    [System.Serializable]

    //declare a custom data structure
    public struct SpawnableObject
    {
        //Game Object'e referans bir değişken yarat.
        public GameObject prefab;

        //spawn ihtimali için değişken
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;


    //minimum ve maksimum spawn aralığını (süre olarak) belirleyen değişkenler
    //1 ve 2 saniye arasında rastgele spawnlayacak
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;


    //çalışınca min max arasında bir değerle spawn fonksiyonunu çalıştırır
    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    //kapanınca durdurur..?
    private void OnDisable()
    {
        CancelInvoke();
    }

    //objenin çıkma olasılık değeri, spawn olasılık değerinden büyükse obje çıkıyor.
    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                //objenin pozisyonunu spawnerın pozisyonuyla ayarlıyoruz.
                obstacle.transform.position += transform.position;
                //bir obje spawnlanınca döngü bitecek, başa dönecek..?
                break;
            }

            //üst ihtimal tutmazsa alt ihtimalin şansını artırır.
            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }


}
