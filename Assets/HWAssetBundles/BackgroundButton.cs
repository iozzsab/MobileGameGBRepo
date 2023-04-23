using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class BackgroundButton : MonoBehaviour
{
    const string backgroundImageAddress = "Assets/_Rewards/Sprites/UI_Graphic_Resource_Background.png";
    public Image image;
    
    public void LoadBackground()
    {
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(backgroundImageAddress);
        handle.Completed += LoadBackgroundComplete;
    }

    private void LoadBackgroundComplete(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            image.GetComponent<Image>().sprite = handle.Result;
        }
    }
    public void RemoveBackground()
    {
        Addressables.Release(image.GetComponent<Image>().sprite);
        image.GetComponent<Image>().sprite = null;
    }
}