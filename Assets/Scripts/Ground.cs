using UnityEngine;

public class Ground : MonoBehaviour
{

    //eklediğimiz komponent için değişken tanımlıyoz
    private MeshRenderer meshRenderer;


    //değişkene eklediğimiz komponenti yakalıyoruz.
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x / 4;
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
