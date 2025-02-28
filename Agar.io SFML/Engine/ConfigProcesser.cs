using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using Agar.io_SFML.Engine;

namespace Agar.io_SFML;

public class ConfigProcesser
{
    private string _configFilePath;

    public ConfigProcesser()
    {
        _configFilePath = PathUtils.ConfigurationDirectory;
        Service<ConfigProcesser>.Set(this);
    }
    
    public void ReadWholeConfig(string configNameWithExtension)
    {
        string line;
        string section;
        Type currentTypeOfConfig = null;
        
        _configFilePath = _configFilePath + "\\" + configNameWithExtension;
        
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

                object? parsedValue = TryParseValue(fieldInfo, fieldValue);
                
                fieldInfo.SetValue(null, parsedValue);
            }
        }
        configReader.Close();
    }

    private object? TryParseValue(FieldInfo fieldInfo, string value)
    {
        Type type = fieldInfo.FieldType;
        
        object? newValue = TypeDescriptor.GetConverter(type)
            .ConvertFromInvariantString(value);
        
        return newValue;
    }

    public void UpdateConfig(string name, string newValue)
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

    private void WriteNewValueAt(string section, string keyName, string value, string filePath)
    {
        WriteValueA(section, keyName, " " + value, filePath);
    }
    
    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
    private static extern long WriteValueA(string section, 
        string keyName, 
        string value, 
        string filePath);
}