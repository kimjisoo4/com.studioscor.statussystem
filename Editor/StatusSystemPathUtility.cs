using UnityEditor;

namespace StudioScor.StatusSystem.Editor
{
    public static class StatusSystemPathUtility
    {
        private static string _RootFolder;
        public static string EditorPath => RootFolder + "Editor/";
        public static string InspectorPath => RootFolder + "Editor/UIBuilder/";
        public static string RootFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_RootFolder))
                {
                    _RootFolder = PathOf("studioscor_statussystem_root");
                }

                return _RootFolder;
            }
        }

        private static string PathOf(string fileName)
        {
            var files = AssetDatabase.FindAssets(fileName);

            if (files.Length == 0)
                return string.Empty;

            var assetPath = AssetDatabase.GUIDToAssetPath(files[0]).Replace(fileName, string.Empty);

            return assetPath;
        }
    }
}
