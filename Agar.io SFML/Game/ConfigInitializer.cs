using System.Reflection;
using Agar.io_SFML.Configs;

namespace Agar.io_SFML;

public static class ConfigInitializer
{
    private static string configFilePath => Path.GetFullPath(@"..\..\..\..\Configuration\config.ini");
    private static StreamReader configReader = new(configFilePath); 
    
    public static void ReadWholeConfig()
    {
        string line = "";
        string section = "";
        Type currentTypeOfConfig = null;
        
        while(!configReader.EndOfStream) 
        {
            line = configReader.ReadLine();
            
            if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#"))
                continue;
            
            string[] lineKeyAndValue = line.Split('=');
            
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                section = line.Substring(1, line.Length - 2);
                currentTypeOfConfig = Type.GetType("Agar.io_SFML.Configs." + section);
            }
            else if(lineKeyAndValue.Length >= 2)
            {
                string fieldName = lineKeyAndValue[0].Trim();
                string fieldValue = lineKeyAndValue[1].Trim();
                
                FieldInfo? fieldInfo = currentTypeOfConfig?.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
                
                if (int.TryParse(fieldValue, out int value))
                {
                    fieldInfo?.SetValue(null, value);
                }
                else
                {
                    fieldInfo?.SetValue(null, fieldValue);
                }
            }
            
        }
    }
}