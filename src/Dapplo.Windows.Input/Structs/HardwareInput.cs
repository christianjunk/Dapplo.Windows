//  Dapplo - building blocks for desktop applications
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

#endregion

namespace Dapplo.Windows.Input.Structs
{
	/// <summary>
	/// Contains information about a simulated message generated by an input device other than a keyboard or mouse.
	/// See <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646269.aspx">HARDWAREINPUT structure</a>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	[SuppressMessage("ReSharper", "ConvertToAutoProperty")]
	[SuppressMessage("ReSharper", "ArrangeAccessorOwnerBody")]
    public struct HardwareInput
    {
        private int _uMsg;
        private short _wParamL;
        private short _wParamH;

        /// <summary>
        /// The message generated by the input hardware.
        /// </summary>
        public int uMsg
        {
            get { return _uMsg; }
            set { _uMsg = value; }
        }

        /// <summary>
        /// The low-order word of the lParam parameter for uMsg.
        /// </summary>
        public short ParamL
        {
            get { return _wParamL; }
            set { _wParamL = value; }
        }

		/// <summary>
		/// The high-order word of the lParam parameter for uMsg.
		/// </summary>
		public short wParamH
		{
		    get { return _wParamH; }
		    set { _wParamH = value; }
		}
    }
}