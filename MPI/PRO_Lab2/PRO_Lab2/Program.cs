using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;
namespace PRO_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;

                int num = 1;
                int it_num = 3;
                double msg = 5.3;

                for (int i = 0; i < it_num; i++)
                {
                    if (comm.Rank == 3)
                    {
                        /* Read message from previous process */
                        comm.Send(msg, 4, 0);
                        Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);

                        msg = comm.Receive<double>(7, 0);
                        Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);
                        //comm.Barrier();
                    }
                    else
                    {
                        if (comm.Rank == 4)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(3, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = msg * 4;
                            comm.Send(msg, 1, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 1)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(4, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = (msg / 2) + 5;
                            comm.Send(msg, 5, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 5)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(1, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = (msg / 2) + 5;
                            comm.Send(msg, 8, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 8)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(5, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = msg * 4;
                            comm.Send(msg, 2, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                          //  comm.Barrier();
                        }

                        if (comm.Rank == 2)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(8, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = msg * 4;
                            comm.Send(msg, 0, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 0)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(2, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = msg * 4;
                            comm.Send(msg, 6, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 6)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(0, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = msg * 4;
                            comm.Send(msg, 7, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);
                           // comm.Barrier();
                        }

                        if (comm.Rank == 7)
                        {
                            /* Read message from previous process */
                            msg = comm.Receive<double>(6, 0);
                            Console.WriteLine("Rank " + comm.Rank + " received value: " + msg);

                            msg = (msg / 2) + 5;
                            comm.Send(msg, 3, 0);
                            Console.WriteLine("Rank " + comm.Rank + " sent value: " + msg);

                            Console.WriteLine("-----------End" + num + "iteration--------------");
                            num++;
                          //  comm.Barrier();

                            if (num == 4)
                            {
                                Console.WriteLine("Rank " + comm.Rank + " Final value: " + msg);
                            }
                        }
                    }
                }

            }

        }
    }
}
