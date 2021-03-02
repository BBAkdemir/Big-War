using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaldiriNoktasi : MonoBehaviour
{
    public GameObject saldiriNoktasi;
    public GameObject Komutan;

    private List<GameObject> BizimAskerler;
    private List<GameObject> DusmanAskerler1;
    private List<GameObject> DusmanAskerler2;
    private List<GameObject> DusmanAskerler3;
    private List<GameObject> DusmanAskerler4;
    private GameObject BizimSecmeAlani;
    private GameObject Dusman1SecmeAlani;
    private GameObject Dusman2SecmeAlani;
    private GameObject Dusman3SecmeAlani;
    private GameObject Dusman4SecmeAlani;

    private int BizimAskerSayisi;
    private int Dusman1AskerSayisi;
    private int Dusman2AskerSayisi;
    private int Dusman3AskerSayisi;
    private int Dusman4AskerSayisi;
    private int BizimX;
    private int Dusman1X;
    private int Dusman2X;
    private int Dusman3X;
    private int Dusman4X;
    private int BizimZ;
    private int Dusman1Z;
    private int Dusman2Z;
    private int Dusman3Z;
    private int Dusman4Z;

    public Vector3 BizimReferansKonum;
    public Vector3 Dusman1ReferansKonum;
    public Vector3 Dusman2ReferansKonum;
    public Vector3 Dusman3ReferansKonum;
    public Vector3 Dusman4ReferansKonum;

    public Vector3 saldiriNoktasi1Konum;
    public Vector3 saldiriNoktasi2Konum;
    public Vector3 saldiriNoktasi3Konum;
    public Vector3 saldiriNoktasi4Konum;


    private BirlikOzellik KomutanBirlikOzellik;
    private BirlikOzellik Dusman1BirlikOzellik;
    private BirlikOzellik Dusman2BirlikOzellik;
    private BirlikOzellik Dusman3BirlikOzellik;
    private BirlikOzellik Dusman4BirlikOzellik;

    private Quaternion yon;

    private GameObject SaldiriNoktasi1;
    private GameObject SaldiriNoktasi2;
    private GameObject SaldiriNoktasi3;
    private GameObject SaldiriNoktasi4;

    private SaldiriNoktasi SaldiriNoktasiComponent;

    private Ozellikler bizimOzellikler;
    private Ozellikler Dusman1Ozellikler;
    private Ozellikler Dusman2Ozellikler;
    private Ozellikler Dusman3Ozellikler;
    private Ozellikler Dusman4Ozellikler;

    void Start()
    {
        BizimAskerler = new List<GameObject>();
        DusmanAskerler1 = new List<GameObject>();
        DusmanAskerler2 = new List<GameObject>();
        DusmanAskerler3 = new List<GameObject>();
        DusmanAskerler4 = new List<GameObject>();

        bizimOzellikler = new Ozellikler();
        Dusman1Ozellikler = new Ozellikler();
        Dusman2Ozellikler = new Ozellikler();
        Dusman3Ozellikler = new Ozellikler();
        Dusman4Ozellikler = new Ozellikler();

        Komutan = gameObject;
        KomutanBirlikOzellik = Komutan.GetComponent<BirlikOzellik>();
        SaldiriNoktasiComponent = Komutan.GetComponent<SaldiriNoktasi>();

        Olustur();

        SaldiriNoktasiComponent.SaldiriNoktasi1 = Instantiate(saldiriNoktasi, saldiriNoktasi1Konum, yon);
        SaldiriNoktasiComponent.SaldiriNoktasi1.name = Komutan.name + "SaldiriNoktasi1";
        SaldiriNoktasiComponent.SaldiriNoktasi2 = Instantiate(saldiriNoktasi, saldiriNoktasi2Konum, yon);
        SaldiriNoktasiComponent.SaldiriNoktasi2.name = Komutan.name + "SaldiriNoktasi2";
        SaldiriNoktasiComponent.SaldiriNoktasi3 = Instantiate(saldiriNoktasi, saldiriNoktasi3Konum, yon);
        SaldiriNoktasiComponent.SaldiriNoktasi3.name = Komutan.name + "SaldiriNoktasi3";
        SaldiriNoktasiComponent.SaldiriNoktasi4 = Instantiate(saldiriNoktasi, saldiriNoktasi4Konum, yon);
        SaldiriNoktasiComponent.SaldiriNoktasi4.name = Komutan.name + "SaldiriNoktasi4";
    }

    public void AskerleriAl(GameObject Komutan, GameObject secmeAlani, List<GameObject> askerler)
    {
        var a = 0;
        for (int i = 0; i < Komutan.transform.childCount; i++)
        {
            if (Komutan.transform.GetChild(i).gameObject.tag == "SecmeAlani")
            {
                secmeAlani = Komutan.transform.GetChild(i).gameObject; //Seçme alanýný aldým.
                a = i;
                break;
            }
        }
        for (int i = 0; i < Komutan.transform.childCount; i++)
        {
            if (i != a)
            {
                askerler.Add(Komutan.transform.GetChild(i).gameObject); // askerleri liseteye attýk
            }
        }
    }

    public class Ozellikler
    {
        public GameObject Komutan { get; set; }
        public GameObject secmeAlani { get; set; }
        public int AskerSayisi { get; set; }
        public List<GameObject> askerler { get; set; }
        public BirlikOzellik KomutanBirlikOzellik { get; set; }
        public int x { get; set; }
        public int z { get; set; }
        public Vector3 ReferansKonum { get; set; }
    }

    public Ozellikler KonumlariBul(GameObject Komutan, GameObject secmeAlani, int AskerSayisi, List<GameObject> askerler, BirlikOzellik KomutanBirlikOzellik, int x, int z, Vector3 ReferansKonum)
    {
        AskerleriAl(Komutan,secmeAlani,askerler);
        AskerSayisi = askerler.Count;
        if (askerler.Count > KomutanBirlikOzellik.AktifDizilim.X)
        {
            x = KomutanBirlikOzellik.AktifDizilim.X;
        }
        else
        {
            x = askerler.Count;
        }
        z = AskerSayisi / x;
        ReferansKonum = askerler[0].transform.position;
        var ozellikler = new Ozellikler()
        {
            Komutan = Komutan,
            secmeAlani = secmeAlani,
            AskerSayisi = AskerSayisi,
            askerler = askerler,
            KomutanBirlikOzellik = KomutanBirlikOzellik,
            x = x,
            z = z,
            ReferansKonum = ReferansKonum
        };
        return ozellikler;
    }
    public void Olustur()
    {
        bizimOzellikler = KonumlariBul(Komutan, BizimSecmeAlani, BizimAskerSayisi, BizimAskerler, KomutanBirlikOzellik, BizimX, BizimZ, BizimReferansKonum);

        if (KomutanBirlikOzellik.Dusman1 != null)
        {
            Dusman1BirlikOzellik = KomutanBirlikOzellik.Dusman1.GetComponent<BirlikOzellik>();
            Dusman1Ozellikler = KonumlariBul(KomutanBirlikOzellik.Dusman1, Dusman1SecmeAlani, Dusman1AskerSayisi, DusmanAskerler1, Dusman1BirlikOzellik, Dusman1X, Dusman1Z, Dusman1ReferansKonum);
            saldiriNoktasi1Konum = new Vector3(bizimOzellikler.ReferansKonum.x + ((bizimOzellikler.x * 1.5f) / 2), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + ((Dusman1Ozellikler.z * 1.5f) / 2) - 1);// Ön
        }
        else
        {
            saldiriNoktasi1Konum = new Vector3(bizimOzellikler.ReferansKonum.x + ((bizimOzellikler.x * 1.5f) / 2), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + (5 * 1.5f));// Ön
        }
        if (KomutanBirlikOzellik.Dusman2 != null)
        {
            Dusman2BirlikOzellik = KomutanBirlikOzellik.Dusman2.GetComponent<BirlikOzellik>();
            Dusman2Ozellikler = KonumlariBul(KomutanBirlikOzellik.Dusman2, Dusman2SecmeAlani, Dusman2AskerSayisi, DusmanAskerler2, Dusman2BirlikOzellik, Dusman2X, Dusman2Z, Dusman2ReferansKonum);
            saldiriNoktasi2Konum = new Vector3(bizimOzellikler.ReferansKonum.x + ((bizimOzellikler.x * 1.5f) / 2), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z - (bizimOzellikler.z * 1.5f) - (Dusman2Ozellikler.z * 1.5f) + 1);//Arka
        }
        else
        {
            saldiriNoktasi2Konum = new Vector3(bizimOzellikler.ReferansKonum.x + ((bizimOzellikler.x * 1.5f) / 2), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z - (5 * 1.5f));//Arka
        }
        if (KomutanBirlikOzellik.Dusman3 != null)
        {
            Dusman3BirlikOzellik = KomutanBirlikOzellik.Dusman3.GetComponent<BirlikOzellik>();
            Dusman3Ozellikler = KonumlariBul(KomutanBirlikOzellik.Dusman3, Dusman3SecmeAlani, Dusman3AskerSayisi, DusmanAskerler3, Dusman3BirlikOzellik, Dusman3X, Dusman3Z, Dusman3ReferansKonum);
            saldiriNoktasi3Konum = new Vector3(bizimOzellikler.ReferansKonum.x - (((Dusman3Ozellikler.z * 1.5f) / 2) - 1), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + ((bizimOzellikler.z * 1.5f) / 2));//Sol
        }
        else
        {
            saldiriNoktasi3Konum = new Vector3(bizimOzellikler.ReferansKonum.x - (5 * 1.5f), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + ((bizimOzellikler.z * 1.5f) / 2));//Sol
        }
        if (KomutanBirlikOzellik.Dusman4 != null)
        {
            Dusman4BirlikOzellik = KomutanBirlikOzellik.Dusman4.GetComponent<BirlikOzellik>();
            Dusman4Ozellikler = KonumlariBul(KomutanBirlikOzellik.Dusman4, Dusman4SecmeAlani, Dusman4AskerSayisi, DusmanAskerler4, Dusman4BirlikOzellik, Dusman4X, Dusman4Z, Dusman4ReferansKonum);
            saldiriNoktasi4Konum = new Vector3(bizimOzellikler.ReferansKonum.x + (bizimOzellikler.x * 1.5f) + (((Dusman4Ozellikler.z * 1.5f) / 2) - 1), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + ((bizimOzellikler.z * 1.5f) / 2));//Sað
        }
        else
        {
            saldiriNoktasi4Konum = new Vector3(bizimOzellikler.ReferansKonum.x + (5 * 1.5f), bizimOzellikler.ReferansKonum.y, bizimOzellikler.ReferansKonum.z + ((bizimOzellikler.z * 1.5f) / 2));//Sað
        }

        yon = new Quaternion(0, 0, 0, 0);
    }
    void Update()
    {
        Olustur();
        SaldiriNoktasiComponent.SaldiriNoktasi1.transform.position = saldiriNoktasi1Konum;
        SaldiriNoktasiComponent.SaldiriNoktasi2.transform.position = saldiriNoktasi2Konum;
        SaldiriNoktasiComponent.SaldiriNoktasi3.transform.position = saldiriNoktasi3Konum;
        SaldiriNoktasiComponent.SaldiriNoktasi4.transform.position = saldiriNoktasi4Konum;
    }
}
