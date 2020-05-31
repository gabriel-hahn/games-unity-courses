using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Screen
{
    MainMenu,
    Password,
    Win
};

public class Hacker : MonoBehaviour
{
    string[] levelOnePasswords = { "books", "aisle", "self", "password", "font", "borrow" };
    string[] levelTwoPasswords = { "prisioner", "handcuffs", "holster", "uniform", "arrest" };

    int level;
    string password;
    Screen currentScreen = Screen.MainMenu;

    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu ()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput (string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2");

        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr Bond");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            Terminal.WriteLine("Well done!");
        }
        else
        {
            Terminal.WriteLine("Sorry, wrong password!");
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();

        switch(level)
        {
            case 1:
                password = levelOnePasswords[Random.Range(0, levelOnePasswords.Length)];
                break;
            case 2:
                password = levelTwoPasswords[Random.Range(0, levelTwoPasswords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }

        Terminal.WriteLine("Please enter your password: ");
    }
}
