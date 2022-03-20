/* Скрипт для тестов 
 * Связь между прицелом, глайдерам и тестовы UI 
 * Что бы можно было редактировать некоторы вещи
 * Прямо во время работы или в билде
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetingsPlayreTest : MonoBehaviour
{
    public InputField SpeedGlaider;
    public InputField SpeedAim;
    public InputField SpeedForwardAim;

    public AimPlayre aimPlayre;
    public GlaiderControllerMove GlaiderMove;
    public PlayerControl playerControl;


    // Update is called once per frame
    void Update()
    {

        if (int.TryParse(SpeedGlaider.text, out var number))
        {
            GlaiderMove.ForceForfardBack = number;
            GlaiderMove.ForceRightleft = number * 0.75f;
        }

        if (int.TryParse(SpeedAim.text, out  number))
        {
            playerControl.SpeedMouseForAim = number;

        }

        if (int.TryParse(SpeedForwardAim.text, out  number))
        {
            aimPlayre.SpeedAIForward = number;

        }
    }
}
