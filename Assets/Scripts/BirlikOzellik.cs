using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirlikOzellik : MonoBehaviour
{
    public int Id;
    public string Ad;
    public AskerTur BirlikTuru;
    public float Saldiri;
    public float Savunma;
    public float Sok;
    public float Hiz;
    public int Sayi;
    public float AskerCan;
    public float Moral;
    public float Guc;
    public string GeneralAd;
    public int Taraf;
    public Dizilim AktifDizilim;
    public bool yenidenDizil = false;
    public List<GameObject> Askerler;
    public GameObject Dusman1;
    public GameObject Dusman2;
    public GameObject Dusman3;
    public GameObject Dusman4;

    public BirlikOzellik birlik;
    void Start()
    {
        birlik = transform.gameObject.GetComponent<BirlikOzellik>();
    }

    private float GucHesapla(BirlikOzellik birlik)
    {
        float Saldiri = birlik.Saldiri;
        float Savunma = birlik.Savunma;
        float Sok = birlik.Sok;
        float Hiz = birlik.Hiz;
        float Moral = birlik.Moral;
        float Sayi = birlik.Sayi;
        float Guc = ((Saldiri * Moral * ((Sayi / 10) + 1)) + (Sok * Hiz)) - (Savunma * Moral * ((Sayi / 10) + 1));
        return Guc;
    }
    void Update()
    {
        Debug.LogWarning(Sayi);
        if (Time.frameCount % 85 == 0) // Her 85 frame'de bir çalışır
        {
            GucHesapla(birlik);
        }
        if (Sayi <= 0)
        {
            Destroy(transform.gameObject);
        }
        /*if (Moral <= 0)
        {
            if (transform.position.x <= 0 && transform.position.z <= 0)
            {
                var gidilecek = new Vector3(-500,transform.position.y,-500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, birlik.Hiz * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x <= 0 && transform.position.z >= 0)
            {
                var gidilecek = new Vector3(-500, transform.position.y, +500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, birlik.Hiz * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x >= 0 && transform.position.z <= 0)
            {
                var gidilecek = new Vector3(+500, transform.position.y, -500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, birlik.Hiz * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x >= 0 && transform.position.z >= 0)
            {
                var gidilecek = new Vector3(+500, transform.position.y, +500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, birlik.Hiz * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
        }*/
    }
}
