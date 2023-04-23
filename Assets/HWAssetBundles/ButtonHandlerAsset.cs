using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandlerAsset : MonoBehaviour
{
    public Button button;
    const string bundlePath = "Assets/HWAssetBundles/AssetBundles/spritetestbundle";

    private bool isLoading = false;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!isLoading)
        {
            isLoading = true;
            button.interactable = false;
            StartCoroutine(LoadBundle());
        }
    }

    private IEnumerator LoadBundle()
    {
        var bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleRequest;

        if (bundleRequest.assetBundle != null)
        {
            var imageRequest = bundleRequest.assetBundle.LoadAssetAsync<Texture2D>("ButtonImage");
            yield return imageRequest;

            if (imageRequest.asset != null)
            {
                button.image.sprite = Sprite.Create((Texture2D)imageRequest.asset, new Rect(0, 0, ((Texture2D)imageRequest.asset).width, ((Texture2D)imageRequest.asset).height), new Vector2(0.5f, 0.5f));
            }

            bundleRequest.assetBundle.Unload(false);
        }

        isLoading = false;
    }
}