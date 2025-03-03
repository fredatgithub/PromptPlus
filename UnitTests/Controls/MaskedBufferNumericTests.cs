﻿using System.Globalization;
using PPlus.Controls;
using PPlus.Controls.Objects;
using PPlus.Tests.Util;

namespace PPlus.Tests.Controls
{

    public class MaskedBufferNumericTests : BaseTest
    {
        [Fact]
        internal void Should_have_accept_Load_validvalues_en_us()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", true));
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.True(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_Load_validvalues_pt_br()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("pt-BR"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123,45", false));
            //then
            Assert.Equal("0.000.000.123,45", maskedBuffer.ToMasked());
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.True(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }


        [Fact]
        internal void Should_have_not_accept_Load_invalidvalues_en_us()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("12A.45", false));
            //then
            Assert.Equal("0,000,000,000.00", maskedBuffer.ToMasked());
            Assert.Equal(9, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_not_accept_Load_invalidvalues_pt_br()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("pt-BR"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("12A.45", false));
            //then
            Assert.Equal("0.000.000.000,00", maskedBuffer.ToMasked());
            Assert.Equal(9, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }


        [Fact]
        internal void Should_have_accept_Clear()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.Clear();
            //then
            Assert.Equal("0,000,000,000.00", maskedBuffer.ToMasked());
            Assert.Equal(9, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_ToStart_ToEnd()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToHome();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(7, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
            //when
            maskedBuffer.ToEnd();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.True(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_ToStart_Forward()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToHome();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(7, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
            //when
            maskedBuffer.Forward();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(8, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_ToEnd_Backward()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToEnd();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.True(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
            //when
            maskedBuffer.Backward();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(10, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }


        [Fact]
        internal void Should_have_accept_ToStart_ToForwardString()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToHome();
            maskedBuffer.Forward();
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(8, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
            //when
            var aux1 = maskedBuffer.ToBackwardString();
            var aux2 = maskedBuffer.ToForwardString();
            //then
            Assert.Equal("0,000,000,1", aux1);
            Assert.StartsWith("23.45", aux2);
        }


        [Fact]
        internal void Should_have_accept_Delete_intpart()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToHome();
            maskedBuffer.Forward();
            maskedBuffer.Delete();
            //then
            Assert.Equal("0,000,000,134.50", maskedBuffer.ToMasked());
            Assert.Equal(8, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_Delete_decpart()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToEnd();
            maskedBuffer.Backward();
            maskedBuffer.Delete();
            //then
            Assert.Equal("0,000,000,123.50", maskedBuffer.ToMasked());
            Assert.Equal(10, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            Assert.True(maskedBuffer.IsMaxInput);
            Assert.False(maskedBuffer.IsEnd);
            Assert.False(maskedBuffer.IsStart);
        }

        [Fact]
        internal void Should_have_accept_Load_validvalues_home_and_sep_valid()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.ToHome();
            maskedBuffer.Insert('.', out _);
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal(10, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
        }

        [Fact]
        internal void Should_have_accept_Load_validvalues_signals()
        {
            // Given
            var maskedBuffer = new MaskedBuffer(new OptionsForMaskeditNumber(new CultureInfo("en-US"), 10, 2, true, null));
            maskedBuffer.Load(maskedBuffer.RemoveMask("123.45", false));
            //when
            maskedBuffer.Insert('+', out _);
            //then
            Assert.Equal("0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal('+', maskedBuffer.SignalNumberInput);
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);
            //when
            maskedBuffer.Insert('-', out _);
            //then
            Assert.Equal("-0,000,000,123.45", maskedBuffer.ToMasked());
            Assert.Equal('-', maskedBuffer.SignalNumberInput);
            Assert.Equal(11, maskedBuffer.Position);
            Assert.Equal(12, maskedBuffer.Length);

        }
    }

    internal class OptionsForMaskeditNumber : MaskEditOptions
    {
        public OptionsForMaskeditNumber(CultureInfo culture, int ammountInteger, int ammountDecimal, bool acceptSignal, string? defaultvalue) : base(PromptPlus.StyleSchema, PromptPlus.Config, PromptPlus._consoledrive, false)
        {
            Type = ControlMaskedType.Number;
            DefaultValue = defaultvalue;
            CurrentCulture = culture;
            AmmountInteger = ammountInteger;
            AmmountDecimal = ammountDecimal;
            AcceptSignal = acceptSignal;
        }
    }
}
