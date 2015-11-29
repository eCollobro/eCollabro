using System.Configuration;

namespace eCollabro.Configuration
{
    public class FeatureConfigurationSection : ConfigurationSection
    {

        [ConfigurationProperty("Features", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(FeatureCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public FeatureCollection Features
        {
            get
            {
                return (FeatureCollection)base["Features"];
            }
        }
       

    }

    public class FeatureConfig : ConfigurationElement
    {
        public FeatureConfig() { }

        public FeatureConfig(string featureCode,string featureName)
        {
            FeatureName = featureCode;
            FeatureCode = featureCode;
        }

        [ConfigurationProperty("FeatureName", IsRequired = true, IsKey = true)]
        public string FeatureName
        {
            get { return (string)this["FeatureName"]; }
            set { this["FeatureName"] = value; }
        }

        [ConfigurationProperty("FeatureCode", IsRequired = true, IsKey = false)]
        public string FeatureCode
        {
            get { return (string)this["FeatureCode"]; }
            set { this["FeatureCode"] = value; }
        }
    }

    public class FeatureCollection : ConfigurationElementCollection
    {
        public FeatureCollection()
        {

        }

        public FeatureConfig this[int index]
        {
            get { return (FeatureConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(FeatureConfig featureConfig)
        {
            BaseAdd(featureConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeatureConfig)element).FeatureCode;
        }

        public void Remove(FeatureConfig featureConfig)
        {
            BaseRemove(featureConfig.FeatureCode);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}
