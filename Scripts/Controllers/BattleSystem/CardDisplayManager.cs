using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<CardSlot> cardSlots = new List<CardSlot>();

    private BattleGrid battleGrid;
    private Dictionary<int, CardSlot> slotLookup;

    public void InitializeBattleGrid()
    {
        battleGrid = new BattleGrid();
        BuildSlotLookup();
    }

    private void BuildSlotLookup()
    {
        slotLookup = new Dictionary<int, CardSlot>();
        foreach (var slot in cardSlots)
        {
            if (slot == null || slot.position < 0)
                continue;
            if (!slotLookup.ContainsKey(slot.position))
                slotLookup[slot.position] = slot;
        }
    }

    public bool PlaceCardModel(CardModel cardModel, int position, BattleCellType owner)
    {
        if (battleGrid == null)
        {
            Debug.LogWarning("BattleGrid is not initialized in CardDisplayManager.");
            return false;
        }

        var slot = GetSlot(position);
        if (slot == null)
        {
            Debug.LogWarning($"No card slot configured for grid position {position}.");
            return false;
        }

        if (slot.owner != owner)
        {
            Debug.LogWarning($"Slot {position} owner {slot.owner} does not match card owner {owner}.");
            return false;
        }

        if (slot.occupant != null)
        {
            Debug.LogWarning($"Slot {position} is already occupied by another card.");
            return false;
        }

        bool assigned = battleGrid.AssignCard(cardModel, position, owner);
        if (!assigned)
            return false;

        slot.occupant = cardModel;
        cardModel.CellOwner = owner;
        cardModel.CellPosition = position;

        if (cardPrefab != null && slot.positionObject != null)
        {
            var spawnPosition = slot.positionObject.transform.position;
            var cardObject = Instantiate(cardPrefab, spawnPosition, Quaternion.identity, slot.positionObject.transform);
            var unit = cardObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.Initialize(cardModel);
                slot.instantiatedUnit = unit;
            }
            else
            {
                Debug.LogWarning("Card prefab does not contain a Unit component.");
            }
        }

        return true;
    }

    public CardSlot GetSlot(int position)
    {
        if (slotLookup != null && slotLookup.TryGetValue(position, out var slot))
            return slot;

        return cardSlots.FirstOrDefault(s => s != null && s.position == position);
    }

    public List<int> GetAvailablePositions(BattleCellType owner)
    {
        return cardSlots
            .Where(slot => slot != null && slot.owner == owner && slot.position >= 0 && slot.occupant == null)
            .Select(slot => slot.position)
            .ToList();
    }

    public BattleCell GetCell(int position)
    {
        return battleGrid?.GetCell(position);
    }

    public IReadOnlyList<BattleCell> GetAllCells()
    {
        return battleGrid?.Cells;
    }
}
