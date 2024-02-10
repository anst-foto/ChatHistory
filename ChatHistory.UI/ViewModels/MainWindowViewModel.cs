using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ChatHistory.Core.Config;
using ChatHistory.Core.Context;
using ChatHistory.Core.Models;

using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace ChatHistory.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDbContext db;
    public ObservableCollection<User> Users { get; }
    public ObservableCollection<Message> Messages { get; }

    private Message selectedMessage;
    public Message SelectedMessage
    {
        get => this.selectedMessage;
        set => this.RaiseAndSetIfChanged(ref this.selectedMessage, value);
    }


    public  ReactiveCommand<Unit, Unit> CommandUserInfo { get; }

    public MainWindowViewModel()
    {
        try
        {
            this.db = new SqliteContext();
            this.Users = new ObservableCollection<User>(this.db.GetAllUsers());
            this.Messages = new ObservableCollection<Message>(this.db.GetAllMessages());
        }
        catch (ConfigException e)
        {
            var result = MessageBoxManager
                .GetMessageBoxStandard("!!! Error !!!", e.Message, ButtonEnum.OkAbort, Icon.Error)
                .ShowAsync()
                .Result;
            if (result == ButtonResult.Abort) Environment.Exit(1);
        }

        this.CommandUserInfo = ReactiveCommand.Create(
            execute: () =>
            {
                MessageBoxManager.GetMessageBoxStandard("!!! Info !!!", $"User: {SelectedMessage.Sender.Name}").ShowAsync();
            });
    }
}
