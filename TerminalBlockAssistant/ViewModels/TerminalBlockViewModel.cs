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
                _countService.SetCount(int.Parse(value));
                    OnPropertyChanged(nameof(Text)); 
            } } 
        private ObservableCollection<ObjectItem> Materials;
        private string selectedMaterial;
        public ICommand CreateTerminalBlockCommand { get; }
        public ICommand SetIndividualNamesCommand { get; }
        public ICommand SetIndividualTextBoxesCommand { get; }
        public ICommand SetTerminalBlocksWithIndexCommand { get; }
        public ObjectCollection ObjectItems { get; set; }
        public string SelectedMaterial { get => selectedMaterial; set { selectedMaterial = value; _submitService.setMaterialName(value);OnPropertyChanged(nameof(SelectedMaterial)); } }
        public List<string> MaterialNames { get; set; }
        public SubmitModel submitModel {get;set;}
        private ObservableCollection<TextBoxInput> _textBoxes;
        public ObservableCollection<TextBoxInput> TextBoxes { get => _textBoxes; set { _textBoxes = value;OnPropertyChanged(nameof(TextBoxes)); } }
        private string _individualText;
        public string IndividualText {get=> _individualText;set { _individualText = value;_countService.SetIndividualCount(int.Parse(value)); OnPropertyChanged(nameof(IndividualText)); } }
        public TerminalBlockViewModel(IEngineeringBaseService engineeringBaseService, NavigationStore navigationStore,ICountService countService,ISubmitService submitService, ITextBoxInputService textBoxInputService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _navigationStore = navigationStore;
            _countService = countService;
            _submitService = submitService;
            _textBoxInputService = textBoxInputService;

            CreateTerminalBlockCommand = new CreateTerminalBlockCommand(_engineeringBaseService,_countService,_submitService);
            SetIndividualNamesCommand = new SetIndividualNamesCommand(_textBoxInputService,_countService,_engineeringBaseService,_submitService);
            SetIndividualTextBoxesCommand = new SetIndividualTextBoxesCommand(_textBoxInputService, _countService, this);
            SetTerminalBlocksWithIndexCommand = new SetTerminalBlocksWithIndexCommand(_engineeringBaseService,_countService,_submitService);



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
                    //submitModel = new SubmitModel(SelectedMaterial, ObjectItems);
                    _submitService.setObjectItems(ObjectItems);
                    break;
                }
            }
        }
        public void GenerateTextBoxes() 
        {
            _textBoxInputService.SetTextBoxInputs(new ObservableCollection<TextBoxInput>());
            for (int i = 0; i < int.Parse(Text); i++)
            {
                _textBoxInputService.GetTextBoxInputs().Add(new TextBoxInput(""));
            }
            TextBoxes = _textBoxInputService.GetTextBoxInputs();
            //ggf Button auf visible toggeln
        }

    }
}
