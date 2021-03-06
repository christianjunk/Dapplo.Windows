﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2017 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Windows
// 
//  Dapplo.Windows is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Windows is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Windows. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Dapplo.Windows.Common.Structs;
using Dapplo.Windows.Desktop;
using Dapplo.Windows.Input;
using Dapplo.Windows.Input.Enums;
using Dapplo.Windows.User32;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Windows.Tests
{
    public class InputTests
    {

        public InputTests(ITestOutputHelper testOutputHelper)
        {
            LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
        }

        /// <summary>
        ///     Test typing in a notepad
        /// </summary>
        /// <returns></returns>
        [Fact]
        private async Task TestInput()
        {
            // Start a process to test against
            using (var process = Process.Start("notepad.exe"))
            {
                // Make sure it's started
                Assert.NotNull(process);
                // Wait until the process started it's message pump (listening for input)
                process.WaitForInputIdle();

                // Find the belonging window
                var notepadWindow = await WindowsEnumerator.EnumerateWindowsAsync()
                    .Where(interopWindow =>
                    {
                        int processId;
                        User32Api.GetWindowThreadProcessId(interopWindow.Handle, out processId);
                        return processId == process.Id;
                    })
                    .FirstOrDefaultAsync();
                Assert.NotNull(notepadWindow);

                // Send input
                var sentInputs = InputGenerator.KeyPress(VirtualKeyCodes.KEY_R, VirtualKeyCodes.KEY_O, VirtualKeyCodes.KEY_B, VirtualKeyCodes.KEY_I, VirtualKeyCodes.KEY_N);
                // Test if we indead sent 10 inputs (5 x down & up)
                Assert.Equal((uint) 10, sentInputs);

                // Kill the process
                process.Kill();
            }
        }

        /// <summary>
        ///     Test typing in a notepad
        /// </summary>
        /// <returns></returns>
        [Fact]
        private void TestMouseInput()
        {
            InputGenerator.MoveMouse(new NativePoint(10, 10));
            Thread.Sleep(1000);
            InputGenerator.MoveMouse(new NativePoint(100, 100));
            Thread.Sleep(1000);
        }
    }
}