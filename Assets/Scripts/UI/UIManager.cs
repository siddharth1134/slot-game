using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the slot game's user interface.
/// Displays player balance, bet, and win information.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField]
    private SlotGameManager slotGameManager;

    [Header("UI References")]
    [SerializeField]
    private TMP_Text balanceText;

    [SerializeField]
    private TMP_Text betText;

    [SerializeField]
    private TMP_Text winText;

    [SerializeField]
    private Button spinButton;


    private void Start()
    {
        if (slotGameManager == null)
        {
            Debug.LogError("SlotGameManager is not assigned in UIManager.");
            return;
        }

        UpdateUI();
    }


    /// <summary>
    /// Updates all player-related UI elements.
    /// </summary>
    public void UpdateUI()
    {
        if (slotGameManager == null)
        {
            return;
        }

        PlayerData playerData = slotGameManager.PlayerData;

        if (playerData == null)
        {
            return;
        }

        UpdateBalance(playerData.Balance);
        UpdateBet(playerData.Bet);
    }


    /// <summary>
    /// Updates the balance text in the UI.
    /// </summary>
    private void UpdateBalance(int balance)
    {
        if (balanceText != null)
        {
            balanceText.text = $"Balance: {balance}";
        }
    }


    /// <summary>
    /// Updates the bet display.
    /// </summary>
    private void UpdateBet(int bet)
    {
        if (betText != null)
        {
            betText.text = $"Bet: {bet}";
        }
    }


    /// <summary>
    /// Displays the latest win amount in the UI.
    /// </summary>
    public void ShowWin(int winAmount)
    {
        if (winText != null)
        {
            winText.text = $"Win: {winAmount}";
        }
    }


    /// <summary>
    /// Enables or disables the Spin button.
    /// </summary>
    public void SetSpinButtonInteractable(bool interactable)
    {
        if (spinButton != null)
        {
            spinButton.interactable = interactable;
        }
    }
}