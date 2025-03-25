using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<Character> allCharacters;
    private Character _player1;
    private Character _player2;

    public UIManager uiManager;

    [SerializeField] private TMP_Text _player1HPText; 
    [SerializeField] private TMP_Text _player2HPText; 

    private bool _isBattleActive = false;

    private void Start()
    {
        foreach (var character in allCharacters)
        {
            if (character != null)
            {
                character.gameObject.SetActive(false);
            }
        }

        StartNewBattle();
    }

    public void SelectRandomCharacters()
    {
        foreach (var character in allCharacters)
        {
            character.gameObject.SetActive(false);

            character.ResetHealth();
        }

        var random = new System.Random();
        var selected = allCharacters.OrderBy(x => random.Next()).Take(2).ToArray();

        _player1 = selected[0];
        _player2 = selected[1];

        _player1.gameObject.SetActive(true);
        _player2.gameObject.SetActive(true);

        _player1.transform.position = new Vector3(-3, 0, 0);
        _player2.transform.position = new Vector3(3, 0, 0);

        _player1.SetHealthText(_player1HPText);
        _player2.SetHealthText(_player2HPText);

        uiManager.HideVictoryScreen();
        _player1.ResetHealth();
        _player2.ResetHealth();
    }

    public void StartBattle()
    {
        _isBattleActive = true;
        StartCoroutine(Battle());
    }

    public void EndBattle() => _isBattleActive = false;

    private IEnumerator Battle()
    {
        while (_isBattleActive)
        {
            _player1.Attack(_player2, uiManager);
            yield return new WaitForSeconds(1f);

            if (!_player2.IsAlive())
            {
                uiManager.ShowVictoryScreen(_player1.GetName());
                EndBattle();
                yield break;
            }

            _player2.Attack(_player1, uiManager);
            yield return new WaitForSeconds(1f);

            if (!_player1.IsAlive())
            {
                uiManager.ShowVictoryScreen(_player2.GetName());
                EndBattle();
                yield break;
            }
        }
    }
    public void StartNewBattle()
    {
        foreach (var character in allCharacters)
        {
            if (character != null)
            {
                character.ResetHealth();
                Debug.Log($"{character.GetName()}: Reset health before new battle");
            }
        }

        uiManager.ResetBattleState();

        SelectRandomCharacters();

        StartBattle();
    }
}
