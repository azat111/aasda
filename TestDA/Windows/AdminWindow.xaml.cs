using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public  TestDemoContext context = new TestDemoContext();
        public AdminWindow()
        {
            InitializeComponent();
            ListV.ItemsSource = context.Tovaris.Include(a=>a.Type).ToList();

		}

		private void DeleteTovBtn(object sender, RoutedEventArgs e)
		{
            Tovari a = (Tovari)(sender as Button).DataContext;
            if (MessageBox.Show("Удалить","Удалить?",MessageBoxButton.YesNo)== MessageBoxResult.Yes)
            {
				using (TestDemoContext contextDel = new TestDemoContext())
				{
					contextDel.Remove(a);
					contextDel.SaveChanges();
					ListV.ItemsSource = contextDel.Tovaris.Include(a => a.Type).ToList();
				}			
			}

		}

		private void CombType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CombType.SelectedIndex==0)
			{
				ListV.ItemsSource = context.Tovaris.Include(a => a.Type).ToList();
				return;
			}
			ListV.ItemsSource = context.Tovaris.Include(a => a.Type).Where(q=>q.TypeId==CombType.SelectedIndex).ToList();
			
		}

		private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			ListV.ItemsSource = context.Tovaris.Include(a => a.Type).Where(x => x.Name.Contains(SearchTxt.Text)).ToList();
			//ListV.ItemsSource = context.Tovaris.Where(x=>x.Name.Contains(SearchTxt.Text)).ToList();
		}

		private void EditBtn(object sender, RoutedEventArgs e)
		{
			Tovari a = (Tovari)(sender as Button).DataContext;
			AddTovarWindow addTovarWindow = new AddTovarWindow(this, a);
			addTovarWindow.Show();
		}
		public void UpdateLV(TestDemoContext context2)
		{
			using (context2 = new TestDemoContext())
			{
				ListV.ItemsSource = context2.Tovaris.Include(a => a.Type).ToList();
				context = context2;
			}
		}
	}
}
