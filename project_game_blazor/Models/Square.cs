﻿namespace project_game_blazor.Models
{
    public class Square
    {
        // Alla rutorna i spelet

        // Ruta 1 till 9
        public int Number { get; }

        //Koll om markerad
        public MarkEnum? Mark { get; set; }


        // Konstruktor
        public Square(int number)
        {
            Number = number;
        }
    }
}
