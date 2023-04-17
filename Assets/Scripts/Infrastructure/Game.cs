using Services.Input;
using UnityEngine;

public class Game
{
    public static IInputService InputService;

    public Game()
    {
        RegisterInputService();
    }

    private static void RegisterInputService()
    {
        InputService = Application.isEditor ? new StandaloneInputService() : new MobileInputService();
    }
}