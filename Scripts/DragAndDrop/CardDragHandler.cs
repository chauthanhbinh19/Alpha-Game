using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Texture cardTexture;
    public int team_id;
    public string mainPosition;
    public object obj;
    public Transform InTeam;
    public Transform positionPanel;
    private Vector2 originalPosition;
    // Delegate để gọi lại hàm
    public System.Action OnDragEnd; // Callback khi kết thúc kéo

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Lưu vị trí ban đầu khi thẻ được khởi tạo
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Quay lại vị trí ban đầu khi kết thúc kéo
        rectTransform.anchoredPosition = originalPosition;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        InTeam.gameObject.SetActive(true);

        // Gọi lại hàm callback
        OnDragEnd?.Invoke();
    }
    // Phương thức để cập nhật team_id
    public void UpdateTeamId(int newTeamId)
    {
        team_id = newTeamId;
    }
    public void OnCardClicked()
    {
        // Tìm vị trí trống đầu tiên và thêm thẻ vào đó
        FindAndDropCard();
    }
    private void FindAndDropCard()
    {
        // Tìm danh sách các vị trí (GameObject)
        // Transform positionPanel = transform.parent; // Giả sử CardDragHandler nằm trong một panel chứa các vị trí
        CardDropHandler[] dropHandlers = positionPanel.GetComponentsInChildren<CardDropHandler>();

        foreach (CardDropHandler dropHandler in dropHandlers)
        {
            RawImage positionImage = dropHandler.transform.Find("Image").GetComponent<RawImage>();
            if (positionImage.texture == null) // Kiểm tra xem vị trí có trống không
            {
                // Thêm thẻ vào vị trí này
                DropCardIntoPosition(dropHandler);
                return; // Thoát khỏi vòng lặp sau khi tìm thấy vị trí trống đầu tiên
            }
        }

        // Nếu không tìm thấy vị trí trống nào, thông báo hoặc thực hiện hành động khác
        Debug.Log("No empty position available.");
    }
    public void DropCardIntoPosition(CardDropHandler dropHandler)
    {
        // Kiểm tra xem liệu có texture được gán không
        RawImage positionImage = dropHandler.transform.Find("Image").GetComponent<RawImage>();
        if (positionImage != null)
        {
            positionImage.texture = this.cardTexture;
            // Debug.Log("Card dropped: " + draggedCard.gameObject.name + " at position: " + gameObject.name);
        }
        object obj = this.obj;
        string position = mainPosition + "-" + dropHandler.position_id;
        Teams teams = new Teams();
        if (obj is CardHeroes cardHeroes)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardHeroes.UpdateTeamFactCardHeroes(null, null, dropHandler.card_id);
                cardHeroes.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                if (cardHeroes.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardHeroes.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardHeroes.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardHeroes.UpdateTeamFactCardHeroes(team_id, position, cardHeroes.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.all_power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardCaptains.UpdateTeamFactCardCaptains(null, null, dropHandler.card_id);
                cardCaptains.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                if (cardCaptains.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardCaptains.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardCaptains.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardCaptains.UpdateTeamFactCardCaptains(team_id, position, cardCaptains.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.all_power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardColonels.UpdateTeamFactCardColonels(null, null, dropHandler.card_id);
                cardColonels.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                if (cardColonels.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardColonels.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardColonels.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardColonels.UpdateTeamFactCardColonels(team_id, position, cardColonels.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.all_power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardGenerals.UpdateTeamFactCardGenerals(null, null, dropHandler.card_id);
                cardGenerals.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                if (cardGenerals.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardGenerals.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardGenerals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardGenerals.UpdateTeamFactCardGenerals(team_id, position, cardGenerals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.all_power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardAdmirals.UpdateTeamFactCardAdmirals(null, null, dropHandler.card_id);
                cardAdmirals.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                if (cardAdmirals.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardAdmirals.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardAdmirals.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardAdmirals.UpdateTeamFactCardAdmirals(team_id, position, cardAdmirals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.all_power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardMonsters.UpdateTeamFactCardMonsters(null, null, dropHandler.card_id);
                cardMonsters.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                if (cardMonsters.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardMonsters.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardMonsters.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardMonsters.UpdateTeamFactCardMonsters(team_id, position, cardMonsters.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.all_power, 1);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardMilitary.UpdateTeamFactCardMilitary(null, null, dropHandler.card_id);
                cardMilitary.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                if (cardMilitary.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardMilitary.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardMilitary.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardMilitary.UpdateTeamFactCardMilitary(team_id, position, cardMilitary.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.all_power, 1);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            double currentPower = teams.GetTeamsPower(User.CurrentUserId);
            if (dropHandler.card_id != 0)
            {
                cardSpell.UpdateTeamFactCardSpell(null, null, dropHandler.card_id);
                cardSpell.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                if (cardSpell.all_power >= dropHandler.card_all_power)
                {
                    double newPower = cardSpell.all_power - dropHandler.card_all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_all_power - cardSpell.all_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                cardSpell.UpdateTeamFactCardSpell(team_id, position, cardSpell.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.all_power, 1);
            }
        }
    }
}