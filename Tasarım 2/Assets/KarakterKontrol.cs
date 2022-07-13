using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;


public class KarakterKontrol : MonoBehaviourPunCallbacks
{
    public Sprite[] beklemeAnim; //publıc sprıte bekleme animasyon için gerekli
    int beklemeAnimS = 0; 
    

    public GameObject dogus;
    SpriteRenderer SpriteRenderer;
    Rigidbody2D fizik;
    Vector3 vec;
    Vector3 kamerasonpoz;
    Vector3 kamerailkpoz;
    public Text health;
    public Image death;
    int Health = 100;

     PhotonView MyPhotonView;
    

    float horizontal = 0;     //karakter x yörüngesindeki hızı
    float beklemeAnimZ;
    float deathSayac;
    float MainmenüT;
    bool Birkerezipla = true;  
    private Animator Anim;


    GameObject Kamera;
    private void Awake()
    {
        MyPhotonView = GetComponent<PhotonView>();
        dogus = GameObject.FindGameObjectWithTag("Dogma");
    }
    
    void Start()
    {
        if (MyPhotonView.IsMine) { 
                                        //Oyun Başında çağırılan metodlar, kamera ve can göstergesi
        SpriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>(); 
        Anim = GetComponent<Animator>();
        Kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kamerailkpoz= Kamera.transform.position-transform.position;
        health.text= "Health=  " + Health;
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel",SceneManager.GetActiveScene().buildIndex);
        }
        }
    }

    void Update()
    {
        if (MyPhotonView.IsMine)
        {
        if (Input.GetKeyDown(KeyCode.Space))    //Zıplama 
        {
            if (Birkerezipla)                  // zıplama kontrol
            {
                fizik.AddForce(new Vector2(0,800));
                Birkerezipla = false;
            }
            

        } }
    }
    
    void FixedUpdate()
    {
           if (Health<=0)            // Karakte Ölürse Devreye girecek metod
        {
           PhotonNetwork.LeaveRoom();
           PhotonNetwork.LeaveLobby();
           PhotonNetwork.Disconnect();
           SceneManager.LoadScene(5);
            
        }
         if (MyPhotonView.IsMine)
        {
        karakterHareket();
        Animasyon();
        }
    }
    void LateUpdate()
  {
     if (MyPhotonView.IsMine)
        {
        kamerakontrol();
        }  
 }

    void karakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");          //Karakterin Yatay düzlemde hareket etmesini sağlayan metod
        vec = new Vector3(horizontal*10,fizik.velocity.y,0);
        fizik.velocity = vec;

    }

    void OnCollisionEnter2D(Collision2D col) {    //Karakterin Katı Bir Yüzeye değdiği anda çalışcak metod
        Birkerezipla = true;
      
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
          if (col.gameObject.CompareTag("Enemy"))
        {
            Health-=50;
            health.text= "Health  " + Health; 
        }
         if (col.gameObject.CompareTag("testere"))
        {
            Health-=50;
            health.text= "Health  " + Health; 
        }
          if (col.gameObject.CompareTag("finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
          if (col.gameObject.CompareTag("chest"))
        {
           col.gameObject.GetComponent<Animator>().Play("Chest");
            Health += 50;
            health.text= "Health  " + Health; 
            col.GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(col.gameObject,2);
          
        }  
        if (col.gameObject.CompareTag("yer"))
        {
            Health-=200;
            health.text= "Health  " + Health; 
        }

    }

    

    void kamerakontrol()     //Gecikmeli Kamera0
    {
        kamerasonpoz = kamerailkpoz + transform.position;
        Kamera.transform.position = Vector3.Lerp(Kamera.transform.position,kamerasonpoz,0.03F);

    }


    void Animasyon()
    {  
        if(Birkerezipla)
        {
            
            if (horizontal == 0)    //bekleme animasyon
        {
            Anim.Play("ıdle");
            beklemeAnimZ += Time.deltaTime;
            if (beklemeAnimZ>0.05)
            {
                   SpriteRenderer.sprite = beklemeAnim[beklemeAnimS++]; // 12 adet sprite attık ama sayaç saymaya devam ediyor
            if (beklemeAnimS == beklemeAnim.Length) // if komutu ile sıfırlayrak animasyonu tekrarlı hale getiriyoruz
            {
                beklemeAnimS = 0;
            }
            beklemeAnimZ = 0;

            }
         
        }

        else if (horizontal > 0) //sağa sola yürüme hareketi animasyonları
        {
            Anim.Play("run");
            
            transform.localScale = new Vector3(1,1,1);

        }

        else if (horizontal < 0)
        {
             Anim.Play("run");
             
            transform.localScale = new Vector3(-1,1,1);

        } }

        else       //ziplama 
        {
            if ( fizik.velocity.y > 0)
            {
                Anim.Play("zipla");
            }

            else
            {
                Anim.Play("zipla2");
            }

            if(horizontal<0)
            {
                transform.localScale = new Vector3(-1,1,1); // Karakterin yönünü değiştiren kod satırı.
            }

            else if(horizontal>0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        }
       
    }

}
