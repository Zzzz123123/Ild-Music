using ShareInstances;
using ShareInstances.Services.Entities;
using ShareInstances.Services.Storage;
using ShareInstances.PlayerResources;
using ShareInstances.Services.Entities;
using ShareInstances.PlayerResources.Interfaces;
using Ild_Music;
using Ild_Music.Command;
using Ild_Music.ViewModels.Base;
using Ild_Music.ViewModels;

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Avalonia.Controls.Selection;

namespace Ild_Music.ViewModels
{
    //Types of Lists
    public enum ListType
    {
        ARTISTS,
        PLAYLISTS,
        TRACKS
    }

    public class ListViewModel : BaseViewModel
    {
        public static readonly string nameVM = "ListVM";        
        public override string NameVM => nameVM;
        
        private static IPlayer _player;

        #region Services
        private SupporterService supporter => (SupporterService)App.Stage.GetServiceInstance("SupporterService");
        private FactoryService factory => (FactoryService)base.GetService("FactoryService");
        private ViewModelHolder<BaseViewModel> holder => (ViewModelHolder<BaseViewModel>)base.GetService("HolderService");
        #endregion

        #region Storage Scope
        private Storage storage = new(0);
        public bool IsStorageEmpty => storage.Count == 0;
        #endregion

        #region Properties
        public CommandDelegator AddCommand { get; }
        public CommandDelegator DeleteCommand { get; }
        public CommandDelegator EditCommand { get; }
        public CommandDelegator BackCommand { get; }
        public CommandDelegator ItemSelectCommand { get; }
        
        public CommandDelegator DefineListTypeCommand { get; }

        public ListType ListType {get; private set;}
        public static ObservableCollection<string> Headers { get; private set; } = new() {"Artists","Playlists","Tracks"};
        public static string Header { get; set; }
        public static ObservableCollection<ICoreEntity> CurrentList { get; set; } = new();
        public ICoreEntity CurrentItem { get; set; }

        public SelectionModel<object> HeaderSelection { get; }

        private MainViewModel MainVM => (MainViewModel)App.ViewModelTable[MainViewModel.nameVM];
        #endregion

        #region Ctor
        public ListViewModel()
        {
            AddCommand = new(Add, null);
            DeleteCommand = new(Delete, null);
            EditCommand = new(Edit, null);
            ItemSelectCommand = new(ItemSelect, null);
            DefineListTypeCommand = new(InitCurrentList, null);
        }
        #endregion

        #region Public Methods
        private void InitCurrentList(object obj)
        {
            DisplayProviders();
        }

        public void UpdateProviders()
        {
            DisplayProviders();
        }

        private void DisplayProviders()
        {
            CurrentList.Clear();
            switch (Header)
            {
                case "Artists":
                    supporter.ArtistsCollection.ToList().ForEach(a => CurrentList.Add(a));
                    break;
                case "Playlists":
                    supporter.PlaylistsCollection.ToList().ForEach(p => CurrentList.Add(p));
                    break;
                case "Tracks":
                    supporter.TracksCollection.ToList().ForEach(t => CurrentList.Add(t));
                    break;
            }      
        }
        #endregion

        #region CommandMethods
        private void Add(object obj)
        {
            var factory = (FactoryViewModel)App.ViewModelTable[FactoryViewModel.nameVM];
            // var main = (MainViewModel)App.ViewModelTable[MainViewModel.nameVM];

            switch (Header)
            {
                case "Artists":
                    factory.SetSubItem(index:0);
                    break;
                case "Playlists":
                    factory.SetSubItem(index:1);
                    break;
                case "Tracks":
                    factory.SetSubItem(index:2);
                    break;
            }

            MainVM.PushVM(this, factory);
            MainVM.ResolveWindowStack();
        }

        private void Delete(object obj) 
        {
            supporter.DeleteInstance(CurrentItem);
            UpdateProviders();
        }
        
        private void Edit(object obj)
        {
            var factory = (FactoryViewModel)App.ViewModelTable[FactoryViewModel.nameVM];
            factory.SetEditableItem(CurrentItem);
            // var main = (MainViewModel)App.ViewModelTable[MainViewModel.nameVM];

            switch (Header)
            {
                case "Artists":
                    factory.SetSubItem(index:0);
                    break;
                case "Playlists":
                    factory.SetSubItem(index:1);
                    break;
                case "Tracks":
                    factory.SetSubItem(index:2);
                    break;
            }

            MainVM.PushVM(this, factory);
            MainVM.ResolveWindowStack();
        }

        private void ItemSelect(object obj)
        {
            MainVM.ResolveInstance(this, CurrentItem);
        }
        #endregion
    }
}