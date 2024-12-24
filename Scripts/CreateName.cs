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
        MainPanel.SetActive(true);
        WaitingPanel.SetActive(false);
    }
}
