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
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogAnalyzer
{
	public delegate void CLEAR_LOG_DELEGATE();
	public delegate void SET_LOG_DELEGATE(List<string> content);
	public delegate void APPEND_LOG_DELEGATE(List<string> content);
	public delegate void SET_PROGRESS_BAR_VALUE_DELEGATE(int percent);
	public delegate void SET_EXCEPTION(string description);

	public partial class MainForm : Form
	{
		private CLEAR_LOG_DELEGATE _Clear_Log_Delegate;
		private SET_LOG_DELEGATE _Set_Log_Delegate;
		private APPEND_LOG_DELEGATE _Append_Log_Delegate;
		private SET_PROGRESS_BAR_VALUE_DELEGATE _Set_Progress_Bar_Value_Delegate;
		private SET_EXCEPTION _Set_Exception_Delegate;

		private void InitializeDelegates()
		{
			_Clear_Log_Delegate = new CLEAR_LOG_DELEGATE(this.ClearLog);
			_Set_Log_Delegate = new SET_LOG_DELEGATE(this.SetLog);
			_Append_Log_Delegate = new APPEND_LOG_DELEGATE(this.AppendLog);
			_Set_Progress_Bar_Value_Delegate = new SET_PROGRESS_BAR_VALUE_DELEGATE(this.SetProgressBarValue);
			_Set_Exception_Delegate = new SET_EXCEPTION(this.SetException);
		}

		private void ClearLog()
		{
			LogListView.VirtualListSize = 0;
			LogListView.Enabled = false;
			ExportMenuItem.Enabled = false;

			LogListView.Items.Clear();
			MainStatusStripLabel.Text = "";
		}

		private void SetLog(List<string> content)
		{
			_Filtered_Log = new List<string>(content);

			LogListView.SetVirtualListSize(_Filtered_Log.Count + 1);
			MainStatusStripLabel.Text = _Filtered_Log.Count.ToString() + " lines";
			ExportMenuItem.Enabled = true;
			LogListView.Enabled = true;

			if (_Log_List_View_Follow_Enable && LogEngine.Instance.Follow)
			{
				GoToEnd();
			}
		}

		private void AppendLog(List<string> content)
		{
			_Filtered_Log.AddRange(content);
			LogListView.SetVirtualListSize(_Filtered_Log.Count + 1);
			MainStatusStripLabel.Text = _Filtered_Log.Count.ToString() + " lines";

			if (_Log_List_View_Follow_Enable && LogEngine.Instance.Follow)
			{
				GoToEnd();
			}
		}

		private void GoToEnd()
		{
			int count;
			ListViewItem l;

			count = LogListView.Items.Count;
			if (count > 0)
			{
				foreach (int i in LogListView.SelectedIndices)
				{
					l = LogListView.Items[i];

					l.Selected = false;
					l.Focused = false;
				}

				l = LogListView.Items[count - 1];
				l.EnsureVisible();
			}
		}

		private void SetProgressBarValue(int value)
		{
			if ((value > 0) && (value < PROGRESS_BAR_MAX_VALUE))
			{
				MainStatusStripProgressBar.Value = value + 1;
				MainStatusStripProgressBar.Visible = true;
			}
			else
			{
				MainStatusStripProgressBar.Value = 0;
				MainStatusStripProgressBar.Visible = false;
			}
		}

		private void SetException(string description)
		{
			MessageBox.Show(this, description, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
