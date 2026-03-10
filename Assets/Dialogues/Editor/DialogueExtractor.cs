using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Linq;

public class DialogTextExporter
{
    [MenuItem("Assets/Exportar Textos de Diálogo (CSV)", false, 20)]
    public static void ExportDialogTexts()
    {
        string savePath = EditorUtility.SaveFilePanel("Exportar Textos de Diálogo", "Assets", "DialogTexts.csv", "csv");
        if (string.IsNullOrEmpty(savePath)) return;

        StringBuilder csvContent = new StringBuilder();
        // Cabeceras del CSV
        csvContent.AppendLine("DialogGraph,NodeType,NodeName,Text");

        // Buscar todos los DialogGraph en el proyecto
        string[] graphGuids = AssetDatabase.FindAssets("t:ScriptableObject"); // Buscamos objetos genéricos para filtrar
        int exportedCount = 0;

        foreach (string guid in graphGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // Cargar todos los sub-assets (aquí es donde viven los SentenceNode dentro del DialogGraph)
            Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            foreach (Object obj in allAssets)
            {
                if (obj == null) continue;

                // Filtrar solo por SentenceNode (o cualquier nodo que contenga 'Node' en su nombre de clase)
                if (obj.GetType().Name.Contains("SentenceNode") || obj.GetType().Name.Contains("Node"))
                {


                    SerializedObject so = new SerializedObject(obj);
                    string characterName = GetStringFromSerializedObject(so, "characterName");
                    string textValue = GetStringFromSerializedObject(so, "text");

                    if (!string.IsNullOrWhiteSpace(textValue))
                    {
                        string graphName = Path.GetFileNameWithoutExtension(assetPath);
                        string nodeName = obj.name;

                        // Escapado básico para CSV (reemplazar comillas dobles y encerrar en comillas)
                        string escapedText = textValue.Replace("\"", "\"\"");
                        csvContent.AppendLine($"Conversation: \"{graphName}\", \"{characterName}\", \"{escapedText}\"");
                        exportedCount++;
                    }
                }
            }
        }

        File.WriteAllText(savePath, csvContent.ToString(), new UTF8Encoding(true));
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Éxito", $"Se han exportado {exportedCount} textos a:\n{savePath}", "OK");
    }

    private static string GetStringFromSerializedObject(SerializedObject so, string propName)
    { 

        // Intentar buscar propiedades comunes donde los nodos suelen guardar el texto


        SerializedProperty sentence = so.FindProperty("sentence");
        if(sentence == null)
        {
            return "empty";
        }

        SerializedProperty prop = sentence.FindPropertyRelative(propName);
        if (prop != null && prop.propertyType == SerializedPropertyType.String)
        {
            return prop.stringValue;
        }

        // Fallback: Si no coincide con los nombres, busca la primera propiedad de tipo string que encuentre
        SerializedProperty iterator = so.GetIterator();
        while (iterator.NextVisible(true))
        {
            if (iterator.propertyType == SerializedPropertyType.String && iterator.name != "m_Name")
            {
                return iterator.stringValue;
            }
        }

        return string.Empty;
    }
}