using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverEntity : Entity
{
    [SerializeField]
    private List<CoverPointEntity> Points;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Wanderer");
        FindPoints();
    }

    void Update()
    {
        GetAvailablePoints();
    }

    //Поиск всех точек для укрытия у одного объекта.
    void FindPoints()
    {
        Points = new List<CoverPointEntity>(GetComponentsInChildren<CoverPointEntity>());
    }

    //Проверка на наличие доступных точек.
    public void GetAvailablePoints()
    {
        foreach(CoverPointEntity point in Points)
        {
            point.CheckPointVisibility(Player.transform.position);
        }
    }

    //Проверка точки укрытия на доступность.
    public bool CheckPointAvailability()
    {
        int NumberAvailabilityPoints = 0;
        
        for (int i = 0; i < Points.Count; i++)
        {
            if (Points[i].IsAvailable())
            {
                NumberAvailabilityPoints++;
            }
        }
        if (NumberAvailabilityPoints > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    //Поиск Ближайшей точки для укрытия.
    public CoverPointEntity FindNearestPoint(Vector3 BotPosition)
    {
        float MinRange = float.MaxValue;
        CoverPointEntity NearestPoint = null;
        for (int i = 0; i < Points.Count; i++)
        {
            float distance = Vector3.Distance(Points[i].transform.position, BotPosition);
            if (Points[i].Available && Points[i].Flag && (distance < MinRange))
            {
                NearestPoint = Points[i];
                MinRange = distance;
            }
        }
        return NearestPoint;
    }

}
