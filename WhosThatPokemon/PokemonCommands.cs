using System;
using System.Windows.Forms;

namespace WhosThatPokemon
{
    public class PokemonCommands
    {
        public static string Table { get; set; }
        public static string Column { get; set; }

        public static string ColumnValue { get; set; }
        public static string Value { get; set; }

        public static string RandomPokemon { get; set; }

        

            public static string PokemonNameSearch(string value, int score)
            {
                Table = PokemonRegionSelector.PokemonScoreRegionSelector(score);


            Column = "DexId";
            ColumnValue = "Name";
            Value = value;

            var pokemonIdSearch = new DataManager();
            return pokemonIdSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string LevelTwoPokemonNameSearch(string value)
        {

            Table = "2Gen";
            Column = "DexId";
            ColumnValue = "Name";
            Value = value;

            var pokemonIdSearch = new DataManager();
            return pokemonIdSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string PokemonIdSearch(string value, int score)
        {
            Table = PokemonRegionSelector.PokemonScoreRegionSelector(score);


            Column = "Name";
            ColumnValue = "DexId";
            Value = value;

            var pokemonIdSearch = new DataManager();
            return pokemonIdSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string LevelTwoPokemonIdSearch(string value)
        {

            Table = "2Gen";
            Column = "Name";
            ColumnValue = "DexId";
            Value = value;

            var pokemonIdSearch = new DataManager();
            return pokemonIdSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string RandomPokemonSearch()
        {
            
            Table = "randomPokemon";
            Column = "pokemonNumber";
            ColumnValue = "pokemonNumber";

            var pokemonIdSearch = new DataManager().RandomReader();
            if (String.IsNullOrWhiteSpace(pokemonIdSearch))
            {
                MessageBox.Show("No Random Pokemon Available");
            }

            return pokemonIdSearch;
        }

        public static string RandomPokemonWriter(int score)
        {
            Table = "randomPokemon";
            ColumnValue = "pokemonNumber";

            var random = new Random();

            if (score <= 151)
            {
                RandomPokemon = Convert.ToString(random.Next(1, 151));
            }

            if (score > 151 && score <= 251)
            {
                RandomPokemon = Convert.ToString(random.Next(152, 251));
            }
            if (score > 251 && score <= 386)
            {
                RandomPokemon = Convert.ToString(random.Next(252, 386));
            }
            if (score > 386 && score <= 493)
            {
                RandomPokemon = Convert.ToString(random.Next(387, 493));
            }
            if (score > 493 && score <= 649)
            {
                RandomPokemon = Convert.ToString(random.Next(494, 649));
            }
            if (score > 649 && score <= 721)
            {
                RandomPokemon = Convert.ToString(random.Next(650, 721));
            }


            var pokemonIdSearch = new DataManager();

            pokemonIdSearch.DataWriter(Table,ColumnValue, RandomPokemon);
            return RandomPokemon;
        }

        public static string LevelTwoRandomPokemonWriter()
        {
            Table = "randomPokemon";
            ColumnValue = "pokemonNumber";

            var random = new Random();
            var randomPokemon = random.Next(152, 251);
            RandomPokemon = Convert.ToString(randomPokemon);
            var pokemonIdSearch = new DataManager();

            pokemonIdSearch.DataWriter(Table, ColumnValue, RandomPokemon);
            return RandomPokemon;
        }

        public static string RandomPokemonRemover()
        {
            var pokemonRemover = "";
            try
            {
                Table = "randomPokemon";
                Column = "pokemonNumber";
                ColumnValue = "pokemonNumber";
                RandomPokemon = RandomPokemonSearch();

                pokemonRemover = new DataManager().DataRemover(Table, ColumnValue, RandomPokemon);
            }
            catch (Exception e)
            {
                MessageBox.Show("No Random Pokemon Found" + e);
            }

            return pokemonRemover;

        }

        public static string AllRandomPokemonRemover()
        {
            var pokemonRemover = "";
            Table = "randomPokemon";

            try
            {

                pokemonRemover = new DataManager().DataRemover(Table);

            }
            catch (Exception e)
            {
                MessageBox.Show("Random Database Empty" + e);
            }

            if (string.IsNullOrWhiteSpace(pokemonRemover))
            {
                pokemonRemover = "Database empty";
            }

            return pokemonRemover;

        }
    }
}