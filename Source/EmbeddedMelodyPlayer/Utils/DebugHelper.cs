using System;
using EmbeddedMelodyPlayer.Infrastructure;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Utils
{
    public static class DebugHelper
    {
         public static void PrintCommandExceptionMessage(Exception exception, ICommand command)
         {
             Debug.Print("Exception: " + exception.Message + " in command " + command + ".");
         }
    }
}