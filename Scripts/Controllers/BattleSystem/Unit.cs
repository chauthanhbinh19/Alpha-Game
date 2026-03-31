using UnityEngine;

public class Unit : MonoBehaviour
{
    public CardModel cardModel;

    public void Initialize(CardModel model)
    {
        cardModel = model;
    }
}
