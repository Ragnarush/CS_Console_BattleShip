using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_Final_Version
{
    internal class Program
    {
        static int cpt = 1;
        static void Fill_Char_Tab(char[,] tab, char c)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tab[i, j] = c;
                }
            }
        }

        static void Show_Tab(char[,] tab)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("{0} ", tab[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Ss_hidden(char[,] tab)
        {
            int number_i, number_j, orientation;

            do   // boucle pour valider quon pette pas le bord avec le sous-marin de 3 case
            {
                Random rand = new Random();
                number_i = rand.Next(10);   //random i
                number_j = rand.Next(10);   //ranbom j
                tab[number_i, number_j] = 'S';   //assigner 'S' a la case random 
                orientation = rand.Next(4);   //orientation 0 = nord, orientation 1 = est, etc

                if (number_i == 0 || number_i == 1 && orientation == 0)  //si lorientaion est nord et que le point de depart est dans le top 2
                {                                                        // on recommence
                    tab[number_i, number_j] = 'X';
                    continue;
                }
                else if (number_i == 9 || number_i == 8 && orientation == 2)   //meme chose
                {
                    tab[number_i, number_j] = 'X';
                    continue;
                }
                else if (number_j == 0 || number_j == 1 && orientation == 3)   //meme chose
                {
                    tab[number_i, number_j] = 'X';
                    continue;
                }
                else if (number_j == 9 || number_j == 8 && orientation == 1)   //meme chose
                {
                    tab[number_i, number_j] = 'X';
                    continue;
                }
                else if (orientation == 0)  //la si c nord on assigne vers le haut
                {
                    tab[number_i - 1, number_j] = 'S';
                    tab[number_i - 2, number_j] = 'S';
                }
                else if (orientation == 1)  // la si c est on assigne vers la droite
                {
                    tab[number_i, number_j + 1] = 'S';
                    tab[number_i, number_j + 2] = 'S';
                }

                else if (orientation == 2)   // la si c sud on assigne vers le bas
                {
                    tab[number_i + 1, number_j] = 'S';
                    tab[number_i + 2, number_j] = 'S';
                }
                else if (orientation == 3)   // la si c ouest on assigne vers la gauche
                {
                    tab[number_i, number_j - 1] = 'S';
                    tab[number_i, number_j - 2] = 'S';
                }
            }
            while (tab[number_i, number_j] == 'X');
        }

        static int Attack_Row()
        {
            Console.Write("entrez la ligne de votre coordonner d'attaque: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        static int Attack_Col()
        {
            Console.Write("entrez la colonne de votre coordonner d'attaque: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        static void Presentation()
        {
            Console.WriteLine("Bienvenue sur Battleship !!! et n'oubliez pas ... le jeu! c'est serieux! ");
            Console.WriteLine("");
            Console.WriteLine("vous devrai entrez les coordonner d'attaque");
            Console.WriteLine("appuyer sur une touche pour commencer");
            Console.ReadLine();
        }

        static void Prochain_tour()
        {
            Console.WriteLine("appuyez une touche pour continuer");
            Console.ReadLine();
            cpt++;
        }

        static void Debut_tour()
        {
            Console.Clear();
            Console.WriteLine("tour No.{0}", cpt);
        }

        static void Main(string[] args)
        {
            //Variables
            char[,] hidden_tab = new char[10, 10];
            char[,] shown_tab = new char[10, 10];
            int row, col, ss_hit_count = 0;

            //Methodes
            Fill_Char_Tab(hidden_tab, 'X');
            Ss_hidden(hidden_tab);
            Fill_Char_Tab(shown_tab, '?');

            //Debut du jeux
            Presentation();
            do
            {
                Debut_tour();

                Show_Tab(shown_tab);

                row = Attack_Row();
                col = Attack_Col();

                shown_tab[row, col] = hidden_tab[row, col];

                if (shown_tab[row, col] == 'S')
                    ss_hit_count++;

                if (ss_hit_count == 3)
                {
                    Console.WriteLine("Felicitation , vous avez coulez le sous-marin en {0} tour", cpt);
                    Console.ReadLine();
                }
                    

                Show_Tab(shown_tab);
                Console.WriteLine("you hit: {0}", shown_tab[row, col]);

                Prochain_tour();
            }
            while (ss_hit_count < 3);

            Console.ReadLine();
        }
    }
}