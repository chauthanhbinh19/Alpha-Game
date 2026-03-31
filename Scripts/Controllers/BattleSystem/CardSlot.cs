using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp này mô tả mỗi ô grid, có thể gán vào object Unity để điều chỉnh vị trí trực tiếp.
[System.Serializable]
public class CardSlot
{
    public GameObject positionObject;
    public int slotIndex;
    public BattleCellType owner = BattleCellType.None;
    public int position = -1;
    public CardModel occupant;
    public Unit instantiatedUnit;
}

