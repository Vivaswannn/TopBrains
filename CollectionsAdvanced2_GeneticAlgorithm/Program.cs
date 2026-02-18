using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAdvanced2_GeneticAlgorithm
{
    public interface IChromosome<TGene, TFitness> : IComparable<IChromosome<TGene, TFitness>>
        where TFitness : IComparable<TFitness>
    {
        IReadOnlyList<TGene> Genes { get; }
        TFitness Fitness { get; }
        IChromosome<TGene, TFitness> Crossover(IChromosome<TGene, TFitness> other);
        void Mutate(double mutationRate);
    }

    public class SimpleChromosome : IChromosome<int, double>
    {
        private readonly List<int> _genes = new List<int>();
        private static readonly Random Rnd = new Random();

        public IReadOnlyList<int> Genes => _genes;
        public double Fitness { get; private set; }

        public SimpleChromosome(IEnumerable<int> genes)
        {
            _genes.AddRange(genes);
            Fitness = _genes.Sum(g => g);
        }

        public int CompareTo(IChromosome<int, double> other) => Fitness.CompareTo(other.Fitness);

        public IChromosome<int, double> Crossover(IChromosome<int, double> other)
        {
            var child = new List<int>();
            for (int i = 0; i < _genes.Count; i++)
                child.Add(Rnd.NextDouble() < 0.5 ? _genes[i] : other.Genes[i]);
            return new SimpleChromosome(child);
        }

        public void Mutate(double mutationRate)
        {
            for (int i = 0; i < _genes.Count; i++)
                if (Rnd.NextDouble() < mutationRate)
                    _genes[i] = Rnd.Next(0, 10);
            Fitness = _genes.Sum(g => g);
        }
    }

    public class EvolutionaryAlgorithm<TGene, TFitness, TChromosome>
        where TChromosome : class, IChromosome<TGene, TFitness>
        where TFitness : struct, IComparable<TFitness>
    {
        private readonly List<TChromosome> _population = new List<TChromosome>();

        public void AddToPopulation(TChromosome chromosome)
        {
            _population.Add(chromosome);
        }

        public TChromosome GetBest()
        {
            return _population.OrderByDescending(c => c).FirstOrDefault();
        }

        public IEnumerable<TChromosome> GetTop(int n)
        {
            return _population.OrderByDescending(c => c).Take(n);
        }
    }

    class Program
    {
        static void Main()
        {
            var algo = new EvolutionaryAlgorithm<int, double, SimpleChromosome>();
            algo.AddToPopulation(new SimpleChromosome(new[] { 1, 2, 3 }));
            algo.AddToPopulation(new SimpleChromosome(new[] { 4, 5, 6 }));
            var best = algo.GetBest();
            Console.WriteLine("Best fitness: " + (best?.Fitness ?? 0));
        }
    }
}
