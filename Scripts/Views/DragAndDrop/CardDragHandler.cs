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
        if (obj is CardHeroes cardHeroes)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardHeroesService.UpdateTeamCardHeroes(null, null, dropHandler.card_id);
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.id);
                if (cardHeroes.power >= dropHandler.card_power)
                {
                    double newPower = cardHeroes.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardHeroes.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardHeroesService.UpdateTeamCardHeroes(team_id, position, cardHeroes.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardHeroes.power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardCaptainsService.UpdateTeamCardCaptains(null, null, dropHandler.card_id);
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.id);
                if (cardCaptains.power >= dropHandler.card_power)
                {
                    double newPower = cardCaptains.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardCaptains.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardCaptainsService.UpdateTeamCardCaptains(team_id, position, cardCaptains.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardCaptains.power, 1);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardColonelsService.UpdateTeamCardColonels(null, null, dropHandler.card_id);
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.id);
                if (cardColonels.power >= dropHandler.card_power)
                {
                    double newPower = cardColonels.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardColonels.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardColonelsService.UpdateTeamCardColonels(team_id, position, cardColonels.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardColonels.power, 1);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardGeneralsService.UpdateTeamCardGenerals(null, null, dropHandler.card_id);
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.id);
                if (cardGenerals.power >= dropHandler.card_power)
                {
                    double newPower = cardGenerals.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardGenerals.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardGeneralsService.UpdateTeamCardGenerals(team_id, position, cardGenerals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardGenerals.power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(null, null, dropHandler.card_id);
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.id);
                if (cardAdmirals.power >= dropHandler.card_power)
                {
                    double newPower = cardAdmirals.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardAdmirals.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardAdmiralsService.UpdateTeamCardAdmirals(team_id, position, cardAdmirals.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardAdmirals.power, 1);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardMonstersService.UpdateTeamCardMonsters(null, null, dropHandler.card_id);
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.id);
                if (cardMonsters.power >= dropHandler.card_power)
                {
                    double newPower = cardMonsters.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardMonsters.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMonstersService.UpdateTeamCardMonsters(team_id, position, cardMonsters.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMonsters.power, 1);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardMilitaryService.UpdateTeamCardMilitary(null, null, dropHandler.card_id);
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.id);
                if (cardMilitary.power >= dropHandler.card_power)
                {
                    double newPower = cardMilitary.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardMilitary.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardMilitaryService.UpdateTeamCardMilitary(team_id, position, cardMilitary.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardMilitary.power, 1);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            if (!string.IsNullOrEmpty(dropHandler.card_id))
            {
                userCardSpellService.UpdateTeamCardSpell(null, null, dropHandler.card_id);
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.id);
                if (cardSpell.power >= dropHandler.card_power)
                {
                    double newPower = cardSpell.power - dropHandler.card_power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 1);
                }
                else
                {
                    double newPower = dropHandler.card_power - cardSpell.power;
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower, 0);
                }
            }
            else
            {
                userCardSpellService.UpdateTeamCardSpell(team_id, position, cardSpell.id);
                FindObjectOfType<Power>().ShowPower(currentPower, cardSpell.power, 1);
            }
        }
    }
}