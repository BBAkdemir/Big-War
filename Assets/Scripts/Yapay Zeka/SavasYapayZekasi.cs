using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavasYapayZekasi : MonoBehaviour
{
    public BolumOlustur Ordular;
    public BirlikOzellik Birligimiz;

    public List<AskerTur> MizrakliAtliSaldiri;
    public List<AskerTur> MizrakliAtliSavunma;
    public List<AskerTur> AtliOkcuSaldiri;
    public List<AskerTur> AtliOkcuSavunma;
    public List<AskerTur> KilicliPiyadeSaldiri;
    public List<AskerTur> KilicliPiyadeSavunma;
    public List<AskerTur> MizrakliPiyadeSaldiri;
    public List<AskerTur> MizrakliPiyadeSavunma;
    public List<AskerTur> OkcuSaldiri;
    public List<AskerTur> OkcuSavunma;
    public List<YapayZekaSaldiriOncelik> TumDusmanlar;

    private BirlikOzellik DusmanMi;
    Collider[] hitCollider;

    private List<YapayZekaSaldiriOncelik> EtraftakiDusmanlar;
    public List<YapayZekaSaldiriOncelik> EtraftakiDostlar;
    private BirlikOzellik Saldirilacak;
    private BirlikOzellik SaldiriOnceligiHangisi;
    private BirlikOzellik KendiGucu;
    private BirlikOzellik Destek;
    private BirlikOzellik icIceGiren;

    private BirlikOzellik Kacilacak;
    private bool baskaDusmanBul = false;

    public int rastgele1 = 0;
    public int rastgele2 = 0;

    void Start()
    {
        Ordular = GameObject.Find("GameDesign").GetComponent<BolumOlustur>();
        Birligimiz = transform.gameObject.GetComponent<BirlikOzellik>();
        //if (Birligimiz.Taraf == 1)
        //{
        //    this.enabled = false;
        //}


        MizrakliAtliSaldiri.Add(AskerTur.OkcuPiyade);
        MizrakliAtliSaldiri.Add(AskerTur.KilicliPiyade);
        MizrakliAtliSaldiri.Add(AskerTur.AtliOkcu);
        MizrakliAtliSaldiri.Add(AskerTur.MizrakliPiyade);
        MizrakliAtliSaldiri.Add(AskerTur.MizrakliAtli);

        MizrakliAtliSavunma.Add(AskerTur.OkcuPiyade);
        MizrakliAtliSavunma.Add(AskerTur.KilicliPiyade);
        MizrakliAtliSavunma.Add(AskerTur.AtliOkcu);
        MizrakliAtliSavunma.Add(AskerTur.MizrakliPiyade);
        MizrakliAtliSavunma.Add(AskerTur.MizrakliAtli);

        AtliOkcuSaldiri.Add(AskerTur.KilicliPiyade);
        AtliOkcuSaldiri.Add(AskerTur.MizrakliPiyade);
        AtliOkcuSaldiri.Add(AskerTur.MizrakliAtli);
        AtliOkcuSaldiri.Add(AskerTur.OkcuPiyade);
        AtliOkcuSaldiri.Add(AskerTur.AtliOkcu);

        AtliOkcuSavunma.Add(AskerTur.KilicliPiyade);
        AtliOkcuSavunma.Add(AskerTur.MizrakliPiyade);
        AtliOkcuSavunma.Add(AskerTur.MizrakliAtli);
        AtliOkcuSavunma.Add(AskerTur.OkcuPiyade);
        AtliOkcuSavunma.Add(AskerTur.AtliOkcu);

        KilicliPiyadeSaldiri.Add(AskerTur.KilicliPiyade);
        KilicliPiyadeSaldiri.Add(AskerTur.MizrakliPiyade);
        KilicliPiyadeSaldiri.Add(AskerTur.OkcuPiyade);
        KilicliPiyadeSaldiri.Add(AskerTur.MizrakliAtli);
        KilicliPiyadeSaldiri.Add(AskerTur.AtliOkcu);

        KilicliPiyadeSavunma.Add(AskerTur.MizrakliPiyade);
        KilicliPiyadeSavunma.Add(AskerTur.KilicliPiyade);
        KilicliPiyadeSavunma.Add(AskerTur.MizrakliAtli);
        KilicliPiyadeSavunma.Add(AskerTur.OkcuPiyade);
        KilicliPiyadeSavunma.Add(AskerTur.AtliOkcu);

        MizrakliPiyadeSaldiri.Add(AskerTur.MizrakliAtli);
        MizrakliPiyadeSaldiri.Add(AskerTur.AtliOkcu);
        MizrakliPiyadeSaldiri.Add(AskerTur.MizrakliPiyade);
        MizrakliPiyadeSaldiri.Add(AskerTur.KilicliPiyade);
        MizrakliPiyadeSaldiri.Add(AskerTur.OkcuPiyade);

        MizrakliPiyadeSavunma.Add(AskerTur.MizrakliAtli);
        MizrakliPiyadeSavunma.Add(AskerTur.AtliOkcu);
        MizrakliPiyadeSavunma.Add(AskerTur.KilicliPiyade);
        MizrakliPiyadeSavunma.Add(AskerTur.MizrakliPiyade);
        MizrakliPiyadeSavunma.Add(AskerTur.OkcuPiyade);

        OkcuSaldiri.Add(AskerTur.KilicliPiyade);
        OkcuSaldiri.Add(AskerTur.MizrakliPiyade);
        OkcuSaldiri.Add(AskerTur.MizrakliAtli);
        OkcuSaldiri.Add(AskerTur.AtliOkcu);
        OkcuSaldiri.Add(AskerTur.OkcuPiyade);

        OkcuSavunma.Add(AskerTur.KilicliPiyade);
        OkcuSavunma.Add(AskerTur.MizrakliPiyade);
        OkcuSavunma.Add(AskerTur.MizrakliAtli);
        OkcuSavunma.Add(AskerTur.AtliOkcu);
        OkcuSavunma.Add(AskerTur.OkcuPiyade);

        TumDusmanlar = new List<YapayZekaSaldiriOncelik>();
        EtraftakiDostlar = new List<YapayZekaSaldiriOncelik>();
    }
    private List<YapayZekaSaldiriOncelik> AlaniTaraSaldiri(float TespitMesafesi)
    {
        TumDusmanlar = new List<YapayZekaSaldiriOncelik>();
        hitCollider = Physics.OverlapSphere(transform.position, TespitMesafesi);
        foreach (var item in hitCollider)
        {
            if (item.gameObject.tag == "Komutan")
            {
                DusmanMi = item.gameObject.GetComponent<BirlikOzellik>();
                if (DusmanMi.Taraf != Birligimiz.Taraf)
                {
                    TumDusmanlar.Add(YapayZekaSaldiriOncelikDoldur(item.gameObject, false));
                }
            }
        }
        return TumDusmanlar;
    }
    private List<YapayZekaSaldiriOncelik> AlaniTaraKacis(float TespitMesafesi)
    {
        EtraftakiDusmanlar = new List<YapayZekaSaldiriOncelik>();
        EtraftakiDostlar = new List<YapayZekaSaldiriOncelik>();
        hitCollider = Physics.OverlapSphere(transform.position, TespitMesafesi);
        foreach (var item in hitCollider)
        {
            if (item.gameObject.tag == "Komutan")
            {
                DusmanMi = item.gameObject.GetComponent<BirlikOzellik>();
                if (DusmanMi.Taraf != Birligimiz.Taraf)
                {
                    EtraftakiDusmanlar.Add(YapayZekaSaldiriOncelikDoldur(item.gameObject, false));
                }
                if (DusmanMi.Taraf == Birligimiz.Taraf)
                {
                    EtraftakiDostlar.Add(YapayZekaSaldiriOncelikDoldur(item.gameObject, false));
                }
            }
        }
        return EtraftakiDusmanlar;
    }
    /*private void SaldiriSavunma(List<YapayZekaSaldiriOncelik> TumDusmanlar, List<AskerTur> SaldiriOnceligi, AskerTur askerTur, List<YapayZekaSaldiriOncelik> EtraftakiDusmanlar, List<AskerTur> SavunmaOnceligi)
    {
        bool a = false;
        SaldiriOnceligiHangisi = transform.gameObject.GetComponent<BirlikOzellik>();
        if (SaldiriOnceligiHangisi.BirlikTuru == askerTur)
        {
            for (int i = 0; i < SaldiriOnceligi.Count; i++)
            {
                foreach (var item in TumDusmanlar)
                {
                    Saldirilacak = item.Asker.GetComponent<BirlikOzellik>();
                    KendiGucu = transform.gameObject.GetComponent<BirlikOzellik>();
                    if (Saldirilacak.BirlikTuru == SaldiriOnceligi[i] && (item.Saldiri == false || Saldirilacak.Dusman == transform.gameObject))// && (Saldirilacak.Guc - 3f) <= KendiGucu.Guc)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, item.Asker.transform.position, KendiGucu.Hiz * Time.deltaTime);
                        transform.LookAt(item.Asker.transform.position);
                        Debug.DrawLine(transform.position, item.Asker.transform.position, Color.blue);
                        Saldirilacak.Dusman = transform.gameObject;
                        item.Saldiri = true;
                        a = true;
                        break;
                    }
                    else
                    {
                        baskaDusmanBul = true;
                    }
                }
                foreach (var item in EtraftakiDusmanlar)
                {
                    Kacilacak = item.Asker.GetComponent<BirlikOzellik>();

                    foreach (var item2 in EtraftakiDostlar)
                    {
                        Destek = item2.Asker.GetComponent<BirlikOzellik>();
                        if (Destek.Dusman == Kacilacak)// && (Destek.Guc + Birligimiz.Guc) >= Kacilacak.Guc)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, item.Asker.transform.position, KendiGucu.Hiz * Time.deltaTime);
                            transform.LookAt(item.Asker.transform.position);
                            Debug.DrawLine(transform.position, item.Asker.transform.position, Color.blue);
                            Birligimiz.Dusman = item.Asker;
                            item.Saldiri = true;
                            a = true;
                            break;
                        }
                        else
                        {
                            item2.Asker.transform.position = Vector3.MoveTowards(transform.position, item.Asker.transform.position, KendiGucu.Hiz * Time.deltaTime);
                            item2.Asker.transform.LookAt(item.Asker.transform.position);
                            Debug.DrawLine(item2.Asker.transform.position, item.Asker.transform.position, Color.blue);
                            Destek.Dusman = item.Asker;
                            item.Saldiri = true;
                            a = true;
                            break;
                        }
                    }
                    if (a == true)
                        break;
                    if (Kacilacak.BirlikTuru != Saldirilacak.BirlikTuru && (item.Saldiri == false || Kacilacak.Dusman == transform.gameObject))
                    {
                        if (Kacilacak.transform.position.z < transform.position.z)
                        {
                            var gidilecekKonum = new Vector3((transform.position.x + (150 - Mathf.Abs(transform.position.x - Kacilacak.transform.position.x))), transform.position.y, (transform.position.z + (150 - Mathf.Abs(transform.position.z - Kacilacak.transform.position.z))));
                            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, KendiGucu.Hiz * Time.deltaTime);
                            transform.LookAt(gidilecekKonum);
                            Debug.DrawLine(transform.position, gidilecekKonum, Color.red);

                            KacisYonuBul();

                            a = false;
                            break;
                        }
                        if (Kacilacak.transform.position.z > transform.position.z)
                        {
                            var gidilecekKonum = new Vector3((transform.position.x - (150 - Mathf.Abs(transform.position.x - Kacilacak.transform.position.x))), transform.position.y, (transform.position.z - (150 - Mathf.Abs(transform.position.z - Kacilacak.transform.position.z))));
                            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, KendiGucu.Hiz * Time.deltaTime);
                            transform.LookAt(gidilecekKonum);
                            Debug.DrawLine(transform.position, gidilecekKonum, Color.red);

                            KacisYonuBul();

                            a = false;
                            break;
                        }
                    }
                    if (a == true)
                        break;
                }
                if (a == true)
                    break;
            }
        }
    }*/
    private YapayZekaSaldiriOncelik YapayZekaSaldiriOncelikDoldur(GameObject asker, bool saldiri)
    {
        var yapayZekaSaldiriOncelik = new YapayZekaSaldiriOncelik();
        yapayZekaSaldiriOncelik.Asker = asker;
        yapayZekaSaldiriOncelik.Saldiri = saldiri;
        return yapayZekaSaldiriOncelik;
    }
    private void KacisYonuBul()
    {
        if (transform.position.x >= ((Ordular.Terrain.terrainData.size.x / 2) - 50))
        {
            var gidilecekKonum = new Vector3(transform.position.x - rastgele2, transform.position.y, transform.position.z + (-transform.position.z / 10));
            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecekKonum);
            Debug.DrawLine(transform.position, gidilecekKonum, Color.green);
        }
        else if (transform.position.x <= -((Ordular.Terrain.terrainData.size.x / 2) - 50))
        {
            var gidilecekKonum = new Vector3(transform.position.x + rastgele2, transform.position.y, transform.position.z + (-transform.position.z / 10));
            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecekKonum);
            Debug.DrawLine(transform.position, gidilecekKonum, Color.green);
        }
        else if (transform.position.z >= ((Ordular.Terrain.terrainData.size.z / 2) - 50))
        {
            var gidilecekKonum = new Vector3(transform.position.x, transform.position.y, transform.position.z - rastgele2);
            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecekKonum);
            Debug.DrawLine(transform.position, gidilecekKonum, Color.green);
        }
        else if (transform.position.z <= -((Ordular.Terrain.terrainData.size.z / 2) - 50))
        {
            var gidilecekKonum = new Vector3(transform.position.x, transform.position.y, transform.position.z + rastgele2);
            transform.position = Vector3.MoveTowards(transform.position, gidilecekKonum, Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecekKonum);
            Debug.DrawLine(transform.position, gidilecekKonum, Color.green);
        }
    }


    void Update()
    {
        if (Time.frameCount % 10 == 0) // Her 85 frame'de bir çalışır
        {
            TumDusmanlar = AlaniTaraSaldiri(400);
            EtraftakiDusmanlar = AlaniTaraKacis(150);
            var rnd = new System.Random();
            rastgele1 = rnd.Next(-50, 50);
            var rnd1 = new System.Random();
            rastgele2 = rnd1.Next(50);
        }

        if (TumDusmanlar.Count > 0)
        {
            /*SaldiriSavunma(TumDusmanlar, MizrakliAtliSaldiri, AskerTur.MizrakliAtli, EtraftakiDusmanlar, MizrakliAtliSavunma);
            SaldiriSavunma(TumDusmanlar, AtliOkcuSaldiri, AskerTur.AtliOkcu, EtraftakiDusmanlar, AtliOkcuSavunma);
            SaldiriSavunma(TumDusmanlar, KilicliPiyadeSaldiri, AskerTur.KilicliPiyade, EtraftakiDusmanlar, KilicliPiyadeSavunma);
            SaldiriSavunma(TumDusmanlar, MizrakliPiyadeSaldiri, AskerTur.MizrakliPiyade, EtraftakiDusmanlar, MizrakliPiyadeSavunma);
            SaldiriSavunma(TumDusmanlar, OkcuSaldiri, AskerTur.OkcuPiyade, EtraftakiDusmanlar, OkcuSavunma);*/
        }
        else if (baskaDusmanBul = true)
        {
            var gidilecek = new Vector3(transform.position.x + rastgele1, transform.position.y, transform.position.z + (-transform.position.z / 10));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + rastgele1, transform.position.y, transform.position.z + (-transform.position.z / 10)), Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecek);
            Debug.DrawLine(transform.position, gidilecek, Color.red);

            KacisYonuBul();
        }
        else
        {
            var gidilecek = new Vector3(transform.position.x + rastgele1, transform.position.y, transform.position.z + (-transform.position.z / 10));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + rastgele1, transform.position.y, transform.position.z + (-transform.position.z / 10)), Birligimiz.Hiz * Time.deltaTime);
            transform.LookAt(gidilecek);
            Debug.DrawLine(transform.position, gidilecek, Color.red);

            KacisYonuBul();
        }

    }
}
