using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections;
using TMPro;

public class CopyToClipboard : MonoBehaviour
{
    public Text textToCopy;    // UI Text to copy
    // public TMP_InputField inputField; // UI InputField to paste

    [DllImport("__Internal")]
    private static extern void copyToClipboard(string text);

    // [DllImport("__Internal")]
    //   private static extern void pasteFromClipboard(string gameObjectName);

    void Start()
    {
        // Optional: Assign button events in the Inspector
    }

    

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.V) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        // {
        //     #if UNITY_WEBGL && !UNITY_EDITOR
        //         pasteFromClipboard("CopyToClipboard"); // Make sure this is the correct GameObject name
        //     #else
        //         inputField.text += GUIUtility.systemCopyBuffer;
        //     #endif
        // }
    }

    // public void OnClipboardPaste(string pastedText)
    // {
    //     Debug.Log("Received Clipboard Data: " + pastedText);
    //     inputField.text += pastedText;
    // }
    public void CopyText()
    {
        string text = textToCopy.text;

        #if UNITY_WEBGL && !UNITY_EDITOR
            copyToClipboard(text);
        #else
            GUIUtility.systemCopyBuffer = text;
        #endif

        Debug.Log("Copied: " + text);
    }

    
}
