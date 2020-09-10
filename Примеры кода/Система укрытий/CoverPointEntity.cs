using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPointEntity : Entity
{
    public int SecondsTimer = 1;
    public bool Available;

    [SerializeField]
    private float Counter = 0;
    public bool Flag;

    [Header("Debug")]
    public Renderer DebugObject;
    public Material[] DebugMaterials;

    void Start()
    {
        Available = true;
        Flag = true;
    }

    //Отображение в режиме дебага доступных и недоступных точек.
    public void DebugMode()
    {
        int index = 0;
        if (Available)
            index = 1;
        DebugObject.material = DebugMaterials[index];
    }

    public bool IsAvailable()
    {
        return Available;
    }

    public void ChangeAvailability()
    {
        Available = !Available;
    }

    public void Take()
    {
        Flag = false;
    }

    public void Free()
    {
        Flag = true;
    }

    //Проверка на видимость точки игроком, если игрок видит точку, то она недоступна.
    public void CheckPointVisibility(Vector3 PlayerPosition)
    {
        if (Physics.Linecast(transform.position, PlayerPosition, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.GetComponentInParent<CoverEntity>() != null)
            {
                Debug.DrawLine(transform.position, PlayerPosition);
                Available = true;
            }
            else 
            {
                Available = false;
            }
        }
    }

    void Update()
    {
        DebugMode();
    }
}
