using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    

    public string[] sceneNames; 

    bool load; 

    protected override void OnCollide(Collider2D coll) 
    {

        if(coll.name == "player_0")
        {
            // Teleport Player
            GameManager.instance.SaveState();
            
            if(!load)
            {
                load = true; 

                StartCoroutine(ChangeScene());
            }




        }
    }

    IEnumerator ChangeScene()
    {
        // Reset and declare as public variable to determine scene loading 
        int nextSceneIndex = 1; 

        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Additive);

        Scene nextScene = SceneManager.GetSceneAt(1);

        // TP Player
        SceneManager.MoveGameObjectToScene(GameObject.Find("player_0"), nextScene);
        // TP Camera 
        SceneManager.MoveGameObjectToScene(GameObject.Find("MainCamera"), nextScene);

        SceneManager.MoveGameObjectToScene(GameObject.Find("GameManager"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.Find("Canvas"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.Find("EventSystem"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.Find("Menu"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.Find("Hud"), nextScene);

        yield return null; 

        SceneManager.UnloadScene(nextSceneIndex - 1);


    }

}
