using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeLogic : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitFacebook()
    {
        FB.Init();
    }

    public UnityEvent loggingIn;
    public UnityEvent afterLoginSuccess;
    public UnityEvent afterLogout;

    public void Start() 
    {
        if (FB.IsLoggedIn){
            afterLoginSuccess.Invoke();
        }
    }

    public void Login()
    {
        loggingIn.Invoke();
        string[] permissions = { "public_profile","email" };
        FB.LogInWithReadPermissions(permissions, result =>
        {
            // Some platforms return the empty string instead of null.
            if (!string.IsNullOrEmpty(result.Error))
            {
                //error
            }
            else if (result.Cancelled)
            {
                //cancel
            }
            else if (!string.IsNullOrEmpty(result.RawResult))
            {
                //success
                afterLoginSuccess.Invoke();
            }
            else
            {
                //empty
            }
        });
    }

    public RawImage profileImage;
    public TextMeshProUGUI facebookName;
    public void GetFacebookName()
    {
         FB.API("/me?fields=name", HttpMethod.GET, result =>
         {
            if (!string.IsNullOrEmpty(result.RawResult))
            {
                facebookName.text = (string)result.ResultDictionary["name"];
            }
         });
    }

    public void GetProfileImage()
    {
         FB.API("/me/picture?type=large", HttpMethod.GET, result =>
         {
            if (!string.IsNullOrEmpty(result.RawResult))
            {
                Debug.Log("Got profile image!! " + result.RawResult);
                profileImage.texture = result.Texture;
            }
         });
    }

    public void ShareTest()
    {
        FB.ShareLink(new Uri("https://google.com"),"Find The Rainbow","ชวนเพื่อนมาเล่นเกมสายรุ้งกัน!",null,null);
    }

    public void Logout()
    {
        FB.LogOut();
        afterLogout.Invoke();
    }

    public void nextscene (){
        SceneManager.LoadScene("story1");
    }
}
