using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;


public class PokeMoves : MonoBehaviour
{
    public string name;
    public string type;
    public string maxxPP;
    public int movePower;
    public string accuracy;

    private const string URL = "https://pokeapi.co/api/v2/move/1";
    private const string HOST = "https://pokeapi.co/api/v2/";

    [SerializeField] DialougeControl DC;
    [SerializeField] Unit Enemy;
    [SerializeField] Unit Player;

    public void PlayMoves()
    {
        StartCoroutine(GetMoves(URL));
    }

    private IEnumerator GetMoves(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {

            request.SetRequestHeader("X-RapidAPI-Host", HOST);
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ERROR: " + request.error);

            }
            else
            {

                JSONNode MoveData = JSON.Parse(request.downloadHandler.text);
                
                string MoveName = MoveData["name"];
                string MoveType = MoveData["type"]["name"];
                string MoveAccuracy = MoveData["accuracy"];
                string PP = MoveData["pp"];
                int power = MoveData["power"];

                Debug.Log("Generated Pokemon Move:" +
                    $"Name: {MoveName}" +
                    $"Type: {MoveType}" +
                    $"Accuracy: {MoveAccuracy}" +
                    $"Max PP: {PP}" +
                    $"Power: {power}");

                name = MoveName;
                type = MoveType;
                maxxPP = PP;
                movePower = power;

                Enemy.damage = movePower;
                Player.damage = movePower;

                DC.UpdateMoveDetails(MoveName, MoveType, PP);

            }
        }


    }

    public class MoveStore
    {
        public List<PokeMoves> Moves;
    }
    
}






    






