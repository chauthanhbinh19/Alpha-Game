using UnityEngine;
using System.Collections.Generic;

public class SortTest : MonoBehaviour
{
    void Start()
    {
        List<Equipments> heroes = new()
        {
            new Equipments{Name = "Hero A", Power = 120},
            new Equipments{Name = "Hero B", Power = 300},
            new Equipments{Name = "Hero C", Power = 200},
        };

        ListSortHelper.SortByPower(heroes);

        foreach (var h in heroes)
            Debug.Log($"{h.Name} - {h.Power}");
    }
}
