using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UI;
using System.Threading.Tasks;

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
    UserCardMilitariesService userCardMilitaryService;
    UserCardSpellsService userCardSpellService;
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
        userCardMilitaryService = UserCardMilitariesService.Create();
        userCardSpellService = UserCardSpellsService.Create();
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
        _ = FindAndDropCardAsync();
    }
    private async Task FindAndDropCardAsync()
    {
        // Tìm danh sách các vị trí (GameObject)
        // Transform positionPanel = transform.parent; // Giả sử CardDragHandler nằm trong một panel chứa các vị trí
        CardDropHandler[] dropHandlers = positionPanel.GetComponentsInChildren<CardDropHandler>();

        foreach (CardDropHandler dropHandler in dropHandlers)
        {
            RawImage positionImage = dropHandler.transform.Find("Image").GetComponent<RawImage>();
            Texture texture = TextureHelper.LoadTextureCached($"UI/Background4/Background_V4_408");
            if (positionImage.texture == texture) // Kiểm tra xem vị trí có trống không
            {
                // Thêm thẻ vào vị trí này
                await DropCardIntoPositionAsync(dropHandler);
                return; // Thoát khỏi vòng lặp sau khi tìm thấy vị trí trống đầu tiên
            }
        }

        // Nếu không tìm thấy vị trí trống nào, thông báo hoặc thực hiện hành động khác
        Debug.Log("No empty position available.");
    }
    public async Task DropCardIntoPositionAsync(CardDropHandler dropHandler)
    {
        // Kiểm tra xem liệu có texture được gán không
        RawImage positionImage = dropHandler.transform.Find("Image").GetComponent<RawImage>();
        if (positionImage != null)
        {
            positionImage.texture = this.cardTexture;
            // Debug.Log("Card dropped: " + draggedCard.gameObject.name + " at position: " + gameObject.name);
        }
        object obj = this.obj;
        string position = mainPosition + "-" + dropHandler.PositionId;
        double currentPower = User.CurrentUserPower;
        if (obj is Equipments cardHero)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardHeroesService.UpdateTeamCardHeroAsync(null, null, dropHandler.CardId);
                await userCardHeroesService.UpdateTeamCardHeroAsync(team_id, position, cardHero.Id);
                if (cardHero.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardHero.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardHero.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardHeroesService.UpdateTeamCardHeroAsync(team_id, position, cardHero.Id);
                double updatedPower = currentPower + cardHero.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardHero.Power, 1);
            }
        }
        else if (obj is CardCaptains cardCaptain)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(null, null, dropHandler.CardId);
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(team_id, position, cardCaptain.Id);
                if (cardCaptain.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardCaptain.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardCaptain.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardCaptainsService.UpdateTeamCardCaptainAsync(team_id, position, cardCaptain.Id);
                double updatedPower = currentPower + cardCaptain.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardCaptain.Power, 1);
            }
        }
        else if (obj is CardColonels cardColonel)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardColonelsService.UpdateTeamCardColonelAsync(null, null, dropHandler.CardId);
                await userCardColonelsService.UpdateTeamCardColonelAsync(team_id, position, cardColonel.Id);
                if (cardColonel.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardColonel.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardColonel.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardColonelsService.UpdateTeamCardColonelAsync(team_id, position, cardColonel.Id);
                double updatedPower = currentPower + cardColonel.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardColonel.Power, 1);
            }
        }
        else if (obj is CardGenerals cardGeneral)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(null, null, dropHandler.CardId);
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(team_id, position, cardGeneral.Id);
                if (cardGeneral.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardGeneral.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardGeneral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardGeneralsService.UpdateTeamCardGeneralAsync(team_id, position, cardGeneral.Id);
                double updatedPower = currentPower + cardGeneral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardGeneral.Power, 1);
            }
        }
        else if (obj is CardAdmirals cardAdmiral)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(null, null, dropHandler.CardId);
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(team_id, position, cardAdmiral.Id);
                if (cardAdmiral.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardAdmiral.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardAdmiral.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardAdmiralsService.UpdateTeamCardAdmiralAsync(team_id, position, cardAdmiral.Id);
                double updatedPower = currentPower + cardAdmiral.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardAdmiral.Power, 1);
            }
        }
        else if (obj is CardMonsters cardMonster)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardMonstersService.UpdateTeamCardMonsterAsync(null, null, dropHandler.CardId);
                await userCardMonstersService.UpdateTeamCardMonsterAsync(team_id, position, cardMonster.Id);
                if (cardMonster.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardMonster.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardMonster.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardMonstersService.UpdateTeamCardMonsterAsync(team_id, position, cardMonster.Id);
                double updatedPower = currentPower + cardMonster.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMonster.Power, 1);
            }
        }
        else if (obj is CardMilitaries cardMilitary)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(null, null, dropHandler.CardId);
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(team_id, position, cardMilitary.Id);
                if (cardMilitary.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardMilitary.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardMilitary.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardMilitaryService.UpdateTeamCardMilitaryAsync(team_id, position, cardMilitary.Id);
                double updatedPower = currentPower + cardMilitary.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardMilitary.Power, 1);
            }
        }
        else if (obj is CardSpells cardSpell)
        {
            if (!string.IsNullOrEmpty(dropHandler.CardId))
            {
                await userCardSpellService.UpdateTeamCardSpellAsync(null, null, dropHandler.CardId);
                await userCardSpellService.UpdateTeamCardSpellAsync(team_id, position, cardSpell.Id);
                if (cardSpell.Power >= dropHandler.CardPower)
                {
                    double diffPower = cardSpell.Power - dropHandler.CardPower;
                    double updatedPower = currentPower + diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 1);
                }
                else
                {
                    double diffPower = dropHandler.CardPower - cardSpell.Power;
                    double updatedPower = currentPower - diffPower;

                    await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                    User.CurrentUserPower = updatedPower;

                    FindObjectOfType<PowerController>().ShowPower(currentPower, diffPower, 0);
                }
            }
            else
            {
                await userCardSpellService.UpdateTeamCardSpellAsync(team_id, position, cardSpell.Id);
                double updatedPower = currentPower + cardSpell.Power;
                await UserService.Create().UpdateUserPowerAsync(User.CurrentUserId, updatedPower);
                User.CurrentUserPower = updatedPower;

                FindObjectOfType<PowerController>().ShowPower(currentPower, cardSpell.Power, 1);
            }
        }
    }
}