using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorMessageProvider : Singleton<ErrorMessageProvider>
{
    private readonly Dictionary<int, string> _errorCodes = new ()
    {
        { 400, "Login or password is wrong" },
        { 500, "Connection to server was lost" },
        { -1, "Login or password is empty" },
        { 0, "Unknown error" }
    };

    public string GetErrorMessage(int code)
    {
        return _errorCodes[code];
    }
}
