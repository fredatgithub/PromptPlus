# <img align="left" width="100" height="100" src="./docs/images/icon.png">Welcome to PromptPlus
[![Build](https://github.com/FRACerqueira/PromptPlus/workflows/Build/badge.svg)](https://github.com/FRACerqueira/PromptPlus/actions/workflows/build.yml)
[![Publish](https://github.com/FRACerqueira/PromptPlus/actions/workflows/publish.yml/badge.svg)](https://github.com/FRACerqueira/PromptPlus/actions/workflows/publish.yml)
[![License](https://img.shields.io/github/license/FRACerqueira/PromptPlus)](https://github.com/FRACerqueira/PromptPlus/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/PromptPlus)](https://www.nuget.org/packages/PromptPlus/)
[![Downloads](https://img.shields.io/nuget/dt/PromptPlus)](https://www.nuget.org/packages/PromptPlus/)


**Interactive command-line toolkit for .Net core with powerful controls and commands to create professional console applications.**

**PromptPlus** was developed in c# with the **netstandard2.1**, **.Net 6** and **.Net 7** target frameworks.
**[Visit the official page for more documentation of PromptPlus](https://fracerqueira.github.io/PromptPlus)**

## Table of Contents

- [Features](#features)
- [Migrate Version V3.3 to V4.0](#migrate-version)
- [Console Engine](#console-engine)
- [Installing](#installing)
- [Examples](#examples)
- [Controls Snapshot](#controls-snapshot)
- [Usage](#usage)
- [Culture](#culture)
- [Colors](#colors)
- [Hotkeys](#hotkeys)
- [Keypress Extensions Emacs](#keypress-extensions-emacs)
- [Validators](#validators)
- [Supported Platforms](#supported-platforms)
- [Inspiration Notes](#inspiration-notes)
- [Code of Conduct](#code-of-conduct)
- [Contributing](#contributing)
- [License](#license)
- [API Reference](https://fracerqueira.github.io/PromptPlus/apis/apis.html)

## Whats news in V4.0.4
- Fixed bug PromptPlus not restore terminal original setting when shutdown application
- Fixed bug Autocomplete does not change result when backspace is pressed during search
- Added Property CurrentBuffer in console drive to return Current Buffer running (Primary/Secondary)
- Added SwapBuffer command to swith Primary/Secondar buffer (Valid only When console 'ansi'  suported)
- Renamed 'AlternateScreen' to 'RunOnBuffer'. Now executes a custom action on TargetBuffer and returns to CurrentBuffer
- Refactored console drivers initialization, control options initialization
- Added auto create Environment 'PromptPlusConvertCodePage' to custom automate convert codepage to unicode-codepage
    - Default value is = '850;65001'

## Whats news in V4.0.3
- New control to switch Alternate screen 
- Fixed bug Console does not change foreground/background color correctly
- Fixed bug Control ProgressBar
    - Not show gradient when setted ProgressBarType.Fill
- Improve testability of result classes/struct (Internal to public)

## Whats news in V4.0.2
- New Control Pipeline
    - PromptPlus.Pipeline(T startvalue)
- Changed WaitControl to take context value in tasks and return context in result
    - There are small break-chages  
- Add Answer key check equals "Yes"/"No" using config values
    - IsYesResponseKey(this ConsoleKeyInfo keyinfo)
    - IsNoResponseKey(this ConsoleKeyInfo keyinfo)

## Whats news in V4.0.X

### Newest controls and color improvement and layout

![](./docs/images/calendar1.gif)
![](./docs/images/chartbar1.gif)
![](./docs/images/multiselectbrowser1.gif)
![](./docs/images/treeview1.gif)
![](./docs/images/treeview2.gif)
![](./docs/images/waittask1.gif)
![](./docs/images/progressbar1.gif)

## Features
[**Top**](#table-of-contents)

**All features have IntelliSense. The PromptPlus have more 20 controls with many features like: filters, validators, history, sugestions, spinner(19 embeding type and plus custom yours!), colors and styles for control-elements** :
- Banner Ascii
- Input text / Secret / AutoComplete with spinner
- MaskEdit Generic / Only Date / Only Time / DateTime / Number /  Currency
- Calendar with multiple layouts
- Select and Multi-Select(with group select!) 
- AddTo(Add/Remove) items for text and masked text
- Wait Keypress with animate spinner
- Slider numeric ranger with gradient colors
- Up-Down numeric ranger 
- Switch (style on/off)
- Wait Process (Run background tasks Sequential/Parallel) with elapsedtime and spinner 
- Wait Time with countdown and spinner
- ChartBar with enabled Interaction to switch layout, Legend and order when browse the charts / Legends.
- Progress bar with 8 types , gradient colors and spinner
- Browser File and Folder with multi-select, colors and spinner
- Treeview hierarchical structures with multi-select and colors
- Switch Alternate screen
- Execution pipeline with conditions

**All controls** have the same organization (see in action: [**Controls Snapshot**](#controls-snapshot)):
- input/filter (except Masked input) using **[GNU Readline](https://en.wikipedia.org/wiki/GNU_Readline) Emacs keyboard shortcuts**.  
- Prompt, description and data entry (ever)
- Extra actions per stage : OnStartControl/OnInputRender/OnTryAcceptInput/OnFinishControl (ever)
- Tooltips (ever and configurable) 
- Filter by Contains / StartsWith (configurable) (depends on the control)
- Collection subset items and interations (depends on the control)
- Page information and page-size(depends on the control)
- Spinner animmation (depends on the control)
- Error message (depends on the control and validators)
 
PromptPlus driver console  **Supports 4/8/24-bit colors** in the terminal with **auto-detection** of the current terminal's capabilities.

## Migrate Version
[**Top**](#table-of-contents)

Until version 3 the console engine was based on a model from another project that has several serious problems that cause exceptions during execution in addition to increasing the complexity of the code for correct rendering...
**PromptPlus v4** has been **completely rebuilt** for a better experience, with significant improvements with new controls and more developer power. The console driver now supports better rendering, with the ability to detect terminal capabilities and allow for 24-bit color, text overflow strategies based on terminal size, and left and right margins for a nicer layout.
**The Controls have been revised to be more responsive, allow color styles in many of their elements**, and adapt to the terminal size even with resizing.

For migrate V3.3 to V4.0 [**see this link**](https://fracerqueira.github.io/PromptPlus/migrateversion.html).

## Console Engine
[**Top**](#table-of-contents)

The console driver have the ability to detect terminal capabilities and allow for **24-bit color and text overflow strategies**  based on terminal size, and left and right margins for a nicer layout and automatic color conversion.
The new engine detects support ansi commands and adjust output for this functionality respecting OS differences , terminal mode and Windows console mode. The Colors are automatically adjusted to the capacity of the terminal. This automatic adjustment may slightly modify the final color when converting to a lower bit resolution.

### Sample Output detect (ConsoleFeaturesSamples)
![](./docs/images/consoleinfo.gif)

### Sample Output Overflow Capacity (ConsoleFeaturesSamples)

![](./docs/images/consoleoverflowcapacity.gif)

### Sample color capacity (ConsoleFeaturesSamples)

**_Note: This layout and code was inspired by the excellent project:spectreconsole, having the same color palette_**

![](./docs/images/consolecolorcapacity.gif)

## Installing
[**Top**](#table-of-contents)

```
Install-Package PromptPlus [-pre]
```

```
dotnet add package PromptPlus [--prerelease]
```

**_Note:  [-pre]/[--prerelease] usage for pre-release versions_**

## Examples
[**Top**](#table-of-contents)

The folder [**Samples**](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples) contains more **30** samples!.

```
dotnet run --project [name of sample]
```

## Controls Snapshot

For each snapshot, the title is **name of projet** sample in folder **samples**

### Pipeline

[**Top**](#table-of-contents)  | [PipelineSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/PipelineSamples)

**Not have snapshot**

### Alternate screen

[**Top**](#table-of-contents) | [AlternateScreenSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/AlternateScreenSamples)

![](./docs/images/alternatescreen1.gif)

### Input

[**Top**](#table-of-contents) | [InputBasicSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputBasicSamples)

![](./docs/images/inputsample1.gif)

[**Top**](#table-of-contents) | [InputSecretSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputSecretSamples)

![](./docs/images/inputsample2.gif)

[**Top**](#table-of-contents) | [InputWithHistorySamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputWithHistorySamples)

![](./docs/images/inputsample3.gif)

[**Top**](#table-of-contents) | [InputWithSugestionSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputWithSugestionSamples)

![](./docs/images/inputsample4.gif)

[**Top**](#table-of-contents) | [InputWithValidatorSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputWithValidatorSamples)

![](./docs/images/inputsample5.gif)

**Other samples input**

[**Top**](#table-of-contents) | [InputOverwriteDefaultFromSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/InputOverwriteDefaultFromSamples)

### Calendar

[**Top**](#table-of-contents) | [CalendarSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/CalendarSamples)

![](./docs/images/calendar1.gif)

### AutoComplete

[**Top**](#table-of-contents) | [AutoCompleteSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/AutoCompleteSamples)

![](./docs/images/autocompletesample1.gif)

### MaskEdit

[**Top**](#table-of-contents) | [MaskEditGenericSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditGenericSamples)

![](./docs/images/maskedit1.gif)

[**Top**](#table-of-contents) | [MaskEditDateTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditDateTypeSamples)

![](./docs/images/maskedit2.gif)

[**Top**](#table-of-contents) | [MaskEditTimeTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditTimeTypeSamples)

![](./docs/images/maskedit3.gif)

[**Top**](#table-of-contents) | [MaskEditDateTimeTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditDateTimeTypeSamples)

![](./docs/images/maskedit4.gif)

[**Top**](#table-of-contents) | [MaskEditNumberTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditNumberTypeSamples)

![](./docs/images/maskedit5.gif)

[**Top**](#table-of-contents) | [MaskEditCurrencyTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MaskEditCurrencyTypeSamples)

![](./docs/images/maskedit6.gif)

### KeyPress

[**Top**](#table-of-contents) | [KeyPressSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/KeyPressSamples)

![](./docs/images/keypress1.gif)

[**Top**](#table-of-contents) | [ConfirmSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/ConfirmSamples)

![](./docs/images/confirm1.gif)

### Select

[**Top**](#table-of-contents) | [SelectBasicSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SelectBasicSamples)

![](./docs/images/select1.gif)

**Other samples Select**

[**Top**](#table-of-contents) | [SelectUserScopeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SelectUserScopeSamples) ,
[SelectUserTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SelectUserTypeSamples)

### Multi Select
[**Top**](#table-of-contents)

[MultiSelectBasicSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MultiSelectBasicSamples)

![](./docs/images/multiselect1.gif)

**Other samples Multi-Select**
[MultiSelectUserScopeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MultiSelectUserScopeSamples) ,
[MultiSelectUserTypeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/MultiSelectUserTypeSamples)

### Wait Process

[**Top**](#table-of-contents) | [WaitTasksSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/WaitTasksSamples)

![](./docs/images/waittask1.gif)

### Wait Time

[**Top**](#table-of-contents) | [WaitTimerSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/WaitTimerSamples)

![](./docs/images/waittime1.gif)

### Chart Bar

[**Top**](#table-of-contents) | [ChartSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/ChartSamples)

![](./docs/images/chartbar1.gif)

### Progress Bar

[**Top**](#table-of-contents) | [ProgressBarSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/ProgressBarSamples)

![](./docs/images/progressbar1.gif)

### Slider Switch

[**Top**](#table-of-contents) | [SliderSwitchSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SliderSwitchSamples)

![](./docs/images/sliderswitch1.gif)

### Slider Number

[**Top**](#table-of-contents) | [SliderNumberUpDownModeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SliderNumberUpDownModeSamples)

![](./docs/images/slidernumber2.gif)

[**Top**](#table-of-contents) | [SliderNumberLeftRightModeSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/SliderNumberLeftRightModeSamples)

![](./docs/images/slidernumber1.gif)

### Add to List

[**Top**](#table-of-contents) | [AddToListSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/AddToListSamples)

![](./docs/images/addtolist1.gif)

[**Top**](#table-of-contents) | [AddtoMaskEditListSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/AddtoMaskEditListSamples)

![](./docs/images/addtolist2.gif)

### Browser Select

[**Top**](#table-of-contents) | [BrowserSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/BrowserSamples)

![](./docs/images/browser1.gif)

### Browser Multi Select

[**Top**](#table-of-contents) | [BrowserMultSelectSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/BrowserMultSelectSamples)

![](./docs/images/multiselectbrowser1.gif)

### TreeView Select

[**Top**](#table-of-contents) | [TreeViewSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/TreeViewSamples)

![](./docs/images/treeview1.gif)

### TreeView Multi Select

[**Top**](#table-of-contents) | [TreeViewMultiSelectSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/TreeViewMultiSelectSamples)

![](./docs/images/treeview2.gif)

### Banner

[**Top**](#table-of-contents) | [BannerSamples](https://github.com/FRACerqueira/PromptPlus/tree/main/Samples/BannerSamples)

![](./docs/images/banner1.gif)

## Usage
[**Top**](#table-of-contents)

All controls use **fluent interface**; an object-oriented API whose design relies extensively on method chaining. Its goal is to increase code legibility. The term was coined in 2005 by Eric Evans and Martin Fowler.
```csharp
//MaskEdit Generic
var mask = PromptPlus.MaskEdit("input", "MaskEdit Generic input")
    .Mask(@"\XYZ 9{3}-L{3}-C[ABC]N{1}[XYZ]-A{3}")
    .DescriptionWithInputType(FormatWeek.Short)
    .Run();

if (!mask.IsAborted)
{
    PromptPlus.WriteLine($"You input with mask is {mask.Value.Masked}");
    PromptPlus.WriteLine($"You input without mask is {mask.Value.Input}");
}

//AnyKey
var kp1 = PromptPlus
    .KeyPress()
    .Run();

if (!kp1.IsAborted)
{
    PromptPlus.WriteLine($"You Pressed {kp1.Value.Key}");
}

//input
var in1 = PromptPlus
    .Input("Input sample1")
    .Run();

if (!in1.IsAborted)
{
    PromptPlus.WriteLine($"You input is {in1.Value}");
}
```

## Culture
[**Top**](#table-of-contents)

PromptPlus applies the language/culture **only when running controls**. The language/culture of the application is **not affected**. If language/culture is not informed, the application's language/culture will be used with fallback to en-US.

All messages are affected when changed language/culture. PromptPlus has languages embeded:
- en-US (Default)
- pt-BR

To use a non-embedded language/culture:

- Copy the **PromptPlusResources.resx** file in folder PromptPlus/Resources
- Translate messages with same format to your language/culture
- Convert .resx files to binary .resources files ([**reference link here**](https://docs.microsoft.com/en-us/dotnet/core/extensions/work-with-resx-files-programmatically))
- Publish the compiled file (**PromptPlus.[Language].resources**) in the same folder as the binaries.

## Colors
[**Top**](#table-of-contents)

PromptPlus is in accordance with informal standard [**NO COLOR**](https://no-color.org/). when there is the environment variable "no_color" the colors are disabled.

PromptPlus also has commands for coloring parts of the text using **direct console, styles and Over elememnts of controls**.

Promptplus uses the **same default colors and engine(softly modified)** as the third party project: spectreconsole.
For more details [visit the **official page**](https://fracerqueira.github.io/PromptPlus/#colors) or see the samples in folder **Samples**


## Hotkeys
[**Top**](#table-of-contents)

Hotkeys (global and control-specific) are configurable. Some hotkeys are internal and reserved.
For more details [visit the **official page**](https://fracerqueira.github.io/PromptPlus/#hotkeys)

## Keypress Extensions Emacs
[**Top**](#table-of-contents)

PromptPlus have a lot extensions to check Key-press with GNU Readline Emacs keyboard shortcuts.
For more details [visit the **official page**](https://fracerqueira.github.io/PromptPlus/#keypress-extensions-emacs)

## Validators

PromptPlus have a lot extensions to **commons validator** and **validator import**(No duplicate code!) 
For more details [visit the **official page**](https://fracerqueira.github.io/PromptPlus/#validators) or see the samples in folder **Samples**

```csharp
private class MylCass
{
    [Required(ErrorMessage = "{0} is required!")]
    [MinLength(3, ErrorMessage = "Min. Length = 3.")]
    [MaxLength(5, ErrorMessage = "Max. Length = 5.")]
    [Display(Prompt ="My Input")]
    public string MyInput { get; set; }
}
```
```csharp
var inst = new MylCass();

PromptPlus
    .Input("Input sample2", "import validator from decorate")
    .Default(inst.Text)
    .AddValidators(PromptValidators.ImportValidators(inst,x => x!.Text!))
    .Run();

if (name.IsAborted)
{
   return;
}
PromptPlus.WriteLine($"Your input: {name.Value}!");
```

## Supported platforms
[**Top**](#table-of-contents)

- Windows
    - Command Prompt, PowerShell, Windows Terminal
- Linux (Ubuntu, etc)
    - Windows Terminal (WSL 2)
- macOS
    - Terminal.app

## Inspiration notes
[**Top**](#table-of-contents)

- FIGlet was inspired by the work of [FIGlet.Net](https://github.com/WenceyWang/FIGlet.Net).
- The colors and some of its dependencies were inspired by the work of [spectreconsole](https://spectreconsole.net/).
- The API Reference was inspired by the work of [xmldoc2md (Fork with improvements)](https://github.com/FRACerqueira/xmldoc2md).
- The EastAsianWidth and base-control was inspired by the work of [Sharprompt](https://github.com/shibayan/Sharprompt)

## Code of Conduct
[**Top**](#table-of-contents)

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [Code of Conduct](CODE_OF_CONDUCT.md).

## Contributing

See the [Contributing guide](CONTRIBUTING.md) for developer documentation.

## License
[**Top**](#table-of-contents)

Copyright 2021 @ Fernando Cerqueira

PromptPlus project is licensed under the  the MIT license. For more information see [License](LICENSE.md).