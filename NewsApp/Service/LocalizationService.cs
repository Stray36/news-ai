using NewsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;

namespace NewsApp.Service
{
    public class LocalizationService
    {
        private readonly ResourceManager _resourceManager;
        public LocalizationService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void ChangeLocalization(string cultureCode)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);
        }

        public string GetLocalizationValue(string localizationKey)
        {
            return _resourceManager.GetString(localizationKey);
        }

        public List<LocalizedEnum<T>> GetLocalizedEnumValues<T>() where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
            var localizedValues = new List<LocalizedEnum<T>>();

            foreach (var value in enumValues)
            {
                string resourceKey = $"{typeof(T).Name}_{value}";
                string localizedValue = _resourceManager.GetString(resourceKey);
                localizedValues.Add(new LocalizedEnum<T> { Value = value, DisplayName = localizedValue });
            }

            return localizedValues;
        }
    }
}