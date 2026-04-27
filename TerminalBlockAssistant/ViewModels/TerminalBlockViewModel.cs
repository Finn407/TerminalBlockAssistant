using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TerminalBlockAssistant.Commands;
using TerminalBlockAssistant.Models;
using TerminalBlockAssistant.Services;
using TerminalBlockAssistant.Stores;

namespace TerminalBlockAssistant.ViewModels
{
    public class TerminalBlockViewModel:ViewModelBase
    {
        private readonly IEngineeringBaseService _engineeringBaseService;
        private readonly NavigationStore _navigationStore;
        private readonly ICountService _countService;
        private readonly ISubmitService _submitService;
        private readonly ITextBoxInputService _textBoxInputService;
        private string _text;
        public string Text { get => _text; set 
            {
                _text = value;
                _countService.SetCount(value);
                    OnPropertyChanged(nameof(Text)); 
            } } 

        public ICommand CreateTerminalBlockCommand { get; }
        public ICommand SetIndividualTextBoxesCommand { get; }
        public ObjectCollection ObjectItems { get; set; }
        private string _selectedMaterial;
        public string SelectedMaterial 
        { 
            get => _selectedMaterial; 
            set 
            { 
                _selectedMaterial = value; 
                _submitService.setMaterialName(value);
                OnPropertyChanged(nameof(SelectedMaterial)); 
            } 
        }
        private List<string> _materialNames;
        public List<string> MaterialNames 
        { 
            get => _materialNames; 
            set 
            { 
                _materialNames = value;
                OnPropertyChanged(nameof(MaterialNames)); 
            } 
        }
        private ObservableCollection<TextBoxInput> _textBoxes;
        public ObservableCollection<TextBoxInput> TextBoxes 
        { 
            get => _textBoxes; 
            set 
            { 
                _textBoxes = value;
                OnPropertyChanged(nameof(TextBoxes)); 
            } 
        }
        private string _individualText;
        public string IndividualText 
        {
            get=> _individualText;
            set 
            { 
                _individualText = value;
                _countService.SetIndividualCount(value); 
                OnPropertyChanged(nameof(IndividualText)); 
            } 
        }
        private Visibility _individualCreateVisibility;
        public Visibility IndividualCreateVisibility 
        { 
            get => _individualCreateVisibility; 
            set 
            { 
                _individualCreateVisibility = value; 
                _countService.setIndividualCreateVisibility(value); 
                OnPropertyChanged(nameof(IndividualCreateVisibility)); 
            } 
        }
        private Visibility _indexCreateVisibility;
        public Visibility IndexCreateVisibility 
        { 
            get => _indexCreateVisibility; 
            set 
            { 
                _indexCreateVisibility = value; 
                _countService.setIndexCreateVisibility(value);
                OnPropertyChanged(nameof(IndexCreateVisibility)); 
            } 
        }
        public TerminalBlockViewModel(IEngineeringBaseService engineeringBaseService, NavigationStore navigationStore,ICountService countService,ISubmitService submitService, ITextBoxInputService textBoxInputService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _navigationStore = navigationStore;
            _countService = countService;
            _submitService = submitService;
            _textBoxInputService = textBoxInputService;

            CreateTerminalBlockCommand = new CreateTerminalBlockCommand(_engineeringBaseService,_countService,_submitService,_textBoxInputService);
            SetIndividualTextBoxesCommand = new SetIndividualTextBoxesCommand(_textBoxInputService, _countService, this);
            
            IndividualCreateVisibility = Visibility.Hidden;
            IndexCreateVisibility = Visibility.Visible;

            var catalogs = _engineeringBaseService.GetApplication().Folders.Catalogs;
            MaterialNames = new List<string>();
            foreach (var item in catalogs.Children) 
            {
                if (item.Name == "IBKfra_260316") 
                {
                    FilterExpression filter = _engineeringBaseService.GetApplication().CreateFilter();
                    filter.TypeId = ObjectType.DevTerminal;
                    ObjectItems = item.FindObjects(filter, SearchBehavior.Deep);
                    foreach (var material in ObjectItems) 
                    {
                        MaterialNames.Add(material.Name);
                    }
                    _submitService.setObjectItems(ObjectItems);
                    break;
                }
            }
        }
        public bool GenerateTextBoxes() 
        {
            int.TryParse(Text, out int result);
            if (result <= 0)
            {
                MessageBox.Show("Es wurde keine Anzahl von Klemmen angegeben");
                return false;
            }
            _textBoxInputService.SetTextBoxInputs(new ObservableCollection<TextBoxInput>());
            for (int i = 0; i < result; i++)
            {
                _textBoxInputService.GetTextBoxInputs().Add(new TextBoxInput(""));
            }
            TextBoxes = _textBoxInputService.GetTextBoxInputs();
            return true;
        }
    }
}
