using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShoot : MonoBehaviour
{

    private AndroidUltimatePluginController androidUltimatePluginController;

    Camera mainCamera;
    RenderTexture renderTex;
    Texture2D screenshot;
    Texture2D LoadScreenshot;
    int width = Screen.width;   // for Taking Picture
    int height = Screen.height; // for Taking Picture
    string fileName;
    string screenShotName = "AR_Edukasi_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png";

    void Start()
    {
        androidUltimatePluginController = AndroidUltimatePluginController.GetInstance();
    }

    public void Snapshot()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen()
    {
        yield return null; // Wait till the last possible moment before screen rendering to hide the UI

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame(); // Wait for screen rendering to complete
        if (Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            mainCamera = Camera.main.GetComponent<Camera>(); // for Taking Picture
            renderTex = new RenderTexture(height, width, 24);
            mainCamera.targetTexture = renderTex;
            RenderTexture.active = renderTex;
            mainCamera.Render();
            screenshot = new Texture2D(height, width, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, height, width), 0, 0);
            screenshot.Apply(); //false
            RenderTexture.active = null;
            mainCamera.targetTexture = null;
        }
        // on Win7 - C:/Users/Username/AppData/LocalLow/CompanyName/GameName
        // on Android - /Data/Data/com.companyname.gamename/Files
        File.WriteAllBytes(Application.persistentDataPath + "/" + screenShotName, screenshot.EncodeToPNG());
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true; // Show UI after we're done
    }
}