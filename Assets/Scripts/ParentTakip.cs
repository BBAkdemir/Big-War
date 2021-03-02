using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentTakip : MonoBehaviour
{
    int rand;
    int a;

    public Transform target;
    public GameObject SecmeAlani;
    public Terrain terrain;
    private List<GameObject> askerler;
    public float speedPosition = 2f, speedRotation = 1f;
    
    private NavMeshAgent yapayZeka;
    private NavMeshPath navpath;
    public Vector3 YedekHedefPozisyon;
    public List<Komutan> komutans;

    BirlikOzellik komutan;
    OrduHareket komutanOrduHareket;
    Vector3 move;
    void Start()
    {
        komutan = target.transform.parent.gameObject.GetComponent<BirlikOzellik>();
        komutanOrduHareket = target.transform.parent.gameObject.GetComponent<OrduHareket>();
        yapayZeka = GetComponent<NavMeshAgent>();
        speedPosition = UnityEngine.Random.Range(komutan.Hiz, komutan.Hiz + 0.3f);
        askerler = new List<GameObject>();
        for (int i = 0; i < target.transform.parent.gameObject.transform.childCount; i++)
        {
            if (target.transform.parent.gameObject.transform.GetChild(i).gameObject.tag == "SecmeAlani")
            {
                SecmeAlani = target.transform.parent.gameObject.transform.GetChild(i).gameObject; //Sçme alanını aldım.
                break;
            }
        }
        askerler = komutan.Askerler;
        var rnd1 = new System.Random();
        rand = rnd1.Next(askerler.Count);
    }
    void Update()
    {
        SecmeAlaniTakip(SecmeAlani);
        move = new Vector3(target.position.x, transform.position.y, target.position.z);

        #region Gidilemeyecek Yerlere Basmamamızı Sağlayan Kod
        navpath = new NavMeshPath();
        Vector3 pos = komutanOrduHareket.HedefPozisyonu;
        pos.y = Terrain.activeTerrain.SampleHeight(komutanOrduHareket.HedefPozisyonu);
        yapayZeka.CalculatePath(pos, navpath);
        if (navpath.status == NavMeshPathStatus.PathComplete)
        {
            yapayZeka.isStopped = false;
            yapayZeka.destination = move;
            YedekHedefPozisyon = komutanOrduHareket.HedefPozisyonu;
        }
        

        else
        {
            komutans = komutanOrduHareket.komutans;

            foreach (var item in komutans)
            {
                if (item.KomutanNesnesi.transform.parent.gameObject == target.transform.parent.gameObject)
                {
                    item.HedefKonum = YedekHedefPozisyon;
                }
            }
        }
        #endregion

        yapayZeka.speed = speedPosition;
    }
    public void SecmeAlaniTakip(GameObject secmeAlani)
    {
        while(askerler[rand] == null){
            var rnd1 = new System.Random();
            rand = rnd1.Next(askerler.Count);
        }
        GameObject asker = askerler[rand];
        Vector3 pos = new Vector3(asker.transform.position.x, asker.transform.position.y + 10, asker.transform.position.z);
        secmeAlani.transform.position = pos;
    }
}
