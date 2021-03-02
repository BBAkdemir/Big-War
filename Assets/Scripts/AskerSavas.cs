using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskerSavas : MonoBehaviour
{
    private ParentTakip AskerErisim;
    private SphereCollider EtkiAlani;
    private BirlikOzellik KomutanErisim;
    public GameObject Asker;
    private GameObject Komutan;
    public GameObject Dusman;
    private AskerSavas DusmanMi;

    Collider[] hitCollider;

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
    void Start()
    {
        AskerErisim = Asker.GetComponent<ParentTakip>();
        EtkiAlani = Asker.GetComponent<SphereCollider>();
        Komutan = AskerErisim.target.gameObject.transform.parent.gameObject;
        KomutanErisim = Komutan.GetComponent<BirlikOzellik>();

        Id = KomutanErisim.Id;
        Ad = KomutanErisim.Ad;
        BirlikTuru = KomutanErisim.BirlikTuru;
        Saldiri = KomutanErisim.Saldiri;
        Savunma = KomutanErisim.Savunma;
        Sok = KomutanErisim.Sok;
        Hiz = KomutanErisim.Hiz;
        Sayi = KomutanErisim.Sayi;
        AskerCan = KomutanErisim.AskerCan;
        Moral = KomutanErisim.Moral;
        Guc = KomutanErisim.Guc;
        GeneralAd = KomutanErisim.GeneralAd;
        Taraf = KomutanErisim.Taraf;
        AktifDizilim = KomutanErisim.AktifDizilim;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dustumu")
        {
            transform.position = new Vector3(transform.position.x, AskerErisim.target.position.y, transform.position.z);
        }
    }
    void Update()
    {
        if (Time.frameCount % 10 == 0) // Her 10 frame'de bir çalışır
        {
            makeRaycast();
        }
    }
    private void makeRaycast()
    {
        float range = EtkiAlani.radius + 1.5f;
        hitCollider = Physics.OverlapSphere(transform.position, range);
        foreach (var item in hitCollider)
        {
            DusmanMi = item.gameObject.GetComponent<AskerSavas>();
            if (DusmanMi != null && DusmanMi.Taraf != Taraf)
            {
                Dusman = item.gameObject;
                DusmanMi.AskerCan -= Guc;
                if (AskerCan <= 0)
                {
                    KomutanErisim.yenidenDizil = true;
                    KomutanErisim.Sayi -= 1;
                    Debug.Log("asker öldü, sayi:" + KomutanErisim.Sayi);
                    KomutanErisim.Moral -= 0.05f;
                    Destroy(Asker);
                    Destroy(AskerErisim.target.gameObject);
                }
            }
        }
    }


}
