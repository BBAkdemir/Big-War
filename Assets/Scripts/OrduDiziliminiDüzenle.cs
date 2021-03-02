//using Assets.Scripts.Classlar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrduDiziliminiDüzenle : MonoBehaviour
{
    private HangisiHareketEtsin erisim;
    private ParentTakip Asker;
    private AskerSavas AskerSavasScript;
    private HangisiHareketEtsin hareketEdecek;
    private OrduHareket KomutaniAl;
    private BirlikOzellik BirlikOzellik;
    private Dizilim dizilim;

    private GameObject Komutan;
    private GameObject hareketEdecekObje;
    private GameObject hareketEdecekObjeEbeveyni;
    private GameObject hareketEdecekObjeAsker;
    private GameObject SecmeAlaniAl;
    private GameObject secmeAlani;
    private GameObject ebeveynYedek;

    private List<GameObject> askerler;
    public List<Dizilim> dizilimListesi;

    private bool f1Tiklanmismi;
    
    private Quaternion Yon;
    private Vector3 SecmeAlaniKonum;
    private Vector3 KomutanKonum;
    private Vector3 SecmeAlaniBoyut;

    public OrduDiziliminiDüzenle()
    {
        List<Dizilim> dizilimListesi = new List<Dizilim>();
        dizilimListesi.Add(dizilim1());
        dizilimListesi.Add(dizilim2());
        dizilimListesi.Add(dizilim3());
        this.dizilimListesi = dizilimListesi;
    }
    private void Start()
    {
        Komutan = gameObject.transform.parent.gameObject;
        BirlikOzellik = Komutan.GetComponent<BirlikOzellik>();
    }
    void Update()
    {
        hareketEdecek = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        askerler = new List<GameObject>();
        
        if (BirlikOzellik.yenidenDizil == true)
        {
            hareketEdecekObjeEbeveyni = Komutan;
            ebeveynYedek = hareketEdecekObjeEbeveyni;
            ListeyeAl(Komutan);
            DizilimiDegistir(BirlikOzellik.AktifDizilim, Komutan, ebeveynYedek);
        }

        if (f1Tiklanmismi && (hareketEdecek.hareketEdecek.tag == "SecmeAlani" || hareketEdecek.hareketEdecek.tag == "Askerler1"))//hareket edecek varmı diye kontrol et
        {
            PivotVeSecmeAlaniAl();
            ebeveynYedek = hareketEdecekObjeEbeveyni;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DizilimiDegistir(dizilim1(), hareketEdecekObjeEbeveyni, ebeveynYedek);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DizilimiDegistir(dizilim2(), hareketEdecekObjeEbeveyni, ebeveynYedek);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DizilimiDegistir(dizilim3(), hareketEdecekObjeEbeveyni, ebeveynYedek);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            f1Tiklanmismi = true;
        }
    }
    public void DizilimiDegistir(Dizilim dizilim, GameObject komutan, GameObject komutanYedek)
    {
        BirlikOzellik.AktifDizilim = dizilim;
        Quaternion yon = komutan.transform.rotation;
        SecmeAlaniBoyut = new Vector3(1.5f * dizilim.X + 2, 10, 1.5f * dizilim.Z + 2);
        SecmeAlaniKonum = new Vector3(komutan.transform.position.x, komutan.transform.position.y - 25, komutan.transform.position.z);
        KomutanKonum = new Vector3(komutan.transform.position.x, komutan.transform.position.y, komutan.transform.position.z);

        secmeAlani.transform.localScale = SecmeAlaniBoyut;
        secmeAlani.transform.position = SecmeAlaniKonum;

        Yon = new Quaternion(0, -komutanYedek.transform.rotation.y, 0, 0);

        ParentDegistir(false, secmeAlani, askerler);

        komutanYedek.transform.position = KomutanKonum;

        ParentDegistir(true, secmeAlani, askerler);

        secmeAlani.transform.rotation = Yon;

        ParentDegistir(false, secmeAlani, askerler);

        Yon = new Quaternion(0, 0, 0, 0);
        komutanYedek.transform.rotation = Yon;

        ParentDegistir(true, secmeAlani, askerler);

        int b = 0;
        int a = 0;

        for (int i = dizilim.Z; i > 0; i--)
        {
            for (int j = dizilim.X; j > 0; j--)
            {
                if (dizilim.Detays[a].Deger == 1)
                {
                    Vector3 PivotKonumArtacak = new Vector3((j * 1.5f) + (komutan.transform.position.x) - ((dizilim.X / 2) * 1.5f), komutan.transform.position.y, (i * 1.5f) + (komutan.transform.position.z) - ((1.5f * dizilim.Z + 2) / 2));
                    if (askerler.Count <= b)
                    {
                        break;
                    }
                    askerler[b].transform.rotation = Yon;
                    askerler[b].transform.position = PivotKonumArtacak;
                    b++;
                }
                a++;
            }
            if (askerler.Count <= b)
            {
                break;
            }
        }
        komutan.transform.rotation = yon;
        f1Tiklanmismi = false;
    }
    public void PivotVeSecmeAlaniAl()
    {
        erisim = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        askerler = new List<GameObject>();
        KomutaniAl = GameObject.FindWithTag("Komutan").GetComponent<OrduHareket>();
        Asker = GameObject.Find(erisim.hareketEdecek.name).GetComponent<ParentTakip>();
        if (erisim.hareketEdecek.tag == "Askerler1")
        {
            hareketEdecekObjeAsker = Asker.target.gameObject;
        }

        if (erisim.hareketEdecek != null && (erisim.hareketEdecek.tag == "SecmeAlani" || erisim.hareketEdecek.tag == "Askerler1"))
        {
            var a = 0;
            if (erisim.hareketEdecek.tag == "SecmeAlani")
            {
                hareketEdecekObjeEbeveyni = erisim.hareketEdecek.transform.parent.gameObject;
            }
            else if (erisim.hareketEdecek.tag == "Askerler1")
            {
                hareketEdecekObjeEbeveyni = hareketEdecekObjeAsker.transform.parent.gameObject;
            }
            ListeyeAl(hareketEdecekObjeEbeveyni);
        }
    }
    public void ParentDegistir(bool açKapa, GameObject obje, List<GameObject> askerler)
    {
        if (açKapa == true)
        {
            obje.transform.parent = ebeveynYedek.transform;
            foreach (var item in askerler)
            {
                item.transform.parent = ebeveynYedek.transform;
            }
        }
        else
        {
            obje.transform.parent = null;
            foreach (var item in askerler)
            {
                item.transform.parent = null;
            }
        }
    }
    public void ListeyeAl(GameObject obje)
    {
        var a = 0;
        for (int i = 0; i < obje.transform.childCount; i++)
        {
            if (obje.transform.GetChild(i).gameObject.tag == "SecmeAlani")
            {
                secmeAlani = obje.transform.GetChild(i).gameObject; //Sçme alanını aldım.
                a = i;
                break;
            }

        }
        for (int i = 0; i < obje.transform.childCount; i++)
        {
            if (i != a)
            {
                askerler.Add(obje.transform.GetChild(i).gameObject); // askerleri liseteye attık
            }
        }
    }
    public Dizilim dizilim1()
    {
        dizilim = new Dizilim();
        dizilim.X = 10;
        dizilim.Z = 10;
        dizilim.Adi = "Klasik Dizilim";
        dizilim.Detays = new List<Detay>();

        for (int i = 0; i < dizilim.X; i++)
        {
            for (int j = 0; j < dizilim.Z; j++)
            {
                var detay = new Detay();
                detay.X = i;
                detay.Z = j;
                detay.Deger = 1;
                dizilim.Detays.Add(detay);
            }
        }
        return dizilim;
    }
    public Dizilim dizilim2()
    {
        dizilim = new Dizilim();
        dizilim.X = 20;
        dizilim.Z = 5;
        dizilim.Adi = "Geniş Dizilim";
        dizilim.Detays = new List<Detay>();

        for (int i = 0; i < dizilim.X; i++)
        {
            for (int j = 0; j < dizilim.Z; j++)
            {
                var detay = new Detay();
                detay.X = i;
                detay.Z = j;
                detay.Deger = 1;
                dizilim.Detays.Add(detay);
            }
        }
        return dizilim;
    }
    public Dizilim dizilim3()
    {
        dizilim = new Dizilim();
        dizilim.X = 19;
        dizilim.Z = 10;
        dizilim.Adi = "Klasik Dizilim";
        dizilim.Detays = new List<Detay>();
        var a = 1;

        for (int i = 0; i < dizilim.Z; i++)
        {

            var b = (dizilim.X - a) / 2;
            for (int j = 0; j < b; j++)
            {
                var detay = new Detay();
                detay.Deger = 0;
                dizilim.Detays.Add(detay);
            }
            for (int j = 0; j < a; j++)
            {
                var detay = new Detay();
                detay.Deger = 1;
                dizilim.Detays.Add(detay);
            }
            for (int j = 0; j < b; j++)
            {
                var detay = new Detay();
                detay.Deger = 0;
                dizilim.Detays.Add(detay);
            }
            a = a + 2;
        }
        return dizilim;
    }
}
