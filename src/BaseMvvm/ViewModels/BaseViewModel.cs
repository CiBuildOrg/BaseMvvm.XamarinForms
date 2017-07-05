using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Helpers;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.ViewModels
{
    public delegate void OnCommandDelegate();

    public partial class BaseViewModel : ObservableObject
    {
        private FileImageSource _icon;

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public FileImageSource Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        private bool _isBusy;

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string title = String.Empty;

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private readonly Dictionary<string, OnCommandDelegate> cmds
            = new Dictionary<string, OnCommandDelegate>();

        private ICommand _commands;

        public ICommand Commands
        {
            get { return _commands ?? (_commands = new Command<string>(async (multipleCommand) => await ExecuteCommands(multipleCommand))); }
        }

        /// <summary>
        /// set custom command with external method, be careful for the multiple addition 
        /// </summary>
        /// <param name="commandName">
        /// unique cmd name 
        /// </param>
        /// <param name="externalMethod">
        /// triggered method 
        /// </param>
        public void SetCommand(string commandName, OnCommandDelegate externalMethod)
        {
            cmds[commandName] = externalMethod;
        }

        /// <summary>
        /// </summary>
        /// <param name="commandName">
        /// unique cmd name 
        /// </param>
        /// <param name="useBusyIndicator">
        /// use with activityindicator 
        /// </param>
        public void CallCommand(string commandName, bool useBusyIndicator = true)
        {
            if (!cmds.ContainsKey(commandName)) throw new Exception("CMD NOT FOUND");
            Commands.Execute($"{commandName},{useBusyIndicator}");
        }

        /// <summary>
        /// </summary>
        /// <param name="multipleCommand">
        /// multiple command separates with comma [ First:commandName{string},
        /// Second:useBusyIndicator{boolean} ]
        /// </param>
        /// <returns>
        /// </returns>
        private async Task ExecuteCommands(string multipleCommand)
        {
            bool useBusyIndicator = true;
            var parameters = multipleCommand.Split(',');
            if (parameters.Length > 1)
                useBusyIndicator = Boolean.Parse(parameters[1]);

            if (useBusyIndicator)
            {
                if (IsBusy)//there will be a problem while working with async
                    return;
                await Task.Run(() =>
                    {
                        IsBusy = true;
                        cmds[parameters[0]]();
                        IsBusy = false;
                    }
                );
            }
            else
            {
                await Task.Run(() =>
                    {
                        cmds[parameters[0]]();
                    }
                );
            }
        }
    }
}