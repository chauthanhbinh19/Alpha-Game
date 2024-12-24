using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button signInButton;
    public Button signUpButton;
    public GameObject signInPanel;
    public GameObject signUpPanel;
    public GameObject createNamePanel;
    public GameObject MainPanel;
    public GameObject WaitingPanel;
    public Text ErrorUsername;
    public Text ErrorPassword;
    void Start()
    {
        signUpButton.onClick.AddListener(signUpButtonClicked);
        signInButton.onClick.AddListener(signInButtonClicked);
    }

    void Update()
    {
        
    }
    public void signUpButtonClicked(){
        signUpPanel.SetActive(!signUpPanel.activeSelf);
        signInPanel.SetActive(!signInPanel.activeSelf);
    }
    public void signInButtonClicked(){
        string username=usernameInput.text;
        string password=passwordInput.text;
        User user=new User(username,password,createNamePanel, signInPanel);
        int status=user.SignInUser();
        if(status==1){
            MainPanel.SetActive(true);
            WaitingPanel.SetActive(false);
        }else if(status==0){
            ErrorUsername.text="Your account does not exist";
        }
    }
}
