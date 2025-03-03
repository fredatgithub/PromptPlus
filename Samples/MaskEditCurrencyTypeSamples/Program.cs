﻿using System.Globalization;
using PPlus;
using PPlus.Controls;

namespace MaskEditCurrencyTypeSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PromptPlus.WriteLine("Hello, World!");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var cult = Thread.CurrentThread.CurrentCulture;
            PromptPlus.Config.DefaultCulture = cult;

            PromptPlus.DoubleDash($"Your default Culture is {cult.Name}", DashOptions.HeavyBorder, style: Style.Default.Foreground(Color.Yellow));

            PromptPlus.DoubleDash($"Control:MaskEdit Currency ({cult.Name}) - minimal usage");

            var mask = PromptPlus.MaskEdit("input", "MaskEdit Currency input")
                .Mask(MaskedType.Currency)
                .AmmoutPositions(10, 2, false)
                .Run();

            if (!mask.IsAborted)
            {
                PromptPlus.WriteLine($"You input with mask is {mask.Value.Masked}");
                PromptPlus.WriteLine($"You input without mask is {mask.Value.Input}");
            }

            PromptPlus.DoubleDash($"Control:MaskEdit Currency ({cult.Name}) - with Positive/Negative style");

            PromptPlus.MaskEdit("input", "MaskEdit Currency input")
                .Mask(MaskedType.Currency)
                .AmmoutPositions(10, 2, true)
                .PositiveStyle(Style.Default.Foreground(Color.Green))
                .NegativeStyle(Style.Default.Foreground(Color.IndianRed))
                .Run();

            PromptPlus.DoubleDash($"Control:Currency - with overwrite culture:pt-br.");

            PromptPlus.MaskEdit("input", "MaskEdit Currency input")
                .Mask(MaskedType.Currency)
                .AmmoutPositions(10, 2, true)
                .Culture(new CultureInfo("pt-br"))
                .Run();


            PromptPlus.DoubleDash("For other features below see - input samples (same behaviour)");
            PromptPlus.WriteLine(". [yellow]ChangeDescription[/] - InputBasicSamples");
            PromptPlus.WriteLine(". [yellow]ValidateOnDemand[/] - InputWithValidatorSamples");
            PromptPlus.WriteLine(". [yellow]AddValidators[/] - InputWithValidatorSamples");
            PromptPlus.WriteLine(". [yellow]InputToCase[/] - InputBasicSamples");
            PromptPlus.WriteLine(". [yellow]OverwriteDefaultFrom[/] - InputOverwriteDefaultFromSamples");
            PromptPlus.WriteLine(". [yellow]Default[/] - InputBasicSamples");
            PromptPlus.WriteLine(". [yellow]DefaultIfEmpty[/] - InputBasicSamples");
            PromptPlus.WriteLine(". [yellow]SuggestionHandler[/] - InputWithSugestionSamples");
            PromptPlus.WriteLine(". [yellow]HistoryEnabled[/] - InputWithHistorySamples");
            PromptPlus.WriteLine(". [yellow]HistoryMinimumPrefixLength[/] - InputWithHistorySamples");
            PromptPlus.WriteLine(". [yellow]HistoryTimeout[/] - InputWithHistorySamples");
            PromptPlus.WriteLine(". [yellow]HistoryMaxItems[/] - InputWithHistorySamples");
            PromptPlus.WriteLine(". [yellow]HistoryPageSize[/] - InputWithHistorySamples");
            PromptPlus.WriteLines(2);
            PromptPlus.KeyPress("End Sample!, Press any key", cfg => cfg.ShowTooltip(false))
                .Run();
        }
    }
}