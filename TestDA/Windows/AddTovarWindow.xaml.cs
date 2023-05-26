using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestDA.Model;

namespace TestDA.Windows
{
	/// <summary>
	/// Логика взаимодействия для AddTovarWindow.xaml
	/// </summary>
	public partial class AddTovarWindow : Window
	{
		AdminWindow adminWindow;
		Tovari tovari;
		public int? a = 0;
		public static TestDemoContext context=new TestDemoContext();
		public AddTovarWindow(AdminWindow adminWindow, Tovari tovari)
		{
			InitializeComponent();
			this.adminWindow = adminWindow;
			this.tovari = context.Tovaris.FirstOrDefault(x=>x.Name==tovari.Name);

			TN.Text = tovari.Name;
			TPrice.Text = tovari.Price.ToString();
			TQ.Text=tovari.Quantity.ToString();
			TCOM.ItemsSource = context.Types.ToList();
			TCOM.SelectedItem = context.Types.FirstOrDefault(a=>a.IdType==tovari.TypeId);

		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{

			tovari.Price = Convert.ToInt32(TPrice.Text);
			tovari.Name = TN.Text;
			tovari.Quantity= Convert.ToInt32(TQ.Text);
			tovari.TypeId = TCOM.SelectedIndex+1;

			context.Update(tovari);
			context.SaveChanges();
			adminWindow.UpdateLV(context);
		}
	}
}
