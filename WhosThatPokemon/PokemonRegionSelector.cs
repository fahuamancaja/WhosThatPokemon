using System;

namespace WhosThatPokemon
{
    public class PokemonRegionSelector
    {
        public static string PokemonScoreRegionSelector(int score)
        {
            string table = "";

            if (score <= 151)
            {

                table = "1Gen";
            }

            else if (score > 151 && score <= 251)
            {
                table = "2Gen";

            }

            else if (score > 251 && score <= 386)
            {
                table = "3Gen";

            }

            else if (score > 386 && score <= 493)
            {
                table = "4Gen";

            }

            else if (score > 493 && score <= 649)
            {
                table = "5Gen";

            }

            else if (score > 649)
            {
                table = "6Gen";

            }
            else
            {
                Console.WriteLine("WHAT?!");
            }

            return table;

        }
    }
}