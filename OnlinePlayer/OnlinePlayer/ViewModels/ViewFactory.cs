
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePlayer.ViewModels
{
    public abstract class ViewFactory
    {
        protected MainWindowVM mainVM;
        public ViewFactory(MainWindowVM mainVM)
        {
            this.mainVM = mainVM;
        }

        public abstract object CreateAssetVideo();
        public abstract object CreateOnlineVideo();
        public abstract object CreateStartPanel();

        public abstract ITab CreateTab();

        public abstract ITabButton CreateTabButton();
    }
}
