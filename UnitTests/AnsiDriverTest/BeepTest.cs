﻿using PPlus.Tests.Util;

namespace PPlus.Tests.AnsiDriverTest
{
    
    public class BepTest : BaseTest
    {
        [Fact]
        public void Should_can_beep()
        {
            PromptPlus.Beep();
        }
    }
}
