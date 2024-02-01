<a name='assembly'></a>
# xyLOGIX.Core.Common.Params.Factories

## Contents

- [MakeNewActionParams](#T-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams 'xyLOGIX.Core.Common.Params.Factories.MakeNewActionParams')
  - [DynamicInvoke(self)](#M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-DynamicInvoke-xyLOGIX-Core-Common-Params-Interfaces-IActionParams- 'xyLOGIX.Core.Common.Params.Factories.MakeNewActionParams.DynamicInvoke(xyLOGIX.Core.Common.Params.Interfaces.IActionParams)')
  - [For(action)](#M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-For-System-Delegate- 'xyLOGIX.Core.Common.Params.Factories.MakeNewActionParams.For(System.Delegate)')
  - [WithArguments(self,arguments)](#M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-WithArguments-xyLOGIX-Core-Common-Params-Interfaces-IActionParams,System-Object[]- 'xyLOGIX.Core.Common.Params.Factories.MakeNewActionParams.WithArguments(xyLOGIX.Core.Common.Params.Interfaces.IActionParams,System.Object[])')
- [Resources](#T-xyLOGIX-Core-Common-Params-Factories-Properties-Resources 'xyLOGIX.Core.Common.Params.Factories.Properties.Resources')
  - [Culture](#P-xyLOGIX-Core-Common-Params-Factories-Properties-Resources-Culture 'xyLOGIX.Core.Common.Params.Factories.Properties.Resources.Culture')
  - [ResourceManager](#P-xyLOGIX-Core-Common-Params-Factories-Properties-Resources-ResourceManager 'xyLOGIX.Core.Common.Params.Factories.Properties.Resources.ResourceManager')

<a name='T-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams'></a>
## MakeNewActionParams `type`

##### Namespace

xyLOGIX.Core.Common.Params.Factories

##### Summary

Creates new instances of objects that implement the
[IActionParams](#T-Core-Common-Params-Interfaces-IActionParams 'Core.Common.Params.Interfaces.IActionParams') interface, and
returns references to them.

<a name='M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-DynamicInvoke-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-'></a>
### DynamicInvoke(self) `method`

##### Summary

Dynamically invokes (late-bound) the method represented by the
current delegate.

##### Returns

`null` if the `self` parameter or
the [Action](#P-Core-Common-Params-Interfaces-IActionParams-Action 'Core.Common.Params.Interfaces.IActionParams.Action')
property property of the `self` parameter is set to a
`null` reference, or if the
[Action](#P-Core-Common-Params-Interfaces-IActionParams-Action 'Core.Common.Params.Interfaces.IActionParams.Action') returns
`void`; otherwise, a reference to the data returned by the code specified
by the [Action](#P-Core-Common-Params-Interfaces-IActionParams-Action 'Core.Common.Params.Interfaces.IActionParams.Action')
property.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| self | [xyLOGIX.Core.Common.Params.Interfaces.IActionParams](#T-xyLOGIX-Core-Common-Params-Interfaces-IActionParams 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams') | (Required.) Reference to an instance of an object that
implements the [IActionParams](#T-Core-Common-Params-Interfaces-IActionParams 'Core.Common.Params.Interfaces.IActionParams')
interface. |

<a name='M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-For-System-Delegate-'></a>
### For(action) `method`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| action | [System.Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') | (Required.) Reference to an instance of
[Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') that should be invoked. Reference to the same
instance of the object that called this method, for fluent use. |

<a name='M-xyLOGIX-Core-Common-Params-Factories-MakeNewActionParams-WithArguments-xyLOGIX-Core-Common-Params-Interfaces-IActionParams,System-Object[]-'></a>
### WithArguments(self,arguments) `method`

##### Summary

Initializes the value of the
[Args](#P-Core-Common-Params-Interfaces-IActionParams-Args 'Core.Common.Params.Interfaces.IActionParams.Args') property.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| self | [xyLOGIX.Core.Common.Params.Interfaces.IActionParams](#T-xyLOGIX-Core-Common-Params-Interfaces-IActionParams 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams') | (Required.) Reference to an instance of an object that
implements the [IActionParams](#T-Core-Common-Params-Interfaces-IActionParams 'Core.Common.Params.Interfaces.IActionParams')
interface. |
| arguments | [System.Object[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object[] 'System.Object[]') | (Required.) Array of references to objects that should
be passed when the delegate is invoked. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Thrown if the required
parameter, `self`, is passed a `null` value. |

##### Remarks

If no `arguments` are passed to this method, then
the method does nothing.

<a name='T-xyLOGIX-Core-Common-Params-Factories-Properties-Resources'></a>
## Resources `type`

##### Namespace

xyLOGIX.Core.Common.Params.Factories.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-xyLOGIX-Core-Common-Params-Factories-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-xyLOGIX-Core-Common-Params-Factories-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
