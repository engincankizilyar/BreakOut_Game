using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptReStart : MonoBehaviour
{
    // gameOver sahnesinde oyunu yeniden başlatmak için gerekli işlemler bu script dosyasında yapılacak.

    // Start is called before the first frame update
    void Start()
    {
        //gameOver sahnesine geçiş yapılırken level1 sahnesindeki canvas ve paddle objelerini destroy'lamamız gerekiyor.
        Destroy(GameObject.Find("paddle"));
        Destroy(GameObject.Find("Canvas"));
        
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartGame(){
        Application.LoadLevel("level1");
    }
}
