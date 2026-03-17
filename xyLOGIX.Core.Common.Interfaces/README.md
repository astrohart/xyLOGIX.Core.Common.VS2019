<a name='assembly'></a>
# xyLOGIX.Core.Common.Interfaces

## Contents

- [ISystem](#T-xyLOGIX-Core-Common-Interfaces-ISystem 'xyLOGIX.Core.Common.Interfaces.ISystem')
  - [Command(command,workingDirectory,useShell)](#M-xyLOGIX-Core-Common-Interfaces-ISystem-Command-System-String,System-String,System-Boolean- 'xyLOGIX.Core.Common.Interfaces.ISystem.Command(System.String,System.String,System.Boolean)')
  - [CommandWithOutput(command,workingDirectory,useShell)](#M-xyLOGIX-Core-Common-Interfaces-ISystem-CommandWithOutput-System-String,System-String,System-Boolean- 'xyLOGIX.Core.Common.Interfaces.ISystem.CommandWithOutput(System.String,System.String,System.Boolean)')
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

<a name='M-xyLOGIX-Core-Common-Interfaces-ISystem-Command-System-String,System-String,System-Boolean-'></a>
### Command(command,workingDirectory,useShell) `method`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| command | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) String containing the command to execute.
May be anything you can type into the `cmd` prompt, and may have
environment variables. |
| workingDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String')
containing a fully-qualified pathname of the working directory for running the
command. |
| useShell | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | (Optional.) Set to `true` to use the Command Interpreter to
execute the command; otherwise, `false` to directly execute
the specified `command` after splitting it on spaces,
assuming that the first space-delimited token is the name of an executable
file, and the rest of the token(s) are its argument(s).



The default value of this parameter is `true`. |

##### Remarks

If this method is passed a `null` or blank value for
the `command` parameter, it does nothing.



By default, no console window is displayed when the command is executed. By
default, this method waits to return until the launched command has completed
execution.

<a name='M-xyLOGIX-Core-Common-Interfaces-ISystem-CommandWithOutput-System-String,System-String,System-Boolean-'></a>
### CommandWithOutput(command,workingDirectory,useShell) `method`

##### Summary

Runs an arbitrary `command` and yields each line it
writes to `STDOUT` or `STDERR` as soon as the line appears.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| command | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) Exact command string as you would type in `cmd.exe`.
Environment variables are allowed. |
| workingDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional working directory.  Falls back to
[CurrentDirectory](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Environment.CurrentDirectory 'System.Environment.CurrentDirectory') when blank or invalid. |
| useShell | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | (Optional.) Set to `true` to use the Command Interpreter to
execute the command; otherwise, `false` to directly execute
the specified `command` after splitting it on spaces,
assuming that the first space-delimited token is the name of an executable
file, and the rest of the token(s) are its argument(s).



The default value of this parameter is `true`. |

##### Remarks

As this method is an iterator, it will not actually get called
until it is enumerated, say, in a `foreach`
loop, for example.



Uses `cmd /C … 2>&1` so both streams arrive in order on
`STDOUT`; no lambdas → no CS1621.

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
