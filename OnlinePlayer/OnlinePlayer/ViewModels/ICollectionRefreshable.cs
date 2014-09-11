using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using OnlinePlayer.ViewModels;

namespace OnlineVideo.ViewModels
{
    interface ICollectionRefreshable
    {
        Dictionary<string, System.ComponentModel.PropertyChangedEventHandler> HandlerDictionary { get; }
    }

    public class VMRefreshCollectionHandler : IEquatable<VMRefreshCollectionHandler>
    {
        public string VMClassName { get; protected set; }
        public int ListenerHashCode { get; protected set; }

        public PropertyChangedEventHandler Handler { get; set; }

        public VMRefreshCollectionHandler(string vmClassName, int listenerHashCode, PropertyChangedEventHandler handler)
        {
            VMClassName = vmClassName;
            ListenerHashCode = listenerHashCode;
            Handler = handler;
        }

        public override bool Equals(object obj)
        {
            VMRefreshCollectionHandler other = obj as VMRefreshCollectionHandler;
            if (obj != null)
                return this.VMClassName == other.VMClassName && this.ListenerHashCode == other.ListenerHashCode;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (VMClassName + ListenerHashCode.ToString()).GetHashCode();
        }

        public bool Equals(VMRefreshCollectionHandler other)
        {
            return this.Equals(other as object);
        }
    }

    public static class VMRefreshCollectionHandlerManipulator
    {
        public static void Add(this List<VMRefreshCollectionHandler> handlerList, Type vmType, int listenerHashCode, PropertyChangedEventHandler handler, IEnumerable<ViewModel> fullVMList)
        {
            if (handlerList.Where(h => h.VMClassName == vmType.Name && h.ListenerHashCode == listenerHashCode).FirstOrDefault() == null)
                handlerList.Add(new VMRefreshCollectionHandler(vmType.Name, listenerHashCode, handler));
            foreach (ViewModel vm in fullVMList)
                vm.RefreshEventHandlers();
        }

        public static void Remove(this List<VMRefreshCollectionHandler> handlerList, Type vmType, int listenerHashCode, PropertyChangedEventHandler handler, IEnumerable<ViewModel> fullVMList)
        {
            VMRefreshCollectionHandler vmh = handlerList.Where(h => h.VMClassName == vmType.Name && h.ListenerHashCode == listenerHashCode).FirstOrDefault();
            if (vmh != null)
            {
                handlerList.Remove(vmh);
                foreach (ViewModel vm in fullVMList)
                    vm.RemoveEventHandlers(vmh);
            }
        }
    }

}
