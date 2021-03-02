using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcIceGirme : MonoBehaviour
{
    private SavasYapayZekasi savasYapayZekasi;

    private bool DegdiMi;
    private Vector3 gidilecek1;
    private Vector3 gidilecek2;
    private Vector3 gidilecek3;
    private Vector3 gidilecek4;
    private BirlikOzellik KendiGucu;
    private List<YapayZekaSaldiriOncelik> etraftakiDostlar;

    // Start is called before the first frame update
    void Start()
    {
        etraftakiDostlar = new List<YapayZekaSaldiriOncelik>();
        savasYapayZekasi = transform.parent.gameObject.GetComponent<SavasYapayZekasi>();
        KendiGucu = transform.gameObject.GetComponent<BirlikOzellik>();
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SecmeAlani")
        {
            gidilecek1 = new Vector3(transform.position.x - 7, transform.parent.position.y, transform.position.z);
            gidilecek2 = new Vector3(transform.position.x + 7, transform.parent.position.y, transform.position.z);
            gidilecek3 = new Vector3(transform.position.x - (((Terrain.activeTerrain.terrainData.size.x / 2) - 50) - transform.position.x) , transform.parent.position.y, transform.position.z);
            gidilecek4 = new Vector3(transform.position.x + (((Terrain.activeTerrain.terrainData.size.x / 2) - 50) - transform.position.x), transform.parent.position.y, transform.position.z);
            
            foreach (var item in etraftakiDostlar)
            {
                if (transform.position.x > item.Asker.transform.position.x)
                {
                    if (transform.position.z > item.Asker.transform.position.z)
                    {
                        if (transform.position.x < -((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.x > ((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.z < -((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else if (transform.position.z > ((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (transform.position.x < -((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.x > ((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.z < -((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else if (transform.position.z > ((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    if (transform.position.z > item.Asker.transform.position.z)
                    {
                        if (transform.position.x < -((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.x > ((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.z < -((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else if (transform.position.z > ((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (transform.position.x < -((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.x > ((Terrain.activeTerrain.terrainData.size.x / 2) - 50))
                        {

                        }
                        else if (transform.position.z < -((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else if (transform.position.z > ((Terrain.activeTerrain.terrainData.size.z / 2) - 50))
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        etraftakiDostlar = savasYapayZekasi.EtraftakiDostlar;
    }
}
