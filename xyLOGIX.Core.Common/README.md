<a name='assembly'></a>
# xyLOGIX.Core.Common

## Contents

- [Do](#T-xyLOGIX-Core-Common-Do 'xyLOGIX.Core.Common.Do')
  - [ToActionParams()](#M-xyLOGIX-Core-Common-Do-ToActionParams-System-Object- 'xyLOGIX.Core.Common.Do.ToActionParams(System.Object)')
  - [UntilSucceeds(action,args)](#M-xyLOGIX-Core-Common-Do-UntilSucceeds-System-Delegate,System-Object[]- 'xyLOGIX.Core.Common.Do.UntilSucceeds(System.Delegate,System.Object[])')
  - [UntilSucceedsThread()](#M-xyLOGIX-Core-Common-Do-UntilSucceedsThread-System-Object- 'xyLOGIX.Core.Common.Do.UntilSucceedsThread(System.Object)')
- [IsThis](#T-xyLOGIX-Core-Common-IsThis 'xyLOGIX.Core.Common.IsThis')
  - [#ctor()](#M-xyLOGIX-Core-Common-IsThis-#ctor 'xyLOGIX.Core.Common.IsThis.#ctor')
  - [Machine](#P-xyLOGIX-Core-Common-IsThis-Machine 'xyLOGIX.Core.Common.IsThis.Machine')
  - [#cctor()](#M-xyLOGIX-Core-Common-IsThis-#cctor 'xyLOGIX.Core.Common.IsThis.#cctor')
  - [ConnectedToTheInternet()](#M-xyLOGIX-Core-Common-IsThis-ConnectedToTheInternet 'xyLOGIX.Core.Common.IsThis.ConnectedToTheInternet')
- [Resources](#T-xyLOGIX-Core-Common-Properties-Resources 'xyLOGIX.Core.Common.Properties.Resources')
  - [Culture](#P-xyLOGIX-Core-Common-Properties-Resources-Culture 'xyLOGIX.Core.Common.Properties.Resources.Culture')
  - [ResourceManager](#P-xyLOGIX-Core-Common-Properties-Resources-ResourceManager 'xyLOGIX.Core.Common.Properties.Resources.ResourceManager')
- [Run](#T-xyLOGIX-Core-Common-Run 'xyLOGIX.Core.Common.Run')
  - [#ctor()](#M-xyLOGIX-Core-Common-Run-#ctor 'xyLOGIX.Core.Common.Run.#ctor')
  - [SyncRoot](#P-xyLOGIX-Core-Common-Run-SyncRoot 'xyLOGIX.Core.Common.Run.SyncRoot')
  - [System](#P-xyLOGIX-Core-Common-Run-System 'xyLOGIX.Core.Common.Run.System')
  - [#cctor()](#M-xyLOGIX-Core-Common-Run-#cctor 'xyLOGIX.Core.Common.Run.#cctor')
  - [Command(command,workingDirectory)](#M-xyLOGIX-Core-Common-Run-Command-System-String,System-String- 'xyLOGIX.Core.Common.Run.Command(System.String,System.String)')
  - [CommandWithOutput(command,workingDirectory)](#M-xyLOGIX-Core-Common-Run-CommandWithOutput-System-String,System-String- 'xyLOGIX.Core.Common.Run.CommandWithOutput(System.String,System.String)')
  - [DetermineCurrentWorkingDirectory(folder)](#M-xyLOGIX-Core-Common-Run-DetermineCurrentWorkingDirectory-System-String- 'xyLOGIX.Core.Common.Run.DetermineCurrentWorkingDirectory(System.String)')

<a name='T-xyLOGIX-Core-Common-Do'></a>
## Do `type`

##### Namespace

xyLOGIX.Core.Common

##### Summary

Exposes static methods to perform actions.

<a name='M-xyLOGIX-Core-Common-Do-ToActionParams-System-Object-'></a>
### ToActionParams() `method`

##### Summary

Casts the specified `arg` to an instance of an object that
implements the [IActionParams](#T-Core-Common-Params-Interfaces-IActionParams 'Core.Common.Params.Interfaces.IActionParams')
interface, that is, if the cast is possible; otherwise, `null`
is returned.

##### Parameters

This method has no parameters.

<a name='M-xyLOGIX-Core-Common-Do-UntilSucceeds-System-Delegate,System-Object[]-'></a>
### UntilSucceeds(action,args) `method`

##### Summary

Executes the specified `action` in a separate
worker thread until the `action` succeeds.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') | (Required.) A [Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') that
points to the code that should be executed. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | (Optional.) Arguments to be passed to the executed code. |

<a name='M-xyLOGIX-Core-Common-Do-UntilSucceedsThread-System-Object-'></a>
### UntilSucceedsThread() `method`

##### Parameters

This method has no parameters.

<a name='T-xyLOGIX-Core-Common-IsThis'></a>
## IsThis `type`

##### Namespace

xyLOGIX.Core.Common

##### Summary

Methods to decide whether certain facts are true.

<a name='M-xyLOGIX-Core-Common-IsThis-#ctor'></a>
### #ctor() `constructor`

##### Parameters

This constructor has no parameters.

<a name='P-xyLOGIX-Core-Common-IsThis-Machine'></a>
### Machine `property`

<a name='M-xyLOGIX-Core-Common-IsThis-#cctor'></a>
### #cctor() `method`

##### Summary

Empty, static constructor to prohibit direct allocation of this
class.

##### Parameters

This method has no parameters.

<a name='M-xyLOGIX-Core-Common-IsThis-ConnectedToTheInternet'></a>
### ConnectedToTheInternet() `method`

##### Returns

`true` if the machine is connected to the Internet;
`false` otherwise.

##### Parameters

This method has no parameters.

<a name='T-xyLOGIX-Core-Common-Properties-Resources'></a>
## Resources `type`

##### Namespace

xyLOGIX.Core.Common.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-xyLOGIX-Core-Common-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-xyLOGIX-Core-Common-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.

<a name='T-xyLOGIX-Core-Common-Run'></a>
## Run `type`

##### Namespace

xyLOGIX.Core.Common

##### Summary

Methods and properties to encapsulate the execution of actions.

<a name='M-xyLOGIX-Core-Common-Run-#ctor'></a>
### #ctor() `constructor`

##### Parameters

This constructor has no parameters.

<a name='P-xyLOGIX-Core-Common-Run-SyncRoot'></a>
### SyncRoot `property`

##### Summary

Gets a reference to an instance of an object that is to be used for thread
synchronization purposes.

<a name='P-xyLOGIX-Core-Common-Run-System'></a>
### System `property`

<a name='M-xyLOGIX-Core-Common-Run-#cctor'></a>
### #cctor() `method`

##### Summary

Empty, static constructor to prohibit direct allocation of this
class.

##### Parameters

This method has no parameters.

<a name='M-xyLOGIX-Core-Common-Run-Command-System-String,System-String-'></a>
### Command(command,workingDirectory) `method`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| command | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) String containing the command to execute.
May be anything you can type into the `cmd` prompt, and may have
environment variables. |
| workingDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String')
containing a fully-qualified pathname of the working directory for running the
command. |

##### Remarks

If this method is passed a `null` or blank value for
the `command` parameter, it does nothing.



By default, no console window is displayed when the command is executed. By
default, this method waits to return until the launched command has completed
execution.

<a name='M-xyLOGIX-Core-Common-Run-CommandWithOutput-System-String,System-String-'></a>
### CommandWithOutput(command,workingDirectory) `method`

##### Summary

Executes a system `command` and returns every line
written to `STDOUT` and `STDERR`.

##### Returns

A read-only list of lines captured from the child process.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| command | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) The command to run – anything you can type at `cmd`.
Environment variables are allowed. |
| workingDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Fully-qualified path to use as the working directory.
Falls back to `Directory.GetCurrentDirectory()` if blank or invalid. |

<a name='M-xyLOGIX-Core-Common-Run-DetermineCurrentWorkingDirectory-System-String-'></a>
### DetermineCurrentWorkingDirectory(folder) `method`

##### Summary

Determines the current working directory, optionally using a specified
`folder`.

##### Returns

The effective working directory path, with trailing backslashes
removed. If `folder` is null or empty, the current directory
of the application is returned.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| folder | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Optional.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing
the fully-qualified pathname of a folder to use as the working directory for
spawning a process.



If this parameter is `null`, blank, or the
[Empty](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String.Empty 'System.String.Empty') value, the method returns the current
directory of the application. |

##### Remarks

This method logs informational and error messages during its
execution.



If an exception occurs, it logs the exception details and defaults to returning
the current directory.
