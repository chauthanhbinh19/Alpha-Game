using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateName : MonoBehaviour
{
    public InputField nameInput;
    public Button start;
    public GameObject createNamePanel;
    public GameObject MainPanel;
    public GameObject WaitingPanel;
    public Transform userPanel;
    public Transform currencyPanel;
    public GameObject currencyPrefab;
    void Start()
    {
        start.onClick.AddListener(createNameButtonClicked);
    }

    void Update()
    {
        
    }
    public void createNameButtonClicked(){
        string userName=nameInput.text;
        User user=new User(createNamePanel);
        user.UpdateUserName(userName);
        User loggedInUser = user.SignInUser();
        if (loggedInUser != null)
        {
            // Đăng nhập thành công, hiển thị MainPanel và ẩn WaitingPanel
            MainPanel.SetActive(true);
            WaitingPanel.SetActive(false);

            // Lưu thông tin người dùng vào nơi bạn muốn, ví dụ lưu vào một biến static hoặc quản lý trong game
            // Ví dụ: lưu vào UserManager
            // UserManager.Instance.InitializeUser(loggedInUser);
            Text nameText = userPanel.transform.Find("NameText").GetComponent<Text>();
            nameText.text = loggedInUser.name;
            Text levelText = userPanel.transform.Find("LevelText").GetComponent<Text>();
            levelText.text = loggedInUser.level.ToString();
            Text powerText = userPanel.transform.Find("PowerText").GetComponent<Text>();
            powerText.text = loggedInUser.power.ToString();
            RawImage avatarImage = userPanel.transform.Find("AvatarImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = loggedInUser.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            avatarImage.texture=texture;
            
            foreach (var currency in loggedInUser.Currencies)
            {
                if (currency.name.Equals("Diamond") || currency.name.Equals("Gold") || currency.name.Equals("Silver"))
                {
                    GameObject currencyObject = Instantiate(currencyPrefab, currencyPanel);
                    RawImage currencyImage = currencyObject.transform.Find("Image").GetComponent<RawImage>();
                    string currencyWithoutExtension = currency.image.Replace(".png", "");
                    Texture currencyTexture = Resources.Load<Texture>($"{currencyWithoutExtension}");
                    currencyImage.texture = currencyTexture;

                    Text currencyQuantity = currencyObject.transform.Find("Content").GetComponent<Text>();
                    currencyQuantity.text = currency.quantity.ToString();
                }
            }
        }
        else
        {
            // Đăng nhập thất bại, hiển thị thông báo lỗi
            Debug.Log("Your account does not exist");
        }
    }
}
