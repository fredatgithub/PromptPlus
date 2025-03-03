﻿// ***************************************************************************************
// MIT LICENCE
// The maintenance and evolution is maintained by the PromptPlus project under MIT license
// ***************************************************************************************

using System;
using System.Collections.Generic;
using System.Threading;

namespace PPlus.Controls
{
    internal class PipelineOptions<T> : BaseOptions
    {
        private PipelineOptions() : base(null, null, null, true)
        {
            throw new PromptPlusException("PipelineOptions CTOR NotImplemented");
        }

        internal PipelineOptions(StyleSchema styleSchema, ConfigControls config, IConsoleControl console, bool showcursor) : base(styleSchema, config, console, showcursor)
        {
            Pipes = new Dictionary<string, Action<EventPipe<T>, CancellationToken>>();
            Conditions = new Dictionary<string, Func<EventPipe<T>, CancellationToken, bool>>();
        }

        public T CurrentValue { get; set; }

        public Dictionary<string, Action<EventPipe<T>, CancellationToken>> Pipes { get; }
        public Dictionary<string, Func<EventPipe<T>, CancellationToken, bool>> Conditions { get; }

        public void AddPipe(string key, Action<EventPipe<T>, CancellationToken> value)
        { 
            Pipes.Add(key, value);  
        }

        public void AddCondition(string key, Func<EventPipe<T>, CancellationToken, bool> value)
        {
            Conditions.Add(key, value);
        }

        public string CurrentPipe { get; set; }
        public bool AbortPipeline { get; set; }
        public bool FinishedPipeline { get; set; }

    }
}
