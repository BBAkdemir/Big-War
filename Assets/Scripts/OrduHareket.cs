using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class OrduHareket : MonoBehaviour
{
    public HangisiHareketEtsin erisim;
    public ParentTakip Asker;
    public GameObject hareketEdecekObje;
    public GameObject hareketEdecekObjeAsker;
    public GameObject SecmeAlaniAl;
    private GameObject hareketEdecekObjeDeğisken;
    public Vector3 HedefPozisyonu;
    public List<Komutan> komutans;
    public NavMeshAgent navMeshAgent;

    public BirlikOzellik ozellik;
    void Start()
    {
        erisim = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        navMeshAgent = GameObject.FindWithTag("Askerler1").GetComponent<NavMeshAgent>();
        HedefPozisyonu = transform.position;

        komutans = new List<Komutan>();
    }
    void gidilecekKonumBul(GameObject hareketEdecek)
    {
        Plane Yuzey = new Plane(Vector3.up, transform.position);
        Ray isinGonder = Camera.main.ScreenPointToRay(Input.mousePosition);
        float Mesafe = 0.0f;
        if (Yuzey.Raycast(isinGonder, out Mesafe))
        {
            HedefPozisyonu = isinGonder.GetPoint(Mesafe);
            var komutan = new Komutan()
            {
                HedefKonum = HedefPozisyonu,
                KomutanNesnesi = hareketEdecek,
                HareketSerbestMi = true
            };
            hareketEdecek.transform.parent.position = hareketEdecek.transform.position;
            if (komutans.Any(a => a.KomutanNesnesi == hareketEdecek))
            {
                var silKomutan = komutans.Where(a => a.KomutanNesnesi == hareketEdecek).FirstOrDefault();
                komutans.Remove(silKomutan);
                komutans.Add(komutan);
            }
            else
            {
                komutans.Add(komutan);
            }
        }
    }

    void NesneyiHareketeGecir(List<Komutan> komutans)
    {
        foreach (var item in komutans)
        {
            if (item.HareketSerbestMi == true)
            {
                ozellik = item.KomutanNesnesi.transform.parent.GetComponent<BirlikOzellik>();
                Vector3 konum = new Vector3(item.HedefKonum.x, item.KomutanNesnesi.transform.parent.position.y, item.HedefKonum.z);
                item.KomutanNesnesi.transform.parent.position = Vector3.MoveTowards(item.KomutanNesnesi.transform.parent.position, konum, 20 * Time.deltaTime);
                if (item.KomutanNesnesi.transform.parent.position == item.HedefKonum)
                {
                    item.HareketSerbestMi = false;
                }
                item.KomutanNesnesi.transform.parent.LookAt(new Vector3(item.HedefKonum.x, item.KomutanNesnesi.transform.parent.position.y, item.HedefKonum.z));
            }
        }
    }
    void Update()
    {
        hareketEdecekObje = erisim.hareketEdecek;
        bool flag = false;
        if (hareketEdecekObje != null)
        {
            if (hareketEdecekObje.tag == "Terrain" || hareketEdecekObje.transform.parent.gameObject == gameObject)
                flag = true;
        }

        if (flag)
        {
            if (hareketEdecekObje != null && hareketEdecekObje.tag == "Askerler1")
            {
                Asker = GameObject.Find(hareketEdecekObje.name).GetComponent<ParentTakip>();
                hareketEdecekObjeAsker = Asker.target.gameObject;
            }
            else
            {
                hareketEdecekObjeAsker = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hareketEdecekObje.tag == "SecmeAlani" || hareketEdecekObje.tag == "Askerler1")
                {
                    hareketEdecekObjeDeğisken = hareketEdecekObje;
                }
                else
                {
                    hareketEdecekObjeDeğisken = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (hareketEdecekObje != null)
                {
                    if (hareketEdecekObje.tag == "SecmeAlani")
                    {
                        gidilecekKonumBul(hareketEdecekObjeDeğisken);

                    }
                    else if (hareketEdecekObje.tag == "Askerler1")
                    {
                        for (int i = 0; i < 101; i++)
                        {
                            if (hareketEdecekObjeAsker.transform.parent.gameObject.transform.GetChild(i).gameObject.tag == "SecmeAlani")
                            {
                                SecmeAlaniAl = hareketEdecekObjeAsker.transform.parent.gameObject.transform.GetChild(i).gameObject; //Sçme alanını aldım.
                                break;
                            }
                        }
                        gidilecekKonumBul(SecmeAlaniAl);
                    }
                }
            }
            flag = false;
        }
        NesneyiHareketeGecir(komutans);
    }
}
