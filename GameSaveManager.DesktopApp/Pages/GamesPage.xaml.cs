﻿namespace GameSaveManager.DesktopApp.Pages
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage(IFactory<IBackupStrategy> backupStrategy, IOptions<ObservableCollection<GameInformationModel>> options)
        {
            InitializeComponent();
            DataContext = new GamesPageViewModel(backupStrategy, options);
        }
    }
}