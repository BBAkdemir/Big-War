using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolumOlustur : MonoBehaviour
{
    public General general;
    public Birlik birlik;
    BirlikTuru birlikTuru;
    public Durum durum;
    public BolumTasarimi bolumTasarimi;
    private BirlikOzellik birlikOzellik;
    public OrduDiziliminiDüzenle dizilimler;

    public List<General> Generaller;
    public List<Ordu> Ordular;
    public List<BirlikTuru> BirlikTurleri;
    public List<Birlik> Birlikler;
    public List<Durum> durumlar;

    public Terrain Terrain;
    private Vector3 TerrainKonum;

    public Transform KomutaninBirligiDogmaNoktasi1;
    public Transform KomutaninBirligiDogmaNoktasi2;
    public Transform KilicliPiyadeDogmaNoktasi1;
    public Transform KilicliPiyadeDogmaNoktasi2;
    public Transform MizrakliPiyadeDogmaNoktasi1;
    public Transform MizrakliPiyadeDogmaNoktasi2;
    public Transform MizrakliAtliDogmaNoktasi1;
    public Transform MizrakliAtliDogmaNoktasi2;
    public Transform AtliOkcuDogmaNoktasi1;
    public Transform AtliOkcuDogmaNoktasi2;
    public Transform OkcuPiyadeDogmaNoktasi1;
    public Transform OkcuPiyadeDogmaNoktasi2;

    public GameObject Komutan;
    public GameObject Asker;
    public GameObject Pivot;
    public GameObject SecmeAlani;
    private Transform takipEdilecek;
    private ParentTakip takipEdilecekObje;

    public int KiliçliPiyadeSayiTaraf1;
    public int MizrakliPiyadeSayiTaraf1;
    public int MizrakliAtliSayiTaraf1;
    public int AtliOkcuSayiTaraf1;
    public int OkcuPiyadeSayiTaraf1;
    public string komutanTaraf1;
    public int KiliçliPiyadeSayiTaraf2;
    public int MizrakliPiyadeSayiTaraf2;
    public int MizrakliAtliSayiTaraf2;
    public int AtliOkcuSayiTaraf2;
    public int OkcuPiyadeSayiTaraf2;
    public string komutanTaraf2;

    private int artir1 = 0;
    private int artir2 = 0;
    private int artir3 = 0;
    private int artir4 = 0;
    private int artir5 = 0;
    private int artir6 = 0;
    private int a = 0;

    GameObject newKomutan;
    public List<GameObject> AskerlerListesi;
    public List<GameObject> AskerlerListesiYedek;

    public Material Taraf1;
    public Material Taraf2;
    void Start()
    {
        #region Generalleri Oluşturma
        General general;
        Generaller = new List<General>();
        general = GeneralDondur(0001, "Berk", 10, 10, 10, 10);
        Generaller.Add(general);
        general = GeneralDondur(0001, "Mert", 8, 8, 8, 8);
        Generaller.Add(general);
        #endregion

        #region Birlik Türü Oluşturma
        BirlikTuru birlikTuru;
        BirlikTurleri = new List<BirlikTuru>();
        birlikTuru = BirlikTuruDondur(AskerTur.KomutaninBirligi, 200);
        BirlikTurleri.Add(birlikTuru);
        birlikTuru = BirlikTuruDondur(AskerTur.KilicliPiyade, 0);
        BirlikTurleri.Add(birlikTuru);
        birlikTuru = BirlikTuruDondur(AskerTur.MizrakliPiyade, 0);
        BirlikTurleri.Add(birlikTuru);
        birlikTuru = BirlikTuruDondur(AskerTur.MizrakliAtli, 50);
        BirlikTurleri.Add(birlikTuru);
        birlikTuru = BirlikTuruDondur(AskerTur.AtliOkcu, 200);
        BirlikTurleri.Add(birlikTuru);
        birlikTuru = BirlikTuruDondur(AskerTur.OkcuPiyade, 200);
        BirlikTurleri.Add(birlikTuru);
        #endregion

        #region Birlik Oluşturma
        birlikTuru = new BirlikTuru();
        Birlikler = new List<Birlik>();
        Birlik birlik;
        dizilimler = new OrduDiziliminiDüzenle();

        birlik = BirlikDondur(0001, "Komutanın Birliği", TurBelirle(AskerTur.KomutaninBirligi), 8, 5, 5, 4, 100, 100, 5, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);

        birlik = BirlikDondur(0002, "Kılıçlı Piyade Birlik", TurBelirle(AskerTur.KilicliPiyade), 5, 5, 2, 2, 100, 100, 3, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);

        birlik = BirlikDondur(0003, "Mızraklı Piyade Birlik", TurBelirle(AskerTur.MizrakliPiyade), 5, 5, 3, 2, 100, 100, 3, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);

        birlik = BirlikDondur(0004, "Mızraklı Atlı Birlik", TurBelirle(AskerTur.MizrakliAtli), 7, 4, 4, 4, 64, 100, 3, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);

        birlik = BirlikDondur(0005, "Atlı Okçu Birlik", TurBelirle(AskerTur.AtliOkcu), 3, 3, 1, 4, 64, 100, 3, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);

        birlik = BirlikDondur(0006, "Okçu Piyade Birlik", TurBelirle(AskerTur.OkcuPiyade), 3, 3, 1, 2, 100, 100, 3, dizilimler.dizilimListesi[0]);
        Birlikler.Add(birlik);
        #endregion

        #region Ordu Oluşturma
        general = new General();
        Ordular = new List<Ordu>();
        Ordu ordu;

        KiliçliPiyadeSayiTaraf1 = 1;
        MizrakliPiyadeSayiTaraf1 = 2;
        MizrakliAtliSayiTaraf1 = 3;
        AtliOkcuSayiTaraf1 = 1;
        OkcuPiyadeSayiTaraf1 = 2;
        komutanTaraf1 = "Berk";
        KiliçliPiyadeSayiTaraf2 = 1;
        MizrakliPiyadeSayiTaraf2 = 2;
        MizrakliAtliSayiTaraf2 = 3;
        AtliOkcuSayiTaraf2 = 1;
        OkcuPiyadeSayiTaraf2 = 2;
        komutanTaraf2 = "Mert";
        
        ordu = OrduDondur(GeneralBelirle(komutanTaraf1), birlikDoldur(KiliçliPiyadeSayiTaraf1, MizrakliPiyadeSayiTaraf1, MizrakliAtliSayiTaraf1, AtliOkcuSayiTaraf1, OkcuPiyadeSayiTaraf1));
        Ordular.Add(ordu);;
       
        ordu = OrduDondur(GeneralBelirle(komutanTaraf2), birlikDoldur(KiliçliPiyadeSayiTaraf2, MizrakliPiyadeSayiTaraf2, MizrakliAtliSayiTaraf2, AtliOkcuSayiTaraf2, OkcuPiyadeSayiTaraf2));
        Ordular.Add(ordu);
        #endregion

        #region Ordu Taraflarını Oluşturma
        Durum durum;
        durumlar = new List<Durum>();
        durum = durumDondur(Ordular[0], 1);
        durumlar.Add(durum);
        durum = durumDondur(Ordular[1], 2);
        durumlar.Add(durum);
        # endregion

        #region Bölüm Tasarımı Oluşturma
        BolumTasarimi bolumTasarimi;
        bolumTasarimi = BolumTasarimiDondur(Ordular, durumlar, 150);
        #endregion

        #region Orduları Getir
        foreach (var Ordu in bolumTasarimi.Ordu)
        {
            artir1 = 0;
            artir2 = 0;
            artir3 = 0;
            artir4 = 0;
            artir5 = 0;
            artir6 = 0;
            foreach (var Birlik in Ordu.Birlikler)
            {
                if (Birlik.BirlikTuru.Adi == AskerTur.KomutaninBirligi && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(KomutaninBirligiDogmaNoktasi1.transform.position, artir1, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir1 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.KomutaninBirligi && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(KomutaninBirligiDogmaNoktasi2.transform.position, artir1, Birlik.Sayi, 2);

                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir1 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.KilicliPiyade && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(KilicliPiyadeDogmaNoktasi1.transform.position, artir2, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir2 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.KilicliPiyade && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(KilicliPiyadeDogmaNoktasi2.transform.position, artir2, Birlik.Sayi, 2);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir2 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.MizrakliPiyade && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(MizrakliPiyadeDogmaNoktasi1.transform.position, artir3, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir3 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.MizrakliPiyade && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(MizrakliPiyadeDogmaNoktasi2.transform.position, artir3, Birlik.Sayi, 2);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir3 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.MizrakliAtli && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(MizrakliAtliDogmaNoktasi1.transform.position, artir4, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir4 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.MizrakliAtli && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(MizrakliAtliDogmaNoktasi2.transform.position, artir4, Birlik.Sayi, 2);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir4 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.AtliOkcu && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(AtliOkcuDogmaNoktasi1.transform.position, artir5, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir5 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.AtliOkcu && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(AtliOkcuDogmaNoktasi2.transform.position, artir5, Birlik.Sayi, 2);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir5 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.OkcuPiyade && Ordu.General.Ad == "Berk")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(OkcuPiyadeDogmaNoktasi1.transform.position, artir6, Birlik.Sayi, 1);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 1, AskerlerListesiYedek);
                    artir6 += 1;
                    a += 1;
                }
                if (Birlik.BirlikTuru.Adi == AskerTur.OkcuPiyade && Ordu.General.Ad == "Mert")
                {
                    AskerlerListesiYedek = new List<GameObject>();
                    AskerlerListesiYedek = OrduyuOlustur(OkcuPiyadeDogmaNoktasi2.transform.position, artir6, Birlik.Sayi, 2);
                    BirlikOzellikAt(Birlik.Id, Birlik.Ad, Birlik.Saldiri, Birlik.Savunma, Birlik.Sok, Birlik.Hiz, Birlik.Sayi, Birlik.AskerCan, Birlik.Moral, GucHesapla(Birlik), Birlik.BirlikTuru.Adi, Ordu.General.Ad, Birlik.AktifDizilim, 2, AskerlerListesiYedek);
                    artir6 += 1;
                    a += 1;
                }
            }
        }
        #endregion
    }
    public void BirlikOzellikAt(int Id, string Ad, float Saldiri, float Savunma, float Sok, float Hiz, int Sayi, float AskerCan, float Moral, float Guc, AskerTur BirlikTuru, string GeneralAd, Dizilim AktifDizilim, int taraf, List<GameObject> Askerler)
    {
        birlikOzellik = GameObject.Find(newKomutan.name).GetComponent<BirlikOzellik>();
        birlikOzellik.Id = Id;
        birlikOzellik.Ad = Ad;
        birlikOzellik.Saldiri = Saldiri;
        birlikOzellik.Savunma = Savunma;
        birlikOzellik.Sok = Sok;
        birlikOzellik.Hiz = Hiz;
        birlikOzellik.Sayi = Sayi;
        birlikOzellik.AskerCan = AskerCan;
        birlikOzellik.Moral = Moral;
        birlikOzellik.Guc = Guc;
        birlikOzellik.BirlikTuru = BirlikTuru;
        birlikOzellik.GeneralAd = GeneralAd;
        birlikOzellik.AktifDizilim = AktifDizilim;
        birlikOzellik.Taraf = taraf;
        birlikOzellik.Askerler = Askerler;
    }
    public List<Birlik> birlikDoldur(int KiliçliPiyadeSayi, int MizrakliPiyadeSayi, int MizrakliAtliSayi, int AtliOkcuSayi, int OkcuPiyadeSayi)
    {
        var birlikler = new List<Birlik>();
        foreach (var item in Birlikler)
        {
            if (item.Ad == "Komutanın Birliği")
            {
                birlikler.Add(item);
            }
            if (item.Ad == "Kılıçlı Piyade Birlik")
            {
                for (int i = 0; i < KiliçliPiyadeSayi; i++)
                {
                    birlikler.Add(item);
                }
            }
            if (item.Ad == "Mızraklı Piyade Birlik")
            {
                for (int i = 0; i < MizrakliPiyadeSayi; i++)
                {
                    birlikler.Add(item);
                }
            }
            if (item.Ad == "Mızraklı Atlı Birlik")
            {
                for (int i = 0; i < MizrakliAtliSayi; i++)
                {
                    birlikler.Add(item);
                }
            }
            if (item.Ad == "Atlı Okçu Birlik")
            {
                for (int i = 0; i < AtliOkcuSayi; i++)
                {
                    birlikler.Add(item);
                }
            }
            if (item.Ad == "Okçu Piyade Birlik")
            {
                for (int i = 0; i < OkcuPiyadeSayi; i++)
                {
                    birlikler.Add(item);
                }
            }
        }
        return birlikler;
    }
    public General GeneralDondur(int Id, string Ad, int EkSaldiri, int EkSavunma, int EkSok, int EkMoral)
    {
        var general = new General();
        general.Id = Id;
        general.Ad = Ad;
        general.EkSaldiri = EkSaldiri;
        general.EkSavunma = EkSavunma;
        general.EkSok = EkSok;
        general.EkMoral = EkMoral;

        return general;
    }
    public BirlikTuru BirlikTuruDondur(AskerTur Ad, int Deger)
    {
        var birlikTuru = new BirlikTuru();
        birlikTuru.Adi = Ad;
        birlikTuru.Deger = Deger;
        return birlikTuru;
    }
    public Birlik BirlikDondur(int Id, string Ad, BirlikTuru BirlikTuru, int Saldiri, int Savunma, int Sok, int Hiz, int Sayi, int AskerCan, int Moral, Dizilim AktifDizilim)
    {
        var birlik = new Birlik();

        birlik.Id = Id;
        birlik.Ad = Ad;
        birlik.BirlikTuru = BirlikTuru;
        birlik.Saldiri = Saldiri;
        birlik.Savunma = Savunma;
        birlik.Sok = Sok;
        birlik.Hiz = Hiz;
        birlik.Sayi = Sayi;
        birlik.AskerCan = AskerCan;
        birlik.Moral = Moral;
        birlik.AktifDizilim = AktifDizilim;

        return birlik;
    }
    public Ordu OrduDondur(General General, List<Birlik> Birlikler)
    {
        var ordu = new Ordu();
        ordu.General = General;
        ordu.Birlikler = Birlikler;

        return ordu;
    }
    public Durum durumDondur(Ordu Ordu, int Taraf)
    {
        var durum = new Durum();
        durum.Ordu = Ordu;
        durum.Taraf = Taraf;

        return durum;
    }
    public BolumTasarimi BolumTasarimiDondur(List<Ordu> Ordu, List<Durum> Durum, int BolumPuani)
    {
        var bolumTasarimi = new BolumTasarimi();
        bolumTasarimi.Ordu = Ordu;
        bolumTasarimi.Durum = Durum;
        bolumTasarimi.BolumPuani = BolumPuani;
        return bolumTasarimi;
    }
    public BirlikTuru TurBelirle(AskerTur TurIsmi)
    {
        birlikTuru = new BirlikTuru();
        foreach (var item in BirlikTurleri)
        {
            if (item.Adi == TurIsmi)
            {
                birlikTuru = item;
            }
        }
        return birlikTuru;
    }
    public General GeneralBelirle(string generalIsmi)
    {
        foreach (var item in Generaller)
        {
            if (item.Ad == generalIsmi)
            {
                general = item;
            }
        }
        return general;
    }
    private float GucHesapla(Birlik birlik)
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
    public List<GameObject> OrduyuOlustur(Vector3 DogmaNoktasi, int artir, int Sayi, int taraf)
    {
        Vector3 KomutanKonum;
        Quaternion KomutanYon;
        Vector3 SecmeAlaniKonum;
        Quaternion SecmeAlaniYon;
        Vector3 saldiriNoktasiKonum;
        AskerlerListesi = new List<GameObject>();
        GameObject newSecmeAlani;
        GameObject newSaldiriNoktasi;
        double Karekok = Math.Sqrt(Sayi);
        int k = 7;
        int l = 1;
        int m = 5;
        int n = 1;
        int o = 6;
        int p = 1;

        Vector3 pos = new Vector3(DogmaNoktasi.x + artir * 25, 0, DogmaNoktasi.z);

        KomutanKonum = new Vector3(pos.x, Terrain.activeTerrain.SampleHeight(pos), pos.z);
        KomutanYon = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        SecmeAlaniKonum = new Vector3(DogmaNoktasi.x + artir * 25, 10.0f , DogmaNoktasi.z + 6.5f);
        SecmeAlaniYon = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        newKomutan = Instantiate(Komutan, KomutanKonum, KomutanYon);
        newKomutan.name = "Komutan" + a;

        newSecmeAlani = Instantiate(SecmeAlani, SecmeAlaniKonum, SecmeAlaniYon);
        newSecmeAlani.name = "SecmeAlani" + a;
        newSecmeAlani.transform.parent = newKomutan.transform;

        for (int i = 0; i < Karekok; i++)
        {
            for (int j = 1; j < (Karekok / 2); j++)
            {

                Vector3 PivotKonumArtacak = new Vector3(j * 1.5f, 0.0f, i * 1.5f);
                GameObject newPivot = Instantiate(Pivot, KomutanKonum + PivotKonumArtacak, KomutanYon);
                newPivot.name = "Pivot" + a + k + l;
                GameObject newAsker = Instantiate(Asker, KomutanKonum + PivotKonumArtacak, KomutanYon);
                newAsker.name = "Asker" + a + k + l;

                if (taraf == 1)
                {
                    MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                    renk.material = Taraf1;
                }
                else
                {
                    MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                    renk.material = Taraf2;
                }

                newPivot.transform.parent = newKomutan.transform;
                takipEdilecekObje = newAsker.GetComponent<ParentTakip>();
                takipEdilecekObje.target = newPivot.transform;
                AskerlerListesi.Add(newAsker);
                k += 1;

            }
            l += 1;
            k = 7;
        }
        for (int i = 1; i <= (Karekok / 2); i++)
        {
            for (int j = 0; j < Karekok; j++)
            {
                Vector3 PivotKonumArtacak = new Vector3(-i * 1.5f, 0.0f, j * 1.5f);
                GameObject newPivot = Instantiate(Pivot, KomutanKonum + PivotKonumArtacak, KomutanYon);
                newPivot.name = "Pivot" + a + m + n;
                GameObject newAsker = Instantiate(Asker, KomutanKonum + PivotKonumArtacak, KomutanYon);
                newAsker.name = "Asker" + a + m + n;
                if (taraf == 1)
                {
                    MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                    renk.material = Taraf1;
                }
                else
                {
                    MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                    renk.material = Taraf2;
                }
                newPivot.transform.parent = newKomutan.transform;
                takipEdilecekObje = newAsker.GetComponent<ParentTakip>();
                takipEdilecekObje.target = newPivot.transform;
                AskerlerListesi.Add(newAsker);
                n += 1;
            }
            m -= 1;
            n = 1;
        }
        for (int i = 0; i < Karekok; i++)
        {
            Vector3 PivotKonumArtacak = new Vector3(0, 0.0f, i * 1.5f);
            GameObject newPivot = Instantiate(Pivot, KomutanKonum + PivotKonumArtacak, KomutanYon);
            newPivot.name = "Pivot" + a + o + p;
            
            GameObject newAsker = Instantiate(Asker, KomutanKonum + PivotKonumArtacak, KomutanYon);
            newAsker.name = "Asker" + a + o + p;

            if (taraf == 1)
            {
                MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                renk.material = Taraf1;
            }
            else
            {
                MeshRenderer renk = GameObject.Find(newAsker.name).GetComponent<MeshRenderer>();
                renk.material = Taraf2;
            }
            newPivot.transform.parent = newKomutan.transform;
            takipEdilecekObje = newAsker.GetComponent<ParentTakip>();
            takipEdilecekObje.target = newPivot.transform;
            AskerlerListesi.Add(newAsker);
            p += 1;
        }
        return AskerlerListesi;
    }
}
