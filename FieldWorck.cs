using System;

namespace ticTacToe
{
    internal class PlayField
    {
        private int x = 3;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value > 0)
                    x = value;
            }
        }
        private int y = 3;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value > 0)
                    y = value;
            }
        }
        public Figurs[,] Field
        {
            get
            {
                return createField();
            }
        }
        public Figurs chekcViner(Figurs[,] field)
        {
            Figurs reference = Figurs.empty;
            for (int i = 0; i < x && reference == Figurs.empty; i++)
            {
                reference = field[i, 0];
                for (int j = 0; j < y; j++)
                {
                    if (field[i, 0] == Figurs.empty || field[i, 0] != field[i, j])
                    {
                        reference = Figurs.empty;
                        break;
                    }
                }
            }
            for (int j = 0; j < y && reference == Figurs.empty; j++)
            {
                reference = field[0, j];
                for (int i = 0; i < x; i++)
                {
                    if (field[0, j] == Figurs.empty || field[0, j] != field[i, j])
                    {
                        reference = Figurs.empty;
                        break;
                    }
                }
            }
            if (x == y && reference == Figurs.empty)
            {
                int i = 0;
                reference = field[0, 0];
                while (i < x)
                {
                    if (reference != field[i, i] || reference == Figurs.empty)
                    {
                        i = 0;
                        reference = Figurs.empty;
                        break;
                    }
                    i++;
                }
                for (int j = y - 1; i < x; j--)
                {
                    reference = field[0, y - 1];
                    if (reference != field[i, j] || reference == Figurs.empty)
                    {
                        reference = Figurs.empty;
                        break;
                    }
                    i++;
                }
            }
            return reference;
        }

        public Figurs[,] AddRandomPoztion(Figurs[,] field, Figurs item)
        {
            Random random = new Random();
            while (true)
            {
                int i = random.Next(x);
                int j = random.Next(y);
                if (field[i, j] == Figurs.empty)
                {
                    field[i, j] = item;
                    return field;
                }
            }
        }
        private Figurs[,] createField()
        {
            Figurs[,] field = new Figurs[3, 3];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    field[i, j] = Figurs.empty;

            return field;
        }

    }
    internal enum Figurs : short
    {
        empty,
        cross,
        zero,
        draw,
    }
}
