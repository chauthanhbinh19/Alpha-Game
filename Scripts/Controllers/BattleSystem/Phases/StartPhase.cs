using System.Collections;
using UnityEngine;

public class StartPhase : IBattlePhase
{
    private TeamSetupService _teamSetupService;
    private CardDisplayManager _displayManager;

    public StartPhase(CardDisplayManager displayManager)
    {
        _teamSetupService = new TeamSetupService();
        _displayManager = displayManager;
    }
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== Phase 1: Start ===");

        yield return new WaitForSeconds(2.0f);
        
        Debug.Log("Prepare Phase Complete");
        

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Start Phase Complete");
    }
}