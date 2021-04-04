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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace LogAnalyzer
{
	static class App
	{
		[DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
		private static extern uint TimeBeginPeriod(uint uMilliseconds);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		static MainForm _Main_Form;

		///  @brief Main form instance
		///
		static public MainForm Instance
		{
			get
			{
				return (_Main_Form);
			}
		}

		static string _Version;

		///  @brief Application version
		///
		static public string Version
		{
			get
			{
				return (_Version);
			}
		}

		static string _Name;

		///  @brief Application name
		///
		static public string Name
		{
			get
			{
				return (_Name);
			}
		}

		static string _Path;

		///  @brief Application exe path
		///
		static public string Path
		{
			get
			{
				return (_Path);
			}
		}

		static IniFile _Ini_File;

		///  @brief Application ini file
		///
		static public IniFile Ini_File
		{
			get
			{
				return (_Ini_File);
			}
		}

		static string _Ini_File_Name;

		///  @brief Application ini file name
		///
		static public string Ini_File_Name
		{
			get
			{
				return (_Ini_File_Name);
			}
		}

		static IniFile _Pattern_Ini_File;

		///  @brief Filter pattern ini file
		///
		static public IniFile Pattern_Ini_File
		{
			get
			{
				return (_Pattern_Ini_File);
			}
		}

		static string _Pattern_Ini_File_Name;

		///  @brief Filter pattern ini file name
		///
		static public string Pattern_Ini_File_Name
		{
			get
			{
				return (_Pattern_Ini_File_Name);
			}
		}

		[STAThread]
		/// @brief Maion procedure
		///
		/// @param args	command line arguments
		static void Main(string[] args)
		{
			bool created;
			string process_name;
			bool multi_instance;
			string buffer;

			TimeBeginPeriod(1);

			process_name = System.IO.Directory.GetCurrentDirectory() + " - " + Application.ProductName;
			process_name = process_name.Replace("\\", "");
			process_name = process_name.Replace(".", "");
			process_name = process_name.Replace(":", "");

			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			_Version = fvi.FileVersion;
			_Name = fvi.ProductName;
			_Path = System.IO.Path.GetDirectoryName(fvi.FileName) + "\\";

			_Ini_File = new IniFile();
			_Ini_File_Name = _Path + _Name + ".ini";
			_Ini_File.Load(_Ini_File_Name);

			_Pattern_Ini_File = new IniFile();
			_Pattern_Ini_File_Name = _Path + _Name + "Pattern.ini";
			_Pattern_Ini_File.Load(_Pattern_Ini_File_Name);

			buffer = App.Ini_File.GetKeyValue("App", "MultiInstance");
			multi_instance = (buffer == "1");

			if (multi_instance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				_Main_Form = new MainForm(args);
				Application.Run(_Main_Form);
				_Main_Form = null;
			}
			else
			{
				created = true;
				using (Mutex mutex = new Mutex(true, process_name, out created))
				{
					if (created)
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);

						_Main_Form = new MainForm(args);
						Application.Run(_Main_Form);
						_Main_Form = null;
					}
					else
					{
						Process current = Process.GetCurrentProcess();
						foreach (Process process in Process.GetProcessesByName(current.ProcessName))
						{
							if (process.Id != current.Id)
							{
								SetForegroundWindow(process.MainWindowHandle);
								break;
							}
						}
					}
				}
			}
		}
	}
}
