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
  - [ValidExecutableExtensions](#F-xyLOGIX-Core-Common-Run-ValidExecutableExtensions 'xyLOGIX.Core.Common.Run.ValidExecutableExtensions')
  - [System](#P-xyLOGIX-Core-Common-Run-System 'xyLOGIX.Core.Common.Run.System')
  - [Command(command,workingDirectory,useShell)](#M-xyLOGIX-Core-Common-Run-Command-System-String,System-String,System-Boolean- 'xyLOGIX.Core.Common.Run.Command(System.String,System.String,System.Boolean)')
  - [CommandWithOutput(command,workingDirectory,useShell)](#M-xyLOGIX-Core-Common-Run-CommandWithOutput-System-String,System-String,System-Boolean- 'xyLOGIX.Core.Common.Run.CommandWithOutput(System.String,System.String,System.Boolean)')
  - [DetermineCurrentWorkingDirectory(folder)](#M-xyLOGIX-Core-Common-Run-DetermineCurrentWorkingDirectory-System-String- 'xyLOGIX.Core.Common.Run.DetermineCurrentWorkingDirectory(System.String)')
  - [DoesFileExist(pathnamne)](#M-xyLOGIX-Core-Common-Run-DoesFileExist-System-String- 'xyLOGIX.Core.Common.Run.DoesFileExist(System.String)')
  - [ResolveExeOnPath(pathname)](#M-xyLOGIX-Core-Common-Run-ResolveExeOnPath-System-String- 'xyLOGIX.Core.Common.Run.ResolveExeOnPath(System.String)')
  - [SplitExeAndArgs(command,exePath,arguments)](#M-xyLOGIX-Core-Common-Run-SplitExeAndArgs-System-String,System-String@,System-String@- 'xyLOGIX.Core.Common.Run.SplitExeAndArgs(System.String,System.String@,System.String@)')

<a name='T-xyLOGIX-Core-Common-Do'></a>
## Do `type`

##### Namespace

xyLOGIX.Core.Common

##### Summary

Exposes static method(s) to perform actions.

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

Empty, `static` constructor to prohibit direct allocation of this
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

<a name='F-xyLOGIX-Core-Common-Run-ValidExecutableExtensions'></a>
### ValidExecutableExtensions `constants`

##### Summary

Array of valid executable file extension(s) that this class will recognize and
execute.

<a name='P-xyLOGIX-Core-Common-Run-System'></a>
### System `property`

<a name='M-xyLOGIX-Core-Common-Run-Command-System-String,System-String,System-Boolean-'></a>
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

<a name='M-xyLOGIX-Core-Common-Run-CommandWithOutput-System-String,System-String,System-Boolean-'></a>
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

<a name='M-xyLOGIX-Core-Common-Run-DoesFileExist-System-String-'></a>
### DoesFileExist(pathnamne) `method`

##### Summary

Determines whether the file having the specified `pathnamne`
exists on the file system.

##### Returns

`true` if the file having the specified
`pathnamne` exists, `false` otherwwise.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pathnamne | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that contains the fully-qualified
pathname of a file that is to be searched for. |

<a name='M-xyLOGIX-Core-Common-Run-ResolveExeOnPath-System-String-'></a>
### ResolveExeOnPath(pathname) `method`

##### Summary

Attempts to resolve `pathname` to a fully-qualified file
on the current `PATH`.

##### Returns

The resolved, fully-qualified path when the file is found; otherwise the
original `pathname`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pathname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Command name exactly as supplied by the caller (e.g. `git`,
`git.exe`, `myTool.cmd`). |

##### Remarks

* Accepts executables with any of the four legacy extensions
(`.bat`, `.cmd`, `.com`, `.pif`) or `.exe`.
* When no extension is supplied, `.exe` is assumed.
* Returns the original `pathname` if a match is not
found.

<a name='M-xyLOGIX-Core-Common-Run-SplitExeAndArgs-System-String,System-String@,System-String@-'></a>
### SplitExeAndArgs(command,exePath,arguments) `method`

##### Summary

Splits the specified `command` into an executable path and
its argument(s).

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| command | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') containing the command to be
executed. |
| exePath | [System.String@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String@ 'System.String@') | (Required.) A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that receives the fully-qualified
pathname of the executable. |
| arguments | [System.String@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String@ 'System.String@') | A [String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') that receives the
argument(s) that are assumed to have been passed to the target executable. |
