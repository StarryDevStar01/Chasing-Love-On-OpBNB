using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreBlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }
    public Button savingScoreBtn;
    public TextMeshProUGUI savingScoreBtnTxt;
    public Button replayBtn;
    public TextMeshProUGUI replayBtnText;


    public async void SavingPlayerScore()
    {
        Debug.Log("SavingPlayerScore");
        savingScoreBtn.interactable = false;
        savingScoreBtnTxt.text = "Saving HighestScore!";
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x07d915EBD9815136D5828FDaEdb2632f8385A6A3",
            "[{\"type\":\"event\",\"name\":\"interactionCountEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"openGateEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"scoreSavingEvent\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newSaving\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveInteractionCount\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveScoreSaving\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpenGate\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );
        await contract.Write("giveScoreSaving", Address, ScoreManager.instance.highScore);
        savingScoreBtnTxt.text = "Highest Score: " + ScoreManager.instance.highScore.ToString();
        replayBtn.gameObject.SetActive(true);
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
