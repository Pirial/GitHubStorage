using OnlinePlayer.Views;
using OnlineVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePlayer.ViewModels
{
    public interface IMainWindow
    {
        ITabContainer TabContainer { get; }
        ITabBtnContainer TabBtnContainer { get; }
        MainWindowVM ViewModel { get; }
    }

    public interface ITab
    {
        object Content { get; set; }
        bool Visible { get; set; }
    }

    public interface ITabContainer
    {
        int SelectedIndex { get; set; }
        ITab SelectedItem { get; }
        int TabCount { get; }

        ITab this[int index] { get; }

        void InsertTab(int position, ITab tab);
        void RemoveTab(int index);
        void RemoveTab(ITab tab);
        void ClearTabs();

        object GetDataContext(int index);
        int GetIndex(object obj);
    }

    public interface ITabButton
    {
        int Index { get; set; }
        object Content { get; set; }
        object ToolTip { get; set; }

        Command ClickCommand { get; set; }
        Command MouseDoubleClick { get; set; }

        void SetSelectedStyle(bool selected);
    }

    public interface ITabBtnContainer
    {
        int BtnCount { get; }

        ITabButton this[int index] { get; }
        IEnumerable<ITabButton> Buttons { get; }

        void Insert(int position, ITabButton btn);
        void Remove(int index);
        void RemoveRange(int startIndex, int count);
    }

    public class NewTabArg
    {
        public int? Position { get; set; }
        public string Caption { get; set; }
        public Object Content { get; set; }

        public NewTabArg()
        {
            Position = null;
            Caption = "New Tab";
        }

        public NewTabArg(string caption, object content, int? position = null)
        {
            Caption = caption;
            Content = content;
            Position = position;
        }
    }

    public class MainWindowVM : PropertyNotificator
    {
        private static MainWindowVM instance = null;
        public static MainWindowVM Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainWindowVM();
                return instance;
            }
        }

        public ViewModel ActiveVM { get; set; }
        public List<ViewModel> VMList { get; private set; }

        public ViewModel this[string viewName]
        {
            get { return (from l in VMList where l.Name == viewName select l).FirstOrDefault(); }
        }

        public ViewModel this[int viewID]
        {
            get { return (from l in VMList where l.ID == viewID select l).FirstOrDefault(); }
        }

        public ViewFactory ViewFactory{ get; set; }

        public IMainWindow MainWin { get; set; }

        public delegate void CloseTabHandler(ViewModel vm);
        public event CloseTabHandler OnCloseTab;

        private List<VMRefreshCollectionHandler> refreshCollectionHandlerList = new List<VMRefreshCollectionHandler>();
        public ReadOnlyCollection<VMRefreshCollectionHandler> RefreshCollectionHandlerList
        {
            get { return refreshCollectionHandlerList.AsReadOnly(); }
        }
        public void AddRefreshCollectionHandler<T>(PropertyChangedEventHandler handler, object listener) where T : ViewModel
        {
            this.refreshCollectionHandlerList.Add(typeof(T), listener.GetHashCode(), handler, this.VMList.OfType<T>());
        }
        public void RemoveRefreshCollectionHandler<T>(PropertyChangedEventHandler handler, object listener) where T : ViewModel
        {
            this.refreshCollectionHandlerList.Remove(typeof(T), listener.GetHashCode(), handler, this.VMList.OfType<T>());
        }

        protected MainWindowVM()
        {
            this.VMList = new List<ViewModel>();

            SetActiveTabCmd = new Command(arg => SetActiveTabMethod((int)arg));
            NewTabCmd = new Command(arg => NewTab(arg));
            DelTabCmd = new Command(arg => DelTab(arg));
            ShowAllTabsCmd = new Command(arg => ShowAllTabs(arg));
        }

        public void Start()
        {
            this.NewTab(new NewTabArg("New", this.ViewFactory.CreateStartPanel()));
        }

        public int PreviousTabIndex { get; private set; }
        public int SelectedPageIndex
        {
            get { return MainWin.TabContainer.SelectedIndex; }
            set
            {
                this.SetActiveTabMethod(value);
                RaisePropertyChanged("SelectedPageIndex");
            }
        }

        private bool isEnable = true;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                this.RaisePropertyChanged("IsEnable");
            }
        }

        private double maxHeight;
        public double MaxHeight
        {
            get { return this.maxHeight; }
            set { this.maxHeight = value; this.RaisePropertyChanged("MaxHeight"); }
        }

        public Command NewTabCmd { get; private set; }
        public void NewTab(object parameter)
        {
            NewTabArg newTabArg = parameter as NewTabArg;
            if (newTabArg == null)
                newTabArg = new NewTabArg("", new StartPanel());

            if (MainWin.TabContainer.TabCount <= 10
                && newTabArg != null)
            {
                int position = newTabArg.Position ?? MainWin.TabContainer.TabCount;

                ITab newTab = this.ViewFactory.CreateTab();

                newTab.Visible = false;
                newTab.Content = newTabArg.Content;

                MainWin.TabContainer.InsertTab(position, newTab);

                ITabButton btnTab = this.ViewFactory.CreateTabButton();

                btnTab.Content = newTabArg.Caption;
                btnTab.ToolTip = newTabArg.Caption;
                btnTab.ClickCommand = this.SetActiveTabCmd;
                btnTab.MouseDoubleClick = this.DelTabCmd;

                btnTab.Index = position;

                MainWin.TabBtnContainer.Insert(position, btnTab);

                RefreshTabBtnsIndex();

                SelectedPageIndex = btnTab.Index;
            }
        }

        public void LoadToSelectedTab(NewTabArg tabArg)
        {
            var vmToRemove = GetVMFromContent(MainWin.TabContainer.SelectedIndex);
            MainWin.TabContainer.SelectedItem.Content = tabArg.Content;
            foreach (ITabButton b in MainWin.TabBtnContainer.Buttons)
                if (b.Index == MainWin.TabContainer.SelectedIndex)
                {
                    b.Content = tabArg.Caption;
                    b.ToolTip = tabArg.Caption;
                    break;
                }
            VMList.Remove(vmToRemove);
        }

        private void RefreshTabBtnsIndex()
        {
            int i = 0;
            foreach (ITabButton b in this.MainWin.TabBtnContainer.Buttons)
            {
                if (b.Index != -1 && b.Index != -2)
                    b.Index = i;
                i++;
            }
        }

        public Command ShowAllTabsCmd { get; private set; }
        private void ShowAllTabs(object parameter)
        {
            /*if (parameter is FrameworkElement)
            {
                FrameworkElement tabMenuContainer = parameter as FrameworkElement;
                if (this.MainWin.TabContainer.TabCount > 0)
                {
                    ContextMenu contextMenu = tabMenuContainer.ContextMenu;
                    contextMenu.Items.Clear();
                    foreach (ITabButton b in this.MainWin.TabBtnContainer.Buttons)
                        if (b.Index != -1 && b.Index != -2)
                        {
                            MenuItem menuItem = new MenuItem();

                            menuItem.Style = TabMenuBtnStyle;
                            menuItem.Header = (b as Button).Content.ToString();
                            menuItem.Width = 220;
                            menuItem.Tag = b.Index;
                            menuItem.Click += (s, arg) => SelectedPageIndex = Convert.ToInt32((s as MenuItem).Tag);

                            contextMenu.Items.Add(menuItem);
                        }
                    contextMenu.PlacementTarget = tabMenuContainer;
                    contextMenu.IsOpen = true;
                }
                else
                    tabMenuContainer.ContextMenu.Items.Clear();
            }*/
        }

        private ViewModel GetVMFromContent(int tabIndex)
        {
            return this.MainWin.TabContainer.GetDataContext(tabIndex) as ViewModel;
        }
        public int GetTabIndexByVM(ViewModel vm)
        {
            return this.MainWin.TabContainer.GetIndex(vm);
        }

        public Command SetActiveTabCmd { get; private set; }
        public void SetActiveTabMethod(int tabIndex)
        {
            if (Convert.ToInt32(tabIndex) != this.MainWin.TabContainer.SelectedIndex)
            {
                if (this.MainWin.TabContainer.SelectedIndex != -1)
                    this.PreviousTabIndex = this.MainWin.TabContainer.SelectedIndex;

                this.MainWin.TabContainer.SelectedIndex = tabIndex;
                this.ActiveVM = this.GetVMFromContent(tabIndex);

                foreach (ITabButton b in this.MainWin.TabBtnContainer.Buttons)
                {
                    if (b.Index != -1 && b.Index != -2)
                        b.SetSelectedStyle(b.Index == this.MainWin.TabContainer.SelectedIndex);
                }
            }
        }

        public Command DelTabCmd { get; private set; }
        public void DelTab(object tabIndex)
        {
            int index = 0;
            try { index = Convert.ToInt32(tabIndex); }
            catch { return; }
            bool isCancel = false;

            if (OnCloseTab != null)
                OnCloseTab(GetVMFromContent(index));

            ViewModel vm = this.GetVMFromContent(index);
            ViewModel[] removeList = (from l in this.VMList
                                      where (l.ParentVM == vm) || l == vm
                                      select l).ToArray();
            for (int i = 0; i <= removeList.Count() - 1; i++)
                VMList.Remove(removeList[i]);
            IDisposable TabContent2Dispose = this.MainWin.TabContainer[index].Content as IDisposable;
            if (TabContent2Dispose != null)
                TabContent2Dispose.Dispose();
            this.MainWin.TabContainer.RemoveTab(index);
            this.MainWin.TabBtnContainer.Remove(index);

            RefreshTabBtnsIndex();

            if (MainWin.TabContainer.TabCount > 0)
            {
                if (!(MainWin.TabContainer.SelectedIndex >= 0 && MainWin.TabContainer.SelectedIndex <= MainWin.TabContainer.TabCount - 1))
                    if (PreviousTabIndex >= 0 && this.PreviousTabIndex <= MainWin.TabContainer.TabCount - 1)
                        SelectedPageIndex = this.PreviousTabIndex;
                    else
                        SelectedPageIndex = MainWin.TabContainer.TabCount - 1;
            }
            else
                this.ActiveVM = null;

        }

        public void ClearAllTabs()
        {
            VMList.Clear();
            if (this.VMList.Count == 0)
                this.ActiveVM = null;
            MainWin.TabContainer.ClearTabs();
            MainWin.TabBtnContainer.RemoveRange(0, MainWin.TabBtnContainer.BtnCount - 2);
        }
    }
}
