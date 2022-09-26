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
    public class TanpuraViewModel : INotifyPropertyChanged
    {

        #region Constructor
        public TanpuraViewModel()
        {
            Notes = new List<string> { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            StopCommand = new Command(StopCommandExecute);
            PlayCommand = new Command(PlayCommandExecute);
            NoteSelected = "E";
        }
        #endregion

        #region Commands
        public ICommand PlayCommand { get; set; }
        public ICommand StopCommand { get; set; }
        #endregion

        #region Binding Properties
        private string _noteSelected;
        public string NoteSelected
        {
            get
            {
                return _noteSelected;
            }
            set
            {
                if(_noteSelected != value)
                {
                    _noteSelected = value;
                    OnPropertyChanged(nameof(NoteSelected));
                }
            }
        }
        public List<string> Notes { get; set; }
        #endregion

        #region Properties
        public ISimpleAudioPlayer player { get; set; }
        #endregion

        #region Methods
        private void PlayCommandExecute()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            if (Notes.Contains(NoteSelected) == false)
                return;
            var stream = assembly.GetManifestResourceStream("FindTheNote." + "Notes." + NoteSelected + ".mp3");
            player.Load(stream);
            player.Play();
        }
        private void StopCommandExecute()
        {
            player.Stop();
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
