﻿// ***************************************************************************************
// MIT LICENCE
// The maintenance and evolution is maintained by the PromptPlus project under MIT license
// ***************************************************************************************

using PPlus.Controls.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading;

namespace PPlus.Controls
{
    internal class SelectControl<T> : BaseControl<T>, IControlSelect<T>
    {
        private readonly SelectOptions<T> _options;
        private Paginator<ItemSelect<T>> _localpaginator;
        private readonly EmacsBuffer _filterBuffer = new(CaseOptions.Uppercase,modefilter:true);
        private Optional<T> _defaultHistoric = Optional<T>.Create(null);

        public SelectControl(IConsoleControl console, SelectOptions<T> options) : base(console, options)
        {
            _options = options;
        }

        #region IControlSelect

        public IControlSelect<T> OrderBy(Expression<Func<T, object>> value)
        {
            _options.IsOrderDescending = false;
            _options.OrderBy = value.Compile();
            return this;
        }

        public IControlSelect<T> OrderByDescending(Expression<Func<T, object>> value)
        {
            _options.IsOrderDescending = true;
            _options.OrderBy = value.Compile();
            return this;
        }

        public IControlSelect<T> Interaction<T1>(IEnumerable<T1> values, Action<IControlSelect<T>, T1> action)
        {
            foreach (var item in values)
            {
                action.Invoke(this, item);
            }
            return this;
        }
        public IControlSelect<T> OverwriteDefaultFrom(string value, TimeSpan? timeout)
        {
            _options.OverwriteDefaultFrom = value;
            if (timeout != null)
            {
                _options.TimeoutOverwriteDefault = timeout.Value;
            }
            return this;
        }

        public IControlSelect<T> AddItem(T value, bool disable = false)
        {
            _options.Items.Add(new ItemSelect<T> { Value = value, Disabled = disable });
            return this;
        }

        public IControlSelect<T> EqualItems(Func<T, T, bool> comparer)
        {
            _options.EqualItems = comparer;
            return this;
        }


        public IControlSelect<T> AddItems(IEnumerable<T> value, bool disable = false)
        {
            foreach (var item in value)
            {
                AddItem(item, disable);
            }
            return this;
        }

        public IControlSelect<T> AddItemsTo(AdderScope scope,params T[] values)
        {
            foreach (var item in values)
            {
                switch (scope)
                {
                    case AdderScope.Disable:
                        {
                            _options.DisableItems.Add(item);
                        }
                        break;
                    case AdderScope.Remove:
                        {
                            _options.RemoveItems.Add(item);
                        }
                        break;
                    default:
                        throw new PromptPlusException($"AdderScope : {scope} Not Implemented");
                }
            }
            return this;
        }

        public IControlSelect<T> AutoSelect()
        {
            _options.AutoSelect = true;
            return this;
        }

        public IControlSelect<T> Config(Action<IPromptConfig> context)
        {
            context?.Invoke(_options);
            return this;
        }

        public IControlSelect<T> Default(T value)
        {
            _options.DefaultValue = Optional<T>.Create(value);
            return this;
        }

        public IControlSelect<T> ChangeDescription(Func<T, string> value)
        {
            _options.DescriptionSelector = value;
            return this;
        }

        public IControlSelect<T> FilterType(FilterMode value)
        {
            _options.FilterType = value;
            return this;
        }

        public IControlSelect<T> PageSize(int value)
        {
            if (value < 1)
            {
                value = 1;
            }
            _options.PageSize = value;
            return this;
        }

        public IControlSelect<T> TextSelector(Func<T, string> value)
        {
            _options.TextSelector = value;
            return this;
        }

        #endregion

        public override string InitControl(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_options.OverwriteDefaultFrom))
            {
                LoadHistory();
            }

            if (typeof(T).IsEnum)
            {
                if (_options.TextSelector == null)
                {
                    _options.TextSelector = EnumDisplay;
                }
                AddEnum();
            }
            else
            {
                if (_options.TextSelector == null)
                {
                    _options.TextSelector = (item) => item.ToString();
                }
            }

            if (_options.EqualItems == null)
            {
                _options.EqualItems = (item1, item2) => item1.Equals(item2);
            }

            foreach (var item in _options.Items)
            {
                item.Text = _options.TextSelector.Invoke(item.Value);
            }

            foreach (var item in _options.RemoveItems)
            {
                int index;
                do
                {
                    index = _options.Items.FindIndex(x => _options.EqualItems(x.Value, item));
                    if (index >= 0)
                    {
                        _options.Items.RemoveAt(index);
                    }
                }
                while (index >= 0);
            }

            foreach (var item in _options.DisableItems)
            {
                List<ItemSelect<T>> founds;
                founds = _options.Items.FindAll(x => _options.EqualItems(x.Value, item));
                if (founds.Any())
                {
                    foreach (var itemfound in founds)
                    {
                        itemfound.Disabled = true;
                    }
                }
            }

            
            Optional<T> defvalue = Optional<T>.s_empty;

            Optional<ItemSelect<T>> defvaluepage = Optional<ItemSelect<T>>.s_empty;

            if (_options.DefaultValue.HasValue)
            { 
                defvalue = Optional<T>.Create(_options.DefaultValue.Value);
            }
            if (_defaultHistoric.HasValue)
            {
                defvalue = Optional<T>.Create(_defaultHistoric.Value);
            }

            if (defvalue.HasValue)
            {
                var found = _options.Items.FirstOrDefault(x => _options.EqualItems(x.Value, defvalue.Value));
                if (found != null && !found.Disabled)
                {
                    defvaluepage = Optional<ItemSelect<T>>.Create(found);
                }
            }

            if (_options.OrderBy != null)
            {
                if (_options.IsOrderDescending)
                {
                    _options.Items = _options.Items.OrderByDescending(x => _options.OrderBy.Invoke(x.Value)).ToList();
                }
                else
                {
                    _options.Items = _options.Items.OrderBy(x => _options.OrderBy.Invoke(x.Value)).ToList();
                }
            }

            _localpaginator = new Paginator<ItemSelect<T>>(
                _options.FilterType,
                _options.Items, 
                _options.PageSize,
                defvaluepage,
                (item1, item2) => item1.UniqueId == item2.UniqueId,
                (item) => item.Text??string.Empty, 
                IsEnnabled);

            if (_localpaginator.TotalCount > 0 &&  _localpaginator.SelectedItem != null && _localpaginator.SelectedItem.Disabled) 
            {
                _localpaginator.UnSelected();
                if (!defvalue.HasValue)
                {
                    _localpaginator.FirstItem();
                }
            }

            if (_options.Items.Count == 0)
            {
                _localpaginator.UnSelected();
            }

            FinishResult = string.Empty;
            if (!_localpaginator.IsUnSelected)
            {
                FinishResult = _localpaginator.SelectedItem.Text;
            }
            return FinishResult;
        }

        public override void FinalizeControl(CancellationToken cancellationToken)
        {
        }

        public override void InputTemplate(ScreenBuffer screenBuffer)
        {
            screenBuffer.WritePrompt(_options, "");
            if (_filterBuffer.Length > 0)
            {
                screenBuffer.WriteFilterSelect(_options, FinishResult, _filterBuffer);
                screenBuffer.WriteTaggedInfo(_options, $" ({Messages.Filter})");
            }
            else
            {
                screenBuffer.WriteAnswer(_options, FinishResult);
                screenBuffer.SaveCursor();
            }
            screenBuffer.WriteLineDescriptionSelect(_options,_localpaginator.SelectedItem);
            screenBuffer.WriteLineValidate(ValidateError, _options);
            screenBuffer.WriteLineTooltipsSelect(_options);

            var subset = _localpaginator.ToSubset();
            foreach (var item in subset)
            {
                if (_localpaginator.TryGetSelectedItem(out var selectedItem) && EqualityComparer<ItemSelect<T>>.Default.Equals(item, selectedItem))
                {
                    screenBuffer.WriteLineSelector(_options, item.Text);
                }
                else
                {
                    if (IsDisabled(item))
                    {
                        screenBuffer.WriteLineNotSelectorDisabled(_options, item.Text);
                    }
                    else
                    {
                        screenBuffer.WriteLineNotSelector(_options, item.Text);
                    }
                }
            }
            screenBuffer.WriteLinePagination(_options, _localpaginator.PaginationMessage());
        }

        public override void FinishTemplate(ScreenBuffer screenBuffer, T result, bool aborted)
        {
            string answer = _options.TextSelector(result);
            if (aborted)
            {
                answer = Messages.CanceledKey;
            }
            screenBuffer.WriteDone(_options, answer);
            screenBuffer.NewLine();
            if (!aborted)
            {
                SaveHistory(result);
            }
        }

        public override ResultPrompt<T> TryResult(CancellationToken cancellationToken)
        {
            var endinput = false;
            var abort = false;
            bool tryagain;
            do
            {
                tryagain = false;
                ClearError();
                var keyInfo = WaitKeypress(cancellationToken);

                if (!keyInfo.HasValue)
                {
                    endinput = true;
                    abort = true;
                    break;
                }
                if (CheckAbortKey(keyInfo.Value))
                {
                    abort = true;
                    endinput = true;
                    break;
                }
                if (CheckTooltipKeyPress(keyInfo.Value))
                {
                    continue;
                }
                if (IskeyPageNavegator(keyInfo.Value, _localpaginator))
                {
                    continue;
                }
                else if (_filterBuffer.TryAcceptedReadlineConsoleKey(keyInfo.Value))
                {
                    _localpaginator.UpdateFilter(_filterBuffer.ToString());
                    if (_localpaginator.Count == 1 && !_localpaginator.IsUnSelected && _options.AutoSelect)
                    {
                        endinput = true;
                    }
                }
                else if (keyInfo.Value.IsPressEnterKey())
                {
                    if (_localpaginator.SelectedIndex >= 0)
                    {
                        if (!_localpaginator.SelectedItem.Disabled)
                        {
                            endinput = true;
                            break;
                        }
                    }
                    else
                    {
                        SetError(string.Format(Messages.MultiSelectMinSelection,1));
                    }
                    break;
                }
                else
                {
                    if (ConsolePlus.Provider == "Memory")
                    {
                        if (!KeyAvailable)
                        {
                            break;
                        }
                    }
                    else
                    {
                        tryagain = true;
                    }
                }
            } while (!cancellationToken.IsCancellationRequested && (KeyAvailable || tryagain));
            if (cancellationToken.IsCancellationRequested)
            {
                _filterBuffer.Clear();
                _localpaginator.UpdateFilter(_filterBuffer.ToString());
                _localpaginator.UnSelected();
                endinput = true;
                abort = true;
            }
            FinishResult = string.Empty;
            if (_localpaginator.SelectedIndex >= 0)
            {
                FinishResult = _localpaginator.SelectedItem.Text;
                return new ResultPrompt<T>(_localpaginator.SelectedItem.Value, abort, !endinput);
            }
            else
            {
                endinput = false;
            }
            if (!string.IsNullOrEmpty(ValidateError) || endinput)
            {
                ClearBuffer();
            }
            var notrender = false;
            if (KeyAvailable)
            {
                notrender = true;
            }
            return new ResultPrompt<T>(default, abort, !endinput, notrender);
        }

        private void SaveHistory(T value)
        {
            if (!string.IsNullOrEmpty(_options.OverwriteDefaultFrom))
            {
                var aux = JsonSerializer.Serialize<T>(value);
                FileHistory.ClearHistory(_options.OverwriteDefaultFrom);
                var hist =  FileHistory.AddHistory(aux, _options.TimeoutOverwriteDefault,null);
                FileHistory.SaveHistory(_options.OverwriteDefaultFrom, hist);
            }
        }

        private void LoadHistory()
        {
            _defaultHistoric = Optional<T>.Create(null);
            if (!string.IsNullOrEmpty(_options.OverwriteDefaultFrom))
            {
                var aux = FileHistory.LoadHistory(_options.OverwriteDefaultFrom, 1);
                if (aux.Count == 1)
                {
                    try
                    {
                        _defaultHistoric = Optional<T>.Create(JsonSerializer.Deserialize<T>(aux[0].History));
                    }
                    catch
                    {
                        //invalid Deserialize history 
                    }
                }
            }
        }

        private bool IsDisabled(ItemSelect<T> item)
        {
            return _options.Items.Any(x => x.UniqueId == item.UniqueId && x.Disabled);
        }

        private bool IsEnnabled(ItemSelect<T> item)
        {
            return !IsDisabled(item);
        }

        private void AddEnum()
        {

            var aux = Enum.GetValues(typeof(T));
            var result = new List<Tuple<int, ItemSelect<T>>>();
            foreach (var item in aux)
            {
                var name = item.ToString();
                var displayAttribute = typeof(T).GetField(name)?.GetCustomAttribute<DisplayAttribute>();
                var order = displayAttribute?.GetOrder() ?? int.MaxValue;
                result.Add(new Tuple<int, ItemSelect<T>>(order, new ItemSelect<T> { Value = (T)item, Text = _options.TextSelector((T)item) }));
            }
            foreach (var item in result.OrderBy(x => x.Item1))
            {
                _options.Items.Add(item.Item2);
            }
        }

        private string EnumDisplay(T value)
        {
            var name = value.ToString();
            var displayAttribute = value.GetType().GetField(name)?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.GetName() ?? name;
        }
    }
}
