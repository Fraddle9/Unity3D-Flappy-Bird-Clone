using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    private string registeredEmail;
    private string registeredUsername;
    private string registeredDevices;

    public bool playerRecovered;
    private string userEmail;
    private string userPassword;
    private string username;
    private bool loggedInWithEmail;

    public GameObject addLoginPanel;
    public GameObject recoveryButton;

    public CheckUserCredentials CUC;
    public PlayFabController PFC;


    public void Start()
    {
        loggedInWithEmail = false;
        //PlayerPrefs.DeleteAll();

        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "7CF13"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //PlayerPrefs.DeleteAll();
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginEmailSuccess, OnLoginEmailFailure);
            //loggedInWithEmail = true;
            
        }
        else
        {
#if UNITY_ANDROID
            Debug.Log("Android Login");
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);

            
#endif
#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
#endif
        }
    }

    #region Login
    private void OnLoginEmailSuccess(LoginResult result)
    {
        Debug.Log("Email Login Success");
        if (!PlayerPrefs.HasKey("EMAIL"))
        {
            PlayerPrefs.SetString("EMAIL", userEmail);
            PlayerPrefs.SetString("PASSWORD", userPassword);
        }
        
        recoveryButton.SetActive(false);
        GetUserCredentials();
        PFC.GetStats();
        //SceneManager.LoadScene(0);

    }

    private void OnLoginMobileSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        GetUserCredentials();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("User Registered.");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
    }

    private void OnLoginEmailFailure(PlayFabError error)
    {
        // BU SANIRIM O EMAÝLLE GÝRÝÞTE SIKINTI OLURSA KAYIT OLSUN DÝYE VARDI. KALDIRDIM.
        //var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);

        Debug.Log(error.GenerateErrorReport());
    }

    private void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }


    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginEmailSuccess, OnLoginEmailFailure);
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    public void OpenAddLogin()
    {
        addLoginPanel.SetActive(true);
    }

    public void OnClickAddLogin()
    {
        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoginSuccess, OnRegisterFailure);
    }

    private void OnAddLoginSuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        SceneManager.LoadScene(0);
    }

    public void GetUserCredentials()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetUserCredentialsSuccess, OnGetUserCredentialsFailure);
        
    }

    public void OnGetUserCredentialsSuccess(GetAccountInfoResult result)
    {
        UserAndroidDeviceInfo registeredDevice;

        registeredEmail = result.AccountInfo.PrivateInfo.Email;
        registeredDevice = result.AccountInfo.AndroidDeviceInfo;
        registeredUsername = result.AccountInfo.Username;

        playerRecovered = true;

        Debug.Log("Success : " + "Login Email Address: " + registeredEmail + ", Registered Device ID: " + registeredDevice.AndroidDeviceId);
        CheckCredentials();

    }

    public void OnGetUserCredentialsFailure(PlayFabError error)
    {

        Debug.Log("failure : " + error);
        CheckCredentials();

    }

    public void CheckCredentials()
    {
        if (registeredEmail != null)
        {
            Debug.Log("Checked Credentials");
            recoveryButton.SetActive(false);
        }
    }
}
#endregion