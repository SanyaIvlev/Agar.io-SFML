﻿using System.Reflection;
using System.Runtime.InteropServices;

namespace Agar.io_SFML;

public static class ConfigInitializer
{
    private static readonly string _configFilePath = Path.GetFullPath(@"..\..\..\..\Configuration\config.ini");
    
    public static void ReadWholeConfig()
    {
        string line;
        string section;
        Type currentTypeOfConfig = null;
        
        using StreamReader configReader = new(_configFilePath);
        
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
                
                if (int.TryParse(fieldValue, out int intValue))
                {
                    fieldInfo?.SetValue(null, intValue);
                }
                else if (float.TryParse(fieldValue, out float floatValue))
                {
                    fieldInfo?.SetValue(null, floatValue);
                }
                else
                {
                    fieldInfo?.SetValue(null, fieldValue);
                }
            }
        }
        
        configReader.Close();
    }

    public static void UpdateConfig(string name, string newValue)
    {
        string line;
        string section = "";
        string keyName = "";
        
        using StreamReader configReader = new(_configFilePath);
        
        while (!configReader.EndOfStream)
        {
            line = configReader.ReadLine();
            string[] lineKeyAndValue = line.Split('=');
            
            if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#"))
                continue;
            
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                section = line.Substring(1, line.Length - 2);
            }
            
            string currentKeyName = lineKeyAndValue[0].Trim();

            if (lineKeyAndValue.Length < 2)
                continue;

            if (currentKeyName == name)
            {
                keyName = currentKeyName;
                break;
            }
        }
        
        configReader.Close();
        
        WriteNewValueAt(section, keyName, newValue, _configFilePath);
    }

    private static void WriteNewValueAt(string section, string keyName, string value, string filePath)
    {
        WriteValueA(section, keyName, " " + value, filePath);
    }
    
    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
    public static extern long WriteValueA(string section, 
        string keyName, 
        string value, 
        string filePath);
    
    
}