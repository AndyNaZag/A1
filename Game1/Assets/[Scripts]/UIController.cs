using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //public CanvasRender button;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void OnStartButton_Over()
    {

    }

    public void OnStartButton_Out()
    {
        
    }
}
