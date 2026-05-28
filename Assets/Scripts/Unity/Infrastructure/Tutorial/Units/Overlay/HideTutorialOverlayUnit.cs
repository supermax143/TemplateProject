using System.Collections.Generic;
using Unity.Infrastructure.VisualTutorial;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Infrastructure.VisualTutorial.Units.BaseUnits;
using Zenject;

namespace Controllers.VisualTutorial.Units.Popup
{
    [UnitCategory("Custom/Popup")]
    [UnitTitle("HideTutorialOverlay")]
    public class HideTutorialOverlayUnit : CustomUnit
    {
        [Inject] private ITutorialOverlayController _tutorialOverlay;
        
        protected override IEnumerable<IUnitValuePort> DefineValuePortsInternal()
        {
            yield break;
        }
        
        protected override void OnExecute(Flow flow)
        {
            _tutorialOverlay.HideOverlay();
        }
    }
}
