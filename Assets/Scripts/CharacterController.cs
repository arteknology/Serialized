using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Character Character;
    public Text nameText;
    public InputField InputField;

    private void Save()
    {
        Character = new Character
        {
            Position = transform.position,
            Name = InputField.text
        };
        DataHandler.Save(UnityDirectory.StreamingAssetPath, Character, Character.Name);
    }
    
    private void Load()
    { 
        Character = DataHandler.Load<Character>(UnityDirectory.StreamingAssetPath, InputField.text);
        nameText.text = Character.Name + " Xp: "+Character.Xp;
        transform.position = Character.Position;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire2")) Save();
        else if (Input.GetButtonDown("Fire3")) Load();
        
        if (Input.GetButtonDown("Jump"))
        {
            Character.GainXp();
        }
    }
}

[Serializable]
public class Character
{
    public string Name;

    public int Xp;
    public int Level;
    public Vector3 Position;
    public int Pv;

    public void GainXp()
    {
        Xp++;
    }
}
