﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace PPlus.Controls
{
    ///<inheritdoc cref="IPromptControls{T}"/>
    /// <summary>
    /// Represents the interface with all Methods of the AutoComplete control
    /// </summary>
    public interface IControlAutoComplete: IPromptControls<string>
    {
        /// <summary>
        /// Set max.item view per page.Default value for this control is 10.
        /// </summary>
        /// <param name="value">Number of Max.items</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete PageSize(int value);

        /// <summary>
        /// Overwrite <see cref="SpinnersType"/>. Default value is SpinnersType.Ascii
        /// <br>When use custom spinner, if has unicode values console does not support it, the rendering may not be as expected</br>
        /// </summary>
        /// <param name="spinnersType">The <see cref="SpinnersType"/></param>
        /// <param name="SpinnerStyle">Style of spinner. <see cref="Style"/></param>
        /// <param name="speedAnimation">Number of mileseconds foreach interation of spinner. Valid only to SpinnersType.custom, otherwise will be ignored</param>
        /// <param name="customspinner">IEnumerable values for custom spinner. Valid only to SpinnersType.custom, otherwise will be ignored</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete Spinner(SpinnersType spinnersType, Style? SpinnerStyle = null, int? speedAnimation = null, IEnumerable<string>? customspinner = null);

        /// <summary>
        /// Number minimum of chars to accept autocomplete
        /// <br>Default value : 3. If value less than 0 internal sette to 0.</br>
        /// </summary>
        /// <param name="value">Number of chars</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete MinimumPrefixLength(int value);

        /// <summary>
        /// Number of mileseconds to wait before to start function autocomplete
        /// <br>Default value : 1000. If value less than 10 internal sette to 10.</br>
        /// </summary>
        /// <param name="value">Number of mileseconds</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete CompletionWaitToStart(int value);

        /// <summary>
        /// The max.items to return from function autocomplete.
        /// </summary>
        /// <param name="value">Number of max.items</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete CompletionMaxCount(int value);

        /// <summary>
        /// The function to execute autocomplete. This function is requeried to run!
        /// <br>First param is a current text input</br>
        /// <br>Second param is current cursor postion at text input</br>
        /// <br>third parameter is the control cancellation token</br>
        /// </summary>
        /// <param name="value">function to autocomplete</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete CompletionAsyncService(Func<string, int, CancellationToken, Task<string[]>> value);

        /// <summary>
        /// Default value when finished value is empty.
        /// </summary>
        /// <param name="value">Finished value default</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete DefaultIfEmpty(string value);

        /// <summary>
        /// Default value when stated.
        /// </summary>
        /// <param name="value">Value default</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete Default(string value);

        /// <summary>
        /// Overwrite default start value with last result saved on history.
        /// </summary>
        /// <param name="value">name of file to save history</param>
        /// <param name="timeout">The timeout for valid items saved. Default value is 365 days</param>
        /// <returns>IControlAutoComplete</returns>
        IControlAutoComplete OverwriteDefaultFrom(string value, TimeSpan? timeout = null);

        /// <summary>
        /// Transform char input using <see cref="CaseOptions"/>.
        /// </summary>
        /// <param name="value">Transform option</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete InputToCase(CaseOptions value);

        /// <summary>
        /// Execute a function to accept char input.
        /// <br>If result true accept char input; otherwise, ignore char input.</br>
        /// </summary>
        /// <param name="value">function to accept</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete AcceptInput(Func<char, bool> value);

        /// <summary>
        /// MaxLenght of input text.
        /// </summary>
        /// <param name="value">Lenght</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete MaxLenght(ushort value);

        /// <summary>
        /// Add a validator to accept sucessfull finish of control.
        /// <br>Tip: see <see cref="PromptValidators"/> to validators embeding</br>
        /// </summary>
        /// <param name="validators">the function validator. <see cref="ValidationResult"/></param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete AddValidators(params Func<object, ValidationResult>[] validators);

        /// <summary>
        /// Execute validators foreach input
        /// </summary>
        /// <param name="value">true execute validators foreach input; otherwise, only at finish.</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete ValidateOnDemand(bool value = true);

        /// <summary>
        /// Dynamically change the description using a user role
        /// </summary>
        /// <param name="value">function to apply change</param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete ChangeDescription(Func<string, string> value);

        /// <summary>
        /// Custom config the control.
        /// </summary>
        /// <param name="context">action to apply changes. <see cref="IPromptConfig"/></param>
        /// <returns><see cref="IControlAutoComplete"/></returns>
        IControlAutoComplete Config(Action<IPromptConfig> context);
    }
}
