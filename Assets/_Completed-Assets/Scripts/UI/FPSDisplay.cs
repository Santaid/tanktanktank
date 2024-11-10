using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;  // UI Text コンポーネントをアタッチする変数

    private int frameCount;
    private float elapsedTime;
    private float fps;

    private float minfps = float.MaxValue;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (fpsText.enabled == false)
            {
                fpsText.enabled = true;    
            }
            else
            {
                 fpsText.enabled = false;   
            }
        }

        frameCount++;
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1.0f)
        {
            fps = frameCount / elapsedTime;
            if (fps < minfps){minfps = fps;}
            frameCount = 0;
            elapsedTime = 0;
            fpsText.text = $"FPS: {fps:F2}\nMINFPS:{minfps:F2}";
        }
    }
}
