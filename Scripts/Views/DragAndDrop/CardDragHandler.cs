using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UI;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Texture cardTexture;
    public string team_id;
    public string mainPosition;
    public object obj;
    public Transform InTeam;
    public Transform positionPanel;
    private Vector2 originalPosition;
    // Delegate để gọi lại hàm
    public System.Action OnDragEnd; // Callback khi kết thúc kéo
    UserCardHeroesService userCardHeroesService;
    UserCardCaptainsService userCardCaptainsService;
    UserCardColonelsService userCardColonelsService;
    UserCardGeneralsService userCardGeneralsService;
    UserCardAdmiralsService userCardAdmiralsService;
    UserCardMonstersService userCardMonstersService;
    UserCardMilitaryService userCardMilitaryService;
    UserCardSpellService userCardSpellService;
    TeamsService teamsService;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        userCardHeroesService = UserCardHeroesService.Create();
        userCardCaptainsService = UserCardCaptainsService.Create();
        userCardColonelsService = UserCardColonelsService.Create();
        userCardGeneralsService = UserCardGeneralsService.Create();
        userCardAdmiralsService = UserCardAdmiralsService.Create();
        userCardMonstersService = UserCardMonstersService.Create();
        userCardMilitaryService = UserCardMilitaryService.Create();
        userCardSpellService = UserCardSpellService.Create();
        teamsService = TeamsService.Create();
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
    public void UpdateTeamId(string newTeamId)
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
            Texture texture = Resources.Load<Texture>($"UI/Background4/Background_V4_408");
            if (positionImage.texture == texture) // Kiểm tra xem vị trí có trống không
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
        double currentPower = User.CurrentUserPower;
        if (obj is CardHeroes cardHeroes)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardHeroesService.UpdateTeamCardHeroes(null, null, dropHandler.card_id);
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.Id);
                if (cardHeroes.Power >= dropHandler.card_power)
                {
                    double diffPower = cardHeroes.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardHeroes.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.Id);
                double updatedPower = currentPower + cardHeroes.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardHeroes.Power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardCaptainsService.UpdateTeamCardCaptains(null, null, dropHandler.card_id);
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.Id);
                if (cardCaptains.Power >= dropHandler.card_power)
                {
                    double diffPower = cardCaptains.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardCaptains.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.Id);
                double updatedPower = currentPower + cardCaptains.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptains.Power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardColonelsService.UpdateTeamCardColonels(null, null, dropHandler.card_id);
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.Id);
                if (cardColonels.Power >= dropHandler.card_power)
                {
                    double diffPower = cardColonels.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardColonels.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.Id);
                double updatedPower = currentPower + cardColonels.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonels.Power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardGeneralsService.UpdateTeamCardGenerals(null, null, dropHandler.card_id);
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.Id);
                if (cardGenerals.Power >= dropHandler.card_power)
                {
                    double diffPower = cardGenerals.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardGenerals.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.Id);
                double updatedPower = currentPower + cardGenerals.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardGenerals.Power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, dropHandler.card_id);
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.Id);
                if (cardAdmirals.Power >= dropHandler.card_power)
                {
                    double diffPower = cardAdmirals.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardAdmirals.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.Id);
                double updatedPower = currentPower + cardAdmirals.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmirals.Power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardMonstersService.UpdateTeamCardMonsters(null, null, dropHandler.card_id);
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.Id);
                if (cardMonsters.Power >= dropHandler.card_power)
                {
                    double diffPower = cardMonsters.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardMonsters.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.Id);
                double updatedPower = currentPower + cardMonsters.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonsters.Power, 1);
            }
        }
        else if (obj is CardMilitaries cardMilitary)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardMilitaryService.UpdateTeamCardMilitary(null, null, dropHandler.card_id);
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.Id);
                if (cardMilitary.Power >= dropHandler.card_power)
                {
                    double diffPower = cardMilitary.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardMilitary.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.Id);
                double updatedPower = currentPower + cardMilitary.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
            }
        }
        else if (obj is CardSpells cardSpell)
        {
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardSpellService.UpdateTeamCardSpell(null, null, dropHandler.card_id);
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.Id);
                if (cardSpell.Power >= dropHandler.card_power)
                {
                    double diffPower = cardSpell.Power - dropHandler.card_power;
                    double updatedPower = currentPower + diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.card_power - cardSpell.Power;
                    double updatedPower = currentPower - diffPower;

                    UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.Id);
                double updatedPower = currentPower + cardSpell.Power;
                UserService.Create().UpdateUserPower(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardSpell.Power, 1);
            }
        }
    }
}