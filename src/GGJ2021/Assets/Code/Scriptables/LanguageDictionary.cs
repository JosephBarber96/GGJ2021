using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Dictionary", menuName = "GGJ/Dictionary")]
public class LanguageDictionary : ScriptableObject
{
    [SerializeField]
    public List<LanguageWord> Words = new List<LanguageWord>();



#if UNITY_EDITOR
    [ContextMenu("Populate Dictionary")]
    public void PopulateDictionary()
    {
        Words.Clear();

        // Find all Words 
        string[] guids1 = AssetDatabase.FindAssets("t:LanguageWord", null);

        // Add 
        foreach (string guid in guids1)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Words.Add( (LanguageWord) AssetDatabase.LoadAssetAtPath(path, typeof(LanguageWord)));
        }
    }
#endif
}