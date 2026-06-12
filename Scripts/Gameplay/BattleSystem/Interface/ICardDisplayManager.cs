using System.Collections.Generic;

public interface ICardDisplayManager
{
    void AssignCardsToAllySlots(List<CardBase> cardsToPlace);
    void AssignCardsToEnemySlots(List<CardBase> cardsToPlace);
    void DisplayCardContract(CardContract cardContract);
    void DisplayCardPenalty(CardPenalty cardPenalty);
}