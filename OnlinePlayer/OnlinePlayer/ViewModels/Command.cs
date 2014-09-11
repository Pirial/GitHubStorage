using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlinePlayer.ViewModels
{
 public class Command:ICommand
    {
        public Command(Action<object> action, Predicate<object> enablePredicate = null)
        {
            ExecuteDelegate = action;

            CanExecuteDelegate = enablePredicate;
        }

        public string Name { get; set; }
        public string ImageSource { get; set; }
        
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

 public abstract class PropertyNotificator : INotifyPropertyChanged
 {
     private bool isModified = false;
     public virtual bool IsModified
     {
         get { return isModified; }
         set
         {
             isModified = value;
             RaisePropertyChanged("IsModified");
         }
     }

     public virtual event PropertyChangedEventHandler PropertyChanged;

     public virtual void RaisePropertyChanged(string propertyName)
     {
         if (PropertyChanged != null)
         {
             PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

             PropertyInfo pi = this.GetType().GetProperty(propertyName);
             if (pi != null)
             {
                 MagicAttribute attr = pi.GetCustomAttributes(typeof(MagicAttribute), false).FirstOrDefault() as MagicAttribute;
                 if (attr != null && attr.dependentProperties != null)
                     foreach (string pName in attr.dependentProperties)
                         PropertyChanged(this, new PropertyChangedEventArgs(pName));
             }
         }
         if (propertyName != "IsModified")
             IsModified = true;
     }

 }

 public class MagicAttribute : Attribute
 {
     public string[] dependentProperties { get; private set; }

     public MagicAttribute() { }
     public MagicAttribute(params string[] dependentPropertyName)
     {
         dependentProperties = dependentPropertyName.ToArray<string>();
     }
 }
}
