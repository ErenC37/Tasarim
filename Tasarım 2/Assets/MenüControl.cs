using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenüControl : MonoBehaviour
{
    GameObject Levels,Locks;
    
    void Start()
    {
        Levels = GameObject.Find("Levels");
        Locks = GameObject.Find("Locks");

        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            Levels.transform.GetChild(i).gameObject.SetActive(false);
        }
        
          for (int i = 0; i < Locks.transform.childCount; i++)
        {
            Locks.transform.GetChild(i).gameObject.SetActive(false);
        }

     // PlayerPrefs.DeleteAll();

        for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            Levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void buttonSec(int secbutton)
    {
         if (secbutton == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (secbutton == 2)
        {   
               for (int i = 0; i < Locks.transform.childCount; i++)
        {
            Locks.transform.GetChild(i).gameObject.SetActive(true);
        }

            for (int i = 0; i < Levels.transform.childCount; i++)
            {
                Levels.transform.GetChild(i).gameObject.SetActive(true);
            }
    
             for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                Locks.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (secbutton == 3)
        {
            Application.Quit();
        }
        else if (secbutton == 4)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void levelButton(int gelenlevel)
    {
        SceneManager.LoadScene(gelenlevel);

    }
  
}
