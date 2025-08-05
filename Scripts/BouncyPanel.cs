using UnityEngine;

public class BouncyPanel : MonoBehaviour
{
    public static BouncyPanel instance;
    public float duration = 0.2f;

    private void Awake()
    {
        instance = this;
    }

    public void OpenPanel(GameObject Frame)
    {
        Frame.SetActive(true);
        Frame.transform.localScale = Vector3.zero;

        LeanTween.scale(Frame, Vector3.one * 1.15f, duration)
            .setEaseOutCubic()
            .setOnComplete(() =>
            {
                LeanTween.scale(Frame, Vector3.one * 0.9f, duration * 0.8f)
                    .setEaseInOutSine()
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(Frame, Vector3.one * 1.05f, duration * 0.6f)
                            .setEaseOutCubic()
                            .setOnComplete(() =>
                            {
                                LeanTween.scale(Frame, Vector3.one * 0.97f, duration * 0.5f)
                                    .setEaseInOutSine()
                                    .setOnComplete(() =>
                                    {
                                        LeanTween.scale(Frame, Vector3.one, duration * 0.4f)
                                            .setEaseOutBack();
                                    });
                            });
                    });
            });
    }

    public void ClosePanel(GameObject Frame)
    {
        LeanTween.scale(Frame, Vector3.one * 0.85f, duration)
            .setEaseInCubic()
            .setOnComplete(() =>
            {
                LeanTween.scale(Frame, Vector3.one * 1.1f, duration * 0.8f)
                    .setEaseInOutSine()
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(Frame, Vector3.one * 0.95f, duration * 0.6f)
                            .setEaseInOutSine()
                            .setOnComplete(() =>
                            {
                                LeanTween.scale(Frame, Vector3.zero, duration * 0.5f)
                                    .setEaseInBack()
                                    .setOnComplete(() => Frame.SetActive(false));
                            });
                    });
            });
    }
}
