using OnlinePlayer.ViewModels;

namespace OnlinePlayer.ViewModel
{
    public class Singleton : PropertyNotificator
    {
        static Singleton()
        {
            Instance = new Singleton();
        }
        
        public Singleton(){}
        
        public static Singleton Instance { get; private set; }
        private double height;

        public double Height
        {
            get { return this.height; }           
            set 
            { 
                this.height = value; 
                this.RaisePropertyChanged("Height"); 
            }
        }
    }
}
