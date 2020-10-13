using System;
using UnityEngine;

namespace Game.Prop {
    [Serializable]
    public struct PropReprGroup {
        public PropReprGroup(int population = 0, int populationDelta = 0, int finance = 0, int financeDelta = 0) {
            this.population = population;
            this.populationDelta = populationDelta;
            this.finance = finance;
            this.financeDelta = financeDelta;
        }


        public int this[PropType type] {
            get {
                switch(type) {
                    case PropType.Population:
                        return population;
                    case PropType.PopulationDelta:
                        return populationDelta;
                    case PropType.Finance:
                        return finance;
                    case PropType.FinanceDelta:
                        return financeDelta;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            set {
                switch(type) {
                    case PropType.Population:
                        population = value;
                        return;
                    case PropType.PopulationDelta:
                        populationDelta = value;
                        return;
                    case PropType.Finance:
                        finance = value;
                        return;
                    case PropType.FinanceDelta:
                        financeDelta = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }


        public static PropReprGroup operator+(PropReprGroup a, PropReprGroup b)
            => new PropReprGroup(a.population + b.population, a.populationDelta + b.populationDelta,
                                 a.finance + b.finance, a.financeDelta + b.financeDelta);

        public static PropReprGroup operator+(PropReprGroup a, PropRepr b) {
            switch(b.Type) {
                case PropType.Population:
                    return new PropReprGroup(a.population + b.Count, a.populationDelta, a.finance, a.financeDelta);
                case PropType.PopulationDelta:
                    return new PropReprGroup(a.population, a.populationDelta + b.Count, a.finance, a.financeDelta);
                case PropType.Finance:
                    return new PropReprGroup(a.population, a.populationDelta, a.finance + b.Count, a.financeDelta);
                case PropType.FinanceDelta:
                    return new PropReprGroup(a.population, a.populationDelta, a.finance, a.financeDelta + b.Count);
                default:
                    throw new ArgumentOutOfRangeException(nameof(b.Type), b.Type, null);
            }
        }


        public static PropReprGroup operator-(PropReprGroup a, PropReprGroup b)
            => new PropReprGroup(a.population - b.population, a.populationDelta - b.populationDelta,
                                 a.finance - b.finance, a.financeDelta - b.financeDelta);

        public static PropReprGroup operator-(PropReprGroup a, PropRepr b) {
            switch(b.Type) {
                case PropType.Population:
                    return new PropReprGroup(a.population - b.Count, a.populationDelta, a.finance, a.financeDelta);
                case PropType.PopulationDelta:
                    return new PropReprGroup(a.population, a.populationDelta - b.Count, a.finance, a.financeDelta);
                case PropType.Finance:
                    return new PropReprGroup(a.population, a.populationDelta, a.finance - b.Count, a.financeDelta);
                case PropType.FinanceDelta:
                    return new PropReprGroup(a.population, a.populationDelta, a.finance, a.financeDelta - b.Count);
                default:
                    throw new ArgumentOutOfRangeException(nameof(b.Type), b.Type, null);
            }
        }


        [SerializeField] private int population, populationDelta, finance, financeDelta;
    }
}
