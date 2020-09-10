using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSystem : EntitySystem
{
    [SerializeField]
    private List<CoverEntity> Shelter;
    public GameObject test;

    public static CoverSystem instance;

     /* Алгоритм работы:
     * При начале работы сцены происходит поиск всех укрытий для бота, 
     * поиск точек у этих укрытий и проверка на видимость точек игроком.
     * 
     * Когда боту необходимо найти укрытие он обращается к функции FindPositionForCover, 
     * которая определяет ближайшее укрытие и ближайшую точку для укрытия бота.
     */

    void Start()
    {
        instance = this;
        FindCovers();
    }
    
    //Поиск всех объектов являющихся укрытиями.
    void FindCovers()
    {
        Shelter = new List<CoverEntity>(FindObjectsOfType<CoverEntity>());
    }
    
    //Функция для поиска точки укрытия.
    public CoverPointEntity FindPositionForCover(Vector3 BotPosition)
    {
        CoverEntity NearestShelter = null;
        for (int i = 0; i < Shelter.Count; i++)
        {
            if (CheckCoverAvailability(Shelter[i]))
            {
                float MinRange = float.MaxValue;

                float dist = Vector3.Distance(Shelter[i].transform.position, BotPosition);

                if (dist < MinRange)
                {
                    MinRange = dist;
                    NearestShelter = Shelter[i];
                }
            }
        }
        return NearestShelter.FindNearestPoint(BotPosition);
    } 

    //Проверка доступности укрытия.
    public bool CheckCoverAvailability(CoverEntity Cover)
    {
        if (Cover.CheckPointAvailability())
        {
            return true;
        } else
        {
            return false;
        }
        
    }
}
