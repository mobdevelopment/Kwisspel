/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:QuizGamePack.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace QuizGamePack.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<QuizCrud>();
            SimpleIoc.Default.Register<QuizPlay>();
            SimpleIoc.Default.Register<WindowsViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]

        public QuizCrud Crud
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizCrud>();
            }
        }

        public QuizPlay Quiz
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizPlay>();
            }
        }

        public WindowsViewModel Manager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WindowsViewModel>();
            }
        }

        public static void Cleanup()
        {
        }
    }
}