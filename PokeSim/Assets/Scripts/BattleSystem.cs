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

    [SerializeField]
    private PokeMoveData selectedMove;
   

    [SerializeField]
    private Canvas Canvas;

    public DialougeControl dialougeControl;

    void Start()
    {
        state = BattleState.START;
        
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //this allows me to get access to player and enemy units
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        pokeData.AssignUnits(playerUnit,enemyUnit);

        yield return pokeData.GenerateRequest();

        Debug.Log(playerUnit.pokeImage.texture);

        dialogueText.text = "A Crazy " + enemyUnit.unitName + " approaches";

        Debug.Log("Player " + playerUnit.unitName);
        Debug.Log("Enemy " + enemyUnit.unitName);
        
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        
       
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn ()
    {
        dialogueText.text = "Choose an action";
        dialougeControl.EnableActionSelector(true);
        dialougeControl.EnableMoveSelector(false);
    }


    public void OnMoveButton()
    {
        
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //Debug.Log("Selected Move: " + selectedMove.name + " Power: " + selectedMove.movePower);

        dialougeControl.EnableActionSelector(false);
        dialougeControl.EnableMoveSelector(true);
       
        //StartCoroutine(PlayerAttack());
        
    }

    public void MoveSelected(PokeMoveData move)
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        selectedMove = move;
        
        //dialougeControl.EnableActionSelector(false);
        //dialougeControl.EnableMoveSelector(false);

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        int damage = selectedMove.movePower;
        Debug.Log("Selected Move: " + selectedMove.name + " Power: " + damage);
        
        //Damage Enemy
        bool isDead = enemyUnit.TakeDamage(damage);


        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //end the battle
            Debug.Log("Enemy is Dead");
            state = BattleState.WON;
            EndBattle();

        }else {
            //Enemy turn
            Debug.Log("Enemy survived, switching to enemy turn");
            state = BattleState.ENEMYTURN;
            Debug.Log("State after attack: " + state);
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
