using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public BookController bk;

    public PauseMenu menu;
    public float option1Modifier = 1.5f;
    public float option2Modifier = 1.2f;
    public float option3Modifier = 1f;
    //public AnotherScript scriptToModify;

    public void Option1()
    {
        Debug.Log("Option 1 was selected");
        IncreaseNumBooks();
        //scriptToModify.ModifyValue(option1Modifier);
        menu.HideLvlUpMenu();
    }

    public void Option2()
    {
        //scriptToModify.ModifyValue(option2Modifier);
        menu.HideLvlUpMenu();
    }

    public void Option3()
    {
        //scriptToModify.ModifyValue(option3Modifier);
        menu.HideLvlUpMenu();
    }

    public void IncreaseNumBooks()
    {
        bk.numBooks++;
        bk.radius += 0.5f;
        bk.rotationSpeed += 20f;
    }
}
    /*
    // Scriptable object script here

    public ScriptableObject[] scriptables;
    public GameObject displayPrefab;

    void Start()
    {
        List<ScriptableObject> selectedScriptables = SelectRandomScriptables(3, scriptables);
        for (int i = 0; i < selectedScriptables.Count; i++)
        {
            GameObject display = Instantiate(displayPrefab, transform);
            display.transform.position = transform.position + new Vector3(i * 2, 0, 0);
            DisplayScriptable(display, selectedScriptables[i]);
        }
    }

    List<ScriptableObject> SelectRandomScriptables(int count, ScriptableObject[] scriptables)
    {
        List<ScriptableObject> selectedScriptables = new List<ScriptableObject>();
        while (selectedScriptables.Count < count && selectedScriptables.Count < scriptables.Length)
        {
            int randomIndex = Random.Range(0, scriptables.Length);
            ScriptableObject randomScriptable = scriptables[randomIndex];
            if (!selectedScriptables.Contains(randomScriptable))
            {
                selectedScriptables.Add(randomScriptable);
            }
        }
        return selectedScriptables;
    }

    void DisplayScriptable(GameObject display, ScriptableObject scriptable)
    {
        // implement how to display the scriptable on the display game object
    }*/