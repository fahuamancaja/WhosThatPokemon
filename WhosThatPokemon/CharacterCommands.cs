using System.Collections.Generic;
using System.Windows.Forms;

namespace WhosThatPokemon
{
    public class IteratingList
    {
        public string RunListOfCommands(List<CharacterCommands> commands)
        {
            var returnInfo = "";
            foreach (var command in commands)
            {
                returnInfo = command.RunCommand();
            }

            return returnInfo;
        }
    }
    public class CharacterCommands
    {
        public virtual string RunCommand()
        {
            var message = "Running base RunCommand";
            return message;
        }
        public virtual string RunCommand(string value)
        {
            var message = "Running base RunCommand";
            return message;

        }
        public virtual string RunCommand(string value1,string value2)
        {
            var message = "Running base RunCommand";
            return message;

        }
    }
    public class FindPokemonNameBasedOnId : CharacterCommands
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }

        public override string RunCommand()
        {
            var result = PokemonCommands.PokemonNameSearch(Value1, Value2);

            //MessageBox.Show(result);
            return result;
        }
    }
    public class FindPokemonIdBasedOnName : CharacterCommands
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }

        public override string RunCommand()
        {
            var result =PokemonCommands.PokemonIdSearch(Value1, Value2);

            //MessageBox.Show(result);
            return result;
        }
    }

    public class CreateRandomPokemon : CharacterCommands
    {
        public int Value1 { get; set; }

        public override string RunCommand()
        {
            //var result = "";
            //if (Value1 <= 151)
            //{
            //    result = PokemonCommands.RandomPokemonWriter();
            //    //MessageBox.Show(result);
            //}

            //if (Value1 > 151 && Value1 <= 251)
            //{
            //    result = PokemonCommands.LevelTwoRandomPokemonWriter();
            //}

            var result = PokemonCommands.RandomPokemonWriter(Value1);

            return result;

        }
    }

    public class SearchRandomPokemon : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = PokemonCommands.RandomPokemonSearch();
            //MessageBox.Show(result);
            return result;
        }
    }

    public class DeleteRandomPokemon : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = PokemonCommands.RandomPokemonRemover();
            //MessageBox.Show(result);
            return result;
        }
    }

    public class RemoveAllRandomPokemon : CharacterCommands
    {
        public override string RunCommand()
        {
            var result = PokemonCommands.AllRandomPokemonRemover();
            //MessageBox.Show(result);
            return result;
        }
    }

    /// <summary>
    /// User commands begin
    /// </summary>
    public class UserLogInSearchUser : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = UserCommands.UserSearchByUser(Value1);
            //MessageBox.Show(result);
            return result;
        }
    }
    public class UserLogInSearchPassword : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = UserCommands.PasswordSearchByUser(Value1);

            //MessageBox.Show(result);
            return result;
        }
    }
    public class UserLogInSearchPoint : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = UserCommands.PointSearchByUser(Value1);

            //MessageBox.Show(result);
            return result;
        }
    }

    public class CreateUserName : CharacterCommands
    {
        public string Value1 { get; set; }
        public override string RunCommand()
        {
            var result = UserCommands.NewUserWriter(Value1);

            //MessageBox.Show(result);
            return result;
        }
    }

    public class CreatePasswordFromUser : CharacterCommands
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public override string RunCommand()
        {
            var result = UserCommands.NewPasswordWriter(Value1, Value2);

            //MessageBox.Show(result);
            return result;
        }
    }

    public class CreatePointFromUser : CharacterCommands
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public override string RunCommand()
        {
            var result = UserCommands.NewPointsWriter(Value1, Value2);

            //MessageBox.Show(result);
            return result;
        }
    }
}