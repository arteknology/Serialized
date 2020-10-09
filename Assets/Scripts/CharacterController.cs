using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Character Character = new Character();
    public Text nameText;
    public int Level;
    public int Xp;
    public int Pv;

    private void Start()
    {
        Level = 0;
        Xp = 0;
        Pv = 0;
    }

    private void Save()
    {
        Character.Position = transform.position;
        DataHandler.Save(UnityDirectory.StreamingAssetPath, Character, Character.Name);
    }
    
    private void Load()
    { 
        DataHandler.Load<Character>(UnityDirectory.StreamingAssetPath, Character.Name);
        Level = Character.Level;
        Xp = Character.Xp;
        Pv = Character.Pv;
        nameText.text = Character.Name + " Xp: "+Xp;
        transform.position = Character.Position;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump")) Save();
        else if (Input.GetButtonDown("Horizontal")) Load();
        
        if (Input.GetButtonDown("Vertical"))
        {
            Character.GainXp();
        }
    }
}

[Serializable]
public class Character
{
    [SerializeField] private InputField _inputField;
    public string Name
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }

    public int Xp;
    public int Level;
    public Vector3 Position;
    public int Pv;

    public void GainXp()
    {
        Xp++;
    }
}
