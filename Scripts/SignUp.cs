using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
    public Button signUpButton;
    public Button signInButton;
    public GameObject signInPanel;
    public GameObject signUpPanel;
    public Text ErrorUsername;
    public Text ErrorPassword;
    public Text ErrorConfirmPassword;
    void Start()
    {
        signUpButton.onClick.AddListener(onSignUpButtonClicked);
        signInButton.onClick.AddListener(backButtonClicked);
    }

    void Update()
    {
        
    }
    void onSignUpButtonClicked(){
        string username=usernameInput.text;
        string password=passwordInput.text;
        string confirmPassword=confirmPasswordInput.text;

        ErrorUsername.text = "";
        ErrorPassword.text = "";
        ErrorConfirmPassword.text="";

        if(string.IsNullOrEmpty(username)){
            ErrorUsername.text = "Username can not be empty!";
            return;
        }
        if(string.IsNullOrEmpty(password)){
            ErrorPassword.text = "Password can not be empty!";
            return;
        }
        if(string.IsNullOrEmpty(confirmPassword)){
            ErrorConfirmPassword.text = "Confirm password can not be empty!";
            return;
        }

        if(password!=confirmPassword){
            ErrorPassword.text = "Passwords do not match!";
            return;
        }
        
        User newUser=new User(username,password);
        int registerStatus=newUser.RegisterUser();
        if(registerStatus==0){
            ErrorUsername.text="Account already exists!";
        }else if(registerStatus==1){
            usernameInput.text="";
            passwordInput.text="";
            confirmPasswordInput.text="";
            backButtonClicked();
        }
        // Debug.Log(username +" "+password);
    }
    public void backButtonClicked(){
        signUpPanel.SetActive(!signUpPanel.activeSelf);
        signInPanel.SetActive(!signInPanel.activeSelf);
    }
}
