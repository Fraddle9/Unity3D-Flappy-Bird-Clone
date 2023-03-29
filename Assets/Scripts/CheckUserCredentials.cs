using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class CheckUserCredentials : MonoBehaviour
{

    [SerializeField] private string playerEmail;
    [SerializeField] private string playerPassword;
    [SerializeField] private string playerUsername;
    [SerializeField] private GameObject recoveryButton;
    public bool playerRecovered;


    // Start is called before the first frame update
    void Start()
    {
        playerRecovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUserCredentials()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoEmailSuccess, OnGetFailure);
        CheckCredentials();
    }

    public void OnGetAccountInfoEmailSuccess(GetAccountInfoResult result)
    {
        playerEmail = result.AccountInfo.PrivateInfo.Email;
        playerUsername = result.AccountInfo.Username;

        playerRecovered = true;

        Debug.Log("Success : " + result.AccountInfo.PrivateInfo.Email);

    }

    public void OnGetFailure(PlayFabError error)
    {

        Debug.Log("failure : " + error);

    }

    public void CheckCredentials()
    {
        if (playerEmail != null)
        {
            recoveryButton.SetActive(false);
        }
    }
}


