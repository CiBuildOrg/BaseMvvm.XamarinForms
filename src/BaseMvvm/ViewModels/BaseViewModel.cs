using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Helpers;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.ViewModels
{
    /// <summary>
    /// main action delegate 
    /// </summary>
    public delegate void OnCommandDelegate();

    /// <summary>
    /// base mvvm structure 
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// magic trick for async multiple action 
        /// </summary>
        private readonly Dictionary<string, OnCommandDelegate> cmds
            = new Dictionary<string, OnCommandDelegate>();

        /// <summary>
        /// Main Command Manager 
        /// </summary>
        private ICommand _commands;

        /// <summary>
        /// Page icon 
        /// </summary>
        private FileImageSource _icon;

        /// <summary>
        /// for ActivityIndicator 
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// for page title 
        /// </summary>
        private string title = String.Empty;

        /// <summary>
        /// always use this variable for CommandProperty (do not determine a new ICommand) 
        /// </summary>
        public ICommand Commands
        {
            get { return _commands ?? (_commands = new Command<string>(async (multipleCommand) => await ExecuteCommands(multipleCommand))); }
        }

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public FileImageSource Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        /// <summary>
        /// retrives from baseContent 
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        /// using one ICommand and Calls it with parameters 
        /// </summary>
        /// <param name="commandName">
        /// multiple command separates with comma [ First:commandName{string},
        /// Second:useBusyIndicator{boolean} ]
        /// </param>
        /// <param name="useBusyIndicator">
        /// manages the IsBusyProperty and works async method 
        /// </param>
        public void CallCommand(string commandName, bool useBusyIndicator = true)
        {
            if (!cmds.ContainsKey(commandName)) throw new Exception("CMD NOT FOUND");
            Commands.Execute($"{commandName},{useBusyIndicator}");
        }

        /// <summary>
        /// set custom command with external method, be careful for the multiple addition(with same commandName) 
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
        /// using one ICommand and Calls it with parameters 
        /// </summary>
        /// <param name="multipleCommand">
        /// multiple command separates with comma [ First:commandName{string},
        /// Second:useBusyIndicator{boolean} ]
        /// </param>
        /// <returns>
        /// Task 
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