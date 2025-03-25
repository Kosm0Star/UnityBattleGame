using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text damageText;
    public TMP_Text effectText;

    public GameObject victoryPanel;
    public TMP_Text victoryText;
    public Button newBattleButton;

    private bool _isBattleOver = false;
    
    public void ShowDamage(float damage, string attackerName)
    {
        if (damageText != null)
        {
            damageText.text = $"{attackerName} deals {damage} damage!";
        }
    }

    public IEnumerator ShowEffect(string effectMessage, string sourceName)
    {
        if (effectText != null)
        {
            effectText.text = $"{sourceName} : {effectMessage}";
            yield return new WaitForSeconds(1.5f);
            effectText.text = string.Empty;
        }
    }

    public void ShowVictoryScreen(string winnerName)
    {
        if (victoryPanel != null && victoryText != null)
        {
            _isBattleOver = true;
            victoryPanel.SetActive(true);
            victoryText.text = $"{winnerName} Wins!";
        }
    }

    public void HideVictoryScreen()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    public bool IsBattleOver() => _isBattleOver;

    public void ResetBattleState() => _isBattleOver = false;
}
