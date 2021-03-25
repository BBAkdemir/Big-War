using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentTakip : MonoBehaviour
{
    int rand;
    int a;

    public Transform target;
    public GameObject selectionArea;
    public Terrain terrain;
    private List<GameObject> soldiers;
    public float speedPosition = 2f, speedRotation = 1f;
    
    private NavMeshAgent ArtificialIntelligence;
    private NavMeshPath navpath;
    public Vector3 reserveTargetPosition;
    public List<Komutan> commanders;

    BirlikOzellik commander;
    OrduHareket commanderTake;
    Vector3 move;
    void Start()
    {
        commander = target.transform.parent.gameObject.GetComponent<BirlikOzellik>();
        commanderTake = target.transform.parent.gameObject.GetComponent<OrduHareket>();
        ArtificialIntelligence = GetComponent<NavMeshAgent>();
        speedPosition = UnityEngine.Random.Range(commander.Speed, commander.Speed + 0.3f);
        soldiers = new List<GameObject>();
        for (int i = 0; i < target.transform.parent.gameObject.transform.childCount; i++)
        {
            if (target.transform.parent.gameObject.transform.GetChild(i).gameObject.tag == "selectionArea")
            {
                selectionArea = target.transform.parent.gameObject.transform.GetChild(i).gameObject; //Seçme alanını aldım.
                break;
            }
        }
        soldiers = commander.Soldiers;
        var rnd1 = new System.Random();
        rand = rnd1.Next(soldiers.Count);
    }
    void Update()
    {
        selectionAreaFollow(selectionArea);
        move = new Vector3(target.position.x, transform.position.y, target.position.z);

        #region Gidilemeyecek Yerlere Basmamamızı Sağlayan Kod
        navpath = new NavMeshPath();
        Vector3 pos = commanderTake.targetPosition;
        pos.y = Terrain.activeTerrain.SampleHeight(commanderTake.targetPosition);
        ArtificialIntelligence.CalculatePath(pos, navpath);
        if (navpath.status == NavMeshPathStatus.PathComplete)
        {
            ArtificialIntelligence.isStopped = false;
            ArtificialIntelligence.destination = move;
            reserveTargetPosition = commanderTake.targetPosition;
        }
        else
        {
            commanders = commanderTake.commanders;

            foreach (var item in commanders)
            {
                if (item.CommanderObject.transform.parent.gameObject == target.transform.parent.gameObject)
                {
                    item.TargetLocation = reserveTargetPosition;
                }
            }
        }
        #endregion

        ArtificialIntelligence.speed = speedPosition;
    }
    public void selectionAreaFollow(GameObject selectionArea)
    {
        while(soldiers[rand] == null){
            var rnd1 = new System.Random();
            rand = rnd1.Next(soldiers.Count);
        }
        GameObject soldier = soldiers[rand];
        Vector3 pos = new Vector3(soldier.transform.position.x, soldier.transform.position.y + 10, soldier.transform.position.z);
        selectionArea.transform.position = pos;
    }
}
