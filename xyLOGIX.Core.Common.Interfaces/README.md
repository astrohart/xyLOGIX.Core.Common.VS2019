<a name='assembly'></a>
# xyLOGIX.Core.Common.Interfaces

## Contents

- [ISystem](#T-xyLOGIX-Core-Common-Interfaces-ISystem 'xyLOGIX.Core.Common.Interfaces.ISystem')
  - [Command(command,workingDirectory)](#M-xyLOGIX-Core-Common-Interfaces-ISystem-Command-System-String,System-String- 'xyLOGIX.Core.Common.Interfaces.ISystem.Command(System.String,System.String)')
  - [CommandWithOutput(command,workingDirectory)](#M-xyLOGIX-Core-Common-Interfaces-ISystem-CommandWithOutput-System-String,System-String- 'xyLOGIX.Core.Common.Interfaces.ISystem.CommandWithOutput(System.String,System.String)')
- [Resources](#T-xyLOGIX-Core-Common-Interfaces-Properties-Resources 'xyLOGIX.Core.Common.Interfaces.Properties.Resources')
  - [Culture](#P-xyLOGIX-Core-Common-Interfaces-Properties-Resources-Culture 'xyLOGIX.Core.Common.Interfaces.Properties.Resources.Culture')
  - [ResourceManager](#P-xyLOGIX-Core-Common-Interfaces-Properties-Resources-ResourceManager 'xyLOGIX.Core.Common.Interfaces.Properties.Resources.ResourceManager')

<a name='T-xyLOGIX-Core-Common-Interfaces-ISystem'></a>
## ISystem `type`

##### Namespace

xyLOGIX.Core.Common.Interfaces

##### Summary

Defines the publicly-exposed methods and properties of system-command
methods.

<a name='M-xyLOGIX-Core-Common-Interfaces-ISystem-Command-System-String,System-String-'></a>
### Command(command,workingDirectory) `method`

##### Summary

Runs the specified system `command` but does not
return the output.

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

<a name='M-xyLOGIX-Core-Common-Interfaces-ISystem-CommandWithOutput-System-String,System-String-'></a>
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

<a name='T-xyLOGIX-Core-Common-Interfaces-Properties-Resources'></a>
## Resources `type`

##### Namespace

xyLOGIX.Core.Common.Interfaces.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-xyLOGIX-Core-Common-Interfaces-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-xyLOGIX-Core-Common-Interfaces-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
