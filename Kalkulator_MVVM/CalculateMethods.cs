using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kalkulator_MVVM
{
	class CalculateMethods : ObervedObject
	{
		#region zmienne

		//private ICommand returnX;
		private bool canExecute = true;
		private readonly Numbers number = new Numbers("");
		private string histNumber = "";
		private ICommand resetText;
		private ICommand reverseNumber;
		private ICommand sqrtNumber;
		private ICommand saveFirstNum;
		private ICommand makeEquation;
		#endregion

		#region properties
		public ICommand MakeEquation
		{
			get { return makeEquation; }

			set
			{
				makeEquation = value;
			}
		}
		//public ICommand ReturnX
		//{
		//	get { return returnX; }

		//	set
		//	{
		//		returnX = value;
		//	}
		//}
		public ICommand SaveFirstNum
		{
			get { return saveFirstNum; }

			set
			{
				saveFirstNum = value;
			}
		}
		public ICommand SqrtNumber
		{
			get { return sqrtNumber; }

			set
			{
				sqrtNumber = value;
			}
		}
		public ICommand ReverseNumber
		{
			get { return reverseNumber; }

			set
			{
				reverseNumber = value;
			}
		}
		public ICommand ResetText
		{
			get { return resetText; }
			set
			{
				resetText = value;
			}
		}
		public string X
		{
			get
			{
				if (number.X.Length >= 3 && number.X.Contains(','))
				{
					var count = number.X.Count(x => x == ',');					

					if (count > 1)
					{
						var regex = new Regex(Regex.Escape(","));
						var tempString = regex.Replace(number.X, string.Empty, 1);
						number.X = tempString;						
					}
					
					return number.X;
				}
				if (number.X.Length >= 2 && (number.X[number.X.Length - 1] == '+' || number.X[number.X.Length - 1] == '-' || number.X[number.X.Length - 1] == '*' || number.X[number.X.Length - 1] == '/') && HistNumber == "")
				{
					HistNumber = number.X;
					return number.X = "";
				}
				else if (number.X.Length >= 2 && HistNumber != "" && (number.X[number.X.Length - 1] == '+' || number.X[number.X.Length - 1] == '-' || number.X[number.X.Length - 1] == '*' || number.X[number.X.Length - 1] == '/'))
				{
					HistNumber += number.X;
					MakeEqation(HistNumber);
					return X;
				}
				else
				{
					return number.X;
				}
			}
			set
			{
				number.X = value;
				OnPropertyChanged("X");
			}
		}
		public string HistNumber
		{
			get
			{
				return histNumber;
			}
			set
			{
				histNumber = value;
				OnPropertyChanged("HistNumber");
			}
		}
		#endregion

		#region Methods
		public void ResetingText(object obj)
		{
			X = "";
			HistNumber = "";
		}
		public void ReversingNumber(object obj)
		{
			if (X[0] != '-') X = '-' + X;
			else if (X[0] == '-') X = X.Substring(1, X.Length - 1);
		}
		public void SquareNumber(object obj)
		{
			if (X != "")
			{

				double number = double.TryParse(X, out double param) ? param : default(double);
				double result = Math.Sqrt(number);
				X = result.ToString();
			}
		}
		public void SaveFirstNumber(object obj)
		{
			if (X != "" || X != null)
			{
				HistNumber = X;
				//Y = X.Substring(0, X.Length - 1);
				X = X[X.Length - 1].ToString();
			}
		}

		public void MakeEqation(object eq)
		{
			string _eq = "";

			if (eq == null)
			{
				_eq = HistNumber + number.X;

				if (_eq[_eq.Length-1].GetType() == 1.GetType())
					_eq = _eq.Substring(0, _eq.Length - 1);
			}
			else
			{
				_eq = eq.ToString();
				_eq = _eq.Substring(0, _eq.Length - 1);
			}

			if (_eq.Contains('+'))
			{
				double result = 0;
				string[] subEq = _eq.Split('+');
				foreach (var item in subEq)
				{
					result += double.TryParse(item, out double param) ? param : default(double);
				}

				number.X = result.ToString();
				X = result.ToString();
				HistNumber = "";
			}
			else if (_eq.Contains('/'))
			{
				double result = 0;
				string[] subEq = _eq.Split('/');

				for (int i = 0; i < subEq.Length; i++)
				{
					if (i == 0)
					{
						result = double.TryParse(subEq[i], out double param) ? param : default(double);
					}
					else if(i==1 && subEq[i] == 0.ToString())
					{
						result = 0;
					}
					else
					{
						result /= double.TryParse(subEq[i], out double param) ? param : default(double);
					}
				}				
				X = result.ToString();
				HistNumber = "";
			}
			else if (_eq.Contains('-'))
			{
				double result = 0;
				string[] subEq = _eq.Split('-');
				for (int i = 0; i < subEq.Length; i++)
				{
					if (subEq[i] != "")
					{
						if (i == 0)
						{
							result = double.TryParse(subEq[i], out double param) ? param : default(double);
						}
						else
						{
							result -= double.TryParse(subEq[i], out double param) ? param : default(double);
						}
					}
				}			
				X = result.ToString();
				HistNumber = "";
			}
			else if (_eq.Contains('*'))
			{
				double result = 0;
				string[] subEq = _eq.Split('*');

				for (int i = 0; i < subEq.Length; i++)
				{
					if (subEq[i] != "")
					{
						if (i == 0)
						{
							result = double.TryParse(subEq[i], out double param) ? param : default(double);
						}
						else
						{
							result *= double.TryParse(subEq[i], out double param) ? param : default(double);
						}
					}
				}
				X = result.ToString();
				HistNumber = "";
			}
			else if (_eq.Contains('%'))
			{
				double result = 0;
				string[] subEq = _eq.Split('%');

				for (int i = 0; i < subEq.Length; i++)
				{
					if (subEq[i] != "")
					{
						if (i == 0)
						{
							result = double.TryParse(subEq[i], out double param) ? param : default(double);
						}
						else
						{
							result %= double.TryParse(subEq[i], out double param) ? param : default(double);
						}
					}
				}
				X = result.ToString();
				HistNumber = "";
			}
		}		
		public CalculateMethods()
		{			
			MakeEquation = new RelayCommand(MakeEqation, param => this.canExecute);
			ResetText = new RelayCommand(ResetingText, param => this.canExecute);
			ReverseNumber = new RelayCommand(ReversingNumber, param => this.canExecute);
			SqrtNumber = new RelayCommand(SquareNumber, param => this.canExecute);
			SaveFirstNum = new RelayCommand(SaveFirstNumber, param => this.canExecute);			
		}
		#endregion
	}
}
