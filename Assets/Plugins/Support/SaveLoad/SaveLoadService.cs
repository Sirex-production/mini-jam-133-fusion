using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    

namespace Support {
   
    public sealed class SaveLoadService
    {
       private const string PLAYER_PREFS_TYPES_KEY = "persistantTypes";
       
       private readonly IDataSerializer _serializer;
       
       private readonly Dictionary<Type, bool> _modifiedTypes = new();
       private readonly Dictionary<Type, string> _serializedComponents = new();

       private readonly List<Type> _types = new();
       private List<string> _serializedTypes = new();
       private bool _wasTypeListModified;
       
       public SaveLoadService(IDataSerializer serializer)
       {
          _serializer = serializer;
          var storedTypesPref = PlayerPrefs.GetString(PLAYER_PREFS_TYPES_KEY, "");
      
          if (string.IsNullOrEmpty(storedTypesPref))
             return;
          
          _serializedTypes = _serializer.DeserializeData<List<string>>(storedTypesPref);

          foreach (var stringType in _serializedTypes)
          {
             var type = Type.GetType(stringType);

             if (type == null)
             {
                Debug.LogError("Data is corrupted!!!");
                continue;
             }
            
             
             _types.Add(type);
             
             var serializedComponent = PlayerPrefs.GetString(stringType, "");

             if (string.IsNullOrEmpty(serializedComponent))
             {
                Debug.LogWarning("The data can be damaged");
                continue;
             }

             _serializedComponents.Add(type,serializedComponent);
             _modifiedTypes.Add(type,false);
          }
       }

       public void AddSaveComponent<T>(T cmp)
       {
          var cashedType = typeof(T);
          if(!_types.Contains(cashedType))
          {
             _types.Add(cashedType);
             _serializedComponents.Add(cashedType, _serializer.SerializeData(cmp));
             _modifiedTypes.Add(cashedType, true);
             _wasTypeListModified = true;
             return;
          }
          
          _modifiedTypes[cashedType] = true;
          _serializedComponents[cashedType] = _serializer.SerializeData(cmp);
       }

       public T GetSaveComponent<T>()
       {
          var cashedType = typeof(T);
          if (!_types.Contains(cashedType))
             throw new ArgumentException("The component must be firstly added");

          return _serializer.DeserializeData<T>(_serializedComponents[cashedType]);
       }

       public bool HasSavedComponent<T>()
       {
          return _types.Contains(typeof(T));
       }

       public void Save()
       {
          if(_wasTypeListModified)
          { 
             _serializedTypes = _types.Select(type => type.ToString()).ToList();
             PlayerPrefs.SetString(PLAYER_PREFS_TYPES_KEY, _serializer.SerializeData(_serializedTypes));
          }
          
          foreach (var modifiedKey in _modifiedTypes.Keys.ToList().Where(modifiedKey => _modifiedTypes[modifiedKey]))
          {
             PlayerPrefs.SetString(modifiedKey.ToString(),  _serializedComponents[modifiedKey]);
             _modifiedTypes[modifiedKey] = false;
          }
          
          PlayerPrefs.Save();
       }
    }
}