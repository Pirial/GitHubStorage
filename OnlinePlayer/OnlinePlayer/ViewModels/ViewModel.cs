using OnlinePlayer.ViewModels;
using OnlineVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OnlinePlayer.ViewModels
{
    public abstract class ViewModel : PropertyNotificator
    {
        private int id;
        public int ID
        {
            get { return id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (mainVM.VMList.Count(l => l.Name == value) == 0)
                    name = value;
                else
                    new ApplicationException("ViewModel with such name already exists in MainViewModel!");
            }
        }

        protected MainWindowVM mainVM;
        public MainWindowVM MainVM
        {
            get { return mainVM; }
        }

        public ViewModel ParentVM { get; protected set; }

        public IEnumerable<ViewModel> ChildList
        {
            get
            {
                return
                    from l in this.mainVM.VMList
                    where l.ParentVM == this
                    select l;
            }
        }

        public event PropertyChangedEventHandler OnCollectionPropertyRefresh;
        public void RaiseOnCollectionPropertyRefresh(string propertyName)
        {
            if (OnCollectionPropertyRefresh != null)
                OnCollectionPropertyRefresh(this, new PropertyChangedEventArgs(propertyName));
        }
        protected List<int> collectionRefreshListneres = new List<int>();

        public ViewModel(MainWindowVM mainVM, bool makeActive = true, ViewModel parentVM = null, bool setActiveAsParent = false)
        {
            this.mainVM = mainVM;
            if (setActiveAsParent)
                this.ParentVM = this.MainVM.ActiveVM;
            else
                this.ParentVM = parentVM;
            
            this.id = (mainVM.VMList.Count < 1) ? 1 : (from l in mainVM.VMList select l.ID).Max() + 1;
            this.MainVM.VMList.Add(this);
            if (makeActive & !setActiveAsParent)
                this.MainVM.ActiveVM = this;
            
            this.RefreshEventHandlers();
        }

        public void RefreshEventHandlers()
        {
            foreach(VMRefreshCollectionHandler handler in mainVM.RefreshCollectionHandlerList.Where(h => h.VMClassName == this.GetType().Name))
                if (!collectionRefreshListneres.Contains(handler.ListenerHashCode))
                {
                    this.OnCollectionPropertyRefresh += handler.Handler;
                    collectionRefreshListneres.Add(handler.ListenerHashCode);
                }
        }

        public void RemoveEventHandlers(VMRefreshCollectionHandler handler)
        {
            collectionRefreshListneres.RemoveAt(collectionRefreshListneres.IndexOf(handler.ListenerHashCode));
            this.OnCollectionPropertyRefresh -= handler.Handler;
        }

    }
}
