using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Property {
    [Serializable]
    public struct PropertyReprGroup {
        public PropertyReprGroup(int population = 0, int populationDelta = 0, int finance = 0, int financeDelta = 0) {
            this.population = population;
            this.populationDelta = populationDelta;
            this.finance = finance;
            this.financeDelta = financeDelta;
        }

        [SerializeField] private int population, populationDelta, finance, financeDelta;


        public int this[PropertyType type] {
            get {
                switch(type) {
                    case PropertyType.Population:
                        return population;
                    case PropertyType.PopulationDelta:
                        return populationDelta;
                    case PropertyType.Finance:
                        return finance;
                    case PropertyType.FinanceDelta:
                        return financeDelta;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            set {
                switch(type) {
                    case PropertyType.Population:
                        population = value;
                        return;
                    case PropertyType.PopulationDelta:
                        populationDelta = value;
                        return;
                    case PropertyType.Finance:
                        finance = value;
                        return;
                    case PropertyType.FinanceDelta:
                        financeDelta = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }


        public bool Any(Predicate<int> predicate)
            => predicate(population) || predicate(populationDelta) || predicate(finance) || predicate(financeDelta);


        public static PropertyReprGroup operator+(PropertyReprGroup a, PropertyReprGroup b)
            => new PropertyReprGroup(a.population + b.population,
                                     a.populationDelta + b.populationDelta,
                                     a.finance + b.finance,
                                     a.financeDelta + b.financeDelta);

        public static PropertyReprGroup operator+(PropertyReprGroup a, PropertyRepr b) {
            switch(b.Type) {
                case PropertyType.Population:
                    return new PropertyReprGroup(a.population + b.Count, a.populationDelta, a.finance, a.financeDelta);
                case PropertyType.PopulationDelta:
                    return new PropertyReprGroup(a.population, a.populationDelta + b.Count, a.finance, a.financeDelta);
                case PropertyType.Finance:
                    return new PropertyReprGroup(a.population, a.populationDelta, a.finance + b.Count, a.financeDelta);
                case PropertyType.FinanceDelta:
                    return new PropertyReprGroup(a.population, a.populationDelta, a.finance, a.financeDelta + b.Count);
                default:
                    throw new ArgumentOutOfRangeException(nameof(b.Type), b.Type, null);
            }
        }


        public static PropertyReprGroup operator-(PropertyReprGroup a, PropertyReprGroup b)
            => new PropertyReprGroup(a.population - b.population,
                                     a.populationDelta - b.populationDelta,
                                     a.finance - b.finance,
                                     a.financeDelta - b.financeDelta);

        public static PropertyReprGroup operator-(PropertyReprGroup a, PropertyRepr b) {
            switch(b.Type) {
                case PropertyType.Population:
                    return new PropertyReprGroup(a.population - b.Count, a.populationDelta, a.finance, a.financeDelta);
                case PropertyType.PopulationDelta:
                    return new PropertyReprGroup(a.population, a.populationDelta - b.Count, a.finance, a.financeDelta);
                case PropertyType.Finance:
                    return new PropertyReprGroup(a.population, a.populationDelta, a.finance - b.Count, a.financeDelta);
                case PropertyType.FinanceDelta:
                    return new PropertyReprGroup(a.population, a.populationDelta, a.finance, a.financeDelta - b.Count);
                default:
                    throw new ArgumentOutOfRangeException(nameof(b.Type), b.Type, null);
            }
        }


        public static PropertyReprGroup operator*(PropertyReprGroup a, float f)
            => new PropertyReprGroup((int)(a.population * f),
                                     (int)(a.populationDelta * f),
                                     (int)(a.finance * f),
                                     (int)(a.financeDelta * f));
    }
}
