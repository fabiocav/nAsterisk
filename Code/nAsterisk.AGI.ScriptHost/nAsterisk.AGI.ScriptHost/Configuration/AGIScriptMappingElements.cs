using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace nAsterisk.AGI.ScriptHost.Configuration
{
    public class AGIScriptMappingElements : ConfigurationElementCollection
    {
        public AGIScriptMappingElements()
        {
            this.AddElementName = "mapping";
            this.ClearElementName = "clear";
            this.RemoveElementName = "remove";
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new AGIScriptMappingElement(elementName);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AGIScriptMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AGIScriptMappingElement)element).Name;
        }

        public new AGIScriptMappingElement this[string name]
        {
            get { return (AGIScriptMappingElement)base.BaseGet(name); }
        }

        public AGIScriptMappingElement this[int index]
        {
            get { return (AGIScriptMappingElement)base.BaseGet(index); }
        }

        public int IndexOf(AGIScriptMappingElement element)
        {
            return BaseIndexOf(element);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            if (!(element is AGIScriptMappingElement))
                throw new InvalidOperationException();

            base.BaseAdd(element);
        }

        public void Remove(AGIScriptMappingElement element)
        {
            if (BaseIndexOf(element) > 0)
                BaseRemove(element.Name);
        }

        public void Add(AGIScriptMappingElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

    }
}
