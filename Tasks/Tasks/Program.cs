using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
       


        static void Main(string[] args)
        {
            //SimpleTaskSample();

            //TaskWithArray();
            // TaskWithList();

            //SimpleTaskWithModification();

            // ParallelForLoop();

            //AsyncAwaitMethod();

           // GenericTypes();



        }

        #region GenericTypes

        class MyType1
        {
            public int Data1 { get; set; }
            public int Data2 { get; set; }

            public MyType1()
            {
                Console.WriteLine("MyType1 Constructor is called");
            }





        }

        class MyType2
        {
            public int Data3 { get; set; }


            public MyType2()
            {
                Console.WriteLine("MyType2 Constructor is called");
            }

        }
        private static void GenericTypes()
        {
            MyGenericClass<MyType1, int> myGenericClass = new MyGenericClass<MyType1, int>();
            MyGenericClass<MyType2, string> myGenericClass2 = new MyGenericClass<MyType2, string>();
            myGenericClass.MethodOutside += (() => { Console.WriteLine("I'm a outside method for 1"); });
            myGenericClass2.MethodOutside += (() => { Console.WriteLine("I'm a outside method for 2"); });

            myGenericClass.CommonMethod(2);

            myGenericClass2.CommonMethod("I'm a string");

            Console.ReadKey();
        }

        class MyGenericClass<T, U> where T : class, new()
        {

            public Action MethodOutside { get; set; }

            public MyGenericClass()
            {
                T obj = new T();
            }
            public void CommonMethod(U val)
            {


                Console.WriteLine(val.ToString());
                if (MethodOutside != null)
                {
                    MethodOutside();
                }
                Console.WriteLine("Generic Common Method Ended");



            }



        }

        #endregion
        #region AsyncAwaitMethod
        private static void AsyncAwaitMethod()
        {
            MyTaskClass taskClass = new MyTaskClass();
            var r1 = taskClass.MyTask2Async().Result;
            var r2 = taskClass.MyTaskAsync().Result;

            Console.WriteLine("R1" + r1);
            Console.WriteLine("R2" + r2);

            Console.ReadKey();
        }

        #region MyTaskClass
        class MyTaskClass
        {

            public async Task<int> MyTaskAsync()
            {
                await Task.Delay(1000);

                Console.WriteLine("Task1 Time" + DateTime.Now.ToString("HH:mm:ss:ms"));
                return 100;

            }

            public async Task<int> MyTask2Async()
            {
                await Task.Delay(1000);
                Console.WriteLine("Task 2 Time" + DateTime.Now.ToString("HH:mm:ss:ms"));
                return 200;
            }
        }

        #endregion 
        #endregion
        #region ParallelForLoop
        private static void ParallelForLoop()
        {
            Random r = new Random();
            Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 5 }, i =>
            {

                var wait = r.Next(5000, 10000);
                System.Threading.Thread.Sleep(wait);
                Console.WriteLine("Loop no" + i.ToString() + "Wait" + wait.ToString());

                Console.WriteLine("Loop no" + i.ToString() + "Time" + DateTime.Now.ToString("HH:mm:ss:ms"));


            });

            Console.Read();
        } 
        #endregion
        #region SimpleTaskWithModification
        private static void SimpleTaskWithModification()
        {
            Console.WriteLine("All tasks starts here" + DateTime.Now);
            

            Task<int>  t1 =Task.Run(() =>
            {
                Console.WriteLine("Task 1 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Task 1 ends here" + DateTime.Now);
                // throw new Exception("Etho prachana..!");
                return 20;
                
            }).ContinueWith((i)=> {
                Console.Write(i.Result);
                return i.Result;
            } );

             
            
            
            //var result = t1.Result;

            Console.WriteLine(string.Format("Result {0} DateTime {1} Problem {2} ", t1.Result,DateTime.Now.ToString(),t1.IsCompleted));



            Console.WriteLine("End of all statements" + DateTime.Now);

            Console.ReadKey();
        }

        #endregion
        #region SimpleTaskSample
        private static void SimpleTaskSample()
        {
            Console.WriteLine("All tasks starts here" + DateTime.Now);


            Task t1 = new Task(() =>
            {
                Console.WriteLine("Task 1 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Task 1 ends here" + DateTime.Now);
            });
            t1.Start();



            Task t2 = new Task(() =>
            {
                Console.WriteLine("Task 2 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Task 2 ends here" + DateTime.Now);
            });
            t2.Start();

            Task t3 = new Task(() =>
            {
                Console.WriteLine("Task 3 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Task 3 ends here" + DateTime.Now);
            });
            t3.Start();

            t1.Wait();
            t2.Wait();
            t3.Wait();

            Console.WriteLine("End of all statements" + DateTime.Now);

            Console.ReadKey();
        }

        #endregion
        #region TaskWithArray
        private static void TaskWithArray()
        {
            Task[] tArray = new Task[3];

            Console.WriteLine("All tasks starts here" + DateTime.Now);


            tArray[0] = Task.Run(() =>
            {
                Console.WriteLine("Task 1 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Task 1 ends here" + DateTime.Now);
            });
            tArray[1] = Task.Run(() =>
            {
                Console.WriteLine("Task 2 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Task 2 ends here" + DateTime.Now);
            });


            tArray[2] = Task.Run(() =>
            {
                Console.WriteLine("Task 3 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(50000);
                Console.WriteLine("Task 3 ends here" + DateTime.Now);
            });
            Task.WaitAny(tArray);




            Console.WriteLine("End of all statements" + DateTime.Now);

            Console.ReadKey();
        }
        #endregion
        #region TaskWithList
        private static void TaskWithList()
        {
            List<Task> tList = new List<Task>();

            Console.WriteLine("All tasks starts here" + DateTime.Now);


            tList.Add(Task.Run(() =>
           {
               Console.WriteLine("Task 1 starts here" + DateTime.Now);
               System.Threading.Thread.Sleep(10000);
               Console.WriteLine("Task 1 ends here" + DateTime.Now);
           }));


            Task t2 = Task.Run(() =>
            {
                Console.WriteLine("Task 2 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Task 2 ends here" + DateTime.Now);
            });
            tList.Add(t2);

           var t3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 starts here" + DateTime.Now);
                System.Threading.Thread.Sleep(50000);
                Console.WriteLine("Task 3 ends here" + DateTime.Now);
            });
            tList.Add(t3);


            Task.WhenAny(tList.ToArray()).ContinueWith((i) =>
            {
                Console.WriteLine("Some task has completed" + DateTime.Now);

            });




            Console.WriteLine("End of all statements" + DateTime.Now);

            Console.ReadKey();
        }
        #endregion
    }
}
