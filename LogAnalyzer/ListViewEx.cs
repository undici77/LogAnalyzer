/*
 * This file is part of the LogAnalyzer distribution (https://github.com/undici77/PlugnPutty.git).
 * Copyright (c) 2021 Alessandro Barbieri.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
	public class ListViewEx : ListView
	{
		private FieldInfo _Field_Info;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		/// @brief Constructor
		///
		public ListViewEx()
		{
			_Field_Info = typeof(ListView).GetField("virtualListSize", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance);

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.EnableNotifyMessage, true);
		}

		/// @brief Control notification messave
		///
		/// @param m message
		protected override void OnNotifyMessage(Message m)
		{
			if (m.Msg != 0x14)
			{
				base.OnNotifyMessage(m);
			}
		}

		/// @brief Send control message
		///
		/// @param msg message to send
		/// @param wparam wparam parameter (Windows API standard)
		/// @param lparam lparam parameter (Windows API standard)
		/// @retval	result (Windows API standard)
		private IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		/// @brief Set virtual list view size
		///
		/// @param size size of list
		public void SetVirtualListSize(int size)
		{
			if (size < 0)
			{
				throw new ArgumentException("ListViewVirtualListSizeInvalidArgument");
			}

			_Field_Info.SetValue(this, size);
			if ((base.IsHandleCreated && this.VirtualMode) && !base.DesignMode)
			{
				SendMessage(0x102f, size, 2);
			}
		}
	}
}