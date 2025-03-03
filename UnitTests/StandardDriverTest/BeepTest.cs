﻿using PPlus.Tests.Util;

namespace PPlus.Tests.StandardDriverTest
{
    
    public class StdBepTest : BaseTest
    {
        [Fact]
        public void Should_can_beep()
        {
            // Given
            PromptPlus.Setup((cfg) =>
            {
                cfg.SupportsAnsi = false;
            });
            // When
            PromptPlus.Beep();
        }
    }
}
