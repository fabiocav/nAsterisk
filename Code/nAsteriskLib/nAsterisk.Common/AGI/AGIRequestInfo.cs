using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace nAsterisk.AGI
{
    public class AGIRequestInfo
    {
        private static Dictionary<string, PropertyInfo> _variableMappings;

        static AGIRequestInfo()
        {
            createVariableMappings();
        }

        public AGIRequestInfo(string requestString)
        {
            Dictionary<string, string> agiVariables = new Dictionary<string, string>();

            using (StringReader reader = new StringReader(requestString))
            {
                string line = reader.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    string[] variable = line.Split(new string[] { ": " }, 2, StringSplitOptions.None);

                    agiVariables.Add(variable[0].ToLower(), variable[1]);

                    line = reader.ReadLine();
                }
            }

            populateAGIVariables(agiVariables);
        }

        public AGIRequestInfo(Dictionary<string, string> agiVariables)
        {
            populateAGIVariables(agiVariables);
        }

        private static void createVariableMappings()
        {
            _variableMappings = new Dictionary<string, PropertyInfo>();

            PropertyInfo[] properties = typeof(AGIRequestInfo).GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                object[] attributeObject = propertyInfo.GetCustomAttributes(typeof(AGIRequestVariableAttribute), false);

                if (attributeObject.Length > 0)
                {
                    _variableMappings.Add(((AGIRequestVariableAttribute)attributeObject[0]).AGIVariableName, propertyInfo);
                }
            }
        }

        private void populateAGIVariables(Dictionary<string, string> agiVariables)
        {
            foreach (string key in agiVariables.Keys)
            {
                SetAGIProperty(key, agiVariables[key]);
            }
        }

        public void SetAGIProperty(string agiVariableName, string value)
        {
            if (_variableMappings.ContainsKey(agiVariableName))
            {
                PropertyInfo property = _variableMappings[agiVariableName];

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(this, value ?? string.Empty, null);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(this, value == "yes" , null);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if this is a FastAGI call. (Network)
        /// </summary>
        [AGIRequestVariable("agi_network")]
        public bool IsNetworkScript { get; private set; }

        /// <summary>
        /// Gets the FastAGI script path.
        /// </summary>
        [AGIRequestVariable("agi_network_script")]        
        public string NetworkScriptPath { get; private set; }

        /// <summary>
        /// Gets the full request (URI) of the script originally called.
        /// </summary>
        [AGIRequestVariable("agi_request")]        
        public string Request { get; private set; }

        /// <summary>
        /// Gets the channel name the call is coming from.
        /// </summary>
        [AGIRequestVariable("agi_channel")]
        public string ChannelName { get; private set; }

        /// <summary>
        /// Gets the current language for this call/session.
        /// </summary>
        [AGIRequestVariable("agi_language")]
        public string Language { get; private set; }

        /// <summary>
        /// Gets the channel type for the call (e.g. Console, SIP, Zap).
        /// </summary>
        [AGIRequestVariable("agi_type")]
        public string ChannelType { get; private set; }

        /// <summary>
        /// Gets the Caller Id number.
        /// </summary>
        [AGIRequestVariable("agi_callerid")]
        public string CallerId { get; private set; }

        /// <summary>
        /// Gets the Caller Id name.
        /// </summary>
        [AGIRequestVariable("agi_calleridname")]
        public string CallerIdName { get; private set; }

        /// <summary>
        /// Gets the PRI (Primary Rate Interface) presentation variable.
        /// </summary>
        [AGIRequestVariable("agi_callingpres")]
        public string CallingPres { get; private set; }

        /// <summary>
        /// Gets the ANI (Automatic Number Identification) for PRI (Primary Rate Interface) channels.
        /// </summary>
        [AGIRequestVariable("agi_callingani2")]
        public string ANI { get; private set; }

        /// <summary>
        /// Gets the caller type of number for PRI (Primary Rate Interface) channels.
        /// </summary>
        [AGIRequestVariable("agi_callington")]
        public string CallingTON { get; private set; }

        /// <summary>
        /// Gets the Transit Network Selector for PRI (Primary Rate Interface) channels.
        /// </summary>
        [AGIRequestVariable("agi_callingtns")]
        public string CallingTNS { get; private set; }

        /// <summary>
        /// Gets a unique identifier for this call/session.
        /// </summary>
        [AGIRequestVariable("agi_uniqueid")]
        public string CallId { get; private set; }

        /// <summary>
        /// Gets the Dialed Number Identifier.
        /// </summary>
        [AGIRequestVariable("agi_dnid")]
        public string DNID { get; private set; }

        /// <summary>
        /// Gets the Redirected Dialed Number Information Service.
        /// </summary>
        [AGIRequestVariable("agi_rdnis")]
        public string RDNIS { get; private set; }

        /// <summary>
        /// Gets the current context.
        /// </summary>
        [AGIRequestVariable("agi_context")]
        public string Context { get; private set; }

        /// <summary>
        /// Gets the extension that was called.
        /// </summary>
        [AGIRequestVariable("agi_extension")]
        public string Extension { get; private set; }

        /// <summary>
        /// Gets the current priority in the dial plan.
        /// </summary>
        [AGIRequestVariable("agi_priority")]
        public string Priority { get; private set; }


        [AGIRequestVariable("agi_enhanced")]
        public string Enhanced { get; private set; }

        /// <summary>
        /// Gets the account code if specified.
        /// </summary>
        [AGIRequestVariable("agi_accountcode")]
        public string AccountCode { get; private set; }
    }
}
