using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBattlePhase
{
    IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender);
}