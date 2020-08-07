using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptDeathField : MonoBehaviour
{
    // deathField'ın Collider seçeneklerinden "isTrigger" kısmı seçildi. Çünkü kendisi ile etkileşime girilsin fakat fiziksel olarak aksiyona uğramasını istemiyoruz.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // deathField ile other(top) etikleşime girdiğinde gerçekleşecek olaylar: (deathField ile etkileşime girdiği an top yok olacak ve yeni bir top spawnlanacak.):
    void OnTriggerEnter(Collider other){ 
        //ball'ın script dosyasını elde edelim;
        scriptBall myScriptBall = other.GetComponent<scriptBall>();
        /*
        GameObject[] bricks=GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject brick in bricks){
            brick.GetComponent<scriptBrick>().Die(); 
         }
        */
        myScriptBall.Die();
    }
}
