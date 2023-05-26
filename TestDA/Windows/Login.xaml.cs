using System;
using System.Collections.Generic;
using System.IO;
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
using EasyCaptcha;
using EasyCaptcha.Wpf;

namespace TestDA.Windows
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

			UserCap.Visibility = Visibility.Hidden;
			MyCaptha.Visibility = Visibility.Hidden;
			CapBtn.Visibility = Visibility.Hidden;
			//var path = @"C:\Users\azat\Desktop\Images";
			//var path = @"C:\Users\azat\Desktop\Images";
			//var path = @"C:\Users\azat\Desktop\Images";
			//using (TestDemoContext context = new TestDemoContext())
			//{
			//	var iamges = Directory.GetFiles(path);

			//	foreach (var tovar in context.Tovaris)
			//	{
			//		try
			//		{
			//			tovar.Photo = File.ReadAllBytes(iamges.FirstOrDefault(x => x.Contains(tovar.Id)));
			//		}
			//		catch
			//		{
			//			tovar.Photo = File.ReadAllBytes(iamges.FirstOrDefault(x => x.Contains("abc")));
			//		}
			//	}
			//	context.SaveChanges();
			//}

			var path = @"C:\Users\azat\Desktop\Images";

			using (TestDemoContext a=new TestDemoContext())
			{
				var images = Directory.GetFiles(path);
				foreach (var tov in a.Tovaris)
				{
					try
					{
						tov.Photo = File.ReadAllBytes(images.FirstOrDefault(x=>x.Contains(tov.Id)));
					}
					catch
					{
						tov.Photo=File.ReadAllBytes(images.FirstOrDefault(x=>x.Contains("asd")));
					}
				}
				a.SaveChanges();
			}
		}

		private void EnterBtn(object sender, RoutedEventArgs e)
		{
			try
			{
				using(TestDemoContext context=new TestDemoContext())
				{
					User user = context.Users.FirstOrDefault(x=>x.Login == UserLogTxt.Text && x.Password==UserPasTxt.Text);
					if (user != null && user.RoleId==1)
					{
						AdminWindow adminWindow = new AdminWindow();
						adminWindow.Show();
					}
					else if (user != null && user.RoleId == 2)
					{
						ClientWindow client = new ClientWindow();
						client.Show();
					}
					else
					{
						MessageBox.Show("Такого пользователя нет, проверьте данные");
						UserLogTxt.Visibility = Visibility.Hidden;
						UserPasTxt.Visibility= Visibility.Hidden;
						EnterABtn.Visibility = Visibility.Hidden;

						UserCap.Visibility = Visibility.Visible;
						MyCaptha.Visibility = Visibility.Visible;
						CapBtn.Visibility = Visibility.Visible;
						MyCaptha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Возникла ошибка, попробуйте еще раз");
			}
		}

		private void asdasd(object sender, RoutedEventArgs e)
		{
			if (UserCap.Text == MyCaptha.CaptchaText)
			{
				UserLogTxt.Visibility = Visibility.Visible;
				UserPasTxt.Visibility = Visibility.Visible;
				EnterABtn.Visibility = Visibility.Visible;

				UserCap.Visibility = Visibility.Hidden;
				MyCaptha.Visibility = Visibility.Hidden;
				CapBtn.Visibility = Visibility.Hidden;
			}
			else
			{
				MyCaptha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
			}
		}
	}
}
