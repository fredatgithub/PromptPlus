﻿// ***************************************************************************************
// MIT LICENCE
// The maintenance and evolution is maintained by the PromptPlus project under MIT license
// ***************************************************************************************

namespace PPlus.Controls
{
    /// <summary>
    /// Represents The Sugestion Input struct.
    /// </summary>
    public readonly struct SugestionInput
    {
        /// <summary>
        /// Create a SugestionInput
        /// </summary>
        /// <remarks>
        /// Do not use this constructor!
        /// </remarks>
        public SugestionInput()
        {
            throw new PromptPlusException("SugestionInput CTOR NotImplemented");
        }

        internal SugestionInput(string input, object context)
        {
            Text = input;
            Context = context;
        }

        /// <summary>
        /// Get Sugestion Text input
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Get generic context parameter 
        /// </summary>
        public object Context { get; }
    }
}
