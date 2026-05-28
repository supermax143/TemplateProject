using System.Collections.Generic;
using Unity.Infrastructure.VisualTutorial;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Infrastructure.VisualTutorial.Units.BaseUnits;
using Zenject;

namespace Controllers.VisualTutorial.Units.Popup
{
    [UnitCategory("Custom/Popup")]
    [UnitTitle("ShowTutorialOverlay")]
    public class ShowTutorialOverlayUnit : CustomUnit
    {
        private const string OVERLAY_ALPHA = "overlayAlpha";
        
        [Inject] private ITutorialOverlayController _tutorialOverlay;
        
        protected override IEnumerable<IUnitValuePort> DefineValuePortsInternal()
        {
            yield return ValueInput(OVERLAY_ALPHA, -1f);
        }
        
        protected override void OnExecute(Flow flow)
        {
            var overlayAlpha = GetValue<float>(flow, OVERLAY_ALPHA);
            _tutorialOverlay.ShowOverlay(overlayAlpha);
        }
    }
}
