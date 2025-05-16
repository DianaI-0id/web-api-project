using System;
using System.Collections.Generic;
using domain.UseCase;
using Presence.Desktop.Models;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using System.ComponentModel;
using System.Linq;

namespace Presence.Desktop.ViewModels
{
    public class GroupViewModel : ViewModelBase, IRoutableViewModel, INotifyPropertyChanged
    {
        public ICommand OpenFileDialog { get; }
        public ICommand DeleteStudentsFromGroupCommand { get; } 
        public ICommand DeleteSelectedItems { get; } //для нескольких записей
        public ICommand EditSelectedItem { get; } //для одной записи
        public ICommand DeleteSelectedItem { get; } //для одной записи

        public Interaction<string?, string?> SelectFileInteraction => _SelectFileInteraction;
        public readonly Interaction<string?, string?> _SelectFileInteraction;

        private string? _selectedFile;
        public string? SelectedFile
        {
            get => _selectedFile;
            set => this.RaiseAndSetIfChanged(ref _selectedFile, value);
        }

        //переменные-счетчики выделенных студентов
        private ObservableCollection<StudentPresenter> _selectedRecords = new ObservableCollection<StudentPresenter>(); //приватная
        public ObservableCollection<StudentPresenter> SelectedRecords //публичная для view
        {
            get { return _selectedRecords; }
            set
            {
                if (value != null)
                {
                    _selectedRecords = value;
                    OnPropertyChanged(nameof(SelectedRecords));
                    OnPropertyChanged(nameof(CanDelete));
                    OnPropertyChanged(nameof(CanEdit));
                }
            }
        }

        //отслеживаем кол-во выделенных записей для управления видимостью пунктов контекстного меню
        public bool CanDelete => SelectedRecords.Count > 1;
        public bool CanEdit => SelectedRecords.Count == 1;

        private readonly List<GroupPresenter> _groupPresentersDataSource = new List<GroupPresenter>();
        private ObservableCollection<GroupPresenter> _groups;
        public ObservableCollection<GroupPresenter> Groups => _groups;
        public ObservableCollection<StudentPresenter> StudentsCollection { get => _users; }
        public ObservableCollection<StudentPresenter> _users;

        public GroupPresenter? SelectedGroupItem
        {
            get => _selectedGroupItem;
            set => this.RaiseAndSetIfChanged(ref _selectedGroupItem, value);
        }

        private GroupPresenter? _selectedGroupItem;
        private IGroupUseCase _groupUseCase;

        public GroupViewModel(IGroupUseCase groupUseCase)
        {
            //доступ к классам для взаимодействия с БД
            _groupUseCase = groupUseCase;

            //присвоили командам их реализации
            OpenFileDialog = ReactiveCommand.CreateFromTask(SelectFile);
            DeleteStudentsFromGroupCommand = ReactiveCommand.Create(DeleteStudentsByGroup);
            DeleteSelectedItems = ReactiveCommand.Create(DeleteSelectedStudents);
            EditSelectedItem = ReactiveCommand.Create(EditSelectedStudent);
            DeleteSelectedItem = ReactiveCommand.Create(DeleteSelectedStudent);

            _SelectFileInteraction = new Interaction<string?, string?>();
            _users = new ObservableCollection<StudentPresenter>();

            RefreshGroups(); //обновили список групп

            this.WhenAnyValue(vm => vm.SelectedGroupItem)
                .Subscribe(_ =>
                {
                    RefreshGroups();
                    SetUsers();
                });

            _selectedRecords.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(CanDelete));
                OnPropertyChanged(nameof(CanEdit));
            };
        }

        private void SetUsers()
        {
            if (SelectedGroupItem == null)
            {
                return;
            }  
            StudentsCollection.Clear();
            
            var group = _groups.First(it => it.Id == SelectedGroupItem.Id);
            if (group.Students == null)
            {
                return;
            } 
            
            foreach (var item in group.Students)
            {
                StudentsCollection.Add(item);
            }
        }

        private void RefreshGroups()
        {
            _groupPresentersDataSource.Clear();
            foreach (var item in _groupUseCase.GetGroupsWithStudents())
            {
                GroupPresenter groupPresenter = new GroupPresenter
                {
                    Id = item.Id,
                    Name = item.GroupName,
                    Students = item.Students?.Select(user => new StudentPresenter
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Group = new GroupPresenter 
                        { 
                            Id = item.Id, 
                            Name = item.GroupName 
                        }
                    }
                    ).ToList()
                };
                _groupPresentersDataSource.Add(groupPresenter);
            }
            _groups = new ObservableCollection<GroupPresenter>(_groupPresentersDataSource);
        }

        private void DeleteStudentsByGroup()
        {
            
        }

        private void DeleteSelectedStudents()
        {
            
        }

        private void DeleteSelectedStudent()
        {
            
        }

        public void EditSelectedStudent()
        {
            
        }

        private async Task SelectFile()
        {
            Console.WriteLine("clock");
            SelectedFile = await _SelectFileInteraction.Handle("Choose csv file");
        }

        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        public string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }

        //обновление при изменении
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}