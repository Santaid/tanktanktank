using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;  // UI Text コンポーネントをアタッチする変数

    private int frameCount;
    private float elapsedTime;
    private float fps;

    void Update()
    {
        frameCount++;
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1.0f)
        {
            fps = frameCount / elapsedTime;
            frameCount = 0;
            elapsedTime = 0;
            fpsText.text = $"FPS: {fps:F2}";
        }
    }
}
