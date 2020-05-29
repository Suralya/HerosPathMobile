using System.Collections;
using System;
using System.Collections.Generic;


    class Picker<T>
    {
        public List<WeightedObject<T>> WeightedList = new List<WeightedObject<T>>();
        public float ProbabilitySum;


        public void Add(T Contents)
        {
            WeightedList.Add(new WeightedObject<T>(Contents, 1f));
            ProbabilitySum += WeightedList[WeightedList.Count - 1].Probability;

            for (var i = WeightedList.Count - 1; i >= 0; i--)
            {
                WeightedList[i].Weight = WeightedList[i].Probability / ProbabilitySum;

                if (i <= WeightedList.Count - 2)
                {
                    WeightedList[i].Weight += WeightedList[i + 1].Weight;
                }
            }
        }

        public void Add(T Contents, float probability)
        {
            WeightedList.Add(new WeightedObject<T>(Contents, probability));
            ProbabilitySum += WeightedList[WeightedList.Count - 1].Probability;

            for (var i = WeightedList.Count - 1; i >= 0; i--)
            {
                WeightedList[i].Weight = WeightedList[i].Probability / ProbabilitySum;

                if (i <= WeightedList.Count - 2)
                {
                    WeightedList[i].Weight += WeightedList[i + 1].Weight;
                }
            }
        }


        public T Pick()
        {
            var r = new Random();
            var picker = r.NextDouble();

            return WeightedList.FindLast(
                delegate (WeightedObject<T> current) { return picker <= current.Weight; }
                ).Container;
        }
    }


    internal class WeightedObject<T>
    {
        public readonly T Container;
        public float Probability;
        public float Weight;

        public WeightedObject(T contents, float probability)
        {
            Container = contents;
            Probability = probability;
        }

    }

