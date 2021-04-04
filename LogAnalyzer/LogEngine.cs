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
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LogAnalyzer
{
	class LogEngine
	{
		public enum FILTER
		{
			TEXT,
			REGEX
		};

		private string _File_Name;
		private long _File_Position;
		private long _File_Length;
		private DateTime _File_Write_Time;
		private object _Filter_Lock;
		private FILTER _Filter;
		private List<string> _Log;
		private List<string> _Log_Filtered;
		private Thread _Thread;
		private EventWaitHandle _Parse_Event;

		private string _Filter_Text;
		private string _Filter_Regex;

		private int _Progress_Bar_Max_Value;
		private volatile bool _Follow;

		private Control _User_Interface;
		private Delegate _Show_Exception_Delegate;
		private Delegate _Clear_Log_Delegate;
		private Delegate _Set_Log_Delegate;
		private Delegate _Append_Log_Delegate;
		private Delegate _Set_Progress_Bar_Delegate;

		private const int UPDATE_PROGRESS_BAR_MIN_LOG_LENGTH = 262144;
		private const int LOG_FILE_BLOCK_LENGTH = 16384;
		private const int FOLLOW_POLLING_TIME = 100;

		private readonly string[] PATTERN_SEPARETORS = new[] { ";" };
		private readonly string[] LINE_SEPARETORS = new[] { "\r\n", "\n" };
		private readonly int INPUT_FILTER_TIME = 500;

		private static LogEngine _Instance;

		/// @brief Constructor
		///
		protected LogEngine()
		{
			_Filter_Lock = new object();

			_Log = new List<string>();
			_Log_Filtered = new List<string>();

			_Filter_Text  = "";
			_Filter_Regex = "";

			_Thread = null;
		}

		/// @brief Singleton
		///
		public static LogEngine Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new LogEngine();
				}

				return (_Instance);
			}
		}

		/// @brief Getter/Setter of filter selection (Text or Regex)
		///
		public FILTER Filter
		{
			get
			{
				lock (_Filter_Lock)
				{
					return (_Filter);
				}
			}
			set
			{
				lock (_Filter_Lock)
				{
					_Filter = value;
				}
			}
		}

		/// @brief Start log engine thread
		///
		/// @param file_name filename to filter
		public void StartThread(string file_name)
		{
			if (_Thread != null)
			{
				throw new System.ArgumentNullException("_Thread != null", "thread already running");
			}

			_File_Name = file_name;

			_Parse_Event = new EventWaitHandle(false, EventResetMode.AutoReset);
			_Thread = new Thread(this.FilterThread);
			_Thread.Start();

			while (!_Thread.IsAlive)
				;
		}

		/// @brief Stop log engine thread
		///
		public void StopThread()
		{
			if (_Thread == null)
			{
				return;
			}

			_Thread.Abort();
			_Thread.Join();
			_Thread = null;
			_Parse_Event = null;
		}

		/// @brief Require log engine to refresh filtered data
		///
		public void Refresh()
		{
			if (_Parse_Event != null)
			{
				_Parse_Event.Set();
			}
		}

		/// @brief Clear log and filters
		///
		public void Clear()
		{
			_Log.Clear();
			_Log_Filtered.Clear();
		}

		/// @brief Getter/Setter of text filter
		///
		public string FilterText
		{
			get
			{
				lock (_Filter_Text)
				{
					return (_Filter_Text);
				}
			}
			set
			{
				lock (_Filter_Text)
				{
					_Filter_Text = value;
				}
			}
		}

		/// @brief Getter/Setter of regex filter
		///
		public string FilterRegex
		{
			get
			{
				lock (_Filter_Regex)
				{
					return (_Filter_Regex);
				}
			}
			set
			{
				lock (_Filter_Regex)
				{
					_Filter_Regex = value;
				}
			}
		}
		/// @brief Setter of progress bar max value
		///

		public int ProgressBarMaxValue
		{
			set
			{
				_Progress_Bar_Max_Value = value;
			}
		}


		/// @brief Setter of user interface to send UI async messages
		///
		public Control UserInterface
		{
			set
			{
				_User_Interface = value;
			}
		}

		/// @brief Setter of user interface exception delegate
		///
		public Delegate ShowExceptionDelegate
		{
			set
			{
				_Show_Exception_Delegate = value;
			}
		}

		/// @brief Setter of user interface clear log delegate
		///
		public Delegate ClearLogDelegate
		{
			set
			{
				_Clear_Log_Delegate = value;
			}
		}

		/// @brief Setter of user interface set log delegate
		///
		public Delegate SetLogDelegate
		{
			set
			{
				_Set_Log_Delegate = value;
			}
		}

		/// @brief Setter of user interface append log delegate
		///
		public Delegate AppendLogDelegate
		{
			set
			{
				_Append_Log_Delegate = value;
			}
		}

		/// @brief Setter of user interface set progress bar delegate
		///
		public Delegate SetProgressBarDelegate
		{
			set
			{
				_Set_Progress_Bar_Delegate = value;
			}
		}

		/// @brief Setter of user interface set follow delegate
		///
		public bool Follow
		{
			get
			{
				return (_Follow);
			}

			set
			{
				_Follow = value;
			}
		}

		/// @brief Read/Update/Filter log data thread procedure
		///
		private void FilterThread()
		{
			FILTER filter;
			string filter_text;
			string filter_regex;
			List<string> update;
			List<string> filtered;
			bool end;

			update = null;

			try
			{
				_File_Length = -1;
				_File_Position = -1;
				if (!ReadLog(_File_Name, ref _Log, ref _File_Position, ref _File_Length, ref _File_Write_Time))
				{
					_User_Interface.Invoke(_Show_Exception_Delegate, "Enable to read " + _File_Name);
				}

				end = true;
				while (true)
				{
					lock (_Filter_Text)
					{
						filter_text = _Filter_Text;
					}

					lock (_Filter_Regex)
					{
						filter_regex = _Filter_Regex;
					}

					lock (_Filter_Lock)
					{
						filter = _Filter;
					}

					try
					{
						_User_Interface.Invoke(_Clear_Log_Delegate);
						_Log_Filtered.Clear();

						end = ProcessLog(filter_text, filter_regex, filter, ref _Log, out filtered);

						if (end)
						{
							_User_Interface.Invoke(_Set_Log_Delegate, filtered);
						}
					}
					catch
					{
						end = true;
					}

					if (end)
					{
						while (!_Parse_Event.WaitOne(FOLLOW_POLLING_TIME))
						{
							if (_Follow)
							{
								if (UpdateLog(_File_Name, ref update, ref _File_Position, ref _File_Length, ref _File_Write_Time))
								{
									_Log.AddRange(update);
									ProcessLog(filter_text, filter_regex, filter, ref update, out filtered);
									if (filtered.Count > 0)
									{
										_User_Interface.Invoke(_Append_Log_Delegate, filtered);
									}
								}
							}
						}
					}
					while (_Parse_Event.WaitOne(INPUT_FILTER_TIME))
						;
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				_User_Interface.Invoke(_Show_Exception_Delegate, ex.Message + ex.StackTrace);
			}
		}

		/// @brief Read log file from begin to end tracking file position and last write time
		///
		/// @param file_name log file name
		/// @param log list of extracted log lines
		/// @param position position reached inside file
		/// @param length data read length
		/// @param last_write_time last date time file was read
		/// @retval	true ok, false error (unable to read file)
		private bool ReadLog(string file_name, ref List<string> log, ref long position, ref long length, ref DateTime last_write_time)
		{
			ulong percent;
			ulong last_percent;
			StreamReader reader;
			Stream stream;
			string last_line;
			long file_length;
			char[] block_array;
			string block_string;
			int block_length;

			percent = 0;
			last_percent = 0;

			try
			{
				stream = new FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				reader = new System.IO.StreamReader(stream);
				last_write_time = System.IO.File.GetLastWriteTime(file_name);

				_User_Interface.BeginInvoke(_Set_Progress_Bar_Delegate, 0);

				file_length = stream.Length;

				last_line = "";
				block_array = new char[LOG_FILE_BLOCK_LENGTH];
				block_string = "";

				do
				{
					block_length = reader.ReadBlock(block_array, 0, LOG_FILE_BLOCK_LENGTH);
					if (block_length > 0)
					{
						if (block_length != LOG_FILE_BLOCK_LENGTH)
						{
							Array.Resize(ref block_array, block_length);
						}

						block_string += new String(block_array);

						log.AddRange(block_string.Split(LINE_SEPARETORS, StringSplitOptions.None));
						last_line = log[log.Count - 1];
						log.RemoveAt(log.Count - 1);
						block_string = last_line;

						percent = (ulong)(((ulong)stream.Position * (ulong)_Progress_Bar_Max_Value) / (ulong)file_length);
						if (last_percent != percent)
						{
							_User_Interface.BeginInvoke(_Set_Progress_Bar_Delegate, (int)percent);
							last_percent = percent;
						}
					}
				}
				while (block_length == LOG_FILE_BLOCK_LENGTH);

				position = stream.Position - last_line.Length;

				reader.Close();

				_User_Interface.BeginInvoke(_Set_Progress_Bar_Delegate, _Progress_Bar_Max_Value);

				return (true);
			}
			catch
			{
			}

			return (false);
		}

		/// @brief Update log starting from last read position until the end tracking file position and last write time
		///
		/// @param file_name log file name
		/// @param log list of extracted log lines
		/// @param position position reached inside file
		/// @param length data read length
		/// @param last_write_time last date time file was read
		/// @retval	true ok, false error (unable to read file)
		private bool UpdateLog(string file_name, ref List<string> update, ref long position, ref long length, ref DateTime last_write_time)
		{
			FileInfo info;
			StreamReader reader;
			Stream stream;
			char[] block_array;
			string block_string;
			int block_length;
			string last_line;

			update = null;

			try
			{
				info = new FileInfo(file_name);
				if ((last_write_time != info.LastWriteTime) || (length != info.Length))
				{
					last_write_time = info.LastWriteTime;

					update = new List<string>();

					stream = new FileStream(file_name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					reader = new System.IO.StreamReader(stream);

					if (stream.Length < length)
					{
						stream.Position = 0;
					}
					else
					{
						stream.Position = position;
					}

					last_line = "";
					block_array = new char[LOG_FILE_BLOCK_LENGTH];
					block_string = "";

					do
					{
						block_length = reader.ReadBlock(block_array, 0, LOG_FILE_BLOCK_LENGTH);
						if (block_length > 0)
						{
							if (block_length != LOG_FILE_BLOCK_LENGTH)
							{
								Array.Resize(ref block_array, block_length);
							}

							block_string += new String(block_array);

							update.AddRange(block_string.Split(LINE_SEPARETORS, StringSplitOptions.None));
							last_line = update[update.Count - 1];
							update.RemoveAt(update.Count - 1);
							block_string = last_line;
						}
					}
					while (block_length == LOG_FILE_BLOCK_LENGTH);

					length = stream.Length;
					position = stream.Position - last_line.Length;
					reader.Close();

					return (true);
				}
			}
			catch
			{
			}

			return (false);
		}

		/// @brief Process log data
		///
		/// @param filter_text text filter string
		/// @param filter_regex	regex filter string
		/// @param filter filter selection
		/// @param log log list to filter
		/// @param filtered	filtered log list
		/// @retval
		private bool ProcessLog(string filter_text, string filter_regex, FILTER filter, ref List<string> log, out List<string> filtered)
		{
			bool result;
			Regex regex;
			string[] pattern;

			try
			{
				if (filter == FILTER.TEXT)
				{
					if ((filter_text == null) || (filter_text == ""))
					{
						result = UnfilteredLog(log, out filtered);
					}
					else
					{
						pattern = filter_text.Split(PATTERN_SEPARETORS, StringSplitOptions.RemoveEmptyEntries);
						result = FilterLog(log, pattern, out filtered);
					}
				}
				else
				{
					if ((filter_regex == null) || (filter_regex == ""))
					{
						result = UnfilteredLog(log, out filtered);
					}
					else
					{
						regex = new Regex(filter_regex);
						result = FilterLog(log, regex, out filtered);
					}
				}
			}
			catch
			{
				filtered = new List<string>();
				result = true;
			}

			return (result);
		}

		/// @brief Create unfiltered log
		///
		/// @param log log to print
		/// @param filtered	log printed
		/// @retval	true ok, false error
		private bool UnfilteredLog(List<string> log, out List<string> filtered)
		{
			filtered = new List<string>(log);

			return (true);
		}

		/// @brief Filter log using text pattern
		///
		/// @param log log to print
		/// @param text_pattern text pattern list
		/// @param filtered	log printed
		/// @retval	true ok, false error
		private bool FilterLog(List<string> log, string[] text_pattern, out List<string> filtered)
		{
			int id;
			string line;
			ulong percent;
			ulong last_percent;
			bool update_progress_bar;

			_User_Interface.Invoke(_Set_Progress_Bar_Delegate, 0);

			filtered = new List<string>(log.Count);
			update_progress_bar = log.Count > UPDATE_PROGRESS_BAR_MIN_LOG_LENGTH;

			percent = 0;
			last_percent = 0;
			for (id = 0; id < log.Count; id++)
			{
				line = log[id];
				if (StringMatch(ref line, ref text_pattern))
				{
					filtered.Add(line);
				}

				if (update_progress_bar)
				{
					percent = (ulong)(((ulong)id * (ulong)_Progress_Bar_Max_Value) / (ulong)log.Count);
					if (last_percent != percent)
					{
						if (_Parse_Event.WaitOne(0))
						{
							_User_Interface.Invoke(_Set_Progress_Bar_Delegate, _Progress_Bar_Max_Value);
							return (false);
						}

						_User_Interface.Invoke(_Set_Progress_Bar_Delegate, (int)percent);
						last_percent = percent;
					}
				}
			}

			_User_Interface.Invoke(_Set_Progress_Bar_Delegate, _Progress_Bar_Max_Value);

			return (true);
		}

		/// @brief Filter log using regex
		///
		/// @param log log to print
		/// @param regex regex to use as filter
		/// @param filtered	log printed
		/// @retval	true ok, false error
		private bool FilterLog(List<string> log, Regex regex, out List<string> filtered)
		{
			int id;
			string line;
			ulong percent;
			ulong last_percent;
			bool update_progress_bar;

			_User_Interface.Invoke(_Set_Progress_Bar_Delegate, 0);

			filtered = new List<string>(log.Count);
			update_progress_bar = log.Count > UPDATE_PROGRESS_BAR_MIN_LOG_LENGTH;

			percent = 0;
			last_percent = 0;
			for (id = 0; id < log.Count; id++)
			{
				line = log[id];
				if (regex.IsMatch(line))
				{
					filtered.Add(line);
				}

				if (update_progress_bar)
				{
					percent = (ulong)(((ulong)id * (ulong)_Progress_Bar_Max_Value) / (ulong)log.Count);
					if (last_percent != percent)
					{
						if (_Parse_Event.WaitOne(0))
						{
							_User_Interface.Invoke(_Set_Progress_Bar_Delegate, _Progress_Bar_Max_Value);
							return (false);
						}

						_User_Interface.Invoke(_Set_Progress_Bar_Delegate, (int)percent);
						last_percent = percent;
					}
				}
			}

			_User_Interface.Invoke(_Set_Progress_Bar_Delegate, _Progress_Bar_Max_Value);

			return (true);
		}

		/// @brief Verify if string contains pattern
		///
		/// @param text text to analyze
		/// @param pattern pattern list to search
		/// @retval	true match, false not match
		private bool StringMatch(ref string text, ref string[] pattern)
		{
			foreach (string p in pattern)
			{
				if (text.IndexOf(p) != -1)
				{
					return (true);
				}
			}

			return (false);
		}
	}
}
