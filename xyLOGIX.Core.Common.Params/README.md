<a name='assembly'></a>
# xyLOGIX.Core.Common.Params

## Contents

- [ActionParams](#T-xyLOGIX-Core-Common-Params-ActionParams 'xyLOGIX.Core.Common.Params.ActionParams')
  - [#ctor()](#M-xyLOGIX-Core-Common-Params-ActionParams-#ctor 'xyLOGIX.Core.Common.Params.ActionParams.#ctor')
  - [#ctor(action,args)](#M-xyLOGIX-Core-Common-Params-ActionParams-#ctor-System-Delegate,System-Object[]- 'xyLOGIX.Core.Common.Params.ActionParams.#ctor(System.Delegate,System.Object[])')
  - [Action](#P-xyLOGIX-Core-Common-Params-ActionParams-Action 'xyLOGIX.Core.Common.Params.ActionParams.Action')
  - [Arguments](#P-xyLOGIX-Core-Common-Params-ActionParams-Arguments 'xyLOGIX.Core.Common.Params.ActionParams.Arguments')
  - [#cctor()](#M-xyLOGIX-Core-Common-Params-ActionParams-#cctor 'xyLOGIX.Core.Common.Params.ActionParams.#cctor')
  - [Invoke()](#M-xyLOGIX-Core-Common-Params-ActionParams-Invoke 'xyLOGIX.Core.Common.Params.ActionParams.Invoke')
- [Resources](#T-xyLOGIX-Core-Common-Params-Properties-Resources 'xyLOGIX.Core.Common.Params.Properties.Resources')
  - [Culture](#P-xyLOGIX-Core-Common-Params-Properties-Resources-Culture 'xyLOGIX.Core.Common.Params.Properties.Resources.Culture')
  - [ResourceManager](#P-xyLOGIX-Core-Common-Params-Properties-Resources-ResourceManager 'xyLOGIX.Core.Common.Params.Properties.Resources.ResourceManager')

<a name='T-xyLOGIX-Core-Common-Params-ActionParams'></a>
## ActionParams `type`

##### Namespace

xyLOGIX.Core.Common.Params

##### Summary

POCO that tracks a [Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') and any of its
arguments.

<a name='M-xyLOGIX-Core-Common-Params-ActionParams-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructs a new instance of
[ActionParams](#T-xyLOGIX-Core-Common-Params-ActionParams 'xyLOGIX.Core.Common.Params.ActionParams') and returns a
reference to it.

##### Parameters

This constructor has no parameters.

<a name='M-xyLOGIX-Core-Common-Params-ActionParams-#ctor-System-Delegate,System-Object[]-'></a>
### #ctor(action,args) `constructor`

##### Summary

Constructs a new instance of
[ActionParams](#T-xyLOGIX-Core-Common-Params-ActionParams 'xyLOGIX.Core.Common.Params.ActionParams') and returns a
reference to it.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') | (Required.) A [Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') that
specifies the code that is to be executed. |
| args | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | (Optional.) Collection of arguments that is to be passed to
the specified `action`. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown if the required
parameter, `action`, is passed a `null`
value. |

<a name='P-xyLOGIX-Core-Common-Params-ActionParams-Action'></a>
### Action `property`

##### Summary

Gets or sets a reference to an instance of
[Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') that is to be invoked.

<a name='P-xyLOGIX-Core-Common-Params-ActionParams-Arguments'></a>
### Arguments `property`

##### Summary

Gets or sets an enumerable collection of objects to be passed to the
[Delegate](#P-Core-Common-Params-ActionParams-Delegate 'Core.Common.Params.ActionParams.Delegate') property.

<a name='M-xyLOGIX-Core-Common-Params-ActionParams-#cctor'></a>
### #cctor() `method`

##### Summary

Initializes static data or performs actions that need to be performed once only
for the [ActionParams](#T-xyLOGIX-Core-Common-Params-ActionParams 'xyLOGIX.Core.Common.Params.ActionParams') class.

##### Parameters

This method has no parameters.

##### Remarks

This constructor is called automatically prior to the first instance being
created or before any static members are referenced.



We've decorated this constructor with the `[Log(AttributeExclude = true)]`
attribute in order to simplify the logging output.

<a name='M-xyLOGIX-Core-Common-Params-ActionParams-Invoke'></a>
### Invoke() `method`

##### Returns

Any data that was returned by the
[Action](#P-Core-Common-Params-Interfaces-IActionParams-Action 'Core.Common.Params.Interfaces.IActionParams.Action').

##### Parameters

This method has no parameters.

##### Remarks

If the value of the
[Action](#P-Core-Common-Params-ActionParams-Action 'Core.Common.Params.ActionParams.Action') property is set to a
`null` reference, then this method does nothing and returns
`null`.

<a name='T-xyLOGIX-Core-Common-Params-Properties-Resources'></a>
## Resources `type`

##### Namespace

xyLOGIX.Core.Common.Params.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-xyLOGIX-Core-Common-Params-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-xyLOGIX-Core-Common-Params-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
