using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptBall : MonoBehaviour
{
    // Topun kendi kendisini yok etmesi gibi işlemleri bu script dosyasından yöneteceğiz.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // topun kendi kendini yok etmesini ve tekrar top spawn işlemlerini sağlayacak fonksiyonu yazalım:
    public void Die(){
        Destroy(gameObject); // deathField ile etkileşime girdiği an topa kendisini yok ettirmiş oluyoruz.
        GameObject paddleObject = GameObject.Find("paddle"); // paddleObject'in içerisine geçici olarak unity sahnesindeki paddle kopyalanmış olacak.
        scriptPaddle myPaddle = paddleObject.GetComponent<scriptPaddle>(); // geçici paddle objesini elde ettikten sonra, o objenin içerisindeki script dosyasına erişip tekrar top spawn'ı yaptıracak LooseLife(); isimli fonksiyonu çağırıyoruz.
        myPaddle.LooseLife();
    }
}
