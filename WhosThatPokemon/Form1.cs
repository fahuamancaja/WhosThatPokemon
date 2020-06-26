using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Reflection;
using System.Security.Cryptography;


namespace WhosThatPokemon
{
    public partial class pokemonGameForm : Form
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public string PokemonGuess { get; set; }
        public pokemonGameForm()
        {
            InitializeComponent();
            btBegin.Hide();
            btnAnswer.Hide();
            txtAnswer.Hide();
            pBoxPokemon.Hide();
            lblPokemonScore.Hide();

            //Pokemon Music
            PlayBackgroundMusic();


            //Hide Admin Controls
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            pkmNumber.Hide();
            pkmName.Hide();
            rdmPokemon.Hide();
            nameUser.Hide();
            userByUser.Hide();
            pwdUser.Hide();
            rdmWritePokemon.Hide();
            userPwdPoints.Hide();
            writeUserPassword.Hide();
            userPoints.Hide();
            rmdDeletePokemon.Hide();
            nameDeleteUser.Hide();

        }

        public void HideLoginObjects()
        {
            txtUserNameInput.Hide();
            txtPasswordInput.Hide();
            lblUnPw.Hide();
            lblUn.Hide();
            lblPw.Hide();
            btPwUnSubmit.Hide();

            btBegin.Show();
            btnAnswer.Hide();
            txtAnswer.Show();
            pBoxPokemon.Show();
        }
        public void HideRandomButton(bool state)
        {
            if (state)
            {
                btBegin.Hide();
            }

            if (!state)
            {
                btBegin.Show();
            }
        }

        public void PokemonBackground()
        {
            //var image = Image.FromFile(@"C:\Users\Nando\source\repos\WhosThatPokemon\WhosThatPokemon\Resources\images\regions\kanto.png");
            CurrentPointCheck();

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var map = "";

            if (Score <= 151)
                map = "1";
            if (Score > 151 && Score <= 251)
            {
                map = "2";
            }
            if (Score > 251 && Score <= 386)
            {
                map = "3";
            }
            if (Score > 386 && Score <= 493)
            {
                map = "4";
            }
            if (Score > 493 && Score <= 649)
            {
                map = "5";
            }
            if (Score > 649)
            {
                map = "6";
            }


            var rawPath = directory + @"\images\background\regions\" + map + "RegionMap" + ".png";
            
            
            var realPath = rawPath.Replace(@"\\", @"\");

            this.BackgroundImage = Image.FromFile(realPath);
        }
        public static Bitmap ChangeColor(Bitmap imgSource)
        {
            var newColor = Color.Black;

            //make an empty bitmap the same size as imgSource
            Bitmap imgResult = new Bitmap(imgSource.Width, imgSource.Height);
            for (int x = 0; x < imgSource.Width; x++)
            {
                for (int y = 0; y < imgSource.Height; y++)
                {
                    //get the pixel from the imgSource image
                    var actualColor = imgSource.GetPixel(x, y);

                    // > 150 because.. Images edges can be of low pixel color. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor.A > 150)
                        imgResult.SetPixel(x, y, newColor);
                    else
                        imgResult.SetPixel(x, y, actualColor);
                }
            }
            return imgResult;
        }
        public void DisplayPicture(string randomPokemon, bool silhouette)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var rawPath = directory + @"\images\pokemon\" + randomPokemon + ".png";
            var realPath = rawPath.Replace(@"\\", @"\");

            if (silhouette)
            {
                var image = (Bitmap) Image.FromFile(realPath);
                
                var bmp = ChangeColor(image);
                pBoxPokemon.Image = (Image) bmp;
                pBoxPokemon.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            if (!silhouette)
            {
                var image = Image.FromFile(realPath);
                pBoxPokemon.Image = image;
                pBoxPokemon.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btPasswordInput_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserNameInput.Text) || string.IsNullOrWhiteSpace(txtPasswordInput.Text))
            {
                MessageBox.Show("Please input username and password");
            }
            else
            {
                Username = txtUserNameInput.Text;
                //Password = (SHA256.Create((txtPasswordInput.Text))).ToString();

                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(txtPasswordInput.Text);
                byte[] hash = sha512.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                }
                Password = result.ToString();

                UserLogginMethod();
            }


        }
        private void pkmNumber_Click(object sender, EventArgs e)
        {

            var pointSearch = new List<CharacterCommands>();
            pointSearch.Add(new UserLogInSearchPoint() { Value1 = txtUserNameInput.Text });
            var pointSearchList = new IteratingList();
            var pointSearchResult = Convert.ToInt32(pointSearchList.RunListOfCommands(pointSearch));


            var characters = new List<CharacterCommands>();
            characters.Add(new FindPokemonIdBasedOnName() {Value1 = textBox1.Text, Value2 = pointSearchResult });



            var iteratingList = new IteratingList();
            var result = iteratingList.RunListOfCommands(characters);
            MessageBox.Show(result);

            //var result = PokemonCommands.PokemonIdSearch(textBox1.Text);
            //MessageBox.Show(result);

        }

        private void pkmName_Click(object sender, EventArgs e)
        {
            var pointSearch = new List<CharacterCommands>();
            pointSearch.Add(new UserLogInSearchPoint() { Value1 = txtUserNameInput.Text });
            var pointSearchList = new IteratingList();
            var pointSearchResult = Convert.ToInt32(pointSearchList.RunListOfCommands(pointSearch));


            var characters = new List<CharacterCommands>();
            characters.Add(new FindPokemonNameBasedOnId() { Value1 = textBox1.Text, Value2 = pointSearchResult });



            var iteratingList = new IteratingList();
            var result = iteratingList.RunListOfCommands(characters);

            MessageBox.Show(result);

        }

        private void rdmPokemon_Click(object sender, EventArgs e)
        {
            var result = PokemonCommands.RandomPokemonSearch();
            MessageBox.Show(result);
        }

        private void nameUser_Click(object sender, EventArgs e)
        {
            var result = UserCommands.UserSearchByPassword(textBox1.Text);
            MessageBox.Show(result);
        }

        private void userByUser_Click(object sender, EventArgs e)
        {
            var result = UserCommands.UserSearchByUser(textBox1.Text);
            MessageBox.Show(result);
        }

        private void pwdUser_Click(object sender, EventArgs e)
        {
            var result = UserCommands.PasswordSearchByUser(textBox1.Text);
            MessageBox.Show(result);
        }

        private void rdmWritePokemon_Click(object sender, EventArgs e)
        {
            //PokemonCommands.RandomPokemonWriter();
        }

        private void writeUserName_Click(object sender, EventArgs e)
        {
            UserCommands.NewUserWriter(textBox1.Text);
        }


        private void writeUserPassword_Click(object sender, EventArgs e)
        {

            UserCommands.NewPasswordWriter(textBox1.Text,textBox2.Text);

        }

        private void userPoints_Click(object sender, EventArgs e)
        {
            UserCommands.NewPointsWriter(textBox1.Text, textBox2.Text);
        }

        private void rmdDeletePokemon_Click(object sender, EventArgs e)
        {
            PokemonCommands.RandomPokemonRemover();
        }

        private void nameDeleteUser_Click(object sender, EventArgs e)
        {
            UserCommands.UserRemover(textBox1.Text);
        }

        private void pBoxPokemon_Click(object sender, EventArgs e)
        {

        }

        private void btBegin_Click(object sender, EventArgs e)
        {
            PokemonGameMethod();

        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            ValidateGuess();
        }

        private void UserLogginMethod()
        {
            //Searches in database for username from username input
            var user = new List<CharacterCommands>();
            user.Add(new UserLogInSearchUser() { Value1 = Username });
            var userList = new IteratingList();
            var userResults = userList.RunListOfCommands(user);

            //Searches for password for user from username input
            var password = new List<CharacterCommands>();
            password.Add(new UserLogInSearchPassword() { Value1 = Username});
            var passwordList = new IteratingList();
            var passwordResults = passwordList.RunListOfCommands(password);

            if (String.IsNullOrWhiteSpace(userResults))
            {
                MessageBox.Show("No user found. New user will bre created," + Environment.NewLine + "Username: " + Username + Environment.NewLine + "Password: " + txtPasswordInput.Text);

                //Creates new User and password from username and password input
                var createUser = new List<CharacterCommands>();
                createUser.Add(new CreateUserName() { Value1 = Username });
                createUser.Add(new CreatePasswordFromUser() { Value1 = Username, Value2 = Password });
                createUser.Add(new CreatePointFromUser() { Value1 = Username, Value2 = "0" });

                var userCreateList = new IteratingList();
                userCreateList.RunListOfCommands(createUser);
                HideLoginObjects();

                PokemonScoreBar();


            }
            else if (userResults == Username && passwordResults != Password)
            {
                MessageBox.Show("Incorrect Password");
            }
            else if (userResults == Username && passwordResults == Password)
            {
                MessageBox.Show("Logging user in...");
                HideLoginObjects();

                //CurrentPointCheck();
                //lblPokemonScore.Show();
                //lblPokemonScore.Text = "Pokemon Score: " + Score;
                //PokemonBackground();

                PokemonScoreBar();

            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

        }

        private void PokemonGameMethod()
        {

            //Point Check
            CurrentPointCheck();

            //Searches in database for Random Pokemon based on points obtained for specific region
            var randomPokemon = new List<CharacterCommands>();
            randomPokemon.Add(new CreateRandomPokemon() { Value1 = Score});
            var randomPokemonList = new IteratingList();
            var randomPokemonResults = randomPokemonList.RunListOfCommands(randomPokemon);
            DisplayPicture(randomPokemonResults, true);
            HideRandomButton(true);
            btnAnswer.Show();

        }


        private void ValidateGuess()
        {


            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                MessageBox.Show("Please enter your guess!");
            }
            else
            {
                //Store and filter and validate answer
                //var pokemonGuessLetterUp = txtAnswer.Text.First().ToString().ToUpper() + txtAnswer.Text.Substring(1);
                string pokemonGuessLetterUp = char.ToUpper(txtAnswer.Text[0]) + txtAnswer.Text.Substring(1).ToLower();

                //Removed beginning and ending White spaces
                PokemonGuess = pokemonGuessLetterUp.Trim(' ');

                LevelValidation();

            }

        }

        private void LevelValidation()
        {
            //Current Score
            CurrentPointCheck();


            

            //Looks for stores Random Pokemon and matches with guess
            var randomPokemonSearch = new List<CharacterCommands>();
            randomPokemonSearch.Add(new SearchRandomPokemon());
            var randomPokemonList = new IteratingList();
            var randomPokemonResults = randomPokemonList.RunListOfCommands(randomPokemonSearch);

            var guessPokemonSearch = new List<CharacterCommands>();
            guessPokemonSearch.Add(new FindPokemonIdBasedOnName() { Value1 = PokemonGuess, Value2 = Score });
            var guessPokemonList = new IteratingList();
            var guessPokemonResults = guessPokemonList.RunListOfCommands(guessPokemonSearch);



            if (randomPokemonResults == guessPokemonResults)
            {
                var addPoint = Score + 1;
                MessageBox.Show("You guessed Correct!");
                lblPokemonScore.Text = "Pokemon Score: " + Score;
                DisplayPicture(randomPokemonResults, false);

                var deleteRandomPokemon = new List<CharacterCommands>();
                deleteRandomPokemon.Add(new DeleteRandomPokemon());
                deleteRandomPokemon.Add(new CreatePointFromUser() { Value1 = Username, Value2 = Convert.ToString(addPoint) }); //Add point Value2

                var deletePokemonList = new IteratingList();
                deletePokemonList.RunListOfCommands(deleteRandomPokemon);
                HideRandomButton(false);
                btnAnswer.Hide();
                txtAnswer.Clear();

                PokemonScoreBar();

                PokemonCries(guessPokemonResults);

                System.Threading.Thread.Sleep(1000);

                PlayBackgroundMusic();

            }
            else
            {
                MessageBox.Show("Incorrect guess. Guess again!");
            }
        }

        private void pokemonGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            var randomPokemons = new List<CharacterCommands>();
            randomPokemons.Add(new RemoveAllRandomPokemon());
            var randomPokemonDeletionList = new IteratingList();
            randomPokemonDeletionList.RunListOfCommands(randomPokemons);

        

        }

        private void CurrentPointCheck()
        {
            var pointSearch = new List<CharacterCommands>();
            pointSearch.Add(new UserLogInSearchPoint() { Value1 = Username });
            var pointSearchList = new IteratingList();
            Score = Convert.ToInt32(pointSearchList.RunListOfCommands(pointSearch));
        }

        public void PokemonScoreBar()
        {
            CurrentPointCheck();
            lblPokemonScore.Show();
            lblPokemonScore.Text = "Pokemon Score: " + Score;
            PokemonBackground();
        }

        private void PlayBackgroundMusic()
        {
            var region = "1Region";
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var rawPath = directory + @"\sounds\music\" + region + ".wav";
            var realPath = rawPath.Replace(@"\\", @"\");

            SoundPlayer backgroundMusic = new SoundPlayer(realPath);
            backgroundMusic.PlayLooping();
        }

        private void PokemonCries(string pokemonStringNumber)
        {
            var pokemonNumber = Convert.ToInt32(pokemonStringNumber);
            var pokemonString = "";
            if (pokemonNumber < 10)
            {
                pokemonString = "00" + Convert.ToString(pokemonNumber);

            }
            else if (pokemonNumber < 100)
            {
                pokemonString = "0" + Convert.ToString(pokemonNumber);
            }
            else
            {
                pokemonString = Convert.ToString(pokemonNumber);
            }
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var rawPath = directory + @"\sounds\pokemon\cries\" + pokemonString + ".wav";
            var realPath = rawPath.Replace(@"\\", @"\");

            SoundPlayer pokemonCries = new SoundPlayer(realPath);
            pokemonCries.Play();
        }
    }
}
    

