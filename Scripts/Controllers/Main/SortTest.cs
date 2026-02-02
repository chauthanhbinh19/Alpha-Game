using UnityEngine;
using System.Collections.Generic;

public class SortTest : MonoBehaviour
{
    void Start()
    {
        List<CardHeroes> heroes = new()
        {
            new CardHeroes{Name = "Hero A", Power = 120},
            new CardHeroes{Name = "Hero B", Power = 300},
            new CardHeroes{Name = "Hero C", Power = 200},
        };

        ListSortHelper.SortByPower(heroes);

        foreach (var h in heroes)
            Debug.Log($"{h.Name} - {h.Power}");
    }
}
