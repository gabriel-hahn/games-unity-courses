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
    const string menuHint = "You may type menu at any time.";

    string[] levelOnePasswords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] levelTwoPasswords = { "prisioner", "handcuffs", "holster", "uniform", "arrest" };
    string[] levelThreePasswords = { "starfield", "telescope", "environment", "exploration", "universe" };

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
        Terminal.WriteLine("Press 3 for NASA!");
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
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr Bond");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();

        SetRandomPassword();

        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = levelOnePasswords[Random.Range(0, levelOnePasswords.Length)];
                break;
            case 2:
                password = levelTwoPasswords[Random.Range(0, levelTwoPasswords.Length)];
                break;
            case 3:
                password = levelThreePasswords[Random.Range(0, levelThreePasswords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;

        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Well done! Have a book...");
                break;
            case 2:
                Terminal.WriteLine("Well done! You got the prision key!");
                break;
            case 3:
                Terminal.WriteLine("Welcome to NASA's internal system!");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }
}
