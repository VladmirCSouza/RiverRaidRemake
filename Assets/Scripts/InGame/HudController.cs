using System.Collections;
using System.Collections.Generic;
using Channel3.RetroRaid.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Channel3.RetroRaid.InGame
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Slider fuelSlider;
        
        [Space]
        [SerializeField] private PlayerController playerController;
        
        void Update()
        {
            UpdateFuelSliderGui();
        }

        private void UpdateFuelSliderGui()
        {
            fuelSlider.value = (int)playerController.CurrentFuel;
        }
    }
}