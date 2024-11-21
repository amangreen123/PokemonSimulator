using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN,ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public PokeAPI pokeData;

    void Start()
    {
        state = BattleState.START;
        pokeData.GenerateRequest();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //this allows me to get access to player and enemy units
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        
        //Debug.Log(playerUnit.pokeImage.texture);

        dialogueText.text = "A Crazy " + enemyUnit.unitName + " approaches";

        Debug.Log(playerUnit.unitName);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        
        PlayerTurn();
    }

    void PlayerTurn ()
    {
        dialogueText.text = "Choose an action";
    }

    IEnumerator PlayerAttack()
    {
        //Damage Enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful";


        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //end the battle
            state = BattleState.WON;
            EndBattle();
        }else
        {
            //Enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
        //check if enemy is dead
        //change state based on 
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You Won the Battle!";

        } else if (state == BattleState.LOST) {

            dialogueText.text = "You Were Defeated";
        }

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();

        } else {

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

   public IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Your Heath is Restored";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
       
        StartCoroutine(EnemyTurn());
   }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}
