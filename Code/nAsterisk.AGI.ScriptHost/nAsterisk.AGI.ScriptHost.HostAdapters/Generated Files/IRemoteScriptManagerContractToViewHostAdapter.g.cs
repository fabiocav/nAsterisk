//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nAsterisk.AGI.ScriptHost.HostSideAdapters
{
    
    [System.AddIn.Pipeline.HostAdapterAttribute()]
    public class IRemoteScriptManagerContractToViewHostAdapter : nAsterisk.AGI.ScriptHost.IRemoteScriptManager
    {
        private nAsterisk.AGI.ScriptHost.Contract.IRemoteScriptManagerContract _contract;
        private System.AddIn.Pipeline.ContractHandle _handle;
        static IRemoteScriptManagerContractToViewHostAdapter()
        {
        }
        public IRemoteScriptManagerContractToViewHostAdapter(nAsterisk.AGI.ScriptHost.Contract.IRemoteScriptManagerContract contract)
        {
            _contract = contract;
            _handle = new System.AddIn.Pipeline.ContractHandle(contract);
        }
        public void Execute(IAGIScriptHost host, System.Collections.Generic.Dictionary<string, string> configurationSettings)
        {
            _contract.Execute(nAsterisk.AGI.ScriptHost.HostSideAdapters.IAGIScriptHostHostAdapter.ViewToContractAdapter(host), configurationSettings);
        }
        internal nAsterisk.AGI.ScriptHost.Contract.IRemoteScriptManagerContract GetSourceContract()
        {
            return _contract;
        }
    }
}

