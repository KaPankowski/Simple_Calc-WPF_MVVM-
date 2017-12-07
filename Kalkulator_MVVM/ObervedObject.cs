using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator_MVVM
{
	public abstract class ObervedObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(params string[] items)
		{
			if (PropertyChanged != null)
			{
				foreach (string item in items)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(item));
				}
			}
		}
	}
}
