using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private int maxTurn = 10;
    // Cần gán các GameObject Slot này vào Inspector
    public CardSlot[] AllySlots = new CardSlot[10]; // 5 vị trí cho phe mình
    public CardSlot[] EnemySlots = new CardSlot[10]; // 5 vị trí cho phe địch
    private LoadTeams _loadTeamService;
    private TurnManager turnManager;
    private PlayerController attacker;
    private PlayerController defender;

    private void Awake()
    {
        // Khởi tạo service khi game bắt đầu
        _loadTeamService = new LoadTeams(); 
    }
    private void Start()
    {
        InitializeBattle();
        StartCoroutine(turnManager.RunTurns(attacker, defender));
    }

    private void InitializeBattle()
    {
        attacker = new PlayerController();
        defender = new PlayerController();

        turnManager = new TurnManager(maxTurn);

        var teams = TeamsService.Create().GetUserTeams(User.CurrentUserId);
        var firstTeam = teams.FirstOrDefault(t => t.TeamNumber == 1);
        SetupPlayerTeam(firstTeam.TeamId);

        // Giả lập thêm vài lá bài vào field
        // attacker.AddCard(new DummyCard("🔥 Attacker Card A"));
        // attacker.AddCard(new DummyCard("⚔️ Attacker Card B"));
        // defender.AddCard(new DummyCard("🛡️ Defender Card X"));
        // defender.AddCard(new DummyCard("🧱 Defender Card Y"));

        Debug.Log("Battle initialized successfully!");
    }

    public void SetupPlayerTeam(string team_id)
    {
        // 1. Gọi Service để lấy các đối tượng CardBase đã được ánh xạ
        List<CardBase> loadedCards = _loadTeamService.LoadPlayerTeamCard(team_id);

        // 2. Gán từng Card vào vị trí vật lý trên sân
        foreach (var card in loadedCards)
        {
            int positionIndex = card.MainPosition;
            
            if (positionIndex >= 0 && positionIndex < AllySlots.Length)
            {
                CardSlot targetSlot = AllySlots[positionIndex];
                
                if (targetSlot != null)
                {
                    // Gán CardBase vào CardSlot (GameObject trên sân)
                    targetSlot.PlaceCard(card);
                    
                    // Thêm logic khởi tạo GameObject 3D của thẻ bài ở đây nếu cần.
                }
                else
                {
                    Debug.LogError($"Slot tại vị trí {positionIndex} chưa được gán hoặc bị null.");
                }
            }
        }
    }
}
