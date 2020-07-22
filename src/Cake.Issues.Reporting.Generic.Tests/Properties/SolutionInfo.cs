using System.Runtime.InteropServices;
using Xunit;

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1e92a970-e905-4df8-8eca-5529b701e8e3")]

// There seems to be an issue while executing tests in parallel. See https://github.com/mholo65/gazorator/issues/7
// This disables test execution when running tests from inside Visual Studio.
// To disable parallelization in build script see xunit.runner.json file.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
