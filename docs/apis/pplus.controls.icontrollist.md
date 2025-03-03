# <img align="left" width="100" height="100" src="../images/icon.png">PromptPlus API:IControlList 

[![Build](https://github.com/FRACerqueira/PromptPlus/workflows/Build/badge.svg)](https://github.com/FRACerqueira/PromptPlus/actions/workflows/build.yml)
[![Publish](https://github.com/FRACerqueira/PromptPlus/actions/workflows/publish.yml/badge.svg)](https://github.com/FRACerqueira/PromptPlus/actions/workflows/publish.yml)
[![License](https://img.shields.io/github/license/FRACerqueira/PromptPlus)](https://github.com/FRACerqueira/PromptPlus/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/PromptPlus)](https://www.nuget.org/packages/PromptPlus/)
[![Downloads](https://img.shields.io/nuget/dt/PromptPlus)](https://www.nuget.org/packages/PromptPlus/)

[**Back to List Api**](./apis.md)

# IControlList

Namespace: PPlus.Controls

Represents the interface with all Methods of the AddtoList control

```csharp
public interface IControlList : IPromptControls<IEnumerable<String>>
```

Implements [IPromptControls&lt;IEnumerable&lt;String&gt;&gt;](./pplus.controls.ipromptcontrols-1.md)

## Methods

### <a id="methods-acceptinput"/>**AcceptInput(Func&lt;Char, Boolean&gt;)**

Execute a function to accept input.
 <br>If result true accept input; otherwise, ignore input.

```csharp
IControlList AcceptInput(Func<Char, Boolean> value)
```

#### Parameters

`value` [Func&lt;Char, Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
function to accept input

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-additem"/>**AddItem(String, Boolean)**

Add item to list

```csharp
IControlList AddItem(string value, bool immutable)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text item to add

`immutable` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true the item cannot be removed; otherwise yes.

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-additems"/>**AddItems(IEnumerable&lt;String&gt;, Boolean)**

Add items colletion to list

```csharp
IControlList AddItems(IEnumerable<String> values, bool immutable)
```

#### Parameters

`values` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
items colletion to add

`immutable` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true the item cannot be removed; otherwise yes.

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-addvalidators"/>**AddValidators(params Func&lt;Object, ValidationResult&gt;[])**

Add a validator to accept sucessfull finish of control.
 <br>Tip: see  to validators embeding

```csharp
IControlList AddValidators(params Func<Object, ValidationResult>[] validators)
```

#### Parameters

`validators` [Func&lt;Object, ValidationResult&gt;[]](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
the function validator.

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-allowduplicate"/>**AllowDuplicate()**

Allow duplicate items.Default value for this control is false.

```csharp
IControlList AllowDuplicate()
```

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-config"/>**Config(Action&lt;IPromptConfig&gt;)**

Custom config the control.

```csharp
IControlList Config(Action<IPromptConfig> context)
```

#### Parameters

`context` [Action&lt;IPromptConfig&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>
action to apply changes. [IPromptConfig](./pplus.controls.ipromptconfig.md)

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-default"/>**Default(String)**

Default initial value when when stated.

```csharp
IControlList Default(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
initial value

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-hotkeyedititem"/>**HotKeyEditItem(HotKey)**

Overwrite a HotKey to edit item. Default value is 'F2'

```csharp
IControlList HotKeyEditItem(HotKey value)
```

#### Parameters

`value` [HotKey](./pplus.controls.hotkey.md)<br>
The [HotKey](./pplus.controls.hotkey.md) to edit item

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-hotkeyremoveitem"/>**HotKeyRemoveItem(HotKey)**

Overwrite a HotKey to remove item. Default value is 'F3'

```csharp
IControlList HotKeyRemoveItem(HotKey value)
```

#### Parameters

`value` [HotKey](./pplus.controls.hotkey.md)<br>
The [HotKey](./pplus.controls.hotkey.md) to remove item

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-inputtocase"/>**InputToCase(CaseOptions)**

Transform char input using [CaseOptions](./pplus.controls.caseoptions.md).

```csharp
IControlList InputToCase(CaseOptions value)
```

#### Parameters

`value` [CaseOptions](./pplus.controls.caseoptions.md)<br>
Transform option

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-interaction"/>**Interaction&lt;T&gt;(IEnumerable&lt;T&gt;, Action&lt;IControlList, T&gt;)**

Execute a action foreach item of colletion passed as a parameter

```csharp
IControlList Interaction<T>(IEnumerable<T> values, Action<IControlList, T> action)
```

#### Type Parameters

`T`<br>
Type external colletion

#### Parameters

`values` IEnumerable&lt;T&gt;<br>
Colletion for interaction

`action` Action&lt;IControlList, T&gt;<br>
Action to execute

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-maxlenght"/>**MaxLenght(UInt16)**

MaxLenght of input text.

```csharp
IControlList MaxLenght(ushort value)
```

#### Parameters

`value` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>
Lenght

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-pagesize"/>**PageSize(Int32)**

Set max.item view per page.Default value for this control is 10.

```csharp
IControlList PageSize(int value)
```

#### Parameters

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of Max.items

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-range"/>**Range(Int32, Nullable&lt;Int32&gt;)**

Defines a minimum and maximum (optional) range of items in the list

```csharp
IControlList Range(int minvalue, Nullable<Int32> maxvalue)
```

#### Parameters

`minvalue` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Minimum number of items

`maxvalue` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Maximum number of items

#### Returns

[IControlList](./pplus.controls.icontrollist.md)

### <a id="methods-suggestionhandler"/>**SuggestionHandler(Func&lt;SugestionInput, SugestionOutput&gt;)**

Add Suggestion Handler feature

```csharp
IControlList SuggestionHandler(Func<SugestionInput, SugestionOutput> value)
```

#### Parameters

`value` [Func&lt;SugestionInput, SugestionOutput&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
function to apply suggestions. [SugestionInput](./pplus.controls.sugestioninput.md) and [SugestionOutput](./pplus.controls.sugestionoutput.md)

#### Returns

[IControlList](./pplus.controls.icontrollist.md)


- - -
[**Back to List Api**](./apis.md)
