using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject ArenaDetailsPanelPrefab;
    private GameObject ArenaSlotPrefab;
    private GameObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ArenaDetailsPanelPrefab = UIManager.Instance.GetGameObject("ArenaDetailsPanelPrefab");
        ArenaSlotPrefab = UIManager.Instance.GetGameObject("ArenaSlotPrefab");
    }
    public void CreateArenaButton(Transform arenaMenuPanel)
    {
        Button[] buttons = arenaMenuPanel.gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            button.onClick.AddListener(() => { CreateArenaDetails(buttonName); });
        }
    }
    public void CreateArenaDetails(string type)
    {
        currentObject = Instantiate(ArenaDetailsPanelPrefab, MainPanel);
        RawImage avatarImage = currentObject.transform.Find("DictionaryCards/AvatarImage").GetComponent<RawImage>();
        RawImage borderImage = currentObject.transform.Find("DictionaryCards/BorderImage").GetComponent<RawImage>();
        Transform arenaSlotGroup = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TextMeshProUGUI rankText = currentObject.transform.Find("DictionaryCards/RankText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI rankPointText = currentObject.transform.Find("DictionaryCards/RankPointText").GetComponent<TextMeshProUGUI>();
        Texture avatarTexture = Resources.Load<Texture>($"{User.CurrentUserAvatar.Replace(".png", "").Replace(".jpg", "")}");
        Texture borderTexture = Resources.Load<Texture>($"{User.CurrentUserBorder.Replace(".png", "").Replace(".jpg", "")}");
        avatarImage.texture = avatarTexture;
        borderImage.texture = borderTexture;
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        // Arena arena = new Arena();
        IArenaRepository arenaRepository = new ArenaRepository();
        ArenaService arenaService = new ArenaService(arenaRepository);
        string arena_id = arenaService.GetArenaModeId(type);
        int rank_point = arenaService.GetArenaParticipantPoint(User.CurrentUserId, arena_id);
        rankPointText.text = rank_point.ToString();

        //Lấy xếp hạng của người chơi
        var rankings = arenaService.GetArenaParticipantByRanking(arena_id);
        foreach (var pair in rankings)
        {
            if (pair.Key.Equals(User.CurrentUserId))
            {
                // Nếu là người chơi hiện tại, hiển thị thông tin cá nhân
                avatarImage.texture = avatarTexture;
                borderImage.texture = borderTexture;
                rankText.text = pair.Value.ToString();
            }
            else if (pair.Key.Equals("0")) // Trường hợp không có người chơi nào
            {
                rankText.text = pair.Value.ToString();
            }
            else
            {
                GameObject arenaSlotObject = Instantiate(ArenaSlotPrefab, arenaSlotGroup);

                IUserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);
                User user = userService.GetUserById(pair.Key.ToString());
                
                RawImage arenaAvatarImage = arenaSlotObject.transform.Find("AvatarImage").GetComponent<RawImage>();
                RawImage arenaBorderImage = arenaSlotObject.transform.Find("BorderImage").GetComponent<RawImage>();
                Texture arenaAvatarTexture = Resources.Load<Texture>($"{User.CurrentUserAvatar.Replace(".png", "").Replace(".jpg", "")}");
                Texture arenaBorderTexture = Resources.Load<Texture>($"{User.CurrentUserBorder.Replace(".png", "").Replace(".jpg", "")}");
                arenaAvatarImage.texture = arenaAvatarTexture;
                arenaBorderImage.texture = arenaBorderTexture;
                TextMeshProUGUI arenaRankText = arenaSlotObject.transform.Find("RankText").GetComponent<TextMeshProUGUI>();
                arenaRankText.text = pair.Value.ToString();
                TextMeshProUGUI arenaTitleText = arenaSlotObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                arenaTitleText.text = user.name;
                TextMeshProUGUI arenaLevelText = arenaSlotObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
                arenaLevelText.text = user.level.ToString();
                TextMeshProUGUI arenaPowerText = arenaSlotObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
                arenaPowerText.text = user.power.ToString();
            }
        }
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
