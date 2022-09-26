using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindTheNote
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        #region Constructor
        public MainPageViewModel()
        {
            AnsweredCommand = new Command(AnsweredCommandExecute);
            StartCommand = new Command(StartCommandExecute);
            TanpuraCommand = new Command(TanpuraCommandExecute);
            Notes = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            NotePlayed = "E";
            NoteSelected = string.Empty;
            Reply = string.Empty;
            _random = new Random();
            player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            IsPlaying = false;
            IsNotPlaying = true;
            NoteSelected = "Select a Note";
        }
        #endregion

        #region Properties
        public string NotePlayed { get; set; }
        private Random _random { get; set; }
        private ISimpleAudioPlayer player { get; set; }
        #endregion

        #region Binding Properties
        public List<string> Notes { get; set; }

        private string _noteSelected;
        public string NoteSelected
        {
            get
            {
                return _noteSelected;
            }
            set
            {
                if (_noteSelected != value)
                {
                    _noteSelected = value;
                    OnPropertyChanged(nameof(NoteSelected));
                }
            }
        }

        private string _reply;
        public string Reply
        {
            get
            {
                return _reply;
            }
            set
            {
                if (_reply != value)
                {
                    _reply = value;
                    OnPropertyChanged(nameof(Reply));
                }
            }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    IsNotPlaying = !value;
                    OnPropertyChanged(nameof(IsPlaying));
                }
            }
        }
        public bool IsNotPlaying
        {
            get
            {
                return !_isPlaying;
            }
            set
            {
                OnPropertyChanged(nameof(IsNotPlaying));
            }
        }
        #endregion

        #region Commands
        public ICommand AnsweredCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand TanpuraCommand { get; set; }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Methods
        private void AnsweredCommandExecute()
        {
            player.Stop();
            IsPlaying = false;
            if (NoteSelected != NotePlayed)
            {
                Reply = "Wrong, Correct Answer is " + NotePlayed;
            }
            else
            {
                Reply = "Correct";
            }
        }
        private void StartCommandExecute()
        {
            IsPlaying = true;
            Reply = string.Empty;
            int noteIdx = _random.Next(0, 12);
            var assembly = typeof(App).GetTypeInfo().Assembly;
            NotePlayed = Notes[noteIdx];
            var stream = assembly.GetManifestResourceStream("FindTheNote." + "Notes." + Notes[noteIdx] + ".mp3");
            player.Load(stream);
            player.Play();
        }
        private async void TanpuraCommandExecute()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Tanpura());

        }
        #endregion
    }
}
