using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MyWebApplication.Page.MachineLearning
{
    public static class ConstClass
    {
        public const double infinity_distance = 100000000000;
        public const double ParameterWeightFunction = 0.92;
    }
    public abstract class ObjectProperty<T>
    {
        public T value;
        abstract public void SetValue(string str);
        abstract public T GetValue();
        abstract public double Distance(T value_);

    }
    public class ObjectPropertyBool : ObjectProperty<bool>
    {
        public ObjectPropertyBool(string str)
        {
            if (str == "1") value = true;
            else
            {
                value = false;
            }
        }

        public override void SetValue(string str)
        {
            if (str == "1") value = true;
            else
            {
                value = false;
            }
        }
        public override bool GetValue()
        {
            return value;
        }

        public override double Distance(bool value_)
        {
            if (value_ == value) return 0.0;
            else
            {
                return ConstClass.infinity_distance;
            }
        }
    }
    public class ObjectPropertyString : ObjectProperty<string>
    {
        public ObjectPropertyString(string str)
        {
            value = str;
        }

        public override void SetValue(string str)
        {
            value = str;
        }
        public override string GetValue()
        {
            return value;
        }

        public override double Distance(string value_)
        {
            if (value_ == value) return 0.0;
            else
            {
                return ConstClass.infinity_distance;
            }
        }
    }
    public class ObjectPropertyDouble : ObjectProperty<double>
    {
        public ObjectPropertyDouble(string str)
        {
            value = double.Parse(str, CultureInfo.CreateSpecificCulture("en"));
        }

        public override void SetValue(string str)
        {
            value = double.Parse(str, CultureInfo.CreateSpecificCulture("en"));
        }
        public override double GetValue()
        {
            return value;
        }

        public override double Distance(double value_)
        {
            return Math.Abs(value - value_);
        }
    }
    public class DataBase
    {
        private ListObjOne TeacherInfo, TestInfo;
        public DataBase(string[] text_teacher, string[] text_test, int[] rules)
        {
            TeacherInfo = new ListObjOne(text_teacher, rules);
            TestInfo = new ListObjOne(text_test, rules);
        }
        public double MyMethod(int position, int NumberTestObject, List<int> IndexCharacterString, List<int> IndexCharacterDouble, int MaxStringError, int NumberNeighbours, double ParameterWeightFunction_ = ConstClass.ParameterWeightFunction)
        {
            double NumberError = 0;

            int TeacherInfoObjectCount = GetSizeTeacerInfo();

            for (int i = position; i < position + NumberTestObject; i++)
            {
                List<Pair> Neighbours = new List<Pair>();

                for (int j = 0; j < TeacherInfoObjectCount; j++)
                {
                    if (TestInfo.listObjOne[i].GetStringDistance(IndexCharacterString, TeacherInfo.listObjOne[j], MaxStringError) == MaxStringError)
                        continue;

                    double distance = TestInfo.listObjOne[i].GetDoubleDistance(IndexCharacterDouble, TeacherInfo.listObjOne[j]);

                    if (Neighbours.Count() < NumberNeighbours)
                    {
                        Pair temp_pair = new Pair(distance, TeacherInfo.listObjOne[j].class_id);
                        Neighbours.Add(temp_pair);
                        continue;
                    }
                    if (Neighbours.Count() > NumberNeighbours && Neighbours.Last().First > distance)
                    {
                        Neighbours.RemoveAt(NumberNeighbours);
                        Neighbours.Add(new Pair(distance, TeacherInfo.listObjOne[j].class_id));
                        Neighbours.Sort();
                        continue;
                    }
                    if (Neighbours.Count() == NumberNeighbours)
                    {
                        Neighbours.Add(new Pair(distance, TeacherInfo.listObjOne[j].class_id));
                        Neighbours.Sort();
                    }
                }

                double[] WeightClass = { 0, 0 };
                int SizeNeighbours = Neighbours.Count();
                int resprediction_class;

                for (int j = 0; j < SizeNeighbours; j++)
                {
                    WeightClass[Neighbours[j].Second] += Math.Pow(ParameterWeightFunction_, j);
                }

                if (WeightClass[0] > WeightClass[1]) resprediction_class = 0;
                else
                {
                    resprediction_class = 1;
                }

                if (TestInfo.listObjOne[i].class_id != resprediction_class) NumberError++;
            }
            return NumberError;
        }
        public int GetSizeTeacerInfo()
        {
            return TeacherInfo.GetSizelistObjOne();
        }
        public int GetSizeTestInfo()
        {
            return TestInfo.GetSizelistObjOne();
        }
    }
    public class ListObjOne
    {
        public List<ObjOne> listObjOne;

        public ListObjOne(string[] text, int[] rules)
        {
            listObjOne = new List<ObjOne>();

            for (int i = 0; i < text.Count(); i++)
            {
                string str = text[i];
                ObjOne temp_obj = new ObjOne();

                if (str.IndexOf('?') == -1)
                {
                    string[] str_array = str.Split(',');

                    for (int j = 0; j < str_array.Count() - 1; j++)
                    {
                        if (rules[j] == 1)
                        {
                            temp_obj.Add_objectPropertyBools(str_array[j]);
                        }
                        else if (rules[j] == 2)
                        {
                            temp_obj.Add_ObjectPropertyDouble(str_array[j]);

                        }
                        else if (rules[j] == 3)
                        {
                            temp_obj.Add_objectPropertyStrings(str_array[j]);
                        }
                    }
                    temp_obj.AddClass_id(Convert.ToInt32(str_array.Last()));
                    listObjOne.Add(temp_obj);

                }
            }
        }

        public string GetListObjOneText()
        {
            string text = "";

            for (int i = 0; i < listObjOne.Count(); i++)
            {
                text += listObjOne[i].GetStr() + "\n";
            }

            return text;
        }

        public int GetSizelistObjOne()
        {
            return listObjOne.Count();
        }
    }
    public class Pair : IComparable<Pair>
    {
        public Pair()
        {
        }
        public int CompareTo(Pair other)
        {
            // If other is not a valid object reference, this instance is greater.

            return this.First.CompareTo(other.First);
        }
        public Pair(double first, int second)
        {
            this.First = first;
            this.Second = second;
        }

        public double First { get; set; }
        public int Second { get; set; }
    }
    public class ObjOne
    {
        public int class_id;
        public List<ObjectPropertyBool> objectPropertyBools;
        public List<ObjectPropertyString> objectPropertyStrings;
        public List<ObjectPropertyDouble> ObjectPropertyDouble;

        public int GetStringDistance(List<int> IndexCharacterString, ObjOne objOne, int MaxStringError)
        {
            int NumberError = 0;

            for (int i = 0; i < IndexCharacterString.Count(); i++)
            {
                if (objOne.objectPropertyStrings[IndexCharacterString[i]].value != this.objectPropertyStrings[IndexCharacterString[i]].value)
                    NumberError++;
                if (NumberError == MaxStringError) return MaxStringError;
            }

            return NumberError;
        }

        public double GetDoubleDistance(List<int> IndexCharacterDouble, ObjOne objOne)
        {
            double Distance = 0;

            for (int i = 0; i < IndexCharacterDouble.Count(); i++)
            {
                double TempDistance = this.ObjectPropertyDouble[IndexCharacterDouble[i]].value - objOne.ObjectPropertyDouble[IndexCharacterDouble[i]].value;
                Distance += TempDistance * TempDistance;
            }

            return Distance;
        }

        public ObjOne()
        {
            objectPropertyBools = new List<ObjectPropertyBool>();
            objectPropertyStrings = new List<ObjectPropertyString>();
            ObjectPropertyDouble = new List<ObjectPropertyDouble>();
        }

        public void AddClass_id(int id)
        {
            class_id = id;
        }

        public void Add_objectPropertyBools(string str)
        {
            objectPropertyBools.Add(new ObjectPropertyBool(str));
        }

        public void Add_objectPropertyStrings(string str)
        {
            objectPropertyStrings.Add(new ObjectPropertyString(str));
        }

        public void Add_ObjectPropertyDouble(string str)
        {
            ObjectPropertyDouble.Add(new ObjectPropertyDouble(str));
        }

        public string GetStr()
        {
            string str = "";

            for (int i = 0; i < objectPropertyBools.Count(); i++)
            {
                str += objectPropertyBools[i].GetValue().ToString() + ", ";
            }

            for (int i = 0; i < objectPropertyStrings.Count(); i++)
            {
                str += objectPropertyStrings[i].GetValue().ToString() + ", ";
            }

            for (int i = 0; i < ObjectPropertyDouble.Count(); i++)
            {
                str += ObjectPropertyDouble[i].GetValue().ToString() + ", ";
            }

            str += class_id.ToString();

            return str;
        }
    }
    public class Pair_double : IComparable<Pair_double>
    {
        public Pair_double()
        {
        }
        public int CompareTo(Pair_double other)
        {
            // If other is not a valid object reference, this instance is greater.

            return this.First.CompareTo(other.First);
        }
        public Pair_double(double first, double second)
        {
            this.First = first;
            this.Second = second;
        }

        public double First { get; set; }
        public double Second { get; set; }
    }
}