using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void Level1 () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Level2 () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
   }

   public void Level3 () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
   }

   public void Level4 () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
   }

   public void Back () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
   }
}