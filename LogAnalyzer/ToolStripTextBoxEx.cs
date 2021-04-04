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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class ToolStripTextBoxEx : ToolStripTextBox
{

	/// @brief Constructor
	///
	public ToolStripTextBoxEx() : base()
	{
		if (this.Control != null)
		{
			this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
		}
	}

	/// @brief Constructor
	///
	/// @brief name class name
	public ToolStripTextBoxEx(string name) : base(name)
	{
		if (this.Control != null)
		{
			this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
		}
	}

	/// @brief Dispose
	///
	/// @param disposing disposing state
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (this.Control != null)
			{
				this.Control.HandleCreated -= new EventHandler(OnControlHandleCreated);
			}
		}

		base.Dispose(disposing);
	}

	/// @brief Control handled created event
	///
	/// @param sender object who generate event
	/// @param e events arguments
	void OnControlHandleCreated(object sender, EventArgs e)
	{
		UpdateCue();
	}

	private static readonly uint ECM_FIRST = 0x1500;
	private static readonly uint EM_SETCUEBANNER = ECM_FIRST + 1;

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
	private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

	private string _Cue_Text = String.Empty;

	/// @brief Setter cue text
	///
	public string CueText
	{
		get
		{
			return _Cue_Text;
		}
		set
		{
			if (value == null)
			{
				value = String.Empty;
			}

			if (!_Cue_Text.Equals(value, StringComparison.CurrentCulture))
			{
				_Cue_Text = value;
				UpdateCue();
				OnCueTextChanged(EventArgs.Empty);
			}
		}
	}

	public event EventHandler _Cue_Text_Changed;

	/// @brief Cue text changed
	///
	/// @param e events arguments
	protected virtual void OnCueTextChanged(EventArgs e)
	{
		EventHandler handler = _Cue_Text_Changed;
		if (handler != null)
		{
			handler(this, e);
		}
	}

	private bool _Show_Cue_Text_With_Focus = false;

	/// @brief Setter/Getter show cute text and set focus
	///
	public bool ShowCueTextWithFocus
	{
		get
		{
			return _Show_Cue_Text_With_Focus;
		}
		set
		{
			if (_Show_Cue_Text_With_Focus != value)
			{
				_Show_Cue_Text_With_Focus = value;
				UpdateCue();
				OnShowCueTextWithFocusChanged(EventArgs.Empty);
			}
		}
	}

	public event EventHandler _Show_Cue_Text_With_Focus_Changed;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	/// @brief Show cute textbox change event
	///
	/// @param e events arguments
	protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
	{
		EventHandler handler = _Show_Cue_Text_With_Focus_Changed;
		if (handler != null)
		{
			handler(this, e);
		}
	}

	/// @brief Force update cue
	///
	private void UpdateCue()
	{
		if ((this.Control != null) && (this.Control.IsHandleCreated))
		{
			SendMessage(new HandleRef(this.Control, this.Control.Handle), EM_SETCUEBANNER, (_Show_Cue_Text_With_Focus) ? new IntPtr(1) : IntPtr.Zero, _Cue_Text);
		}
	}

	/// @brief Get preferred size method
	///
	/// @param constraining_size constraining size required by the owner
	/// @retval	size
	public override Size GetPreferredSize(Size constraining_size)
	{
		Int32 width;
		Int32 spring_box_count;
		Size size;

		if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
		{
			return (DefaultSize);
		}

		width = Owner.DisplayRectangle.Width;

		if (Owner.OverflowButton.Visible)
		{
			width = width - Owner.OverflowButton.Width - Owner.OverflowButton.Margin.Horizontal;
		}

		spring_box_count = 0;

		foreach (ToolStripItem item in Owner.Items)
		{
			if (!item.IsOnOverflow)
			{
				if (item is ToolStripTextBoxEx)
				{
					spring_box_count++;
					width -= item.Margin.Horizontal;
				}
				else
				{
					width = width - item.Width - item.Margin.Horizontal;
				}
			}
		}

		if (spring_box_count > 1)
		{
			width /= spring_box_count;
		}

		if (width < DefaultSize.Width)
		{
			width = DefaultSize.Width;
		}

		size = base.GetPreferredSize(constraining_size);
		size.Width = width - 12;

		return (size);
	}
}
