using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InputDataStaticClass")]
public class InputDataStaticClass: ScriptableObject
{
    public string player1Input;
    public string player2Input;

    public ColorBlindMode colorBlindMode;
}
