using OnlinePlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePlayer.Views
{
    class ViewFactoryV:ViewFactory
    {
       public ViewFactoryV(MainWindowVM mainVM) : base(mainVM) { }

       public override object CreateAssetVideo()
       {
           return new AssetVideo();
       }
       public override object CreateOnlineVideo()
       {
           return null;
       }
       public override object CreateStartPanel()
        {
            return new StartPanel();
        }

        public override ITab CreateTab()
        {
            return new Tab();
        }

        public override ITabButton CreateTabButton()
        {
            return new TabButton();
        }
    }
}
