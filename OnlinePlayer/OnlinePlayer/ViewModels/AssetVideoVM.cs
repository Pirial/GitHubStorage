using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePlayer.ViewModels
{
    public class AssetVideoVM
    {
        #region Construector
        static AssetVideoVM()
        {
            AssetVideoVM.ExecuteCmd = new Command(arg => Execute(arg));
        }
        #endregion

        #region Commands
        public static Command ExecuteCmd { get; set; }
        public static void Execute(object obj)
        {
            MainWindowVM.Instance.LoadToSelectedTab(new NewTabArg("", MainWindowVM.Instance.ViewFactory.CreateAssetVideo()));
        }
        #endregion
    }
}
