using System.Collections;
using UnityEngine;

public class StartPhase : IBattlePhase
{
    private TeamSetupService _teamSetupService; 
    private ICardDisplayManager _displayManager;

    public StartPhase(ICardDisplayManager displayManager)
    {
        _teamSetupService = new TeamSetupService(/* inject dependencies here */); 
        _displayManager = displayManager;
    }
    public IEnumerator ExecutePhase(PlayerController attacker, PlayerController defender)
    {
        Debug.Log("=== Start Phase ===");
        // 1. TẠO LUẬT CHƠI (Áp dụng cho cả hai người chơi trong turn này)
        // Contract: Ngẫu nhiên các loại thẻ cần cho 10 vị trí
        var currentContract = TeamSetupService.CreateRandomContract("Contract");
        // Penalty: Ngẫu nhiên các mức phạt, ví dụ: CardHeroes luôn là thẻ phạt nhẹ nhất (0)
        var currentPenalty = TeamSetupService.CreateRandomPenalty("Penalty");
        Debug.Log($"Luật Chơi Mới: Contract '{currentContract.Name}' và Penalty '{currentPenalty.Name}' đã được tạo.");

        if (_displayManager != null)
        {
            _displayManager.DisplayCardContract(currentContract);
            _displayManager.DisplayCardPenalty(currentPenalty);
        }

        // 2. ÁP DỤNG LUẬT CHƠI CHO CẢ HAI PLAYER
        
        // Thiết lập đội hình cho Tấn công
        // Giả định PlayerController có thuộc tính UserId, TeamId và AllySlots
        // _teamSetupService.SetupPlayerTeam(
        //     attacker.UserId, 
        //     attacker.TeamId, 
        //     currentContract, 
        //     currentPenalty, 
        //     attacker.AllySlots // Slots (vị trí) của người chơi
        // );
        Debug.Log("Attacker Team Setup Complete.");

        // Thiết lập đội hình cho Phòng thủ
        // _teamSetupService.SetupPlayerTeam(
        //     defender.UserId, 
        //     defender.TeamId, 
        //     currentContract, 
        //     currentPenalty, 
        //     defender.AllySlots // Slots (vị trí) của người chơi
        // );
        Debug.Log("Defender Team Setup Complete.");

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Start Phase Complete");
    }
}