/*
 * Created by SharpDevelop.
 * User: Charlie
 * Date: 9/7/2014
 * Time: 2:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace PathExplorer
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		// Path class that will be used to validate file and folder locations.
		class Path
		{
			private string value;
			
			public string Value
			{
				get
				{
					return this.value;
				}
				set
				{
					if ( File.Exists(value) ||  Directory.Exists(value) )
					{
						this.value = value;
					}
					
					else
					{
						throw FileNotFoundException;
					}
				}
			}
			
			public Path(string value)
			{
				this.Value = value;
			}
			
		}
		
		// PathExplorer class that will be used for handling getting and setting the path in the registry.
		public class PathExplorer()
		{
			private readonly string PathKey = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
			private readonly List<Path> paths = new List<Path>();
			
			public PathExplorer()
			{
				var temp = Registry.getValue(PathKey, "Path", null);
				var list = temp.Split(';');
				foreach( string item in list )
				{
					this.addPath(item);
				}
				
			}
			
			public List<Path> Paths
			{
				get
				{
					return this.paths;
				}
			}
			
			public void addPath(string value)
			{
				try
				{
					var path = new Path(value);
					this.paths.Add(path);
				}
				
				catch (FileNotFoundException e)
				{
					Console.WriteLine(e);
				}
			}
		}
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	}
}
