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
        if (input == "1")
        {
            level = 1;
            password = "monkey";
            StartGame();
        }
        else if (input == "2")
        {
            level = 2;
            password = "environment";
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
        Terminal.WriteLine("You have chosen level " + level);
        Terminal.WriteLine("Please enter your password: ");
    }
}
