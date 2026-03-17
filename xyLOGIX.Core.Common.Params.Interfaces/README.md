<a name='assembly'></a>
# xyLOGIX.Core.Common.Params.Interfaces

## Contents

- [IActionParams](#T-xyLOGIX-Core-Common-Params-Interfaces-IActionParams 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams')
  - [Action](#P-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Action 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams.Action')
  - [Arguments](#P-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Arguments 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams.Arguments')
  - [Invoke()](#M-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Invoke 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams.Invoke')
- [Resources](#T-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources 'xyLOGIX.Core.Common.Params.Interfaces.Properties.Resources')
  - [Culture](#P-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources-Culture 'xyLOGIX.Core.Common.Params.Interfaces.Properties.Resources.Culture')
  - [ResourceManager](#P-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources-ResourceManager 'xyLOGIX.Core.Common.Params.Interfaces.Properties.Resources.ResourceManager')

<a name='T-xyLOGIX-Core-Common-Params-Interfaces-IActionParams'></a>
## IActionParams `type`

##### Namespace

xyLOGIX.Core.Common.Params.Interfaces

##### Summary

Defines the publicly-exposed methods and properties of a POCO that
carries information about a [Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') and its arguments
(if any).

<a name='P-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Action'></a>
### Action `property`

##### Summary

Gets or sets a reference to an instance of
[Delegate](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Delegate 'System.Delegate') that is to be invoked.

<a name='P-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Arguments'></a>
### Arguments `property`

<a name='M-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Invoke'></a>
### Invoke() `method`

##### Returns

Any data that was returned by the
[Action](#P-xyLOGIX-Core-Common-Params-Interfaces-IActionParams-Action 'xyLOGIX.Core.Common.Params.Interfaces.IActionParams.Action').

##### Parameters

This method has no parameters.

<a name='T-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources'></a>
## Resources `type`

##### Namespace

xyLOGIX.Core.Common.Params.Interfaces.Properties

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources-Culture'></a>
### Culture `property`

##### Summary

Overrides the current thread's CurrentUICulture property for all
  resource lookups using this strongly typed resource class.

<a name='P-xyLOGIX-Core-Common-Params-Interfaces-Properties-Resources-ResourceManager'></a>
### ResourceManager `property`

##### Summary

Returns the cached ResourceManager instance used by this class.
