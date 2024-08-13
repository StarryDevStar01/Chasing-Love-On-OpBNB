using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }

    public Button NormalBtn;
    public TextMeshProUGUI NormalBtnText;
    public Button NFTBtn;
    public TextMeshProUGUI NFTBtnText;

    private void Start()
    {
        NormalBtn.gameObject.SetActive(false);
        NFTBtn.gameObject.SetActive(false);
    }

    public async void IsPlayerHaveGatePass()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        //For Testing
        Address = "0x4C6F5f4e21840f1e103fF8791AC70b4Ff1AD59f9";
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x07d915EBD9815136D5828FDaEdb2632f8385A6A3",
            "[{\"type\":\"event\",\"name\":\"interactionCountEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"openGateEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"scoreSavingEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newSaving\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );
        bool isPlayerHaveGatePass = await contract.Read<bool>("hasOpenGate", Address);

        if (isPlayerHaveGatePass == true)
        {
            NormalBtn.gameObject.SetActive(true);
            NFTBtn.gameObject.SetActive(false);
        }
        else
        {
            NormalBtn.gameObject.SetActive(false);
            NFTBtn.gameObject.SetActive(true);
        }
    }

    public async void PlayerGetPass()
    {
        Debug.Log("PlayerGetPass");
        NFTBtn.interactable = false;
        NFTBtnText.text = "Getting Pass!";
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x07d915EBD9815136D5828FDaEdb2632f8385A6A3",
            "[{\"type\":\"event\",\"name\":\"interactionCountEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"openGateEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"scoreSavingEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newSaving\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );
        await contract.Write("claimOpenGate", Address);
        NormalBtn.gameObject.SetActive(true);
        NFTBtn.gameObject.SetActive(false);
    }

    public async void PlayGame()
    {
        Debug.Log("PlayGame");
        NormalBtn.interactable = false;
        NormalBtnText.text = "Loading!";
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x07d915EBD9815136D5828FDaEdb2632f8385A6A3",
            "[{\"type\":\"event\",\"name\":\"interactionCountEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"openGateEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"scoreSavingEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newSaving\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );
        await contract.Write("giveInteractionCount", Address);
        NormalBtn.interactable = true;
        NormalBtnText.text = "PLAY";
        SceneManager.LoadScene(1);
    }

}
