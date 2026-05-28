using System.Collections;
using System.Collections.Generic;

public class Turn
{
    public int TurnIndex;
    public List<Phase> Phases = new List<Phase>();

    public Turn(int index)
    {
        TurnIndex = index;
    }

    public IEnumerator Execute()
    {
        UnityEngine.Debug.Log($"=== TURN {TurnIndex} START ===");

        foreach (var phase in Phases)
        {
            yield return phase.Execute();
        }

        UnityEngine.Debug.Log($"=== TURN {TurnIndex} END ===");
    }
}