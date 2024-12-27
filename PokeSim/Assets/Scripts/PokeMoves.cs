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
    public int moveLevel;
    public string accuracy;

    private const string URL = "https://pokeapi.co/api/v2/move/";
    private const string HOST = "https://pokeapi.co/api/v2/";

    [SerializeField] DialougeControl DC;
    [SerializeField] Unit Enemy;
    [SerializeField] Unit Player;

    public PokeAPI pokeApi;

    public List<String> availableMoves = new List<String>();

    public void PlayMoves()
    {
        StartCoroutine(GetMoves(pokeApi.Player.unitName));
        Debug.Log("Player Move: " + pokeApi.Player.unitName);
    }


    //Gets the moves the Pokemon can use based on it Level
    private IEnumerator GetMoves(string pokeName)
    {
        string url = HOST + "pokemon/" + pokeName.ToLower();

        //string url = HOST + pokeName;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {

            request.SetRequestHeader("X-RapidAPI-Host", HOST);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ERROR: " + request.error);

            }
            else
            {

                JSONNode PokeData = JSON.Parse(request.downloadHandler.text);
                JSONArray movesArray = PokeData["moves"].AsArray;

                availableMoves.Clear();

                foreach (JSONNode moveNode in movesArray)
                {
                    string MoveName = moveNode["move"]["name"];
                    JSONArray versionDetails = moveNode["version_group_details"].AsArray;

                    int learnedAtLevel = int.MaxValue;

                    foreach (JSONNode versionNode in versionDetails)
                    {
                        if (versionNode["move_learn_method"]["name"] == "level-up")
                        {
                            int currentLevel = versionNode["level_learned_at"];

                            if (currentLevel < learnedAtLevel)
                            {
                                learnedAtLevel = currentLevel;
                            }

                        }
                    }

                    if (learnedAtLevel <= pokeApi.Player.unitLevel)
                    {
                        StartCoroutine(GetMoveType(MoveName));
                    }

                    //string MoveName = PokeData["moves"][0]["move"]["name"];
                    //string MoveLevel = PokeData["moves"][0]["version_group_details"][0]["level_learned_at"];

                    //Debug.Log("Move Name: " + MoveName);
                    //Debug.Log("Move Level: " + MoveLevel);
                    //UpdateMoveUI();
                    //DC.UpdateMoveDetails(MoveName, MoveType, PP);  

                }
            }
        }
    }


        private IEnumerator GetMoveType(string moveName) {
        
        string url = URL + moveName.ToLower();

        using (UnityWebRequest request = UnityWebRequest.Get(url))
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

                string MoveType = MoveData["type"]["name"];
                string PP = MoveData["pp"];
                int movePower = MoveData["power"];
                string accuracy = MoveData["accuracy"];

                PokeMoves pokeMoves = new PokeMoves
                {
                    name = moveName,
                    type = MoveType,
                    maxxPP = PP,
                    movePower = movePower,
                    accuracy = accuracy
                };

                availableMoves.Add(moveName);
                UpdateMoveUI();

                Debug.Log("Move Type: " + MoveType);
                Debug.Log("PP: " + PP);
            }
        }

        }

    private void UpdateMoveUI()
    {
        DC.UpdateMoveDetails(availableMoves);
    }
}














