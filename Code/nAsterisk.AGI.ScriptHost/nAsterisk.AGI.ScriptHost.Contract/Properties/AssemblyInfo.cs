using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// AddIn runtime hint attributes
[assembly: PipelineHints.ShareViews]
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.Views, "nAsterisk.AGI.ScriptHost.Common")]
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.HostSideAdapter, "nAsterisk.AGI.ScriptHost.HostAdapters")]
[assembly: PipelineHints.SegmentAssemblyName(PipelineHints.PipelineSegment.AddInSideAdapter, "nAsterisk.AGI.ScriptHost.RemoteAdapters")]


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("nAsterisk.AGI.ScriptHost.Contract")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("nAsterisk.AGI.ScriptHost.Contract")]
[assembly: AssemblyCopyright("Copyright ©  2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("5138f31c-4141-41a0-b838-489633d650c4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
