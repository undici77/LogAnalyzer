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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LogAnalyzer
{
	public partial class InputForm : Form
	{
		private bool _Ok;
		private string _Input;
		private string _Title;
		private string _Message;
		private Regex _Regex;

		/// @brief Constructor
		///
		/// @param title message box title
		/// @param message main message
		/// @param regex input data validation regex
		public InputForm(string title, string message, string regex)
		{
			try
			{
				InitializeComponent();

				_Title   = title;
				_Message = message;
				_Regex   = new Regex(regex);
				_Input   = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}

		/// @brief Form load event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void InputForm_Load(object sender, EventArgs e)
		{
			OkButton.Enabled = false;

			_Ok = false;
			this.Text = _Title;
			MessageLabel.Text = _Message;

			InputTextBox.Focus();
			ActiveControl = InputTextBox;
		}

		/// @brief Ok buttin click event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void OkButton_Click(object sender, EventArgs e)
		{
			try
			{
				_Input = InputTextBox.Text;
				if (_Input.Length > 0)
				{
					if (_Regex.IsMatch(_Input))
					{
						_Ok = true;
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}

		/// @brief Closing event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void InputForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		/// @brief Get last result
		///
		/// @param data	input data
		/// @retval	true user press ok and regex match, false user cancel operation
		public bool GetResult(out string data)
		{
			data = _Input.Trim();
			return (_Ok);
		}

		/// @brief Cancel button click
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void CancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// @brief Input text changed
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void InputTextBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				_Input = InputTextBox.Text;
				if (_Regex.IsMatch(_Input))
				{
					OkButton.Enabled = true;
				}
				else
				{
					OkButton.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}

		/// @brief Form keydown event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyValue == 0x0D)
				{
					_Input = InputTextBox.Text;
					if (_Input.Length > 0)
					{
						if (_Regex.IsMatch(_Input))
						{
							_Ok = true;
							this.Close();
						}
					}
				}
				else if (e.KeyValue == 0x1B)
				{
					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}

		/// @brief Buttons keydown event
		///
		/// @param sender object who generate event
		/// @param e events arguments
		private void Buttons_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					_Input = InputTextBox.Text;
					if (_Input.Length > 0)
					{
						if (_Regex.IsMatch(_Input))
						{
							_Ok = true;
							this.Close();
						}
					}
				}
				else if (e.KeyCode == Keys.Escape)
				{
					_Ok = false;
					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message + ex.StackTrace);
			}
		}
	}
}
