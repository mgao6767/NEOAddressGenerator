using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressGenerator
{
    class L
    {
        public char Letter;
        public double Length;

        public L(char i, double l)
        {
            Letter = i;
            Length = l;
        }
    }

    static class Function
    {
        static L[] ll = {
            new L('i', 1), new L('j', 1.1), new L('H', 3.3),
            new L('f', 1.4), new L('t', 1.5), new L('r', 1.6),
            new L('1', 1.7), new L('s', 1.9), new L('x', 2),
            new L('k', 2.1), new L('c', 2.2), new L('y', 2.3),
            new L('q', 2.8), new L('z', 2.3), new L('v', 2.3),
            new L('a', 2.5), new L('e', 2.5), new L('7', 2.5),
            new L('3', 2.6), new L('2', 2.6), new L('5', 2.6),
            new L('8', 2.6), new L('9', 2.6), new L('6', 2.6),
            new L('4', 2.7), new L('u', 2.7), new L('h', 2.7),
            new L('n', 2.7), new L('b', 2.8), new L('d', 2.8),
            new L('g', 2.8), new L('o', 2.8), new L('p', 2.8),
            new L('L', 2.4), new L('w', 3.4), new L('m', 4.1),
            new L('l', 1), new L('0', 2.5), new L('I', 1),
            new L('J', 1.4), new L('F', 2.2), new L('S', 2.4),
            new L('E', 2.4), new L('X', 2.4), new L('B', 2.6),
            new L('P', 2.6), new L('K', 2.5), new L('Y', 2.6),
            new L('T', 2.6), new L('Z', 2.7), new L('R', 2.8),
            new L('C', 2.9), new L('V', 3), new L('A', 3.1),
            new L('U', 3.2), new L('D', 3.3), new L('G', 3.3),
            new L('N', 3.5), new L('O', 3.7), new L('Q', 3.7),
            new L('M', 4.1), new L('W', 4.4)
        };
        public static double Length(this char i)
        {
            return ll.First(p => p.Letter == i).Length;
        }
    }

}
