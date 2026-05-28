namespace Unity.Infrastructure.VisualTutorial
{
    public interface ITutorialOverlayController
    {
        void ShowOverlay(float alpha = -1);
        void HideOverlay();
    }
}