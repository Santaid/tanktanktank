using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VersusPlayerButton : MonoBehaviour
{
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {   
       button = GetComponent<Button>();
       button.onClick.AddListener(Onclicked);
    }
    void Onclicked()
    { 
        SceneManager.LoadScene(SceneNames.CompleteGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
