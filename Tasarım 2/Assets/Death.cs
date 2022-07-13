using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour

{
 public void tekrardene() 
 {  
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
 }
 public void giveup()
 {
     SceneManager.LoadScene(0);
 }
}
