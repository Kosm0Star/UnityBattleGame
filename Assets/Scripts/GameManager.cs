using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject warrior;
    public GameObject archer;
    public GameObject wizard;

    public TextMeshProUGUI player1HPText;
    public TextMeshProUGUI player2HPText;

    public GameObject victoryScreen;
    public TextMeshProUGUI victoryText;
    public CanvasGroup victoryCanvasGroup;

    private Character player1;
    private Character player2;
    
    private void Start()
    {
        ClearHPText();

        warrior.SetActive(false); 
        archer.SetActive(false); 
        wizard.SetActive(false);

        int player1Index = Random.Range(0, 3);
        int player2Index = Random.Range(0, 3);

        while (player2Index == player1Index)
        {
            player2Index = Random.Range(0, 3);
        }

        player1 = ActivateCharacter(player1Index, true);
        player2 = ActivateCharacter(player2Index, false);

        StartCoroutine(DelayedUpdateHPText());

        StartCoroutine(Battle());
    }

    private Character ActivateCharacter(int index, bool isPlayer1)
    {
        GameObject characterObject = null;

        if (index == 0)
        {
            characterObject = warrior;
        }
        else if (index == 1)
        {
            characterObject = archer;
        }
        else if (index== 2)
        {
            characterObject = wizard;
        }

        if (characterObject != null)
        {
            Character character = characterObject.GetComponent<Character>();
            character.ResetHealth();

            characterObject.SetActive(true);

            Vector3 position = isPlayer1 ? new Vector3(-3, 0, 0) : new Vector3(3, 0, 0);
            characterObject.transform.position = position;

            return character;
        }

        return null;
    }

    private IEnumerator Battle()
    {
        while (player1.currentHealth > 0 && player2.currentHealth > 0)
        {
            player1.Attack(player2);
            UpdateHPText();
            yield return new WaitForSeconds(1f);

            if (player2.currentHealth > 0)
            {
                player2.Attack(player1);
                UpdateHPText();
                yield return new WaitForSeconds(1f);
            }
        }

        Debug.Log("Battle Ended.");
        EndBattle();
    }

    private void UpdateHPText()
    {
        player1HPText.text = $"{player1.characterName} HP: {player1.currentHealth}";
        player2HPText.text = $"{player2.characterName} HP: {player2.currentHealth}";
    }

    private void EndBattle()
    {
        string winnerName = player1.currentHealth > 0 ? player1.characterName : player2.characterName;

        ShowVictoryScreen(winnerName);
    }

    private void ShowVictoryScreen(string winnerName)
    {
        victoryText.text = $"{winnerName} wins!";
        victoryScreen.SetActive(true);

        victoryCanvasGroup.alpha = 1f;
        victoryCanvasGroup.interactable = true;
        victoryCanvasGroup.blocksRaycasts = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (victoryCanvasGroup != null)
        {
            victoryCanvasGroup.alpha = 0f;
            victoryCanvasGroup.interactable = false;
            victoryCanvasGroup.blocksRaycasts = false;
        }

        ResetCharacters();
    }

    private void ResetCharacters()
    {
        if (warrior != null)
        {
            warrior.SetActive(false);
        }

        if (archer != null)
        {
            archer.SetActive(false);
        }

        if (wizard != null)
        {
            wizard.SetActive(false);
        }
    }

    private void ClearHPText()
    {
        player1HPText.text = "Player1 HP: --";
        player2HPText.text = "Player2 HP: --";
    }

    private IEnumerator DelayedUpdateHPText()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateHPText();
    }
}
