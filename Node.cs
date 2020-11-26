using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GreedyBFS8Puzzle
{
    class Node
    {
        #region Variables y Metodos
        private List<Node> children = new List<Node>();         //Lista para los nodos hijos que resultan al realizar un movimiento
        private Node parent;                                    //Nodo padre
        private int[] puzzle = new int[9];                      //Arreglo del puzzle, o estructura actual
        private int x = 0;                                      //Indicador de posicion del 0
        private int col = 3;                                    //Posiciones del puzzle que es 3 x 3

        public int[] Puzzle { get => puzzle; set => puzzle = value; }
        public int X { get => x; set => x = value; }
        internal List<Node> Children { get => children; set => children = value; }
        internal Node Parent { get => parent; set => parent = value; }
        public int Col { get => col; set => col = value; }
        #endregion

        #region Constructor
        public Node(int [] value)
        {
            SetPuzzle(value); //Establecer el puzzle actual
        }
        #endregion

        //Obtener hijos
        public List<Node> GetChildren()
        {
            return children;
        }

        //Funcion para copear el puzzle inicial a la clase para ser alamacenado
        public void SetPuzzle(int[] value)
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                this.puzzle[i] = value[i];
            }
        }

        public void ExpandNode()
        {
            for(int i = 0; i<puzzle.Length;i++)
            {
                if (puzzle[i] == 0)
                    x = i;                         
            }
            
            MoveToRigth(puzzle,x);
            MoveToLEft(puzzle, x);
            MoveToUp(puzzle, x);
            MoveToDown(puzzle, x);
        }

        //Funcion para verificar que la meta no ha llegado al final o si
        public bool GoalTest()
        {
            bool isGoal = true;
            int m = puzzle[0];

            for(int i = 1; i < puzzle.Length; i++)
            {
                if (m > puzzle[i])
                    isGoal = false;
                m = puzzle[i];
            }
            return isGoal;
        }

        //Funcion para copiar el puzzle cuando se realzia un movimiento y poder evaluar los hijos en ese estado. 
        public void CopyPuzzle(int[] PrimerPuzzle, int[] SecondPuzzle)
        {
            for(int i = 0; i < SecondPuzzle.Length; i++)
            {
                PrimerPuzzle[i] = SecondPuzzle[i]; 
            }
        }

        //Funcion para imprimir el puzzle 
        public void PrintPuzzle()
        {
            Console.WriteLine();
            int m = 0;
            for(int i = 0; i < col; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    Console.Write(puzzle[m] + " ");
                    m++;
                }
                Console.WriteLine();
            }
        }

        public bool IsSamePuzzle(int[] p)
        {
            bool samePuzzle = true;
            for(int i = 0; i < p.Length; i++)
            {
                if(puzzle[i] != p[i])
                {
                    samePuzzle = false;
                }
            }
            return samePuzzle;

        }
        #region Movimientos Derecha, Izquierda, Abajo y Arriba
        //Mover a la derecha y obtener hijos posibles
        public void MoveToRigth(int[] value, int index) //Recibe el puzle actual y el index del 0 o espacio en blanco
        {
            if( index % col < Col - 1)
            {
                int[] puzzle_auxiliar = new int[9];
                CopyPuzzle(puzzle_auxiliar, value);

                int temporal = puzzle_auxiliar[index + 1];
                puzzle_auxiliar[index + 1] = puzzle_auxiliar[index];
                puzzle_auxiliar[index] = temporal;

                Node child = new Node(puzzle_auxiliar);
                children.Add(child);
                child.parent = this; 
            }
        }

        //Mover a la Izquierda y obtener hijos posibles
        public void MoveToLEft(int[] value, int index) //Recibe el puzle actual y el index del 0 o espacio en blanco
        {
            if(index % col > 0)
            {
                int[] puzzle_auxiliar = new int[9];
                CopyPuzzle(puzzle_auxiliar, value);

                int temporal = puzzle_auxiliar[index - 1];
                puzzle_auxiliar[index - 1] = puzzle_auxiliar[index];
                puzzle_auxiliar[index] = temporal;

                Node child = new Node(puzzle_auxiliar);
                children.Add(child);
                child.parent = this;
            }
        }

        //Mover Arriba y obtener hijos posibles
        public void MoveToUp(int[] value, int index)//Recibe el puzle actual y el index del 0 o espacio en blanco
        {
            if(index - col >= 0)
            {
                int[] puzzle_auxiliar = new int[9];
                CopyPuzzle(puzzle_auxiliar, value);

                int temporal = puzzle_auxiliar[index - 3];
                puzzle_auxiliar[index - 3] = puzzle_auxiliar[index];
                puzzle_auxiliar[index] = temporal;

                Node child = new Node(puzzle_auxiliar);
                children.Add(child);
                child.parent = this; 
            }
        }

        //Mover Abajo y obtener hijos posibles
        public void MoveToDown(int[] value, int index) //Recibe el puzle actual y el index del 0 o espacio en blanco
        {
            if (index + col < puzzle.Length)
            {
                int[] puzzle_auxiliar = new int[9];
                CopyPuzzle(puzzle_auxiliar, value);

                int temporal = puzzle_auxiliar[index + 3];
                puzzle_auxiliar[index +3] = puzzle_auxiliar[index];
                puzzle_auxiliar[index] = temporal;

                Node child = new Node(puzzle_auxiliar);
                children.Add(child);
                child.parent = this;
            }
        }
        #endregion
    }
}
