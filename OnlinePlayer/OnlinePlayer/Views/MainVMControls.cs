using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using OnlinePlayer.ViewModels;

namespace OnlinePlayer.Views
{
    public class TabContainer: ITabContainer
    {
        private TabControl adaptee;

        public TabContainer(TabControl adaptee)
        {
            this.adaptee = adaptee;
        }
        
        public int SelectedIndex 
        {
            get { return adaptee.SelectedIndex; }
            set { adaptee.SelectedIndex = value ; }
        }
        public ITab SelectedItem 
        { 
            get { return adaptee.SelectedItem as ITab; } 
        }
        public int TabCount 
        {
            get { return adaptee.Items.Count; }
        }

        public ITab this[int index] 
        {
            get { return adaptee.Items[index] as ITab; }
        }

        public void InsertTab(int position, ITab tab)
        {
            adaptee.Items.Insert(position, tab);
        }
        public void RemoveTab(int index)
        {
            adaptee.Items.RemoveAt(index);
        }
        public void RemoveTab(ITab tab)
        {
            adaptee.Items.Remove(tab);
        }
        public void ClearTabs()
        {
            adaptee.Items.Clear();
        }

        public object GetDataContext(int index)
        {
            return (this[index].Content as UserControl).DataContext;
        }

        public int GetIndex(object vm)
        {
            foreach (ITab u in adaptee.Items)
                if ((u.Content as UserControl).DataContext == vm)
                    return adaptee.Items.IndexOf(u);
            return -1;
        }
    }

    public class TabBtnContainer : ITabBtnContainer
    {
        private StackPanel adaptee;

        public TabBtnContainer(StackPanel adaptee)
        {
            this.adaptee = adaptee;
        }

        public int BtnCount 
        {
            get { return adaptee.Children.Count; }
        }

        public ITabButton this[int index] 
        {
            get { return adaptee.Children[index] as ITabButton; }
        }
        public IEnumerable<ITabButton> Buttons 
        {
            get 
            {
                return adaptee.Children.OfType<ITabButton>().Cast<ITabButton>();
            }
        }

        public void Insert(int position, ITabButton btn)
        {
            adaptee.Children.Insert(position, btn as UIElement);
        }
        public void Remove(int index)
        {
            adaptee.Children.RemoveAt(index);
        }
        public void RemoveRange(int startIndex, int count)
        {
            adaptee.Children.RemoveRange(startIndex, count);
        }
    }
    
    public class Tab : TabItem, ITab
    {
        public bool Visible
        {
            get { return this.Visibility == Visibility.Visible; }
            set
            {
                if (value)
                    this.Visibility = Visibility.Visible;
                else
                    this.Visibility = Visibility.Collapsed;
            }
        }
    }

    public class TabButton : Button, ITabButton
    {
        public TabButton()
        {
            this.Margin = new System.Windows.Thickness(0, 0, 0, -1);
            this.Style = System.Windows.Application.Current.Resources["TabButton"] as Style;

            Binding binding = new Binding("Tag") { RelativeSource = RelativeSource.Self };
            this.SetBinding(Button.CommandParameterProperty, binding);
        }


        public int Index
        {
            get { return Convert.ToInt32(this.Tag); }
            set { this.Tag = value; }
        }

        public void SetSelectedStyle(bool selected)
        {
            if (selected)
                this.Style = System.Windows.Application.Current.Resources["SelectedTabButton"] as Style;
            else
                this.Style = System.Windows.Application.Current.Resources["TabButton"] as Style;
        }

        public Command ClickCommand
        {
            get { return base.Command as Command; }
            set { base.Command = value; }
        }

        public Command MouseDoubleClick
        {
            get { return base.Command as Command; }
            set { base.Command = value; }
        }
    }
}
