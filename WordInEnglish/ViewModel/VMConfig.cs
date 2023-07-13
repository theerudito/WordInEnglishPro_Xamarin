using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglishPro.Application_Context;
using WordInEnglishPro.Helpers;
using WordInEnglishPro.Model;
using Xamarin.Forms;

namespace WordInEnglishPro.ViewModel
{
    public class VMConfig : BaseVM
    {
        public VMConfig(INavigation navigation)
        {
            Navigation = navigation;
            Language = LocalStorange.GetLocalStorange("language");

            if (Language == "EN")
            {
                LanguageConfig();
            }
            else
            {
                LanguageConfig();
            }
        }

        #region Property

        private string _WordEnglish;
        private string _WordSpanish;

        #endregion Property

        #region Objects

        public string WordEnglish
        {
            get => _WordEnglish;
            set { _WordEnglish = value; OnPropertyChanged(); }
        }

        public string WordSpanish
        {
            get { return _WordSpanish; }
            set { _WordSpanish = value; OnPropertyChanged(); }
        }

        #region Language;

        private string _language;
        private string _textEN;
        private string _textES;
        private string _placeholderEN;
        private string _placeholderES;
        private string _btnTextEN;

        public string Language
        {
            get { return _language; }
            set { _language = value; OnPropertyChanged(); }
        }

        public string TextInforEN
        {
            get { return _textEN; }
            set { _textEN = value; OnPropertyChanged(); }
        }

        public string TextInforES
        {
            get { return _textES; }
            set { _textES = value; OnPropertyChanged(); }
        }

        public string PlaceholderTextEN
        {
            get { return _placeholderEN; }
            set { _placeholderEN = value; OnPropertyChanged(); }
        }

        public string PlaceholderTextES
        {
            get { return _placeholderES; }
            set { _placeholderES = value; OnPropertyChanged(); }
        }

        public string BtnText
        {
            get { return _btnTextEN; }
            set { _btnTextEN = value; OnPropertyChanged(); }
        }

        #endregion Language;

        #endregion Objects

        #region Methods

        public void LanguageConfig()
        {
            if (Language == "EN")
            {
                TextInforEN = MyLanguages._TextOneEN;
                TextInforES = MyLanguages._TextTwoEN;
                PlaceholderTextEN = MyLanguages._PlaceholderOneEN;
                PlaceholderTextES = MyLanguages._PlaceholderTwoEN;
                BtnText = MyLanguages._SaveWORDEN;
            }
            else
            {
                TextInforEN = MyLanguages._TextOneES;
                TextInforES = MyLanguages._TextTwoES;
                PlaceholderTextEN = MyLanguages._PlaceholderOneES;
                PlaceholderTextES = MyLanguages._PlaceholderTwoES;
                BtnText = MyLanguages._SaveWORDES;
            }
        }

        public async Task GoConfig()
        {
            await Navigation.PopAsync();
        }

        public async Task SaveNewWord()
        {
            var _context = new Application_ContextDB();
            if (ValidateFields() == true)
            {
                string wordEnglish = WordEnglish.ToUpper().Trim();
                string wordSpanish = WordSpanish.ToUpper().Trim();

                var wordEN = _context.WordsEN.Where(x => x.MyWord == wordEnglish).FirstOrDefault();
                var wordES = _context.WordsES.Where(x => x.MyWord == wordSpanish).FirstOrDefault();

                if (wordEN != null && wordES != null)
                {
                    var wordEN_ID = _context.WordsEN.Where(x => x.IdEN == wordEN.IdEN).FirstOrDefault();
                    var wordES_ID = _context.WordsES.Where(x => x.IdES == wordES.IdES).FirstOrDefault();

                    if (wordEN_ID != null && wordES_ID != null)
                    {
                        await AlertExistWord();
                    }
                    else
                    {
                        await newWord();
                    }
                }
                else
                {
                    await newWord();
                }
            }
        }

        public async Task newWord()
        {
            var _context = new Application_ContextDB();

            if (ValidateFields() == true && MaxLenght() == true)
            {
                var wordEnglish = WordEnglish.ToUpper().Trim();
                var wordSpanish = WordSpanish.ToUpper().Trim();

                bool containsOnlyLettersEN = Regex.IsMatch(wordEnglish, @"^[a-zA-Z\s]+$");
                bool containsOnlyLettersES = Regex.IsMatch(wordSpanish, @"^[a-zA-Z\s]+$");

                if (containsOnlyLettersEN && containsOnlyLettersES)
                {
                    await _context.AddAsync(new WordEN { MyWord = wordEnglish });
                    await _context.AddAsync(new WordES { MyWord = wordSpanish });
                    await _context.SaveChangesAsync();
                    await AlertSaveSuccessfully();
                    CleanFields();
                }
                else
                {
                    if (Language == "EN")
                    {
                        await Alerts.LoadAlert("WordInEnglish", "Allowed only letters", "OK");
                    }
                    else
                    {
                        await Alerts.LoadAlert("WordInEnglish", "Permitido solo letras", "SI");
                    }
                }
            }
        }

        public void CleanFields()
        {
            WordEnglish = string.Empty;
            WordSpanish = string.Empty;
        }

        public bool MaxLenght()
        {
            var wordEnglish = WordEnglish.ToUpper().Trim();
            var wordSpanish = WordSpanish.ToUpper().Trim();

            // permitido solo 20 caracteres
            if (wordEnglish.Length > 20)
            {
                if (Language == "EN")
                {
                    Alerts.LoadAlert("WordInEnglish", "The word must be 20 characters or less", "OK");
                }
                else
                {
                    Alerts.LoadAlert("WordInEnglish", "La palabra debe ser de 20 caracteres o menos", "SI");
                }
                return false;
            }

            if (wordSpanish.Length > 20)
            {
                if (Language == "EN")
                {
                    Alerts.LoadAlert("WordInEnglish", "The word must be 20 characters or less", "OK");
                }
                else
                {
                    Alerts.LoadAlert("WordInEnglish", "La palabra debe ser de 20 caracteres o menos", "SI");
                }
                return false;
            }

            return true;
        }

        public bool ValidateFields()
        {
            if (string.IsNullOrEmpty(WordEnglish))
            {
                AlertValidationFieldOne();
                return false;
            }
            if (string.IsNullOrEmpty(WordSpanish))
            {
                AlertValidationFieldTwo();
                return false;
            }
            return true;
        }

        public async Task AlertExistWord()
        {
            if (Language == "EN")
            {
                await Alerts.LoadAlert("WordInEnglish", "The word already exists", "OK");
            }
            else
            {
                await Alerts.LoadAlert("WordInEnglish", "La palabra ya existe", "SI");
            }
        }

        public async Task AlertSaveSuccessfully()
        {
            if (Language == "EN")
            {
                await Alerts.LoadAlert("WordInEnglish", "Word saved successfully", "OK");
            }
            else
            {
                await Alerts.LoadAlert("WordInEnglish", "Palabra guardada exitosamente", "SI");
            }
        }

        public void AlertValidationFieldOne()
        {
            if (Language == "EN")
            {
                Alerts.LoadAlert("WordInEnglish", "Please enter a word in English", "OK");
            }
            else
            {
                Alerts.LoadAlert("WordInEnglish", "Por favor ingrese una palabra en Ingles", "SI");
            }
        }

        public void AlertValidationFieldTwo()
        {
            if (Language == "EN")
            {
                Alerts.LoadAlert("WordInEnglish", "Please enter a word in Spanish", "OK");
            }
            else
            {
                Alerts.LoadAlert("WordInEnglish", "Por favor ingrese una palabra en Español", "SI");
            }
        }

        #endregion Methods

        #region Commands

        public ICommand btnBack => new Command(async () => await GoConfig());
        public ICommand btnSaveNewWord => new Command(async () => await SaveNewWord());

        #endregion Commands
    }
}