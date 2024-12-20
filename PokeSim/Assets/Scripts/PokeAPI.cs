using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEditor.PackageManager.Requests;
using TMPro;
using System.Security.Cryptography;

public class PokeAPI : MonoBehaviour
{

    private const string URL = "https://pokeapi.co/api/v2/pokemon/";
    private const string HOST = "https://pokeapi.co/api/v2/";

    //[SerializeField] private TMP_Text PokeUIText;
    //[SerializeField] private RawImage PokeIcon;

    public Unit Player;
    public Unit Enemy;

    public IEnumerator GenerateRequest()
    {
        int randomNum = Random.Range(1, 899);
        string randomPokemon = URL + randomNum.ToString();
        
        yield return ProcessRequest(randomPokemon);
    }

    public void AssignUnits(Unit player, Unit enemy)
    {
        Player = player;
        Enemy = enemy;
    }

    public IEnumerator ProcessRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            
            request.SetRequestHeader("X-RapidAPI-Host", HOST);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ERROR: " + request.error);
        
            } else {  
                JSONNode PokeData = JSON.Parse(request.downloadHandler.text);
                
                //Debug.Log($"Generated Pokemon is:  \nName: " + PokeData["name"]);
                //Debug.Log($"Type:{PokeData["types"][0]["type"]["name"]}");
                
                string poke_name = PokeData["name"];
                string enemy_name = PokeData["name"];
                string poke_image_url = PokeData["sprites"]["front_shiny"];

                
                UnityWebRequest pokeImageRequest = UnityWebRequestTexture.GetTexture(poke_image_url);
                
                yield return pokeImageRequest.SendWebRequest();
            
                if(pokeImageRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Image download error: " + pokeImageRequest.error);
                    Debug.LogError(pokeImageRequest.error);
                    
                    yield break;
                }
                  
                    Player.unitName = poke_name;
                    Enemy.unitName = enemy_name;

                    Texture2D texture = DownloadHandlerTexture.GetContent(pokeImageRequest);
                    
                    Player.pokeImage.texture = texture;
                    Enemy.pokeImage.texture = texture;
                    
                    Player.pokeImage.texture.filterMode = FilterMode.Point;
                    Enemy.pokeImage.texture.filterMode = FilterMode.Point;
            }
        }
      
    }




}
