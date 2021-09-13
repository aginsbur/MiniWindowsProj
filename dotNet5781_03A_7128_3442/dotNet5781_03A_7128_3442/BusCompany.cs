using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_7128_3442
{
    public class BusCompany : IEnumerable
    {
        public List<BusLine> busLines = new List<BusLine>();
        public List<BusLine> BL
        {
            get => busLines;
        }
        public IEnumerator GetEnumerator()
        {
            return busLines.GetEnumerator();
        }
        /// <summary>
        /// adds a new bus Line to the company
        /// </summary>
        /// <param name="newLine"></param>bus line being added
        public void addLine(BusLine newLine)
        {
            int counter = 0;
            foreach (BusLine item in busLines)//iterates through company and checks how many times a spesific bus line is found
                if (item.LN == newLine.LN)
                    counter++;
            if (counter >= 1)//if there is more than 1 busline with the same number
                throw new Exception("cannot add this Line, it already exists");
            busLines.Add(newLine);
        }
        /// <summary>
        /// removes a bus Line from the company
        /// </summary>
        /// <param name="busLine"></param>
        public void removeLine(BusLine busLine)
        {

            int index = busLines.IndexOf(busLine);
            if (0 <= index && index <= busLines.Count)//if we found the busLine in the list of lines
                busLines.RemoveAt(index);
            else
                throw new Exception("Error! bus Line does not exist in this company!\n");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<BusLine> linesWithCode(int code)
        {
            bool flag = false;
            List<BusLine> temp = new List<BusLine>();
            foreach (var item1 in busLines)//iterates through bus lines in the list
            {
                foreach (Route_Bus_Stop item2 in item1)//iterates through a bus lines route
                {
                    if (!flag && item2.CS.SC == code)//if a bus stop code is equal to "code", add that busLine to the list
                    {
                        temp.Add(item1);
                        flag = true;
                    }
                }
                flag = false;
            }
            if (temp.Count == 0)
                throw new Exception("Error! there is no bus stop that has the code entered\n");
            return temp;
        }
        /// <summary>
        /// sorts the bus companies busLines according to trip duration
        /// </summary>
        /// <returns></returns>
        public List<BusLine> sortByTime()
        {
            busLines.Sort();
            return busLines;
        }
        /// <summary>
        /// returns a bus line that has the code inputed by the user
        /// </summary>
        /// <param name="code"></param>users input of bus Line code
        public BusLine indexer(int code)
        {

            foreach (var item in busLines)
                if (item.LN == code)
                    return item;
            throw new Exception("Error! the inputed bus line does not exits!");

        }
    }
}

