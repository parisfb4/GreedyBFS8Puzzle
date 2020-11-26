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

        public List<Node> BreadthFirstSearch(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            List<Node> OpenList = new List<Node>(); //Lista que se puede expandir
            List<Node> ClosedList = new List<Node>(); //Las que ya fueron vistas y expandidas

            OpenList.Add(root); // Iniciar con la raíz
            bool goalFound = false; //Se llegó a la meta

            while (OpenList.Count > 0 && !goalFound) //Mientras haya posibilidades y no se haya llegado a la meta
            {
                //Las 3 lineas Simulan una cola 
                Node currentNode = OpenList[0]; //Primer elemento
                ClosedList.Add(currentNode); // Para no volver a entrar ahí
                OpenList.RemoveAt(0); //Sacarlo

                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                for (int i = 0; i < currentNode.Children.Count; i++)
                {

                    Node currentChild = currentNode.Children[i];

                    //Aquí imprimiremos todos los estados

                    if (currentChild.GoalTest())
                    {
                        Console.WriteLine("Goal Found.");
                        goalFound = true;
                        PathTrace(PathToSolution, currentChild);
                        //Trace path to root node
                    }

                    //OpenList contiene currentChild && ClosedList contains currents child
                    if (!Contains(OpenList, currentChild) && !Contains(ClosedList, currentChild))
                    {
                        OpenList.Add(currentChild);
                    }

                }
            }

            return PathToSolution;
        }

        public List<Node> DeepFirstSearch(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            List<Node> OpenList = new List<Node>(); //Lista que se puede expandir
            List<Node> ClosedList = new List<Node>(); //Las que ya fueron vistas y expandidas

            OpenList.Add(root); // Iniciar con la raíz
            bool goalFound = false; //Se llegó a la meta

            while (OpenList.Count > 0 && !goalFound) //Mientras haya posibilidades y no se haya llegado a la meta
            {
                //Las 3 lineas Simulan una cola 
                Node currentNode = OpenList.Last(); //Primer elemento
                ClosedList.Add(currentNode); // Para no volver a entrar ahí
                OpenList.Remove(currentNode); //Sacarlo

                currentNode.ExpandNode();
                currentNode.PrintPuzzle();

                for (int i = 0; i < currentNode.Children.Count; i++)
                {

                    Node currentChild = currentNode.Children[i];

                    //Aquí imprimiremos todos los estados

                    if (currentChild.GoalTest())
                    {
                        Console.WriteLine("Goal Found.");
                        goalFound = true;
                        PathTrace(PathToSolution, currentChild);
                        //Trace path to root node
                    }

                    //OpenList contiene currentChild && ClosedList contains currents child
                    if (!Contains(OpenList, currentChild) && !Contains(ClosedList, currentChild))
                    {
                        OpenList.Add(currentChild);
                    }

                }
            }

            return PathToSolution;
        }

        //Profundidad Iterativa
        public List<Node> DeepFirstSearchIterative(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            List<Node> OpenList; //Lista que se puede expandir
            List<Node> ClosedList; //Las que ya fueron vistas y expandidas
            bool goalFound = false; //Se llegó a la meta


            int profundidad = 1;
            Node currentNode = root;

            while (!goalFound && profundidad < 40)
            {
                //currentNode.ExpandNode(); //  Est_abiertos.inicializar()
                OpenList = new List<Node>(); //  Est_abiertos.inicializar()
                ClosedList = new List<Node>(); //  Est_cerrados.inicializar()
                OpenList.Add(root); //  Est_abiertos.insertar(Estado inicial)
                currentNode = OpenList[0]; //Actual ← Est_abiertos.primero()

                while(!goalFound && OpenList.Count > 0) //  mientras no es_final?(Actual) y no Est_abiertos.vacia?() hacer
                {
                    OpenList.RemoveAt(OpenList.Count -1);
                    ClosedList.Add(currentNode);

                    if (Profundidad(currentNode, root) <= profundidad)
                    {
                        currentNode.ExpandNode();
                        //currentNode.PrintPuzzle();

                        //INICIO
                        for (int i = 0; i < currentNode.Children.Count; i++)
                        {

                            Node currentChild = currentNode.Children[i];//Aqui es

                            //Aquí imprimiremos todos los estados

                            if (currentChild.GoalTest())
                            {
                                Console.WriteLine("Goal Found.");
                                goalFound = true;
                                PathTrace(PathToSolution, currentChild);
                                Console.Write("Profundidad alcanzada: " + profundidad.ToString() + "\n");
                                //Trace path to root node
                            }

                            //OpenList contiene currentChild && ClosedList contains currents child
                            if (!Contains(OpenList, currentChild) && !Contains(ClosedList, currentChild))
                            {
                                OpenList.Add(currentChild);
                            }

                        }
                        //FIN

                        //OpenList.AddRange(currentNode.GetChildren());
                    }
                    if(OpenList.Count > 0) currentNode = OpenList.Last();
                }
                profundidad++;
            }

            return PathToSolution;
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
