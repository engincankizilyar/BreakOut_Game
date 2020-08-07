using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui elementlerini kullanabilmek için

public class scriptPaddle : MonoBehaviour
{
                                                                         // --- BreakOut --- //

    // unity programı içerisinde kontrollerin daha keskinleştirilmesi adına gravity ve sensivity değerleri 1000 yapıldı. Bu sayede tuştan el çekildiği anda daha rahat kontrol sağlanmakta.

    public GameObject prefabBall;
    GameObject attachedBall = null;
    int lives = 6; // oyuna 5 hak ile başlatıyoruz.
    int score = 0;
    public Canvas myCanvas; // canvas'ı script dosyasında kullanabilmek için oluşturduk.
    public Text scoreText; // scoreText objesine erişip script dosyasında çeşitli değişiklikler yapabilmek için oluşturduk.

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); // paddle objesini başlangçta ortadan kaldırma demektir. (level'lar arası geçiş için)
        DontDestroyOnLoad(myCanvas); // canvas'ın da level geçişlerinde yok edilmesini sağlıyoruz.
        LooseLife();
    }
    void spawnBall(){
        attachedBall = (GameObject) Instantiate(prefabBall, transform.position + new Vector3(0, 0.7f,0), Quaternion.identity); // prefabBall'ı oyuna, attachdBall ismiyle script dosyasından dahil ediyoruz. (topu oluşturuyoruz.)
    }

    //paddle objesi, scriptReStart'ta destroy edilirken yapılması gereken işlemler:
    void OnDestroy(){ // paddle objesi, gameOver sahnesine geçtiğinde destroy'lanırken çalışacak olan fonksiyon(gameOver sahnesinde extradan top görünmemesi için.)
        Destroy(attachedBall);
    }

    // level değiştirildiği zaman kullanılan OnLevelWasLoaded fonksiyonu ile çeşitli işlemler:
    public void OnLevelWasLoaded(int level){ // her level değiştiğinde çalışacak fonksiyon.
        // gameOver sahnesinden sonra yeniden başla butonu tıklandığında level1'de 2 adet top görmemek için yapmamız gerek kontroller:
        if(Application.loadedLevelName != "level1"){
            spawnBall();    
        }
    }

    public void LooseLife(){ // top dışarı kaçıp kendini yok etmiş demektir, topu tekrar oluşturmak için (spawnlamak için) bu fonksiyonu kullanacağız.
        
        // top tekrar spawnlandıktan sonra hak sayısı düzenlemeleri:
        lives--;
        myCanvas.GetComponentsInChildren<Text>()[0].text = "Lives: " + lives; // Canvas'ın içerisinden 0.indexteki ilk child olan livesText'e erişiyoruz ve içeriğini değiştiriyoruz.(hak azaldıkça)
        
        // hakkın bitip bitmediğini ve gameOver sahnesine geçiş yapılıp yapılmayacağını denetleyelim:
        if(lives <= 0){
            Application.LoadLevel("gameOver");
        }
        spawnBall(); 
    }

    //brick'leri kırdıkça scoreText'i artırmak için gerekli fonksiyonumuz:
    public void addScore(int n){
        score += n; // n değişkeni, brick'lerin zorluğuna bağlı ne kadar score artışı olacağını belirliyor.
        scoreText.text = "Score: " + score;
        // myCanvas.GetComponentsInChildren<Text>()[1].text = "Score: " + score; // alternatif 
    }

    // Update is called once per frame
    void Update()
    {
        // prefabBall olarak kullanacağımız GameObject'e (attachedBall'a) çeşitli özellikler ekleyelim.
        
        if(attachedBall){ // attachedBall null değilse kontrolü
            attachedBall.GetComponent<Rigidbody>().position = transform.position + new Vector3(0,0.7f,0); // attachedBall'ın, paddle ile birlikte hareketini sağlıyoruz.

            if(Input.GetButtonDown("Jump")){
                attachedBall.GetComponent<Rigidbody>().isKinematic = false; // attachedBall'ın ilk anda titremesini önlemek için
                attachedBall.GetComponent<Rigidbody>().AddForce(300f * Input.GetAxis("Horizontal"),300f,0); // attahcedBall'a kuvvet uygulanıyor
                attachedBall = null;
            }
        }

        // sağ ve sola hareketin daha kullanışlı bir versiyonu:
        transform.Translate(10f * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0); // Time.delta time sebebi: oyundaki etkileşimin her bilgisayarda aynı süre zarfında gerçekleşmesini sağlmak içindir.

        // paddle'ın sağ ve sola hareketini plane'in sınırları içerisinde sınırlandırma işlemleri:
        if(transform.position.x < -2.8f){
            transform.position = new Vector3 (-2.8f, transform.position.y, transform.position.z); // eğer sınırları aşarsa, plane sınırlarının en uç noktasındaki pozisyonu cismimize atıyoruz.
        }
        if(transform.position.x > 2.8f){
            transform.position = new Vector3(2.8f,transform.position.y , transform.position.z); // eğer sınırları aşarsa, plane sınırlarının en uç noktasındaki pozisyonu cismimize atıyoruz.
        }
    }

    // topun paddle'a çarparak açılı hareketinin sağlanması için:
    void OnCollisionEnter(Collision col){
        foreach(ContactPoint contact in col.contacts){
            if(contact.thisCollider == GetComponent<Collider>()){
                float kayma = contact.point.x - transform.position.x; // topun ortaya çarpmadığı zamanki sapmayı tespit ediyoruz.
                contact.otherCollider.GetComponent<Rigidbody>().AddForce(300f * kayma,0,0); // top orta noktaya çarpmadığında hesap ettiğimiz kayma ile birlikte açılı hareketi için kuvvet UYGULUYORUZ.
            }
        }


    }




}
