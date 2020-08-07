using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptBrick : MonoBehaviour
{
    static int numOfBricks = 0; // birck (tuğla) sayısını başlangıçta static olarak elde edip sistemde mevcudiyetine göre işlemler yapabilmek için oluşturduğumuz değişken.
    public int hitPoints = 3; // brick (tuğlaların) durabilty değişkeni olarak düşünülebilir.

    // tuğla çeşitliliği : Brick - ToughBrick - ExtraToughBrick şeklindedir.
    public int scorePoints = 1; // kırılan tuğladan tuğlaya puan çeşitliliği olması adına oluşturduğumuz değişken.

    // Start is called before the first frame update
    void Start()
    {
        numOfBricks++;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision col){ 
        hitPoints--; // her çarpışta durabilty azalıyor ve bu değişken sıfır olduğunda destroy işlemi uygulanıyor.
        if(hitPoints <= 0){
            Die();
        }

    }
    void Die(){ // brick'lere çarptığında destroy işlemini yaptıracağımız fonskiyon.
        Destroy(gameObject);
        GameObject.Find ("paddle").GetComponent<scriptPaddle>().addScore(scorePoints); // paddle objesinin script dosyasındaki addScore isimli fonksiyonu burada (brick destroy'landıktan hemen sonra) çağırıyoruz. ne kadar scorePoints'e sahipse o parametreyi yolluyoruz.

        numOfBricks--; 
        if(numOfBricks <= 0){ // level bitmiş demektir.
            //Debug.Log("Oyun Bitti.");
            // diğer level sahnesine geçme işelmleri
            if(Application.loadedLevelName == "level1"){
                SceneManager.LoadScene("level2");
            }
            else if(Application.loadedLevelName == "level2"){
                SceneManager.LoadScene("level3");
            }
            else if(Application.loadedLevelName == "level3"){
                SceneManager.LoadScene("level4");
            }
            else if(Application.loadedLevelName == "level4"){
                SceneManager.LoadScene("level5");
            }
            else if(Application.loadedLevelName == "level5"){
                SceneManager.LoadScene("gameOver");
            }
            
        }
    }
}
