using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GreedyBFS8Puzzle
{
    class UninformedSearch
    {
        public UninformedSearch()
        {

        }

        public List<Node> BestFirstSearch(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            List<Node> OpenList = new List<Node>(); //Lista que se puede expandir
            List<Node> ClosedList = new List<Node>(); //Las que ya fueron vistas y expandidas

            Console.WriteLine("Puzzle Inicial");
            root.PrintPuzzle();

            OpenList.Add(root); // Iniciar con la raíz
            bool goalFound = false; //Se llegó a la meta

            while (OpenList.Count > 0 && !goalFound) //Mientras haya posibilidades y no se haya llegado a la meta
            {
                Node currentNode = OpenList[0];
                ClosedList.Add(currentNode);
                OpenList.RemoveAt(0);

                currentNode.ExpandNode();

                Node currentChild = LowestNode(ClosedList, currentNode);

                if(currentChild != null)
                {
                    if (currentChild.GoalTest())
                    {
                        Console.WriteLine("Goal Found.");
                        goalFound = true;
                        PathTrace(PathToSolution, currentChild);
                        //Trace path to root node
                    }
                    else
                    {
                        OpenList.Add(currentChild);
                    }
                }
                else
                {
                    Console.WriteLine("Nodo Null"); 
                }
            }
            return PathToSolution;
        }

        //Encontrar el mas pequeno y que no este en la lista cerrada
        public Node LowestNode(List<Node> closedList, Node currentNode)
        {
                                                                                    //f
            //List<Node> sortedList = currentNode.GetChildren().OrderBy(nodo => nodo.Peso).ToList();
            List<Node> sortedList = currentNode.GetChildren().OrderBy(nodo => nodo.F).ToList();

            Console.WriteLine("Lista Ordenada");
            for(int i = 0; i < sortedList.Count; i++)
            {
                Console.WriteLine("Puzzle");
                sortedList[i].PrintPuzzle();
                Console.WriteLine("F");
                Console.WriteLine(sortedList[i].F);
            }

            for(int i = 0; i < sortedList.Count; i++)
            {
                if(!Contains(closedList, sortedList[i]))
                {
                    return sortedList[i];
                }
            }

            return null;
        }


        public void PathTrace(List<Node> path , Node n)
        {
            Console.WriteLine("Tracin path...");
            Node current = n;
            path.Add(current);

            while(current.Parent != null) //Agregará todos los nodos padres de donde está hasta la raíz
            {
                current = current.Parent;
                path.Add(current);
            }

        }

        public static bool Contains(List<Node> list,Node c)
        {
            bool contains = false;

            for(int i = 0; i < list.Count;i++ )
            {
                if(list[i].IsSamePuzzle(c.Puzzle))
                {
                    contains = true;
                }
            }
            return contains;
        }

        //Retornar Profundidad 
        public int Profundidad(Node current, Node root)
        {
            Node auxiliar = current;
            int contador = 0;

            while(auxiliar.Parent != null)
            {                
                auxiliar = auxiliar.Parent;
                contador++;
            }

            return contador;
        }
    }
}
