using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class flash : MonoBehaviour
{
    // Check if flash is currently enabled.
    private bool mFlashEnabled = false;
    public Button bflash;

    void Start()
    {
        Button btn = bflash.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (!mFlashEnabled)
        {
            // Turn on flash if it is currently disabled.
            CameraDevice.Instance.SetFlashTorchMode(true);
            mFlashEnabled = true;
        }
        else
        {
            // Turn off flash if it is currently enabled.
            CameraDevice.Instance.SetFlashTorchMode(false);
            mFlashEnabled = false;
        }
    }
}