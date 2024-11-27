using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using SimpleJSON;


public class PokeMoves : MonoBehaviour
{
    private string name;
    private string type;
    private string maxxPP;
    private string power;
    private string accuracy;

    private const string URL = "https://pokeapi.co/api/v2/move/1";
    private const string HOST = "https://pokeapi.co/api/v2/";


    DialougeControl DC;
    
    //public IEnumerator GenerateMoves()
    //{
    //    yield return GetMoves(URL);
    //}

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
                string power = MoveData["power"];

                Debug.Log($"Generated Pokemon Move:\n" +
                    $"Name: {MoveName}\n" +
                    $"Type: {MoveType}\n" +
                    $"Accuracy: {MoveAccuracy}\n" +
                    $"Max PP: {PP}\n" +
                    $"Power: {power}");


                name = MoveName;
                type = MoveType;
                maxxPP = PP;


            }
        }


    }
    
}






    






