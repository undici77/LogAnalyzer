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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogAnalyzer
{
	public partial class MainForm : Form
	{
		public const int PROGRESS_BAR_MAX_VALUE = 100;

		private struct FILTER_PATTERN_VARIABLE
		{
			public string name;
			public string variable;
			public string regex;
			public string description;
		};

		private struct FILTER_PATTERN
		{
			public string name;
			public string description;
			public string regex;
			public List<FILTER_PATTERN_VARIABLE> variables;
		}

		private List<FILTER_PATTERN> _Filter_Pattern_List;

		private string _Log_File_Name;
		private string _Log_Path;
		private List<string> _Filtered_Log;

		private bool _Log_List_View_Follow_Enable;

		/// @brief Form constructor
		///
		/// @param args	args from main construction
		public MainForm(string[] args)
		{
			string file_name;

			_Log_List_View_Follow_Enable = true;

			_Filtered_Log = new List<string>();

			InitializeComponent();
			InitializeDelegates();

			_Log_File_Name = "";
			if (args.Length > 0)
			{
				file_name = args[0];
				if (File.Exists(file_name))
				{
					_Log_File_Name = file_name;
				}
			}

			_Filter_Pattern_List = new List<FILTER_PATTERN>();
			LoadFilterPatternList();

			UpdateTitle();
		}

		/// @brief Form load event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainForm_Load(object sender, EventArgs e)
		{
			string buffer;
			int width;
			int height;
			int splitter_distance;

			LogEngine.Instance.ProgressBarMaxValue = PROGRESS_BAR_MAX_VALUE;

			LogEngine.Instance.UserInterface = this;
			LogEngine.Instance.ShowExceptionDelegate = _Show_Exception_Delegate;
			LogEngine.Instance.SetLogDelegate = _Set_Log_Delegate;
			LogEngine.Instance.AppendLogDelegate = _Append_Log_Delegate;
			LogEngine.Instance.ClearLogDelegate = _Clear_Log_Delegate;
			LogEngine.Instance.SetProgressBarDelegate = _Set_Progress_Bar_Value_Delegate;

			OpenMenuItem.Image = Properties.Resources.open;
			OpenMenuItem.ToolTipText = "Open";

			ExportMenuItem.Image = Properties.Resources.export;
			ExportMenuItem.ToolTipText = "Export";

			buffer = App.Ini_File.GetKeyValue("Window", "Width");
			if (!int.TryParse(buffer, out width))
			{
				width = 800;
			}

			buffer = App.Ini_File.GetKeyValue("Window", "Height");
			if (!int.TryParse(buffer, out height))
			{
				height = 600;
			}

			buffer = App.Ini_File.GetKeyValue("Window", "SplitterDistance");
			if (!int.TryParse(buffer, out splitter_distance))
			{
				splitter_distance  = width - 200;
			}

			this.Size = new System.Drawing.Size(width, height);
			MainSplitContainer.SplitterDistance = splitter_distance;

			buffer = App.Ini_File.GetKeyValue("Log", "Path");
			if (buffer == null)
			{
				_Log_Path = "";
			}
			else
			{
				_Log_Path = buffer;
			}

			LogEngine.Instance.FilterRegex = App.Ini_File.GetKeyValue("Menu", "FilterRegex");
			LogEngine.Instance.FilterText = App.Ini_File.GetKeyValue("Menu", "FilterText");

			buffer = App.Ini_File.GetKeyValue("Menu", "TextRegex");
			LogEngine.Instance.Filter = (buffer == "1") ? LogEngine.FILTER.REGEX : LogEngine.FILTER.TEXT;

			buffer = App.Ini_File.GetKeyValue("Menu", "Follow");
			LogEngine.Instance.Follow = (buffer == "1");
			LogListView.MultiSelect   = !LogEngine.Instance.Follow;

			TextRegexMenuItemUpdate();
			FollowMenuItemUpdate();

			MainStatusStripLabel.Text = "";

			ExportMenuItem.Enabled = false;
			LogListView.AllowDrop = true;

			if (_Log_File_Name != "")
			{
				ExportMenuItem.Enabled = true;
				LogEngine.Instance.StartThread(_Log_File_Name);
			}

			FilterPatternListView.Columns[0].Width = -2;
			FilterPatternMenuItem.ToolTipText = "Filter pattern table";
			MainSplitContainer.Panel2Collapsed = (App.Ini_File.GetKeyValue("Menu", "FilterPattern") == "1");
			FilterPatternListView.ShowItemToolTips = true;
			if (MainSplitContainer.Panel2Collapsed)
			{
				FilterPatternMenuItem.Image = Properties.Resources.pattern_active;
			}
			else
			{
				FilterPatternMenuItem.Image = Properties.Resources.pattern_not_active;
			}

			foreach (FILTER_PATTERN fp in _Filter_Pattern_List)
			{
				ListViewItem i = new ListViewItem(fp.name);

				i.ToolTipText = fp.description;

				FilterPatternListView.Items.Add(i);
			}
		}

		/// @brief Load filter pattern list from ini file
		///
		void LoadFilterPatternList()
		{
			FILTER_PATTERN pattern;
			FILTER_PATTERN_VARIABLE pattern_variable;
			string[] array;
			string buffer;

			try
			{
				foreach (IniFile.IniSection ini_section in App.Pattern_Ini_File.SectionsName)
				{
					pattern = new FILTER_PATTERN();
					pattern.variables = new List<FILTER_PATTERN_VARIABLE>();

					pattern.name = ini_section.Name;
					pattern.description = ini_section.GetKey("Description").Value.Replace("\\r\\n", "\r\n");
					pattern.regex = ini_section.GetKey("Regex").Value;
					foreach (IniFile.IniSection.IniKey k in ini_section.Keys)
					{
						if ((k.Name != "Description") && (k.Name != "Regex"))
						{
							buffer = '{' + k.Name + '}';
							if (pattern.regex.IndexOf(buffer) != -1)
							{
								array = k.Value.Split(';');
								if (array.Length == 1)
								{
									pattern_variable.name = k.Name;
									pattern_variable.variable = buffer;
									pattern_variable.description = array[0].Replace("\\r\\n", "\r\n");
									pattern_variable.regex = ".*";

									pattern.variables.Add(pattern_variable);
								}
								else if (array.Length == 2)
								{
									pattern_variable.name = k.Name;
									pattern_variable.variable = buffer;
									pattern_variable.description = array[0].Replace("\\r\\n", "\r\n");
									pattern_variable.regex = array[1];

									pattern.variables.Add(pattern_variable);
								}
							}
						}
					}

					_Filter_Pattern_List.Add(pattern);

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}

		/// @brief Form closing event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				LogEngine.Instance.StopThread();
			}
			catch
			{
			}

			try
			{
				App.Ini_File.Save(App.Ini_File_Name);
			}
			catch
			{
			}

		}

		/// @brief Filter text changed
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void FilterTextBoxMenuItem_TextChanged(object sender, EventArgs e)
		{
			if (LogEngine.Instance.Filter == LogEngine.FILTER.REGEX)
			{
				LogEngine.Instance.FilterRegex = FilterTextBoxMenuItem.Text;

				App.Ini_File.SetKeyValue("Menu", "FilterRegex", FilterTextBoxMenuItem.Text, true);
			}
			else
			{
				LogEngine.Instance.FilterText = FilterTextBoxMenuItem.Text;

				App.Ini_File.SetKeyValue("Menu", "FilterText", FilterTextBoxMenuItem.Text, true);
			}

			LogEngine.Instance.Refresh();
		}

		/// @brief Menu File Open event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void OpenMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog;

			dialog = new OpenFileDialog();
			dialog.Filter = "log files (*.txt, *.log)|*.txt; *.log|any files (*.*)|*.*";
			if (dialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			_Log_File_Name = dialog.FileName;
			_Log_Path = Path.GetDirectoryName(dialog.FileName);
			App.Ini_File.SetKeyValue("Log", "Path", _Log_Path, true);

			LogEngine.Instance.StopThread();
			LogEngine.Instance.Clear();

			LogEngine.Instance.StartThread(_Log_File_Name);

			UpdateTitle();
		}

		/// @brief Log listview drag event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_DragEnter(object sender, DragEventArgs e)
		{
			string[] file_name;

			e.Effect = DragDropEffects.None;
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				file_name = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (file_name.Length > 1)
				{
					return;
				}
				else if (!File.Exists(file_name[0]))
				{
					return;
				}

				e.Effect = DragDropEffects.Copy;
			}
		}

		/// @brief Log listview drop event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_DragDrop(object sender, DragEventArgs e)
		{
			string[] file_name;

			file_name = (string[])e.Data.GetData(DataFormats.FileDrop);

			_Log_File_Name = file_name[0];

			LogEngine.Instance.StopThread();
			LogEngine.Instance.Clear();

			LogEngine.Instance.StartThread(_Log_File_Name);

			UpdateTitle();
		}

		/// @brief Form resize event (called continuously in order to resize related controls)
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainForm_Resize(object sender, EventArgs e)
		{
			ResizeControls();
		}

		/// @brief Form resize end event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			ResizeControls();
		}

		/// @brief Form maximize event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainForm_MaximumSizeChanged(object sender, EventArgs e)
		{
			ResizeControls();
		}

		/// @brief Splitter move event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void MainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			ResizeControls();
		}

		/// @brief Resize all controls based on form size and update data to save to ini file (only in RAM)
		///
		private void ResizeControls()
		{
			try
			{
				LogListView.Columns[0].Width = LogListView.ClientSize.Width;
				FilterPatternListView.Columns[0].Width = -1;
				MainStatusStripProgressBar.Width = MainStatusStrip.ClientSize.Width - MainStatusStripLabel.Width - 30;

				App.Ini_File.SetKeyValue("Window", "Width", this.Size.Width.ToString());
				App.Ini_File.SetKeyValue("Window", "Height", this.Size.Height.ToString());
				App.Ini_File.SetKeyValue("Window", "SplitterDistance", MainSplitContainer.SplitterDistance.ToString());
			}
			catch
			{
			}
		}
		/// @brief Export menu click
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void ExportMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog;

			dialog = new SaveFileDialog();
			dialog.Filter = "txt files (*.txt)|*.txt";
			dialog.RestoreDirectory = true;

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				lock (_Filtered_Log)
				{
					File.WriteAllLines(dialog.FileName, _Filtered_Log);
				}
			}
		}

		/// @brief Text/Regex menu click event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void TextRegexMenuItem_Click(object sender, EventArgs e)
		{
			if (LogEngine.Instance.Filter == LogEngine.FILTER.REGEX)
			{
				LogEngine.Instance.Filter = LogEngine.FILTER.TEXT;
				App.Ini_File.SetKeyValue("Menu", "TextRegex", "0");
			}
			else
			{
				LogEngine.Instance.Filter = LogEngine.FILTER.REGEX;
				App.Ini_File.SetKeyValue("Menu", "TextRegex", "1");
			}

			LogEngine.Instance.Refresh();
			TextRegexMenuItemUpdate();
		}

		/// @brief Text/Regex menu item update
		///
		private void TextRegexMenuItemUpdate()
		{
			if (LogEngine.Instance.Filter == LogEngine.FILTER.REGEX)
			{
				TextRegexMenuItem.Image = Properties.Resources.regex;
				TextRegexMenuItem.ToolTipText = "Regular Expression";
				FilterTextBoxMenuItem.Text = LogEngine.Instance.FilterRegex;
				FilterTextBoxMenuItem.CueText = "Regular Expression";
			}
			else
			{
				TextRegexMenuItem.Image = Properties.Resources.text;
				TextRegexMenuItem.ToolTipText = "Text";
				FilterTextBoxMenuItem.Text = LogEngine.Instance.FilterText;
				FilterTextBoxMenuItem.CueText = "Pattern;Pattern;Pattern";
			}
		}

		/// @brief Follow menu item click
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void FollowMenuItem_Click(object sender, EventArgs e)
		{
			LogEngine.Instance.Follow = !LogEngine.Instance.Follow;
			LogListView.MultiSelect   = !LogEngine.Instance.Follow;

			if (LogEngine.Instance.Follow)
			{
				App.Ini_File.SetKeyValue("Menu", "Follow", "1");
				GoToEnd();
			}
			else
			{
				App.Ini_File.SetKeyValue("Menu", "Follow", "0");
			}

			FollowMenuItemUpdate();
		}

		/// @brief Follow menu item update
		///
		private void FollowMenuItemUpdate()
		{
			if (LogEngine.Instance.Follow)
			{
				if (_Log_List_View_Follow_Enable)
				{
					this.FollowMenuItem.Image = Properties.Resources.follow_active;
					this.FollowMenuItem.ToolTipText = "Follow active";
				}
				else
				{
					this.FollowMenuItem.Image = Properties.Resources.follow_not_active;
					this.FollowMenuItem.ToolTipText = "Follow not active";
				}
			}
			else
			{
				this.FollowMenuItem.Image = Properties.Resources.unfollow;
				this.FollowMenuItem.ToolTipText = "Unfollow";
			}
		}

		/// @brief Update title bar data
		///
		private void UpdateTitle()
		{
			this.Text = App.Name + " " + App.Version;
			if ((_Log_File_Name != null) && (_Log_File_Name != ""))
			{
				this.Text += " - " + _Log_File_Name;
			}
		}

		/// @brief Log listview selected index change
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			int id;

			id = -1;
			if (LogListView.SelectedIndices.Count > 0)
			{
				id = LogListView.SelectedIndices[0];
			}

			if (id == -1)
			{
				_Log_List_View_Follow_Enable = true;
			}
			else if (id == (LogListView.Items.Count - 1))
			{
				_Log_List_View_Follow_Enable = true;
				GoToEnd();
			}
			else
			{
				_Log_List_View_Follow_Enable = false;
			}

			FollowMenuItemUpdate();
		}

		/// @brief Log listview key down event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (LogEngine.Instance.Follow && ((e.KeyCode == Keys.End) || (e.KeyCode == Keys.Escape)))
			{
				e.SuppressKeyPress = true;

				GoToEnd();
			}
		}

		/// @brief Log listview retrieve items (in virtual mode in order to be as efficient as possible)
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			try
			{
				lock (_Filtered_Log)
				{
					if (e.ItemIndex < _Filtered_Log.Count)
					{
						e.Item = new ListViewItem(_Filtered_Log[e.ItemIndex]);
					}
					else
					{
						e.Item = new ListViewItem("");
					}
				}

				if (LogListView.Columns[0].Width != LogListView.ClientSize.Width)
				{
					LogListView.Columns[0].Width = LogListView.ClientSize.Width;
				}
			}
			catch
			{
			}
		}

		/// @brief Sidebar pattern listview double click
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void PatternListView_DoubleClick(object sender, EventArgs e)
		{
			int index;
			FILTER_PATTERN pattern;
			InputForm input_form;
			string buffer;
			string value;
			Regex regex;

			try
			{
				index = FilterPatternListView.SelectedIndices[0];

				pattern = _Filter_Pattern_List[index];
				value = pattern.regex;

				foreach (FILTER_PATTERN_VARIABLE pv in pattern.variables)
				{
					input_form = new InputForm(pv.name, pv.description, pv.regex);
					input_form.ShowDialog(this);
					if (!input_form.GetResult(out buffer))
					{
						return;
					}

					regex = new Regex(pv.variable);
					value = regex.Replace(value, buffer);
				}

				LogEngine.Instance.Filter = LogEngine.FILTER.REGEX;
				App.Ini_File.SetKeyValue("Menu", "TextRegex", "1");
				TextRegexMenuItemUpdate();
				FilterTextBoxMenuItem.Text = value;
			}
			catch
			{
			}
		}

		/// @brief Filter item menu click event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void FilterPatternMenuItem_Click(object sender, EventArgs e)
		{
			MainSplitContainer.Panel2Collapsed = !MainSplitContainer.Panel2Collapsed;

			App.Ini_File.SetKeyValue("Menu", "FilterPattern", MainSplitContainer.Panel2Collapsed ? "1" : "0");

			if (MainSplitContainer.Panel2Collapsed)
			{
				FilterPatternMenuItem.Image = Properties.Resources.pattern_active;
			}
			else
			{
				FilterPatternMenuItem.Image = Properties.Resources.pattern_not_active;
			}

			ResizeControls();
		}

		/// @brief Log listview mouse click event (to copy selected ite content)
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListView_MouseClick(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu_strip;

			if (!LogEngine.Instance.Follow && (e.Button == MouseButtons.Right))
			{
				menu_strip = new ContextMenuStrip();
				menu_strip.ItemClicked += LogListViewMenuStrip_ItemClicked;
				menu_strip.Items.Add("Copy");
				menu_strip.Show(this, e.Location);
			}
		}

		/// @brief Log listview menu strip click event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void LogListViewMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			int    id;
			string text;

			text = "";
			if (e.ClickedItem.Text == "Copy")
			{
				for (id = 0; id < LogListView.SelectedIndices.Count; id++)
				{
					text += LogListView.Items[LogListView.SelectedIndices[id]].Text + "\r\n";
				}

				Clipboard.SetText(text);
			}
		}
	}
}
