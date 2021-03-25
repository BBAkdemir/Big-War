using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class OrduHareket : MonoBehaviour
{
    public HangisiHareketEtsin access;
    public ParentTakip Soldier;
    public GameObject movementObjectCreated;
    public GameObject movementObjectCreatedSoldier;
    public GameObject getSelectArea;
    private GameObject movementObjectCreatedVariable;
    public Vector3 targetPosition;
    public List<Komutan> commanders;
    public NavMeshAgent navMeshAgent;

    public BirlikOzellik property;
    void Start()
    {
        access = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        navMeshAgent = GameObject.FindWithTag("Askerler1").GetComponent<NavMeshAgent>();
        targetPosition = transform.position;

        commanders = new List<Komutan>();
    }
    void findTheDestination(GameObject movementObject)
    {
        Plane surface = new Plane(Vector3.up, transform.position);
        Ray sendRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 0.0f;
        if (surface.Raycast(sendRay, out distance))
        {
            targetPosition = sendRay.GetPoint(distance);
            var commander = new Komutan()
            {
                TargetLocation = targetPosition,
                CommanderObject = movementObject,
                IsMovementFree = true
            };
            movementObject.transform.parent.position = movementObject.transform.position;
            if (commanders.Any(a => a.CommanderObject == movementObject))
            {
                var removeCommander = commanders.Where(a => a.CommanderObject == movementObject).FirstOrDefault();
                commanders.Remove(removeCommander);
                commanders.Add(commander);
            }
            else
            {
                commanders.Add(commander);
            }
        }
    }

    void moveOnUnit(List<Komutan> commanders)
    {
        foreach (var item in commanders)
        {
            if (item.IsMovementFree == true)
            {
                property = item.CommanderObject.transform.parent.GetComponent<BirlikOzellik>();
                Vector3 konum = new Vector3(item.TargetLocation.x, item.CommanderObject.transform.parent.position.y, item.TargetLocation.z);
                item.CommanderObject.transform.parent.position = Vector3.MoveTowards(item.CommanderObject.transform.parent.position, konum, 20 * Time.deltaTime);
                if (item.CommanderObject.transform.parent.position == item.TargetLocation)
                {
                    item.IsMovementFree = false;
                }
                item.CommanderObject.transform.parent.LookAt(new Vector3(item.TargetLocation.x, item.CommanderObject.transform.parent.position.y, item.TargetLocation.z));
            }
        }
    }
    void Update()
    {
        movementObjectCreated = access.movementObject;
        bool flag = false;
        GameObject chosen = movementObjectCreated;
        if (movementObjectCreated != null)
        {
            if (movementObjectCreated.transform.parent != null && movementObjectCreated.transform.parent.gameObject == gameObject)
                chosen = movementObjectCreated.transform.parent.gameObject;

            if (movementObjectCreated.tag == "Askerler1")
                chosen = GameObject.Find(movementObjectCreated.name).GetComponent<ParentTakip>().target.transform.parent.gameObject;

            if (movementObjectCreated.tag == "Terrain" || chosen == gameObject)
                flag = true;
        }

        if (flag)
        {
            if (movementObjectCreated != null && movementObjectCreated.tag == "Askerler1")
            {
                Soldier = GameObject.Find(movementObjectCreated.name).GetComponent<ParentTakip>();
                movementObjectCreatedSoldier = Soldier.target.gameObject;
            }
            else
            {
                movementObjectCreatedSoldier = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (movementObjectCreated.tag == "selectionArea" || movementObjectCreated.tag == "Askerler1")
                {
                    movementObjectCreatedVariable = movementObjectCreated;
                }
                else
                {
                    movementObjectCreatedVariable = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (movementObjectCreated != null)
                {
                    if (movementObjectCreated.tag == "selectionArea")
                    {
                        findTheDestination(movementObjectCreatedVariable);

                    }
                    else if (movementObjectCreated.tag == "Askerler1")
                    {
                        for (int i = 0; i < 101; i++)
                        {
                            if (movementObjectCreatedSoldier.transform.parent.gameObject.transform.GetChild(i).gameObject.tag == "selectionArea")
                            {
                                getSelectArea = movementObjectCreatedSoldier.transform.parent.gameObject.transform.GetChild(i).gameObject; //Sçme alanını aldım.
                                break;
                            }
                        }
                        findTheDestination(getSelectArea);
                    }
                }
            }
            flag = false;
        }
        moveOnUnit(commanders);
    }
}
