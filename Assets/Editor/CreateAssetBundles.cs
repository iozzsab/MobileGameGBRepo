using UnityEditor;
public class CreateAssetBundles 
{
   [MenuItem("Assets/AssetBundles")]
   static void CreateAssetBunsles()
   {
      BuildPipeline.BuildAssetBundles("Assets/HWAssetBundles/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
   }
}